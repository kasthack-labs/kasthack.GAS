
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
        Functions funcs;
        Random rnd;
        public IntExpression(Functions _funcs, Random _rnd=null)
        {
            rnd = _rnd==null?new Random():_rnd;
            funcs = _funcs;
        }
        public string GetString()
        {
            return new string(Format == NumberFormat.Decimal ? funcs.int_to_dec_string(rnd.Next(Min,Max+1)) :
                                    funcs.int_to_hex_string(rnd.Next(Min, Max + 1)));
        }

        public char[] GetChars()
        {
            return Format == NumberFormat.Decimal ? funcs.int_to_dec_string(rnd.Next(Min, Max + 1)) :
                                    funcs.int_to_hex_string(rnd.Next(Min, Max + 1));
        }

        public byte[] GetEncodingBytes(Encoding enc)
        {
            return enc.GetBytes(Format == NumberFormat.Decimal ? funcs.int_to_dec_string(rnd.Next(Min, Max + 1)) :
                                    funcs.int_to_hex_string(rnd.Next(Min, Max + 1)));
        }


        public byte[] GetAsciiBytes()
        {
            return Format == NumberFormat.Decimal ? funcs.int_to_dec_string_bytes(rnd.Next(Min, Max + 1)) :
                                    funcs.int_to_hex_string_bytes(rnd.Next(Min, Max + 1));
        }
    }
}
