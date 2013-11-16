using System;
using System.Net;
using System.Net.Sockets;
using GAS.Core.AttackInformation;
using RandomStringGenerator.Helpers;

namespace GAS.Core.Attacks {
    public class AsyncFlooderWrapper:IAttacker {
        private AsyncFlooder _flooder; 
        public AsyncFlooderWrapper(IPEndPoint target, ProtocolType protocol, int threadCount) {
            _flooder = new AsyncFlooder(
                new AttackInfo() {
                    Target = target,
                    Protocol = protocol,
                    Randomizer = (a,b) => {
                        Generators.Random.NextBytes( a );
                        return a.Length;
                    }
                },
                ThreadCount = threadCount
            );
        }

        public override void Start() {
            _flooder.Start();
        }

        public override void Stop() {
            _flooder.Stop();
        }
    }
}
