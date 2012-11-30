using System.Text;
using System.Linq;
using System;
namespace GAS.Core.Strings
{
    public class RepeatExpression : IExpression
    {
        public int Min, Max;
        public FormattedStringGenerator Expression;
        Random rnd;
        public RepeatExpression(Random _rnd=null) {
            rnd=_rnd==null?new Random():_rnd;
        }
        public string GetString() {
            var __r =rnd.Next(Min,Max+1);
            return String.Concat(Enumerable.Range(0,__r).
                Select(
                    a=>Expression.GetString()
                ));
        }
        public byte[] GetAsciiBytes() {
            //return Expression.GetAsciiBytes();
            return Enumerable.Range(0, rnd.Next(Min, Max)).SelectMany(a => Expression.GetAsciiBytes()).ToArray();
        }
        public char[] GetChars() {
            return Enumerable.Range(0, rnd.Next(Min, Max)).SelectMany(a => Expression.GetChars()).ToArray();
        }
        public byte[] GetEncodingBytes(Encoding enc) {
            return Enumerable.Range(0, rnd.Next(Min, Max)).SelectMany(a => Expression.GetEncodingBytes(enc)).ToArray();
        }
        public override string ToString() {
            return GetString();
        }
    }
}
