using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GAS.Core.Strings;
using GAS.Core;
using System.Text;
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
        static string Matchev(Match m)
        {
            //slow. just for prototype
            return new String(Enumerable.Repeat(' ', m.Length).ToArray());
        }
        /*character functions*/
        /*static char[] __random_string_gen(StringFormat f, int MinLen, int MaxLen)
        {
            int __cnt = random.Next(MinLen, MaxLen + 1),c_len=1;
            bool urlencode = ((f & StringFormat.Urlencode) == StringFormat.Urlencode);
            char[] _c =new char[ __cnt * ( urlencode? 3 : 1)];

            for (int i = 0; i < __cnt; i++)
            {
                random.Next(random.Next(0,c_len);
            }
            return null;
        }*/
        public static int qintparse(char[] input)
        {
            return qintparse(input, 0, input.Length);
        }
        public static int qintparse(char[] input, int from, int count)
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
        public static long qlongparse(char[] input)
        {
            return qlongparse(input, 0, input.Length);
        }
        public static long qlongparse(char[] input, int from, int count)
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
        public static unsafe int qintparse(char* input, int from, int count)
        {
            int sum = 0;//, __cnt = 0;
            input += from;
            char* end = input + count;
            bool pos = true;
            if (*input == '-')
            {
                pos = false;
                input++;
            }
            while (input < end)
            {
                sum *= 10;
                sum += ((int)*input++) - 48;
            }
            return pos ? sum : -sum;
        }
        public static unsafe long qlongparse(char* input, int from, int count)
        {
            long sum = 0;//, __cnt = 0;
            input += from;
            char* end = input + count;
            bool pos = true;
            if (*input == '-')
            {
                pos = false;
                input++;
            }
            while (input < end)
            {
                sum *= 10;
                sum += ((int)*input++) - 48;
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
        public static unsafe int FindChar(char* _from, char* __end, char _c)
        {
            int __cnt = 0;
            while (_from < __end && *_from != _c)
            {
                __cnt++;
                _from++;
            };
            return __cnt;
        }
    }
}
