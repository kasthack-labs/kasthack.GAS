using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAS.Core.Strings;
using GAS.Core;
namespace GAS.Core.Strings
{
    public class FormattedStringGenerator:IExpression
    {
        IExpression[] Expressions;


        public string GetString()
        {
            //slow. for prototype only
            return String.Concat(Expressions.Select(a => a.GetString()).ToArray());
        }

        public char[] GetChars()
        {
            //slow. for prototype only
            return Expressions.SelectMany(a => a.GetChars()).ToArray();
        }

        public byte[] GetAsciiBytes()
        {
            //slow. for prototype only
            return Expressions.SelectMany(a => a.GetAsciiBytes()).ToArray();
        }

        public byte[] GetEncodingBytes(Encoding enc)
        {
            //slow. for prototype only
            return Expressions.SelectMany(a => a.GetEncodingBytes(enc)).ToArray();
        }
    }
}
