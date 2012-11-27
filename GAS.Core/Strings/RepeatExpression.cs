using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAS.Core.Strings
{
    public class RepeatExpression:IExpression
    {
        public int Min, Max;
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

        public static unsafe RepeatExpression Parse(ref char* from, ref int cnt, Random rnd=null,ASCIIEncoding enc=null)
        {
            if (enc == null)
                enc = new ASCIIEncoding();
            if (rnd == null)
                rnd = new Random();
            throw new NotImplementedException();
        }
    }
}
