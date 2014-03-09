using System;
using System.Net;
using System.Threading;
using GAS.Core.Attacks;

namespace TestApp {
    class Program {
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
