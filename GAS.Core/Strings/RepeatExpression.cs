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

        public static unsafe RepeatExpression Parse(ref char* from, ref int cnt, Random rnd=null,ASCIIEncoding enc=null)
        {
            if (enc == null)
                enc = new ASCIIEncoding();
            if (rnd == null)
                rnd = new Random();
            from+=3;
            RepeatExpression exp = new RepeatExpression();
            exp.GEN = FormattedStringGenerator.Parse(ref from, ref cnt, enc, rnd);    
            return exp;
        }
    }
}
