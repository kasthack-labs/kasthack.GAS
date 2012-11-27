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
        public StringExpression( Random _rnd)
        {
            rnd = _rnd == null ? new Random() : _rnd;
        }

        public override string ToString()
        {
            return GetString();
        }
        public string GetString()
        {
            switch (Format)
            {
                case StringFormat.Decimal:
                    return new string(Functions.random_ascii(Min,Max, Functions._hex_chars,0, 9));
                case StringFormat.Hexadecimal:
                    return new string(Functions.random_ascii(Min,Max, Functions._hex_chars,0, 15));
                case StringFormat.Letters:
                    return new string(Functions.random_ascii(Min,Max, Functions._ascii_chars,10, 61));
                case StringFormat.LowerCase:
                    return new string(Functions.random_ascii(Min, Max, Functions._ascii_chars, 10, 35));
                case StringFormat.Random:
                    return new string(Functions.random_ascii(Min, Max));
                case StringFormat.Std:
                    return new string(Functions.random_ascii(Min, Max, Functions._ascii_chars, 0, 61));
                case StringFormat.UpperCase:
                    return new string(Functions.random_ascii(Min, Max, Functions._ascii_chars, 36, 61));
                case StringFormat.Urlencode:
                    return new string(Functions.random_utf_urlencode_string(Min, Max));
                default:
                    throw new ArgumentException("Bad string format");
            }
        }

        public char[] GetChars()
        {
            switch (Format)
            {
                case StringFormat.Decimal:
                    return Functions.random_ascii(Min, Max, Functions._hex_chars, 0, 9);
                case StringFormat.Hexadecimal:
                    return Functions.random_ascii(Min, Max, Functions._hex_chars, 0, 15);
                case StringFormat.Letters:
                    return Functions.random_ascii(Min, Max, Functions._ascii_chars, 10, 61);
                case StringFormat.LowerCase:
                    return Functions.random_ascii(Min, Max, Functions._ascii_chars, 10, 35);
                case StringFormat.Random:
                    return Functions.random_ascii(Min, Max);
                case StringFormat.Std:
                    return Functions.random_ascii(Min, Max, Functions._ascii_chars, 0, 61);
                case StringFormat.UpperCase:
                    return Functions.random_ascii(Min, Max, Functions._ascii_chars, 36, 61);
                case StringFormat.Urlencode:
                    return Functions.random_utf_urlencode_string(Min, Max);
                default:
                    throw new ArgumentException("Bad string format");
            }
        }

        public byte[] GetAsciiBytes()
        {
            switch (Format)
            {
                case StringFormat.Decimal:
                    return Functions.random_ascii_bytes(Min, Max, Functions._hex_chars_bytes, 0, 9);
                case StringFormat.Hexadecimal:
                    return Functions.random_ascii_bytes(Min, Max, Functions._hex_chars_bytes, 0, 15);
                case StringFormat.Letters:
                    return Functions.random_ascii_bytes(Min, Max, Functions._ascii_chars_bytes, 10, 61);
                case StringFormat.LowerCase:
                    return Functions.random_ascii_bytes(Min, Max, Functions._ascii_chars_bytes, 10, 35);
                case StringFormat.Random:
                    return Functions.random_ascii_bytes(Min, Max);
                case StringFormat.Std:
                    return Functions.random_ascii_bytes(Min, Max, Functions._ascii_chars_bytes, 0, 61);
                case StringFormat.UpperCase:
                    return Functions.random_ascii_bytes(Min, Max, Functions._ascii_chars_bytes, 36, 61);
                case StringFormat.Urlencode:
                    return Functions.random_utf_urlencode_string_bytes(Min, Max);
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
                    output = enc.GetBytes(Functions.random_ascii(Min, Max, Functions._hex_chars, 0, 9));
                    break;
                case StringFormat.Hexadecimal:
                    output = enc.GetBytes( Functions.random_ascii(Min, Max, Functions._hex_chars, 0, 15));
                    break;
                case StringFormat.Letters:
                    output = enc.GetBytes( Functions.random_ascii(Min, Max, Functions._ascii_chars, 10, 61));
                    break;
                case StringFormat.LowerCase:
                    output = enc.GetBytes( Functions.random_ascii(Min, Max, Functions._ascii_chars, 10, 35));
                    break;
                case StringFormat.Random:
                    output = enc.GetBytes( Functions.random_ascii(Min, Max));
                    break;
                case StringFormat.Std:
                    output = enc.GetBytes( Functions.random_ascii(Min, Max, Functions._ascii_chars, 0, 61));
                    break;
                case StringFormat.UpperCase:
                    output = enc.GetBytes( Functions.random_ascii(Min, Max, Functions._ascii_chars, 36, 61));
                    break;
                case StringFormat.Urlencode:
                    output = enc.GetBytes( Functions.random_utf_urlencode_string(Min, Max));
                    break;
                default:
                    throw new ArgumentException("Bad string format");
            }
            return output;
        }

        public static unsafe StringExpression Parse(ref char* from, ref int outcount, Random rnd=null)
        {
            /*
             * TODO: add string validation
             */
            if (rnd == null)
                rnd = new Random();
            int _cnt = 0;
            char* end = from + outcount;
            StringExpression exp = new StringExpression(rnd);
            from++;
            switch (*(++from))
            {
                case 'D':
                    exp.Format=StringFormat.Decimal;
                    break;
                case 'H':
                    exp.Format=StringFormat.Hexadecimal;
                    break;
                case 'L':
                    exp.Format=StringFormat.Letters;
                    break;
                case 'a':
                    exp.Format=StringFormat.LowerCase;
                    break;
                case 'R':
                    exp.Format=StringFormat.Random;
                    break;
                case 'S':
                    exp.Format=StringFormat.Std;
                    break;
                case 'A':
                    exp.Format=StringFormat.UpperCase;
                    break;
                case 'U':
                    exp.Format=StringFormat.Urlencode;
                    break;
                default:throw new FormatException("Bad string format");
            }
            from+=2;
            outcount = 2;
            while (from < end && *from != ':')
            {
                _cnt++;
                from++;
            }
            exp.Min=Functions.qintparse((char*)(from-_cnt),0,_cnt);
            outcount += _cnt;
            from++;
            _cnt = 0;
            while (from<end&&*from !='}') 
            {
                _cnt++;
                from++;
            };
            exp.Max=Functions.qintparse((char*)(from-_cnt),0,_cnt);
            from++;
            outcount += _cnt;
            return exp;
        }
    }
}
