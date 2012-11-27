using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GAS.Core.Strings;
using GAS.Core;
namespace GAS.Core.Strings
{
    public class Functions
    {
        #region Variables
        static Random random = new Random();
        #region magic!
            //public jfl
            public static char[] _hex_chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            public static char[] _ascii_chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
                                                'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
                                                'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
                                                'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                                                '!','"','#','$','%','&','\'','(',')','*','+',',','-','.','/',':',';',
                                                '<','=','>','?','@','[','\\',']','^','_','`','{','|','}','~'};
            public static byte[] _ascii_chars_bytes = { 48,49,50,51,52,53,54,55,56,57,97,98,99,100,101,102,103,104,105,106,107,
                                                          108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,65,66,67,
                                                          68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,
                                                          33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,58,59,60,61,62,63,64,91,
                                                          92,93,94,95,96,123,124,125,126};
            public static byte[] _hex_chars_bytes = { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 97, 98, 99, 100, 101, 102,};
            public static ushort[] _offsets = { 0, 0, 33, 48, 65, 97 };
            public static ushort[] _lengs = { 65535, 32, 32, 10, 26, 26 };
        #endregion
        static Regex RepeatRegex = new Regex(@"{#R:\{\$\$(?<inner_expression>.*)\$\$\}:(?<from>[0-9]+):(?<to>[0-9]+)#\}");
        static Regex AnyExpressionRegex = new Regex(@"\{#(?<expression_type>[ICS])(?<string_charset>:(?<from>[0-9]+):(?<to>[0-9]+)#\}");
        //static Regex IntExpressionRegex = new Regex(@"\{#I:[DH]:(?<from>[0-9]+):(?<to>[0-9]+)#\}");
        //static Regex CharExpressionRegex = new Regex(@"\{#C:(?<from>[0-9]+):(?<to>[0-9]+)#\}");
        //static Regex StringExpressionRegex = new Regex(@"\{#S:[DHLaRSAU]:(?<from>[0-9]+):(?<to>[0-9]+)#\}");
        //static Regex ExpressionRegex = new Regex(@"\{#E:\{$$.*$$\}:(?<from>[0-9]+):(?<to>[0-9]+)#\}");
        #endregion
        public Functions()
        {
        }
        public static string RandomString()
        {
            char[] ch = new char[6];
            for (int i = 0; i < 6; i++)
                ch[i] = Convert.ToChar(random.Next(65, 90));
            return new string(ch);
        }
        public static string RandomUserAgent()
        {
            String[] osversions = { "5.1", "6.0", "6.1" };
            String[] oslanguages = { "en-GB", "en-US", "es-ES", "pt-BR", "pt-PT", "sv-SE" };
            String version = osversions[random.Next(0, osversions.Length - 1)];
            String language = oslanguages[random.Next(0, oslanguages.Length - 1)];
            String useragent = String.Concat(
                "Mozilla/5.0 (Windows; U; Windows NT ",
                version,
                "; ",
                language,
                "; rv:1.9.2.17) Gecko/20110420 Firefox/3.6.17");
            return useragent;
        }
        /// <summary>
        /// Generate random string
        /// syntax:
        ///     {I:type:from:to} - integer
        ///     \{I:[DH]:[0-9]+:[0-9]+\}
        ///         type
        ///             D:dec
        ///             H- 0x....
        ///         Example:
        ///             {I:D:0:1000}
        ///         Result example
        ///             384
        ///     {C:from:to} -character
        ///     \{C:[0-9]+:[0-9]+\}
        ///         Example
        ///             {C:1:65535}
        ///         Result example
        ///             Ё
        ///     {S:type:min_length:max_length} -string
        ///     \{S:[DHLaRSAU]:[0-9]+:[0-9]+\}
        ///         type
        ///             D,      //0-9
        ///             H,      //0-f
        ///             L,      //a-Z
        ///             a,      //a-z
        ///             R,      //*
        ///             S,      //0-Z
        ///             A,      //A-Z
        ///             U       //full UTF-8
        ///         Example:
        ///             {S:a:3:1000}
        ///         Result example
        ///             gfdfyhtueyrstgdfggfr
        ///     {R:{expressions}:min_count:max_count} - mutiple generator invocation
        ///     \{R:\{.*\}:[0-9]+:[0-9]+\}
        ///     //2+ level expressions are not supported yet
        ///         Example
        ///             {R:{{S:L:1:5}={S:U:1:50}&}:1:5}
        ///         Result example
        ///             werf=%A1%B3&tjy=%5F%9C%2D%42%A1%B3&ertg=%39%7E%E8%B2&
        ///             
        /// 
        ///     \{#[ICS](:[a-zA-Z])?:[0-9]+:[0-9]+#\}
        /// Warning! string will NOT be validated
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /*public static FormattedStringGenerator RandomFormattedString(string input)
        {


            #region shit
            /*Bug is here: */
            /*
            FormattedStringGenerator gen = new FormattedStringGenerator();
            var Matches = RepeatRegex.Matches(input);
            var input2 = RepeatRegex.Replace(input, new MatchEvaluator(Matchev));
            var Matches_r2 = AnyExpressionRegex.Matches(input);
            var Mixed_expressions = ((IEnumerable<Match>)Matches).Concat((IEnumerable<Match>)Matches).OrderBy(a => a.Index).SelectMany(a => new int[] { a.Index, a.Index + a.Length }).ToList();
            if (Mixed_expressions[0] != 0)
                Mixed_expressions.InsertRange(0, new int[] { 0, 0 });
            if (Mixed_expressions.Last() != input.Length - 1)
                Mixed_expressions.AddRange(new int[] { input.Length - 1, input.Length - 1 });
            List<StaticASCIIStringExpression> expr = new List<StaticASCIIStringExpression>();
            for (int i = 1; i < Mixed_expressions.Count - 1; i += 2)
            {
                Ass
                //if (
            }
            */
            /*var Borders = ((IEnumerable<Match>)Matches).SelectMany(a => new int[] { a.Index, a.Index + a.Length });
            if (((IEnumerable<Match>)Matches).First().Index>0)
                Borders.*/
            /*
            throw new NotImplementedException();
            return gen;
           #endregion
        }
        */
        static string Matchev(Match m)
        {
            //slow. just for prototype
            return new String(Enumerable.Repeat(' ', m.Length).ToArray());
        }
        /*character functions*/
        /*static char[] __random_string_gen(StringFormat f, int MinLen, int MaxLen)
        {
            int cnt = random.Next(MinLen, MaxLen + 1),c_len=1;
            bool urlencode = ((f & StringFormat.Urlencode) == StringFormat.Urlencode);
            char[] c =new char[ cnt * ( urlencode? 3 : 1)];

            for (int i = 0; i < cnt; i++)
            {
                random.Next(random.Next(0,c_len);
            }
            return null;
        }*/
        public int qintparse(char[] input)
        {
            return qintparse(input, 0, input.Length);
        }
        public int qintparse(char[] input, int from, int count)
        {
            int sum = 0, cnt = 0;
            bool pos = true;
            if (input[from] == '-')
            {
                pos = false;
                cnt++;
            }
            while (cnt < count)
            {
                sum *= 10;
                sum += ((int)input[(cnt++) + from]) - 48;
            }
            return pos ? sum : -sum;
        }
        public long qlongparse(char[] input)
        {
            return qlongparse(input, 0, input.Length);
        }
        public long qlongparse(char[] input, int from, int count)
        {
            long sum = 0, cnt = 0;
            bool pos = true;
            if (input[from] == '-')
            {
                pos = false;
                cnt++;
            }
            while (cnt < count)
            {
                sum *= 10;
                sum += ((int)input[(cnt++) + from]) - 48;
            }
            return pos ? sum : -sum;
        }
        public static char[] int_to_hex_string(long i)
        {
            if (i == 0) return new char[] { '0' };
            int sz = 0;
            if (i < 0)
            {
                sz++;
                i *= -1;
            }
            long copy = i;
            while ((i >>= 4) > 0) sz++;
            char[] output = new char[sz + 1];
            output[0] = '-';
            do output[sz--] = _hex_chars[copy & 0x0fL]; while ((copy >>= 4) > 0);
            return output;
        }
        public static char[] int_to_dec_string(long i)
        {
            if (i == 0) return new char[] { '0' };
            int sz = 0;
            if (i < 0)
            {
                sz++;
                i *= -1;
            }
            long copy = i;
            while ((i /= 10) > 0) sz++;
            char[] output = new char[sz + 1];
            output[0] = '-';
            do output[sz--] = _hex_chars[copy % 10]; while ((copy /= 10) > 0);
            return output;
        }
        public static char[] int_to_hex_string(int i)
        {
            if (i == 0) return new char[] { '0' };
            int sz = 0;
            if (i < 0)
            {
                sz++;
                i *= -1;
            }
            int copy = i;
            while ((i >>= 4) > 0) sz++;
            char[] output = new char[sz + 1];
            output[0] = '-';
            do output[sz--] = _hex_chars[copy & 0x0fL]; while ((copy >>= 4) > 0);
            return output;
        }
        public static char[] int_to_dec_string(int i)
        {
            if (i == 0) return new char[] { '0' };
            int sz = 0;
            if (i < 0)
            {
                sz++;
                i *= -1;
            }
            int copy = i;
            while ((i /= 10) > 0) sz++;
            char[] output = new char[sz + 1];
            output[0] = '-';
            do output[sz--] = _hex_chars[copy % 10]; while ((copy /= 10) > 0);
            return output;
        }
        public static char[] random_ascii(int min_len, int max_len)
        {
            return random_ascii(min_len, max_len, _ascii_chars, 0, _ascii_chars.Length-1);
        }
        public static char[] random_ascii(int min_len, int max_len, char[] source, int startindex, int maxindex)
        {
            int rnd = random.Next(min_len, max_len + 1);
            char[] output = new char[rnd];
            for (int i = 0; i < rnd; output[i++] = _ascii_chars[random.Next(startindex,maxindex+1)]) ;
            return output;
        }
        public static char[] random_utf_urlencode_string(int min_real_len, int max_real_len)
        {
            int len = random.Next(min_real_len, max_real_len) * 6;
            char[] output = new char[len];
            ushort rnd = 0;
            for (int i = 0; i < len; )
            {
                rnd = (ushort)random.Next(1, 65535);
                output[i++] = '%';
                output[i++] = _hex_chars[rnd >> 12];
                output[i++] = _hex_chars[(rnd >> 8) & 0xf];
                output[i++] = '%';
                output[i++] = _hex_chars[(rnd >> 4) & 0xf];
                output[i++] = _hex_chars[rnd & 0xf];
            }
            return output;
        }
        /*same but with bytes*/
        public static byte[] int_to_hex_string_bytes(long i)
        {
            if (i == 0) return new byte[] { (byte)'0' };
            int sz = 0;
            if (i < 0)
            {
                sz++;
                i *= -1;
            }
            long copy = i;
            while ((i >>= 4) > 0) sz++;
            byte[] output = new byte[sz + 1];
            output[0] = (byte)'-';
            do output[sz--] = _hex_chars_bytes[copy & 0x0fL]; while ((copy >>= 4) > 0);
            return output;
        }
        public static byte[] int_to_dec_string_bytes(long i)
        {
            if (i == 0) return new byte[] { (byte)'0' };
            int sz = 0;
            if (i < 0)
            {
                sz++;
                i *= -1;
            }
            long copy = i;
            while ((i /= 10) > 0) sz++;
            byte[] output = new byte[sz + 1];
            output[0] = (byte)'-';
            do output[sz--] = (byte)(copy % 10 + 48); while ((copy /= 10) > 0);
            return output;
        }
        public static byte[] int_to_hex_string_bytes(int i)
        {
            if (i == 0) return new byte[] { (byte)'0' };
            int sz = 0;
            if (i < 0)
            {
                sz++;
                i *= -1;
            }
            int copy = i;
            while ((i >>= 4) > 0) sz++;
            byte[] output = new byte[sz + 1];
            output[0] = (byte)'-';
            do output[sz--] = _hex_chars_bytes[copy & 0x0fL]; while ((copy >>= 4) > 0);
            return output;
        }
        public static byte[] int_to_dec_string_bytes(int i)
        {
            if (i == 0) return new byte[] { (byte)'0' };
            int sz = 0;
            if (i < 0)
            {
                sz++;
                i *= -1;
            }
            int copy = i;
            while ((i /= 10) > 0) sz++;
            byte[] output = new byte[sz + 1];
            output[0] = (byte)'-';
            do output[sz--] = (byte)(copy % 10 + 48); while ((copy /= 10) > 0);
            return output;
        }
        public static byte[] random_ascii_bytes(int min_len, int max_len)
        {
            return random_ascii_bytes(min_len, max_len, _ascii_chars_bytes, 0, _ascii_chars_bytes.Length-1);
        }
        public static byte[] random_ascii_bytes(int min_len, int max_len, byte[] source, int startindex, int maxindex)
        {
            maxindex++;
            int rnd = random.Next(min_len, max_len + 1);
            byte[] output = new byte[rnd];
            for (int i = 0; i < rnd; output[i++] = source[random.Next(startindex,maxindex+1)]) ;
            return output;
        }
        public static byte[] random_utf_urlencode_string_bytes(int min_real_len, int max_real_len)
        {
            int len = random.Next(min_real_len, max_real_len) * 6;
            byte[] output = new byte[len];
            ushort rnd = 0;
            byte percent =(byte)'%';
            for (int i = 0; i < len; )
            {
                rnd = (ushort)random.Next(1, 65535);
                output[i++] = percent;
                output[i++] = _hex_chars_bytes[rnd >> 12];
                output[i++] = _hex_chars_bytes[(rnd >> 8) & 0xf];
                output[i++] = percent;
                output[i++] = _hex_chars_bytes[(rnd >> 4) & 0xf];
                output[i++] = _hex_chars_bytes[rnd & 0xf];
            }
            return output;
        }
    }
}
