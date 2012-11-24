using System;
using System.Collections.Generic;
using System.Text;

namespace GAS.Core
{
    public interface IExpression
    {
        string GetString();
        char[] GetChars();
        byte[] GetAsciiBytes();
        byte[] GetEncodingBytes(Encoding enc);
        
    }
}
