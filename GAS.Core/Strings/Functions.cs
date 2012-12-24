using System;
using System.Linq;
using System.Text.RegularExpressions;
namespace GAS.Core.Strings
{
    public class Functions
    {
        #region Variables
        static Random random = new Random();
        #region magic!
        //public jfl
        public static char[] _hex_chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        public static char[] _ascii_chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
                                                'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                                                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '!','"','#','$','%','&','\'','(',')','*','+',',','-','.',
                                                '/',':',';', '<','=','>','?','@','[','\\',']','^','_','`','{','|','}','~'};
        public static byte[] _ascii_chars_bytes = { 48,49,50,51,52,53,54,55,56,57,97,98,99,100,101,102,103,104,105,106,107,
                                                          108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,65,66,67,
                                                          68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,
                                                          33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,58,59,60,61,62,63,64,91,92,93,94,95,96,123,124,125,126};
        public static byte[] _hex_chars_bytes = { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 97, 98, 99, 100, 101, 102, };
        public static ushort[] _offsets = { 0, 0, 33, 48, 65, 97 };
        public static ushort[] _lengs = { 65535, 32, 32, 10, 26, 26 };
        #endregion
        #endregion
        public Functions() {
        }
        public static string RandomString() {
            return new string(random_ascii(1, 8, _ascii_chars, 0, 61));
        }
        public static string RandomUserAgent() {
            //slow. very slow
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
        static string Matchev(Match _m) {
            //slow. just for prototype
            return new String(Enumerable.Repeat(' ', _m.Length).ToArray());
        }
        public static int qintparse(char[] _input) {
            return qintparse(_input, 0, _input.Length);
        }
        public static int qintparse(char[] _input, int _from, int _count) {
            int __sum = 0, __cnt = 0;
            bool __pos = true;
            if ( _input[_from] == '-' ) {
                __pos = false;
                __cnt++;
            }
            while ( __cnt < _count ) {
                __sum *= 10;
                __sum += ( (int)_input[( __cnt++ ) + _from] ) - 48;
            }
            return __pos ? __sum : -__sum;
        }
        public static long qlongparse(char[] _input) {
            return qlongparse(_input, 0, _input.Length);
        }
        public static long qlongparse(char[] _input, int _from, int _count) {
            long __sum = 0, __cnt = 0;
            bool __pos = true;
            if ( _input[_from] == '-' ) {
                __pos = false;
                __cnt++;
            }
            while ( __cnt < _count ) {
                __sum *= 10;
                __sum += ( (int)_input[( __cnt++ ) + _from] ) - 48;
            }
            return __pos ? __sum : -__sum;
        }
        public static char[] int_to_hex_string(long _i) {
            if ( _i == 0 ) return new char[] { '0' };
            int __sz = 0;
            if ( _i < 0 ) {
                __sz++;
                _i *= -1;
            }
            long __copy = _i;
            while ( ( _i >>= 4 ) > 0 ) __sz++;
            char[] __output = new char[__sz + 1];
            __output[0] = '-';
            do __output[__sz--] = _hex_chars[__copy & 0x0fL]; while ( ( __copy >>= 4 ) > 0 );
            return __output;
        }
        public static char[] int_to_dec_string(long _i) {
            if ( _i == 0 ) return new char[] { '0' };
            int __sz = 0;
            if ( _i < 0 ) {
                __sz++;
                _i *= -1;
            }
            long __copy = _i;
            while ( ( _i /= 10 ) > 0 ) __sz++;
            char[] __output = new char[__sz + 1];
            __output[0] = '-';
            do __output[__sz--] = _hex_chars[__copy % 10]; while ( ( __copy /= 10 ) > 0 );
            return __output;
        }
        public static char[] int_to_hex_string(int _i) {
            if ( _i == 0 ) return new char[] { '0' };
            int __sz = 0;
            if ( _i < 0 ) {
                __sz++;
                _i *= -1;
            }
            int __copy = _i;
            while ( ( _i >>= 4 ) > 0 ) __sz++;
            char[] __output = new char[__sz + 1];
            __output[0] = '-';
            do __output[__sz--] = _hex_chars[__copy & 0x0fL]; while ( ( __copy >>= 4 ) > 0 );
            return __output;
        }
        public static char[] int_to_dec_string(int _i) {
            if ( _i == 0 ) return new char[] { '0' };
            int __sz = 0;
            if ( _i < 0 ) {
                __sz++;
                _i *= -1;
            }
            int __copy = _i;
            while ( ( _i /= 10 ) > 0 ) __sz++;
            char[] output = new char[__sz + 1];
            output[0] = '-';
            do output[__sz--] = _hex_chars[__copy % 10]; while ( ( __copy /= 10 ) > 0 );
            return output;
        }
        public static char[] random_ascii(int _min_len, int _max_len) {
            return random_ascii(_min_len, _max_len, _ascii_chars, 0, _ascii_chars.Length - 1);
        }
        public static char[] random_ascii(int _min_len, int _max_len, char[] _source, int _startindex, int _maxindex) {
            return random_ascii(random.Next(_min_len, _max_len + 1), _source, _startindex, _maxindex);
        }
        public static char[] random_utf_urlencode_string(int _min_real_len, int _max_real_len) {
            return random_utf_urlencode_string( random.Next(_min_real_len, _max_real_len));
        }
        /*real engine*/
        public static char[] random_utf_urlencode_string(int _len) {
            _len *= 6;
            char[] __output = new char[_len];
            ushort __rnd = 0;
            char __pc = '%';
            for ( int i = 0; i < _len; ) {
                __rnd = (ushort)random.Next(1, 65535);
                __output[i++] = __pc;
                __output[i++] = _hex_chars[__rnd >> 12];
                __output[i++] = _hex_chars[( __rnd >> 8 ) & 0xf];
                __output[i++] = __pc;
                __output[i++] = _hex_chars[( __rnd >> 4 ) & 0xf];
                __output[i++] = _hex_chars[__rnd & 0xf];
            }
            return __output;
        }
        public static char[] random_ascii(int _len, char[] _source, int _startindex, int _maxindex) {
            _maxindex++;
            char[] __output = new char[_len];
            for ( int i = 0; i < _len; __output[i++] = _source[random.Next(_startindex, _maxindex)] ) ;
            return __output;
        }
        /*same but with bytes*/
        public static byte[] int_to_hex_string_bytes(long _i) {
            if ( _i == 0 ) return new byte[] { (byte)'0' };
            int __sz = 0;
            if ( _i < 0 ) {
                __sz++;
                _i *= -1;
            }
            long __copy = _i;
            while ( ( _i >>= 4 ) > 0 ) __sz++;
            byte[] __output = new byte[__sz + 1];
            __output[0] = (byte)'-';
            do __output[__sz--] = _hex_chars_bytes[__copy & 0x0fL]; while ( ( __copy >>= 4 ) > 0 );
            return __output;
        }
        public static byte[] int_to_dec_string_bytes(long _i) {
            if ( _i == 0 ) return new byte[] { (byte)'0' };
            int __sz = 0;
            if ( _i < 0 ) {
                __sz++;
                _i *= -1;
            }
            long __copy = _i;
            while ( ( _i /= 10 ) > 0 ) __sz++;
            byte[] output = new byte[__sz + 1];
            output[0] = (byte)'-';
            do output[__sz--] = (byte)( __copy % 10 + 48 ); while ( ( __copy /= 10 ) > 0 );
            return output;
        }
        public static byte[] int_to_hex_string_bytes(int _i) {
            if ( _i == 0 ) return new byte[] { (byte)'0' };
            int __sz = 0;
            if ( _i < 0 ) {
                __sz++;
                _i *= -1;
            }
            int __copy = _i;
            while ( ( _i >>= 4 ) > 0 ) __sz++;
            byte[] __output = new byte[__sz + 1];
            __output[0] = (byte)'-';
            do __output[__sz--] = _hex_chars_bytes[__copy & 0x0fL]; while ( ( __copy >>= 4 ) > 0 );
            return __output;
        }
        public static byte[] int_to_dec_string_bytes(int _i) {
            if ( _i == 0 ) return new byte[] { (byte)'0' };
            int sz = 0;
            if ( _i < 0 ) {
                sz++;
                _i *= -1;
            }
            int __copy = _i;
            while ( ( _i /= 10 ) > 0 ) sz++;
            byte[] __output = new byte[sz + 1];
            __output[0] = (byte)'-';
            do __output[sz--] = (byte)( __copy % 10 + 48 ); while ( ( __copy /= 10 ) > 0 );
            return __output;
        }
        public static byte[] random_ascii_bytes(int _min_len, int _max_len) {
            return random_ascii_bytes(_min_len, _max_len, _ascii_chars_bytes, 0, _ascii_chars_bytes.Length - 1);
        }
        public static byte[] random_ascii_bytes(int _min_len, int _max_len, byte[] _source, int _startindex, int _maxindex) {
            return random_ascii_bytes(random.Next(_min_len, _max_len + 1), _source, _startindex, _maxindex);
        }
        public static byte[] random_utf_urlencode_string_bytes(int _min_real_len, int _max_real_len) {
            return random_utf_urlencode_string_bytes(random.Next(_min_real_len, _max_real_len));
        }
        /*real generators*/
        public static byte[] random_utf_urlencode_string_bytes(int _real_len) {
            _real_len *= 6;
            byte[] __output = new byte[_real_len];
            ushort __rnd = 0;
            byte __percent = (byte)'%';
            for ( int __i = 0; __i < _real_len; ) {
                __rnd = (ushort)random.Next(1, 65535);
                __output[__i++] = __percent;
                __output[__i++] = _hex_chars_bytes[__rnd >> 12];
                __output[__i++] = _hex_chars_bytes[( __rnd >> 8 ) & 0xf];
                __output[__i++] = __percent;
                __output[__i++] = _hex_chars_bytes[( __rnd >> 4 ) & 0xf];
                __output[__i++] = _hex_chars_bytes[__rnd & 0xf];
            }
            return __output;
        }
        public static byte[] random_ascii_bytes(int _len, byte[] _source, int _startindex, int _maxindex) {
            _maxindex++;
            byte[] __output = new byte[_len];
            for ( int __i = 0; __i < _len; __output[__i++] = _source[random.Next(_startindex, _maxindex)] ) ;
            return __output;
        }
        /*unsafe*/
        public static unsafe int qintparse(char* _input, int _from, int _count) {
            int __sum = 0;//, __cnt = 0;
            _input += _from;
            char* __end = _input + _count;
            bool __pos = true;
            if ( *_input == '-' ) {
                __pos = false;
                _input++;
            }
            while ( _input < __end ) {
                __sum *= 10;
                __sum += ( (int)*_input++ ) - 48;
            }
            return __pos ? __sum : -__sum;
        }
        public static unsafe long qlongparse(char* _input, int _from, int _count) {
            long __sum = 0;//, __cnt = 0;
            _input += _from;
            char* __end = _input + _count;
            bool __pos = true;
            if ( *_input == '-' ) {
                __pos = false;
                _input++;
            }
            while ( _input < __end ) {
                __sum *= 10;
                __sum += ( (int)*_input++ ) - 48;
            }
            return __pos ? __sum : -__sum;
        }
        public static unsafe int FindChar(char* _from, char* _end, char _c) {
            int __cnt = 0;
            while ( _from < _end && *_from != _c ) {
                __cnt++;
                _from++;
            };
            return __cnt;
        }
        
        /*generators with pointers. Warning! NOT TESTED! POTENTIAL CRASH AND LOSS OFF DATA! IT'S NOT CAPSLOCK - IT'S HOLDEN SHIFT!*/
        public static unsafe void random_utf_urlencode_string_bytes_insert(byte* _ptr, int _real_len) {
            byte* __end = (_ptr+ _real_len * 6);
            ushort __rnd = 0;
            byte __percent = (byte)'%';
            while ( _ptr < __end) {
                __rnd = (ushort)random.Next(1, 65535);
                *_ptr++ = __percent;
                *_ptr++ = _hex_chars_bytes[__rnd >> 12];
                *_ptr++ = _hex_chars_bytes[( __rnd >> 8 ) & 0xf];
                *_ptr++ = __percent;
                *_ptr++ = _hex_chars_bytes[( __rnd >> 4 ) & 0xf];
                *_ptr++ = _hex_chars_bytes[__rnd & 0xf];
            }
        }
        public static unsafe void random_ascii_bytes(byte* _ptr, int _len, byte[] _source, int _startindex, int _maxindex) {
            _maxindex++;
            byte* __end = ( _ptr + _len );
            while ( _ptr<__end) *_ptr++ = _source[random.Next(_startindex, _maxindex)];
        }
        public static unsafe void random_utf_urlencode_string(char* _ptr, int _real_len) {
            char* __end = ( _ptr + _real_len * 6 );
            ushort __rnd = 0;
            char __pc = '%';
            while ( _ptr < __end ) {
                __rnd = (ushort)random.Next(1, 65535);
                *_ptr++ = __pc;
                *_ptr++ = _hex_chars[__rnd >> 12];
                *_ptr++ = _hex_chars[( __rnd >> 8 ) & 0xf];
                *_ptr++ = __pc;
                *_ptr++ = _hex_chars[( __rnd >> 4 ) & 0xf];
                *_ptr++ = _hex_chars[__rnd & 0xf];
            }
        }
        public static unsafe void random_ascii(char* _ptr, int _len, char[] _source, int _startindex, int _maxindex) {
            _maxindex++;
            char* __end = ( _ptr + _len );
            while ( _ptr < __end ) *_ptr++ = _source[random.Next(_startindex, _maxindex)];
        }
    }
}