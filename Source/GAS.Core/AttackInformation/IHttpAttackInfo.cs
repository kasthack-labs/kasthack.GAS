using System.Net;
using GAS.Core.Attacks;
using RandomStringGenerator;
using RandomStringGenerator.Expressions;

namespace GAS.Core.AttackInformation {
    public interface IHttpAttackInfo : IAttackInfo {
        /// <summary>
        /// Max bytes to receive.
        /// </summary>
        ulong MaxRead { get; set; }
        /// <summary>
        /// Max bytes to send.
        /// </summary>
        ulong MaxWrite { get; set; }
        /// <summary>
        /// SslInfo
        /// </summary>
        SslInfo Ssl { get; set; }
        /// <summary>
        /// HeaderGenerator
        /// </summary>
        MultiExpression HeaderExpression { get; set; }
    }

    public class HttpAttackInfo : IHttpAttackInfo {
        /// <summary>
        /// Attack Target
        /// </summary>
        public IPEndPoint Target { get; set; }

        /// <summary>
        /// Send buffer size. Default is ok.
        /// </summary>
        public int SendBufferSize { get; set; }

        /// <summary>
        /// Read buffer size. Deafult is set to
        /// </summary>
        public int ReadBufferSize { get; set; }

        /// <summary>
        /// Maximum number of threads
        /// </summary>
        public int MaxThreads { get; set; }

        /// <summary>
        /// Maximum number of connections
        /// </summary>
        public int MaxConnections { get; set; }

        /// <summary>
        /// Max bytes to receive.
        /// </summary>
        public ulong MaxRead { get; set; }

        /// <summary>
        /// Max bytes to send.
        /// </summary>
        public ulong MaxWrite { get; set; }

        /// <summary>
        /// SslInfo
        /// </summary>
        public SslInfo Ssl { get; set; }

        /// <summary>
        /// HeaderGenerator
        /// </summary>
        public MultiExpression HeaderExpression { get; set; }
    }
}