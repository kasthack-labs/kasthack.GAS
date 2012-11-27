using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAS.Core.Strings
{
    public class RepeatExpression:IExpression
    {
        public string GetString()
        {
            throw new NotImplementedException();
        }

        public char[] GetChars()
        {
            throw new NotImplementedException();
        }

        public byte[] GetAsciiBytes()
        {
            throw new NotImplementedException();
        }

        public byte[] GetEncodingBytes(Encoding enc)
        {
            throw new NotImplementedException();
        }

        internal static unsafe IExpression Parse(ref char* from, ref int cnt, ASCIIEncoding _enc)
        {
            throw new NotImplementedException();
        }
    }
}
