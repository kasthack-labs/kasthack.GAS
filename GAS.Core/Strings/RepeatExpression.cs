using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAS.Core.Strings
{
    public class RepeatExpression:IExpression
    {
        public int Min, Max;
        public FormattedStringGenerator GEN;
        public string GetString()
        {
            return GEN.GetString();
        }
        public override string ToString()
        {
            return GetString();
        }
        public char[] GetChars()
        {
            return GEN.GetChars();
        }

        public byte[] GetAsciiBytes()
        {
            return GEN.GetAsciiBytes();
        }

        public byte[] GetEncodingBytes(Encoding enc)
        {
            return GEN.GetEncodingBytes(enc);
        }

        public static unsafe RepeatExpression Parse(ref char* _from, out int _outcount, int _maxcount, Random _rnd=null,ASCIIEncoding _enc=null)
        {
            if (_enc == null)
                _enc = new ASCIIEncoding();
            if (_rnd == null)
                _rnd = new Random();
            _from+=3;
            RepeatExpression exp = new RepeatExpression();
            exp.GEN = FormattedStringGenerator.Parse(ref _from, out _outcount, _maxcount-3,  _enc, _rnd);
            _from += 3;
            _outcount += 6;
            int __cnt = 0;
            __cnt = Functions.FindChar(_from, (char*)(_from + _maxcount - _outcount), ':');
            exp.Min = Functions.qintparse(_from, 0, __cnt);
            _from += __cnt+1;
            __cnt = Functions.FindChar(_from, (char*)(_from + _maxcount - _outcount), '}');
            exp.Max = Functions.qintparse(_from, 0, __cnt);
            _from += __cnt;
            return exp;
        }
    }
}
