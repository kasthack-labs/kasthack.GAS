using System;
using System.Collections.Generic;
using System.Text;
using GAS.Core.Strings;
using GAS.Core;
namespace GAS.Core.Strings
{
    public class CharExpression:IExpression
    {
        Random rnd;
        public int Min, Max;
        public CharExpression()
        {
            rnd = new Random();
        }
        public CharExpression(Random _rnd)
        {
            rnd = _rnd;
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

        internal static unsafe IExpression Parse(ref char* from, ref int cnt, ASCIIEncoding _enc)
        {
            throw new NotImplementedException();
        }
    }
}
