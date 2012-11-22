
using System;
using System.Collections.Generic;
using System.Text;

namespace GAS.Core
{
    class IntExpression:IExpression
    {
        public string GetString()
        {
            throw new NotImplementedException();
        }

        public char[] GetChars()
        {
            throw new NotImplementedException();
        }

        public byte[] GetEncodingBytes(Encoding enc)
        {
            throw new NotImplementedException();
        }
    }
}
