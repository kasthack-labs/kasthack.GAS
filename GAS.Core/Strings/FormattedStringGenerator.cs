using System;
using System.Linq;
using System.Text;
namespace GAS.Core.Strings
{
    public class FormattedStringGenerator : IExpression
    {
        public IExpression[] Expressions;
        /// <summary>
        /// Get string representation of expression execution result
        /// </summary>
        /// <returns>string result</returns>
        public string   GetString() {
            if ( Expressions.Length == 1 )
                return Expressions[0].GetString();
            return new string(GetChars());
        }
        /// <summary>
        /// Get char array representation of expression execution result
        /// </summary>
        /// <returns>char[] result</returns>
        public char[] GetChars() {
                return Functions.GetT<char>(1, Functions.GetCharsF, Expressions);
        }
        /// <summary>
        /// Get native representation of expression execution result
        /// </summary>
        /// <returns>ascii bytes</returns>
        public byte[] GetAsciiBytes() {
            return Functions.GetT<byte>(1, Functions.GetBytesF, Expressions);
        }
        /// <summary>
        /// Get bytes of result encoded with encoding
        /// </summary>
        /// <param name="_enc">encoding for encoding, lol</param>
        /// <returns>bytes</returns>
        public byte[] GetEncodingBytes(Encoding _enc) {
            return Functions.GetT<byte>(1, a => a.GetEncodingBytes(_enc), this.Expressions);
        }
        /// <summary>
        /// alias 4 GetString. 4 debugging
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return GetString();
        }
        public System.Collections.Generic.IEnumerable<byte[]> EnumAsciiBuffers() {
            return Expressions.SelectMany(a => a.EnumAsciiBuffers());
        }
        public System.Collections.Generic.IEnumerable<string> EnumStrings() {
            return Expressions.SelectMany(a => a.EnumStrings());
        }
        public unsafe void ComputeLen(ref int* outputdata) {
            throw new NotImplementedException();
        }
        public int ComputeMaxLenForSize() {
            int sum=0;
            for ( int i = 0; i < Expressions.Length; i++ )
                sum += Expressions[i].ComputeMaxLenForSize();
            return sum;
        }
    }
}
