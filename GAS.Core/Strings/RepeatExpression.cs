using System.Text;

namespace GAS.Core.Strings
{
    public class RepeatExpression : IExpression
    {
        public int Min, Max;
        public FormattedStringGenerator Expression;
        public string GetString() {
            return Expression.GetString();
        }
        public byte[] GetAsciiBytes() {
            return Expression.GetAsciiBytes();
        }
        public char[] GetChars() {
            return Expression.GetChars();
        }
        public byte[] GetEncodingBytes(Encoding enc) {
            return Expression.GetEncodingBytes(enc);
        }
        public override string ToString() {
            return GetString();
        }
    }
}
