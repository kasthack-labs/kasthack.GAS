using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAS.Core.Strings;
using GAS.Core;
namespace GAS.Core.Strings
{
    public class FormattedStringGenerator : IExpression
    {
        IExpression[] Expressions;

        public unsafe static FormattedStringGenerator Parse(ref char* from, ref int count, ASCIIEncoding _enc=null,Random rnd=null)
        {
            if (rnd == null)
                rnd = new Random();
            if (_enc==null)
                _enc=new ASCIIEncoding();
            char* start = from, end = from + count;
            count = 0;
            List<IExpression> exprs = new List<IExpression>();
            while (from < end)
            {
                if (*from == '}') break;
                if (*from == '{')
                {
                    if (--from > start)
                    {
                        exprs.Add(new StaticASCIIStringExpression(new string(start, 0, (int)(from - start)),_enc));
                        start = from;
                    }
                    from++;
                    int cnt=(int)(end-from);
                    exprs.Add(ExprSelect(ref from, ref cnt, rnd,_enc));
                    count += cnt;
                }
                from++;
                count++;
            }
            if (--from > start)
            {
                exprs.Add(new StaticASCIIStringExpression(new string(from, 0, (int)(from - start)),_enc));
                start = from;
            }
            from++;
            //count=end-fr
            return new FormattedStringGenerator()
            {
                Expressions = exprs.ToArray()
            };
        }

        private static unsafe IExpression ExprSelect(ref char* from, ref int cnt, Random rnd=null,ASCIIEncoding enc=null)
        {
            if (enc == null)
                enc = new ASCIIEncoding();
            if (rnd == null)
                rnd = new Random();
            switch (*from++)
            {
                case 'I':
                    return IntExpression.Parse(ref from, ref cnt,rnd);//fine
                case 'C':
                    return CharExpression.Parse(ref from, ref cnt, rnd);//fine
                case 'S':
                    return StringExpression.Parse(ref from, ref cnt,rnd);//
                case 'R':
                    return RepeatExpression.Parse(ref from, ref cnt, rnd,enc);
                default :
                    throw new FormatException("Not supported expression");
            }
        }
        public string GetString()
        {
            //slow. for prototype only
            return String.Concat(Expressions.Select(a => a.GetString()).ToArray());
        }
        public char[] GetChars()
        {
            //slow. for prototype only
            return Expressions.SelectMany(a => a.GetChars()).ToArray();
        }
        public byte[] GetAsciiBytes()
        {
            //slow. for prototype only
            return Expressions.SelectMany(a => a.GetAsciiBytes()).ToArray();
        }
        public byte[] GetEncodingBytes(Encoding enc)
        {
            //slow. for prototype only
            return Expressions.SelectMany(a => a.GetEncodingBytes(enc)).ToArray();
        }
    }
}
