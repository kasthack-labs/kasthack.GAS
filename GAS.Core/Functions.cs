using System;
using System.Collections.Generic;

namespace GAS.Core
{
    public class Functions
    {
        enum StringFormat
        {
            UpperCase,
            LowerCase,
            Digits,
            Punctuation,
            Special,
            Random,
            Urlencode
        }

        static Random random = new Random();
        int ascii_char_count = 0;
        char[] hex_chars = "0123456789abcdef".ToCharArray();
        char[] ascii_chars;
        ushort[] offsets = { 0, 0, 33, 48, 65, 97 };
        ushort[] lengs = { 65535, 32, 32, 10, 26, 26 };

        public Functions()
        {
            List<char> _tmp = new List<char>(100);
            for (char c = '0'; c <= '9'; _tmp.Add(c++)) ;
            for (char c = 'a'; c <= 'z'; _tmp.Add(c++)) ;
            for (char c = 'A'; c <= 'Z'; _tmp.Add(c++)) ;
            this.ascii_chars = _tmp.ToArray();
            ascii_char_count = ascii_chars.Length;
            _tmp.Clear();
            _tmp = null;
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
        ///     {#i:from:to:type#} - integer
        ///         default:dec
        ///         hex - 0x....
        ///     Example:
        ///         {#int:0:1000:dec#}
        ///     {#c:from:to#} -character
        ///     Example
        ///         {#char:1:65535#}
        ///     {#s:type:min_length:max_length#} -string
        ///         type(may be combined)
        ///             A - uppercase ASCII
        ///             a - lowercase ASCII
        ///             0 - digits
        ///             s - punctuation
        ///             S - special
        ///             Z - random unicode
        ///             U - urlencode string
        ///         Example:
        ///         {#string:Aa0s:3:1000#}
        /// Warning! string will NOT be validated
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RandomFormattedString(string input)
        {
            return input;
        }
        /*character functions*/
        static char[] __random_string_gen(StringFormat f, int MinLen, int MaxLen)
        {/*
            int cnt = random.Next(MinLen, MaxLen + 1),c_len=1;
            bool urlencode = ((f & StringFormat.Urlencode) == StringFormat.Urlencode);
            char[] c =new char[ cnt * ( urlencode? 3 : 1)];

            for (int i = 0; i < cnt; i++)
            {
                random.Next(random.Next(0,c_len);
            }*/
            return null;
        }
        int quintparse(char[] input)
        {
            return quintparse(input, 0, input.Length);
        }
        int quintparse(char[] input, int from, int count)
        {
            int sum = 0, cnt = 0;
            while (cnt < count)
            {
                sum *= 10;
                sum += ((int)input[(cnt++) + from]) - 48;
            }
            return sum;
        }
        public char[] int_to_hex_string(long i)
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
            do output[sz--] = hex_chars[copy & 0x0fL]; while ((copy >>= 4) > 0);
            return output;
        }
        public char[] int_to_dec_string(long i)
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
            do output[sz--] = hex_chars[copy % 10]; while ((copy /= 10) > 0);
            return output;
        }
        public char[] int_to_hex_string(int i)
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
            do output[sz--] = hex_chars[copy & 0x0fL]; while ((copy >>= 4) > 0);
            return output;
        }
        public char[] int_to_dec_string(int i)
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
            do output[sz--] = hex_chars[copy % 10]; while ((copy /= 10) > 0);
            return output;
        }
        public char[] random_ascii(int min_len, int max_len)
        {
            int rnd = random.Next(min_len, max_len + 1);
            char[] output = new char[rnd];
            for (int i = 0; i < rnd; output[i++] = ascii_chars[random.Next(ascii_char_count)]) ;
            return output;
        }
        public char[] random_utf_urlencode_string(int min_real_len, int max_real_len)
        {
            int len = random.Next(max_real_len, max_real_len) * 6;
            char[] output = new char[len];
            ushort rnd = 0;
            for (int i = 0; i < len; )
            {
                rnd = (ushort)random.Next(1, 65535);
                output[i++] = '%';
                output[i++] = hex_chars[rnd >> 12];
                output[i++] = hex_chars[(rnd >> 8) & 0xf];
                output[i++] = '%';
                output[i++] = hex_chars[(rnd >> 4) & 0xf];
                output[i++] = hex_chars[rnd & 0xf];
            }
            return output;
        }
        /*same but with bytes*/
        public byte[] int_to_hex_string_bytes(long i)
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
            do output[sz--] = (byte)hex_chars[copy & 0x0fL]; while ((copy >>= 4) > 0);
            return output;
        }
        public byte[] int_to_dec_string_bytes(long i)
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
        public byte[] int_to_hex_string_bytes(int i)
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
            do output[sz--] = (byte)hex_chars[copy & 0x0fL]; while ((copy >>= 4) > 0);
            return output;
        }
        public byte[] int_to_dec_string_bytes(int i)
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
        public byte[] random_ascii_bytes(int min_len, int max_len)
        {
            int rnd = random.Next(min_len, max_len + 1);
            byte[] output = new byte[rnd];
            for (int i = 0; i < rnd; output[i++] = (byte)ascii_chars[random.Next(ascii_char_count)]) ;
            return output;
        }
        public byte[] random_utf_urlencode_string_bytes(int min_real_len, int max_real_len)
        {
            int len = random.Next(max_real_len, max_real_len) * 6;
            byte[] output = new byte[len];
            ushort rnd = 0;
            for (int i = 0; i < len; )
            {
                rnd = (ushort)random.Next(1, 65535);
                output[i++] = (byte)'%';
                output[i++] = (byte)hex_chars[rnd >> 12];
                output[i++] = (byte)hex_chars[(rnd >> 8) & 0xf];
                output[i++] = (byte)'%';
                output[i++] = (byte)hex_chars[(rnd >> 4) & 0xf];
                output[i++] = (byte)hex_chars[rnd & 0xf];
            }
            return output;
        }
    }
}
