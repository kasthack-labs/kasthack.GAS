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
        IEnumerable<byte[]> EnumAsciiBuffers();
        IEnumerable<string> EnumStrings();
        unsafe void ComputeLen(ref int* outputdata);
        int ComputeMaxLenForSize();
    }
}
