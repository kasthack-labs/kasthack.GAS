using System;
using System.Net;
using System.Net.Sockets;
using GAS.Core.AttackInformation;
using GAS.Core.Attacks;

namespace GAS.Core {
    public class Manager {
        #region Attack info
        public int Timeout = 30;
        public int Threads = 10;
        public int Spt = 50;
        public int Port = 80;
        public int Delay;
        //public AttackMethod Method;
        public IPAddress Target = Locolhaust;
        public bool WaitForResponse;
        public bool AppendRandomChars;
        public bool AppendRandomCharsUrl;
        public bool UseGZIP;
        public bool UseGet;
        public string Subsite = "/";
        private string _dnsString = Locolhaust.ToString();
        #endregion
        private static readonly IPAddress Locolhaust = IPAddress.Parse( "127.0.0.1" );
        public IAttacker Worker;
        public string Data;

        public int Requested {
            get { return this.Worker.Requested; }
        }

        public int Failed {
            get { return this.Worker.Failed; }
        }

        public int Downloaded {
            get { return this.Worker.Downloaded; }
        }

        public bool LockOn( string host ) {
            host = host.Trim().ToLower();
            if ( IPAddress.TryParse(
                host,
                out this.Target ) ) {
                this._dnsString = this.Target.ToString();
                return true;
            }
            try {
                if ( !host.StartsWith( "http://" ) && !host.StartsWith( "https://" ) )
                    host = String.Concat(
                        "http://",
                        host );
                var trg = new Uri( host );
                this.Target = Dns.GetHostEntry( trg.Host ).AddressList[ 0 ];
                this._dnsString = trg.Host;
                this.Subsite = trg.PathAndQuery;
                return true;
            }
            catch {
                this.Target = Locolhaust;
                return false;
            }
        }

        public void Stop() {
            if ( this.Worker != null )
                this.Worker.Stop();
        }

        public void Start() {
            this.Stop();
           
            this.Worker.Start();
        }
    }
}