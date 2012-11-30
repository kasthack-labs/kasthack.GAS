using System.Text;
using System.Linq;
using System;
namespace GAS.Core.Strings
{
    public class RepeatExpression : IExpression
    {
        public int Min, Max;
        public FormattedStringGenerator Expressions;
        Random rnd;
        public RepeatExpression(Random _rnd=null) {
            rnd=_rnd==null?new Random():_rnd;
        }
        public string GetString() {
            var __r =rnd.Next(Min,Max+1);
            return String.Concat(Enumerable.Range(0,__r).
                Select(
                    a=>Expressions.GetString()
                ));
        }
        public byte[] GetAsciiBytes() {
            //return Expressions.GetAsciiBytes();
            return Enumerable.Range(0, rnd.Next(Min, Max)).SelectMany(a => Expressions.GetAsciiBytes()).ToArray();
        }
        public char[] GetChars() {
            return Enumerable.Range(0, rnd.Next(Min, Max)).SelectMany(a => Expressions.GetChars()).ToArray();
        }
        public byte[] GetEncodingBytes(Encoding enc) {
            return Enumerable.Range(0, rnd.Next(Min, Max)).SelectMany(a => Expressions.GetEncodingBytes(enc)).ToArray();
        }
        public override string ToString() {
            return GetString();
        }


        public System.Collections.Generic.IEnumerable<byte[]> EnumAsciiBuffers() {
            return Enumerable.Range(0, rnd.Next(Min, Max)).SelectMany(a => Expressions.EnumAsciiBuffers()).ToArray();
        }

        public System.Collections.Generic.IEnumerable<string> EnumStrings() {
            var __r = rnd.Next(Min, Max + 1);
            return Enumerable.Range(0, __r).
                SelectMany(
                    a => Expressions.EnumStrings()
                );
        }
    }
}
