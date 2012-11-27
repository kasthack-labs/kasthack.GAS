using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAS.Core.Strings;
using GAS.Core;
namespace GAS.Core.Strings
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
            switch (Format)
            {
                case StringFormat.Decimal:
                    return funcs.random_ascii_bytes(Min, Max, Functions._hex_chars_bytes, 0, 9);
                case StringFormat.Hexadecimal:
                    return funcs.random_ascii_bytes(Min, Max, Functions._hex_chars_bytes, 0, 15);
                case StringFormat.Letters:
                    return funcs.random_ascii_bytes(Min, Max, Functions._ascii_chars_bytes, 10, 61);
                case StringFormat.LowerCase:
                    return funcs.random_ascii_bytes(Min, Max, Functions._ascii_chars_bytes, 10, 35);
                case StringFormat.Random:
                    return funcs.random_ascii_bytes(Min, Max);
                case StringFormat.Std:
                    return funcs.random_ascii_bytes(Min, Max, Functions._ascii_chars_bytes, 0, 61);
                case StringFormat.UpperCase:
                    return funcs.random_ascii_bytes(Min, Max, Functions._ascii_chars_bytes, 36, 61);
                case StringFormat.Urlencode:
                    return funcs.random_utf_urlencode_string_bytes(Min, Max);
                default:
                    throw new ArgumentException("Bad string format");
            }
        }

        public byte[] GetEncodingBytes(Encoding enc)
        {
            byte[] output;
            switch (Format)
            {
                case StringFormat.Decimal:
                    output = enc.GetBytes(funcs.random_ascii(Min, Max, Functions._hex_chars, 0, 9));
                    break;
                case StringFormat.Hexadecimal:
                    output = enc.GetBytes( funcs.random_ascii(Min, Max, Functions._hex_chars, 0, 15));
                    break;
                case StringFormat.Letters:
                    output = enc.GetBytes( funcs.random_ascii(Min, Max, Functions._ascii_chars, 10, 61));
                    break;
                case StringFormat.LowerCase:
                    output = enc.GetBytes( funcs.random_ascii(Min, Max, Functions._ascii_chars, 10, 35));
                    break;
                case StringFormat.Random:
                    output = enc.GetBytes( funcs.random_ascii(Min, Max));
                    break;
                case StringFormat.Std:
                    output = enc.GetBytes( funcs.random_ascii(Min, Max, Functions._ascii_chars, 0, 61));
                    break;
                case StringFormat.UpperCase:
                    output = enc.GetBytes( funcs.random_ascii(Min, Max, Functions._ascii_chars, 36, 61));
                    break;
                case StringFormat.Urlencode:
                    output = enc.GetBytes( funcs.random_utf_urlencode_string(Min, Max));
                    break;
                default:
                    throw new ArgumentException("Bad string format");
            }
            return output;
        }

        internal static unsafe IExpression Parse(ref char* from, ref int cnt, ASCIIEncoding _enc)
        {
            throw new NotImplementedException();
        }
    }
}
