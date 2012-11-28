using System;
using System.Collections.Generic;
using System.Text;
using GAS.Core.Strings;
using GAS.Core;
namespace GAS.Core.Strings
{
    public class CharExpression : IExpression
    {
        Random rnd;
        public override string ToString()
        {
            return GetString();
        }
        public int Min, Max;
        public CharExpression(Random _rnd=null)
        {
            rnd = _rnd != null ? rnd : new Random();
        }
        public string GetString()
        {
            return ((char)rnd.Next(Min, Max + 1)).ToString();
        }
        public char[] GetChars()
        {
            return new char[] { (char)rnd.Next(Min, Max + 1) };
        }
        public byte[] GetEncodingBytes(Encoding enc)
        {
            return enc.GetBytes(new char[] { (char)rnd.Next(Min, Max + 1) });
        }
        public byte[] GetAsciiBytes()
        {
            return new byte[] { (byte)rnd.Next(Min, Max) };
        }
        public static unsafe CharExpression Parse(ref char* from, out int outcount, int maxcount, Random rnd = null)
        {
            /*
             * TODO: add string validation
             */
            int _cnt = 0;
            char* end = from + maxcount;
            CharExpression exp = new CharExpression(rnd);
            if (rnd == null)
                rnd = new Random();
            from++;
            outcount = 0;
            while (from < end && ((int)*(from)) != ':')
            {
                _cnt++;
                from++;
            }
            exp.Min = Functions.qintparse((char*)(from - _cnt), 0, _cnt);
            from++;
            outcount = 0;
            while (from < end && ((int)*(from)) != '}') { _cnt++; from++; };
            exp.Max = Functions.qintparse((char*)(from - _cnt), 0, _cnt);
            from++;
            return exp;
        }
    }
}
