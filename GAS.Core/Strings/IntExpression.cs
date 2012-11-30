using System;
using System.Text;
namespace GAS.Core.Strings
{
    public class IntExpression : IExpression
    {
        public NumberFormat Format;
        public int Min, Max;
        Random rnd;
        [System.Diagnostics.DebuggerNonUserCode]
        public IntExpression(Random _rnd = null) {
            rnd = _rnd == null ? new Random() : _rnd;
        }
        /// <summary>
        /// Get string representation of expression execution result
        /// </summary>
        /// <returns>string result</returns>
        public string GetString() {
            return new string(Format == NumberFormat.Decimal ? Functions.int_to_dec_string(rnd.Next(Min, Max + 1)) :
                                    Functions.int_to_hex_string(rnd.Next(Min, Max + 1)));
        }
        /// <summary>
        /// Get char array representation of expression execution result
        /// </summary>
        /// <returns>char[] result</returns>
        public char[] GetChars() {
            return Format == NumberFormat.Decimal ? Functions.int_to_dec_string(rnd.Next(Min, Max + 1)) :
                                    Functions.int_to_hex_string(rnd.Next(Min, Max + 1));
        }
        /// <summary>
        /// Get native representation of expression execution result
        /// </summary>
        /// <returns>ascii bytes</returns>
        public byte[] GetAsciiBytes() {
            return Format == NumberFormat.Decimal ? Functions.int_to_dec_string_bytes(rnd.Next(Min, Max + 1)) :
                                    Functions.int_to_hex_string_bytes(rnd.Next(Min, Max + 1));
        }
        /// <summary>
        /// Get bytes of result encoded with encoding
        /// </summary>
        /// <param name="_enc">encoding for encoding, lol</param>
        /// <returns>bytes</returns>
        public byte[] GetEncodingBytes(Encoding enc) {
            return enc.GetBytes(Format == NumberFormat.Decimal ? Functions.int_to_dec_string(rnd.Next(Min, Max + 1)) :
                                   Functions.int_to_hex_string(rnd.Next(Min, Max + 1)));
        }
        /// <summary>
        /// alias 4 GetString. 4 debugging
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return GetString();
        }
    }
}
