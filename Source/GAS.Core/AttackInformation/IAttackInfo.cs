using System.Net;

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
}