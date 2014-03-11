using System.Net;
using GAS.Core.Attacks;

namespace GAS.Core.AttackInformation {
    public interface IAttackInfo {
        /// <summary>
        /// Attack Target
        /// </summary>
        IPEndPoint Target { get; set; }
        /// <summary>
        /// Send buffer size. Default is ok.
        /// </summary>
        int SendBufferSize { get; set; }
        /// <summary>
        /// Read buffer size. Deafult is set to
        /// </summary>
        int ReadBufferSize { get; set; }
        /// <summary>
        /// Maximum number of threads
        /// </summary>
        int MaxThreads { get; set; }
        /// <summary>
        /// Maximum number of connections
        /// </summary>
        int MaxConnections { get; set; }
    }

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
    }
}