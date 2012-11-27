using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAS.Core.Strings;
using GAS.Core;
namespace GAS.Core.Strings
{
    public class FormattedStringGenerator : IExpression
    {
        IExpression[] Expressions;
        /// <summary>
        /// Parses string as ExpressionTree
        /// </summary>
        /// <param name="from">pointer to start parsing</param>
        /// <param name="outcount">output to save move offset</param>
        /// <param name="_enc">encoding instanse for generated expressions</param>
        /// <param name="_rnd">randomizer instanse for generated expressions</param>
        /// <param name="_max_count">max string parse length</param>
        /// <returns>expression tree</returns>
        public unsafe static FormattedStringGenerator Parse(ref char* from, ref int outcount, int _max_count, ASCIIEncoding _enc = null, Random _rnd = null)
        {
            #region Variables
            List<IExpression> __exprs = new List<IExpression>();
            char*   start = from,
                    end = from + _max_count;
            if (_rnd == null)
                _rnd = new Random();
            if (_enc == null)
                _enc = new ASCIIEncoding();
            #endregion
            #region Parse
            while (from < end)
            {
                if (*from == '}') break;
                if (*from == '{')
                {
                    #region Add prev string
                    if (--from > start)
                    {
                        __exprs.Add(new StaticASCIIStringExpression(new string(start, 0, (int)(from + 1 - start)), _enc));
                        start = from;
                    }
                    from++;
                    #endregion
                    int cnt = 0;//(int)(end - from);
                    __exprs.Add(Functions.ExprSelect(ref from, ref cnt, (int)(end-from), _rnd, _enc));
                    outcount += cnt;
                }
                from++;
                outcount++;
            }
            #endregion
            #region Ending string
            if (--from > start)
            {
                __exprs.Add(new StaticASCIIStringExpression(new string(start, 0, (int)(from + 1 - start)), _enc));
                start = from;
            }
            from++;
            #endregion
            return new FormattedStringGenerator() { Expressions = __exprs.ToArray() };
        }
        /// <summary>
        /// Get string representation of expression execution result
        /// </summary>
        /// <returns>string result</returns>
        public string GetString()
        {
            //slow. for prototype only
            return String.Concat(Expressions.Select(a => a.GetString()).ToArray());
        }
        /// <summary>
        /// Get char array representation of expression execution result
        /// </summary>
        /// <returns>char[] result</returns>
        public char[] GetChars()
        {
            //slow. for prototype only
            return Expressions.SelectMany(a => a.GetChars()).ToArray();
        }
        /// <summary>
        /// Get native representation of expression execution result
        /// </summary>
        /// <returns>ascii bytes</returns>
        public byte[] GetAsciiBytes()
        {
            //slow. for prototype only
            return Expressions.SelectMany(a => a.GetAsciiBytes()).ToArray();
        }
        /// <summary>
        /// Get bytes of result encoded with encoding
        /// </summary>
        /// <param name="_enc">encoding for encoding, lol</param>
        /// <returns>bytes</returns>
        public byte[] GetEncodingBytes(Encoding _enc)
        {
            //slow. for prototype only
            return Expressions.SelectMany(a => a.GetEncodingBytes(_enc)).ToArray();
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
