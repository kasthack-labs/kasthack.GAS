using System;
using System.Net;
using System.Net.Sockets;

namespace GAS.Core.AttackInformation {
    public class AttackInfo {
        /// <summary>
        /// Attack Target
        /// </summary>
        public IPEndPoint Target;
        /// <summary>
        /// Protocol
        /// </summary>
        public ProtocolType Protocol;
        /// <summary>
        /// Max packets to receive.
        /// </summary>
        public ulong MaxRead = 0;
        /// <summary>
        /// Max packets to send.
        /// </summary>
        public ulong MaxWrite = ulong.MaxValue;
        /// <summary>
        /// Data generator for sending
        /// first parameter - buffer
        /// second - thread identifer
        /// </summary>
        public Func<byte[], int, int> Randomizer;
        /// <summary>
        /// Send/Read buffer size. Default is ok.
        /// </summary>
        public int bufferSize = 50000;
    }
}