using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using GAS.Core.Strings;
using GAS.Core;
namespace GAS.Core.Strings
{
    public class IntExpression:IExpression
    {
        public NumberFormat Format;
        public int Min, Max;
        Random rnd;
        public IntExpression(Random _rnd=null)
        {
            rnd = _rnd==null?new Random():_rnd;
        }
        public string GetString()
        {
            return new string(Format == NumberFormat.Decimal ? Functions.int_to_dec_string(rnd.Next(Min,Max+1)) :
                                    Functions.int_to_hex_string(rnd.Next(Min, Max + 1)));
        }

        public char[] GetChars()
        {
            return Format == NumberFormat.Decimal ? Functions.int_to_dec_string(rnd.Next(Min, Max + 1)) :
                                    Functions.int_to_hex_string(rnd.Next(Min, Max + 1));
        }

        public byte[] GetEncodingBytes(Encoding enc)
        {
            return enc.GetBytes(Format == NumberFormat.Decimal ? Functions.int_to_dec_string(rnd.Next(Min, Max + 1)) :
                                   Functions.int_to_hex_string(rnd.Next(Min, Max + 1)));
        }


        public byte[] GetAsciiBytes()
        {
            return Format == NumberFormat.Decimal ? Functions.int_to_dec_string_bytes(rnd.Next(Min, Max + 1)) :
                                    Functions.int_to_hex_string_bytes(rnd.Next(Min, Max + 1));
        }

        internal static unsafe IntExpression Parse(ref char* from, ref int cnt, Random rnd=null)
        {
            /*
             * TODO: add string validation
             */
            if (rnd == null)
                rnd = new Random();
            int _cnt = 0;
            char* end = from + cnt;
            IntExpression exp = new IntExpression(rnd);
            if (rnd == null)
                rnd = new Random();
            from++;
            switch (*(from++))
            {
                case 'D':
                case 'd':
                    exp.Format=NumberFormat.Decimal;
                    break;
                case 'H':
                case 'h':
                    exp.Format=NumberFormat.Hex;
                    break;
                default:break;
            }
            from++;
            cnt = 0;
            while (from<end&&((int)*(from))!=':' ) {_cnt++;from++};
            exp.Min=Functions.qintparse((char*)(from-_cnt),0,_cnt);
            from++;
            cnt = 0;
            while (from<end&&((int)*(from)) !='}') {_cnt++;from++};
            exp.Max=Functions.qintparse((char*)(from-_cnt),0,_cnt);
            from++;
            return exp;
        }
    }
}
