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
        /// <summary>
        /// parse IntExpression _from string
        /// pointer will point to closing } of expression
        /// </summary>
        /// <param name="_from">pointer to 1st char after opening {</param>
        /// <param name="_outcount">returned value 4 read character count </param>
        /// <param name="_rnd">randomizer</param>
        /// <returns>parsed expression</returns>
        internal static unsafe IntExpression Parse(ref char* _from, out int _outcount, int _max_count, Random _rnd = null)
        {
            /*
             * TODO: add string validation
             */
            #region Variables
            if (_rnd == null)
                _rnd = new Random();
            int __cnt = 0;
            char* __end = _from + _max_count;
            _outcount = 0;
            IntExpression exp = new IntExpression(_rnd);
            #endregion
            #region Parse Format
            switch (*(_from += 2))//skip expression type+separator
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
            _from += 2;//skip format+separator
            _outcount += 4;//total move
            #endregion
            #region Parse Min
            //get min value length
            __cnt = Functions.FindChar(_from, __end, ':');
            /*while (_from < __end && *_from != ':')
            {
                __cnt++;
                _from++;
            };*/
            //parse min length
            exp.Min = Functions.qintparse(_from, 0, __cnt);
            _from += __cnt + 1;//skip separator
            _outcount += __cnt+1;//add skip 4 min
            #endregion
            #region Parse Max
            //same for max
            __cnt = Functions.FindChar(_from, __end, '}');
            /*while (_from < __end && *_from != '}')
            {
                __cnt++;
                _from++;
            };*/
            exp.Max = Functions.qintparse(_from, 0, __cnt);
            _from += __cnt;
            _outcount += __cnt;
            //skip closing bracket
            //_from++;
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
