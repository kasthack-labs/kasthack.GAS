using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GAS.Core
{
    public class StringExpression:IExpression
    {
        public StringFormat Format;
        public int Min, Max;
        Random rnd;
        Functions funcs;
        public StringExpression(Functions _funcs, Random _rnd)
        {
            funcs = _funcs;
            rnd = _rnd == null ? new Random() : _rnd;
        }

        public string GetString()
        {
            switch (Format)
            {
                case StringFormat.Decimal:
                    return new string(funcs.random_ascii(Min,Max, Functions._hex_chars,0, 9));
                case StringFormat.Hexadecimal:
                    return new string(funcs.random_ascii(Min,Max, Functions._hex_chars,0, 15));
                case StringFormat.Letters:
                    return new string(funcs.random_ascii(Min,Max, Functions._ascii_chars,10, 61));
                case StringFormat.LowerCase:
                    return new string(funcs.random_ascii(Min, Max, Functions._ascii_chars, 10, 35));
                case StringFormat.Random:
                    return new string(funcs.random_ascii(Min, Max));
                case StringFormat.Std:
                    return new string(funcs.random_ascii(Min, Max, Functions._ascii_chars, 0, 61));
                case StringFormat.UpperCase:
                    return new string(funcs.random_ascii(Min, Max, Functions._ascii_chars, 36, 61));
                case StringFormat.Urlencode:
                    return new string(funcs.random_utf_urlencode_string(Min, Max));
                default:
                    throw new ArgumentException("Bad string format");
            }
        }

        public char[] GetChars()
        {
            switch (Format)
            {
                case StringFormat.Decimal:
                    return funcs.random_ascii(Min, Max, Functions._hex_chars, 0, 9);
                case StringFormat.Hexadecimal:
                    return funcs.random_ascii(Min, Max, Functions._hex_chars, 0, 15);
                case StringFormat.Letters:
                    return funcs.random_ascii(Min, Max, Functions._ascii_chars, 10, 61);
                case StringFormat.LowerCase:
                    return funcs.random_ascii(Min, Max, Functions._ascii_chars, 10, 35);
                case StringFormat.Random:
                    return funcs.random_ascii(Min, Max);
                case StringFormat.Std:
                    return funcs.random_ascii(Min, Max, Functions._ascii_chars, 0, 61);
                case StringFormat.UpperCase:
                    return funcs.random_ascii(Min, Max, Functions._ascii_chars, 36, 61);
                case StringFormat.Urlencode:
                    return funcs.random_utf_urlencode_string(Min, Max);
                default:
                    throw new ArgumentException("Bad string format");
            }
        }

        public byte[] GetAsciiBytes()
        {
            throw new NotImplementedException();
        }

        public byte[] GetEncodingBytes(Encoding enc)
        {
            throw new NotImplementedException();
        }
    }
}
