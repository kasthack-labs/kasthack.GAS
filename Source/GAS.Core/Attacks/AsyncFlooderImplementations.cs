using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading.Tasks;
using RandomStringGenerator;
using RandomStringGenerator.Expressions;

namespace GAS.Core.Attacks {
    public class AsyncFlooderImplementations {
        /// <summary>
        /// Http response reader.
        /// </summary>
        /// <param name="stream">Stream to read</param>
        /// <param name="delay">Delay between requests</param>
        /// <param name="maxRead">Read length limit</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="buffer">Shared buffer.</param>
        /// <exception cref="SocketException"></exception>
        /// <returns>Was everything OK</returns>
        public static async Task<bool> ReceiveResponse(
            Stream stream,
            int delay = 0,
            long maxRead = long.MaxValue,
            int bufferSize = 1024,
            Func<bool> cancellationToken = null,
            byte[] buffer = null
            ) {
            cancellationToken = cancellationToken ?? ( () => false );
            buffer = buffer ?? new byte[ bufferSize ];
            if ( buffer.Length < bufferSize )
                throw new IndexOutOfRangeException( "bufferSize > buffer.Length" );
            var totalRead = 0L;
            int readCurrent;
            var isNetStream = stream is NetworkStream;
            var ns = stream as NetworkStream;
            do {
                if ( cancellationToken() ) return false;
                if ( delay > 0 ) {
                    if ( isNetStream )
                        while ( ns.DataAvailable )
                            totalRead += await ns.ReadAsync( buffer, 0, 1 );
                    await Task.Delay( delay );
                }
                if ( cancellationToken() ) return false;
                totalRead += ( readCurrent = await stream.ReadAsync( buffer, 0, bufferSize ) );
            } while ( readCurrent > 0 && totalRead < maxRead );
            return true;
        }
        /// <summary>
        /// Opens ssl stream without sslcert validation
        /// </summary>
        /// <param name="stream">Inner stream</param>
        /// <param name="domain">Domain for auth</param>
        /// <returns>Stream</returns>
        public static async Task<SslStream> GetSslStream(
            Stream stream,
            string domain ) {
            var sStream = new SslStream(
                stream,
                false,
                ( sender, certificate, chain, errors ) => true
                );
            await sStream.AuthenticateAsClientAsync(
                domain,
                null,
                SslProtocols.Default,
                false
                );
            return sStream;
        }

        /// <summary>
        /// Sends body
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="generator">Data generator. Takes buffer & token, returns byte count</param>
        /// <param name="generatorToken">Token for generator</param>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="buffer">Shared buffer</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="delay">Delay between write() commands</param>
        /// <returns></returns>
        public static async Task<bool> SendData(
            Stream stream,
            Func<byte[], object, int> generator,
            object generatorToken = null,
            int bufferSize = 1024,
            byte[] buffer = null,
            Func<bool> cancellationToken = null,
            int delay = 0
            ) {
            cancellationToken = cancellationToken ?? ( () => false );
            buffer = buffer ?? new byte[ bufferSize ];
            if ( buffer.Length < bufferSize )
                throw new IndexOutOfRangeException( "bufferSize > buffer.Length" );
            int w;
            while ( ( w = generator( buffer, generatorToken ) ) > 0 ) {
                if ( cancellationToken() ) return false;
                await stream.WriteAsync( buffer, 0, w );
                if ( cancellationToken() ) return false;
                if ( delay > 0 )
                    await Task.Delay( delay );
            }
            return true;
        }

        /// <summary>
        /// Fill header
        /// </summary>
        /// <param name="buffer">Buffer to write</param>
        /// <param name="expression">Expression</param>
        /// <returns>Number of written bytes</returns>
        public static int Generate(byte[] buffer, MultiExpression expression) {
            var sizeBufferSize = expression.ComputeLengthDataSize();
            var sizeBuffer = new int[sizeBufferSize];
            expression.GetInsertLength( sizeBuffer );
            return expression.InsertAsciiBytes( sizeBuffer, buffer );
        }
    }
}