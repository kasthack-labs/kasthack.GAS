using System;
using System.Text;
namespace GAS.Core.Strings
{
    public class StringExpression : IExpression
    {
        public StringFormat Format;
        public int Min, Max;
        Random rnd;
        [System.Diagnostics.DebuggerNonUserCode]
        public StringExpression(Random _rnd) {
            rnd = _rnd == null ? new Random() : _rnd;
        }
        public byte[] GetAsciiBytes() {
            switch ( Format ) {
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
        public byte[] GetEncodingBytes(Encoding enc) {
            byte[] output;
            switch ( Format ) {
                case StringFormat.Decimal:
                    output = enc.GetBytes(Functions.random_ascii(Min, Max, Functions._hex_chars, 0, 9));
                    break;
                case StringFormat.Hexadecimal:
                    output = enc.GetBytes(Functions.random_ascii(Min, Max, Functions._hex_chars, 0, 15));
                    break;
                case StringFormat.Letters:
                    output = enc.GetBytes(Functions.random_ascii(Min, Max, Functions._ascii_chars, 10, 61));
                    break;
                case StringFormat.LowerCase:
                    output = enc.GetBytes(Functions.random_ascii(Min, Max, Functions._ascii_chars, 10, 35));
                    break;
                case StringFormat.Random:
                    output = enc.GetBytes(Functions.random_ascii(Min, Max));
                    break;
                case StringFormat.Std:
                    output = enc.GetBytes(Functions.random_ascii(Min, Max, Functions._ascii_chars, 0, 61));
                    break;
                case StringFormat.UpperCase:
                    output = enc.GetBytes(Functions.random_ascii(Min, Max, Functions._ascii_chars, 36, 61));
                    break;
                case StringFormat.Urlencode:
                    output = enc.GetBytes(Functions.random_utf_urlencode_string(Min, Max));
                    break;
                default:
                    throw new ArgumentException("Bad string format");
            }
            return output;
        }
        public char[] GetChars() {
            switch ( Format ) {
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
        public string GetString() {
            switch ( Format ) {
                case StringFormat.Decimal:
                    return new string(Functions.random_ascii(Min, Max, Functions._hex_chars, 0, 9));
                case StringFormat.Hexadecimal:
                    return new string(Functions.random_ascii(Min, Max, Functions._hex_chars, 0, 15));
                case StringFormat.Letters:
                    return new string(Functions.random_ascii(Min, Max, Functions._ascii_chars, 10, 61));
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
        public override string ToString() {
            return GetString();
        }
    }
}
