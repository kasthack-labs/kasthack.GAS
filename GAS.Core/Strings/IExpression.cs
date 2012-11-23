using System;
using System.Collections.Generic;
using System.Text;

namespace GAS.Core
{
    public interface IExpression
    {
        public string GetString();
        public char[] GetChars();
        public byte[] GetAsciiBytes();
        public byte[] GetEncodingBytes(Encoding enc);
        
    }
}
