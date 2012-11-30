using System;
using System.Text;
namespace GAS.Core.Strings
{
    public class CharExpression : IExpression
    {
        Random rnd;
        public int Min, Max;
        [System.Diagnostics.DebuggerNonUserCode]
        public CharExpression(Random _rnd = null) {
            rnd = _rnd != null ? rnd : new Random();
        }
        public byte[] GetAsciiBytes() {
            return new byte[] { (byte)rnd.Next(Min, Max) };
        }
        public char[] GetChars() {
            return new char[] { (char)rnd.Next(Min, Max + 1) };
        }
        public byte[] GetEncodingBytes(Encoding enc) {
            return enc.GetBytes(new char[] { (char)rnd.Next(Min, Max + 1) });
        }
        public string GetString() {
            return ( (char)rnd.Next(Min, Max + 1) ).ToString();
        }
        public override string ToString() {
            return GetString();
        }
    }
}
