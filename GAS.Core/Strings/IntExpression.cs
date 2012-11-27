using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using GAS.Core.Strings;
using GAS.Core;
namespace GAS.Core.Strings
{
    public class IntExpression : IExpression
    {
        public NumberFormat Format;
        public int Min, Max;
        Random rnd;
        public IntExpression(Random _rnd = null)
        {
            rnd = _rnd == null ? new Random() : _rnd;
        }
        /// <summary>
        /// Get string representation of expression execution result
        /// </summary>
        /// <returns>string result</returns>
        public string GetString()
        {
            return new string(Format == NumberFormat.Decimal ? Functions.int_to_dec_string(rnd.Next(Min, Max + 1)) :
                                    Functions.int_to_hex_string(rnd.Next(Min, Max + 1)));
        }
        /// <summary>
        /// Get char array representation of expression execution result
        /// </summary>
        /// <returns>char[] result</returns>
        public char[] GetChars()
        {
            return Format == NumberFormat.Decimal ? Functions.int_to_dec_string(rnd.Next(Min, Max + 1)) :
                                    Functions.int_to_hex_string(rnd.Next(Min, Max + 1));
        }
        /// <summary>
        /// Get native representation of expression execution result
        /// </summary>
        /// <returns>ascii bytes</returns>
        public byte[] GetAsciiBytes()
        {
            return Format == NumberFormat.Decimal ? Functions.int_to_dec_string_bytes(rnd.Next(Min, Max + 1)) :
                                    Functions.int_to_hex_string_bytes(rnd.Next(Min, Max + 1));
        }
        /// <summary>
        /// Get bytes of result encoded with encoding
        /// </summary>
        /// <param name="_enc">encoding for encoding, lol</param>
        /// <returns>bytes</returns>
        public byte[] GetEncodingBytes(Encoding enc)
        {
            return enc.GetBytes(Format == NumberFormat.Decimal ? Functions.int_to_dec_string(rnd.Next(Min, Max + 1)) :
                                   Functions.int_to_hex_string(rnd.Next(Min, Max + 1)));
        }
        internal static unsafe IntExpression Parse(ref char* from, ref int outcount, Random rnd = null)
        {
            /*
             * TODO: add string validation
             */
            #region Variables
            if (rnd == null)
                rnd = new Random();
            int _cnt = 0;
            char* end = from + outcount;
            IntExpression exp = new IntExpression(rnd);
            outcount = 0;
            #endregion
            #region Parse Format
            switch (*(from += 2))//skip expression type+separator
            {
                case 'D':
                case 'd':
                    exp.Format = NumberFormat.Decimal;
                    break;
                case 'H':
                case 'h':
                    exp.Format = NumberFormat.Hex;
                    break;
                default: break;
            }
            from += 2;//skip format+separator
            outcount = 4;//total move
            #endregion
            #region Parse Min
            //get min value length
            while (from < end && *from != ':')
            {
                _cnt++;
                from++;
            };
            //parse min length
            exp.Min = Functions.qintparse((char*)(from - _cnt), 0, _cnt);
            from++;//skip separator
            outcount += _cnt;//add skip 4 min
            #endregion
            #region Parse Max
            //same for max
            _cnt = 0;
            while (from < end && *from != '}')
            {
                _cnt++;
                from++;
            };
            exp.Max = Functions.qintparse((char*)(from - _cnt), 0, _cnt);
            outcount += _cnt;
            //skip closing bracket
            from++;
            #endregion
            return exp;
        }
        /// <summary>
        /// alias 4 GetString. 4 debugging
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetString();
        }
    }
}
