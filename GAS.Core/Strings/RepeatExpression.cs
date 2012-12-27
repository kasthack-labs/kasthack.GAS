using System.Text;
using System.Linq;
using System;
namespace GAS.Core.Strings
{
    public class RepeatExpression : IExpression
    {
        public int Min, Max;
        public IExpression[] Expressions;
        Random rnd;
        public RepeatExpression(Random _rnd=null) {
            rnd=_rnd==null?new Random():_rnd;
        }
        public string GetString() {
            return new string(GetChars());
        }
        public byte[] GetAsciiBytes() {
            return GetAsciiBytes(rnd.Next(Min, Max));
        }
        public byte[] GetAsciiBytes(int _RepeatCount) {
            return Functions.GetT<byte>(_RepeatCount, Functions.GetBytesF, Expressions);
        }
        public char[] GetChars() {
            return GetChars(rnd.Next(Min, Max));
        }
        public char[] GetChars(int _RepeatCount) {
            //same as get ascii bytes but with chars
            return Functions.GetT<char>(_RepeatCount, Functions.GetCharsF, Expressions);
        }
        public byte[] GetEncodingBytes(Encoding _enc) {
            return GetEncodingBytes(_enc, rnd.Next(Min,Max));
        }
        public byte[] GetEncodingBytes(Encoding _enc, int _RepeatCount) {
            return Functions.GetT<byte>(_RepeatCount, a => a.GetEncodingBytes(_enc), this.Expressions);
        }
        public override string ToString() {
            return GetString();
        }
        public System.Collections.Generic.IEnumerable<byte[]> EnumAsciiBuffers() {
            return Enumerable.Range(0, rnd.Next(Min, Max)).SelectMany(a => Expressions.SelectMany(b=>b.EnumAsciiBuffers())).ToArray();
        }
        public System.Collections.Generic.IEnumerable<string> EnumStrings() {
            var __r = rnd.Next(Min, Max + 1);
            return Enumerable.Range(0, __r).
                SelectMany(
                    a => Expressions.SelectMany(b=>b.EnumStrings())
                );
        }
        public unsafe void ComputeLen(ref int* _outputdata) {
            throw new NotImplementedException();
        }
        public int ComputeMaxLenForSize() {
            int sum=0;
            for ( int i = 0; i < Expressions.Length; i++ )
                sum += Expressions[i].ComputeMaxLenForSize();
            return sum * Max+2;
        }

    }   
}
