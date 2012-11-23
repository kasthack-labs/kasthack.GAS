using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAS.Core
{
    public class StaticASCIIStringExpression:IExpression
    {
        ASCIIEncoding enc;
        byte[] buf;
        public StaticASCIIStringExpression(string _str, ASCIIEncoding _enc = null)
        {
            enc = _enc == null ? new ASCIIEncoding() : _enc;
            buf = enc.GetBytes(_str);
        }
        public StaticASCIIStringExpression(char[] _str, ASCIIEncoding _enc = null)
        {
            enc = _enc == null ? new ASCIIEncoding() : _enc;
            buf = enc.GetBytes(_str);
        }
        public StaticASCIIStringExpression(byte[] _str, ASCIIEncoding _enc = null)
        {
            enc = _enc == null ? new ASCIIEncoding() : _enc;
            buf = _str;
        }
        public string GetString()
        {
            return enc.GetString(buf);
        }
        public char[] GetChars()
        {
            return enc.GetChars(buf);
        }
        public byte[] GetAsciiBytes()
        {
            return buf;
        }
        public byte[] GetEncodingBytes(Encoding _enc)
        {
            return _enc.GetBytes(enc.GetChars(buf));
        }
    }
}