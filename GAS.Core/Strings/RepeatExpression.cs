using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAS.Core.Strings
{
    public class RepeatExpression:IExpression
    {
        public int Min, Max;
        public FormattedStringGenerator Expression;
        public string GetString()
        {
            return Expression.GetString();
        }
        public override string ToString()
        {
            return GetString();
        }
        public char[] GetChars()
        {
            return Expression.GetChars();
        }

        public byte[] GetAsciiBytes()
        {
            return Expression.GetAsciiBytes();
        }

        public byte[] GetEncodingBytes(Encoding enc)
        {
            return Expression.GetEncodingBytes(enc);
        }
    }
}
