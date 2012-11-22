
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GAS.Core
{
    class IntExpression:IExpression
    {
        NumberFormat Format;
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
