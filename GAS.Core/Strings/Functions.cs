using SharpNeatLib.Maths;
using System;
using System.Linq;
using System.Text.RegularExpressions;
namespace GAS.Core.Strings
{
	public class Functions
	{
		#region Variables
		public static FastRandom random = new FastRandom();
		public static Func<IExpression, char[]> GetCharsF = a => a.GetChars();
		public static Func<IExpression, byte[]> GetCharsEF = null;
		public static Func<IExpression, byte[]> GetBytesF = a => a.GetAsciiBytes();
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
		static char ___c = '-';
		static byte ___b = (byte)'-';
		static char[] ___zca = new char[] { '0' };
		static byte[] ___zba = new byte[] { (byte)'0' };
		#endregion
		#endregion
		#region Legacy
		public static string RandomString() {
			return new string(RandomASCII(1, 8, _ascii_chars, 0, 61));
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
		#endregion
		#region Shit(wrappers 4 wrappers
		/*parsers*/
		public static int QIntParse(char[] _input) {
			return QIntParse(_input, 0, _input.Length);
		}
		public static long QLongParse(char[] _input) {
			return QLongParse(_input, 0, _input.Length);
		}
		/*random strings*/
		public static char[] RandomASCII(int _min_len, int _max_len) {
			return RandomASCII(_min_len, _max_len, _ascii_chars, 0, _ascii_chars.Length - 1);
		}
		public static char[] RandomASCII(int _min_len, int _max_len, char[] _source, int _startindex, int _maxindex) {
			return RandomASCII(random.Next(_min_len, _max_len + 1), _source, _startindex, _maxindex);
		}
		public static char[] RandomUTFURLEncodeString(int _min_real_len, int _max_real_len) {
			return RandomUTFURLEncodeString(random.Next(_min_real_len, _max_real_len));
		}
		/*random_strings*/
		public static byte[] RandomASCIIBytes(int _min_len, int _max_len) {
			return RandomASCIIBytes(_min_len, _max_len, _ascii_chars_bytes, 0, _ascii_chars_bytes.Length - 1);
		}
		public static byte[] RandomASCIIBytes(int _min_len, int _max_len, byte[] _source, int _startindex, int _maxindex) {
			return RandomASCIIBytes(random.Next(_min_len, _max_len + 1), _source, _startindex, _maxindex);
		}
		public static byte[] RandomUTFURLEncodeStringBytes(int _min_real_len, int _max_real_len) {
			return RandomUTFURLEncodeStringBytes(random.Next(_min_real_len, _max_real_len));
		}
		#endregion
		#region unsafe Wrappers
		/*parsers*/
		public static unsafe long QLongParse(char[] _input, int _from, int _count) {
			fixed ( char* __input = _input )
				return QLongParse(__input + _from, _count); 
		}
		public static unsafe int QIntParse(char[] _input, int _from, int _count) {
			fixed ( char* __input = _input )
				return QIntParse(__input + _from, _count); 
		}
		/*to_strings*/
		public static unsafe char[] IntToHexString(long _i) {
			if ( _i == 0 ) return ___zca;
			byte __sz = GetHexStringLength(_i);
			char[] __output = new char[__sz];
			fixed ( char* __out_arr = __output )
				fixed ( char* __src_arr = _hex_chars )
					IntToHexStringInsert(__out_arr, __src_arr, _i, __sz);
			return __output;
		}
		public static unsafe char[] IntToDecString(long _i) {
			if ( _i == 0 ) return ___zca;
			byte __sz = GetDecStringLength(_i);
			char[] __output = new char[__sz];
			fixed ( char* __out_arr = __output )
				fixed ( char* __src_arr = _hex_chars )
					IntToDecStringInsert(__out_arr, _i, __sz);
			return __output;
		}
		public static unsafe char[] IntToHexString(int _i) {
			if ( _i == 0 ) return ___zca;
			byte __sz = GetHexStringLength(_i);
			char[] __output = new char[__sz];
			fixed ( char* __out_arr = __output )
				fixed ( char* __src_arr = _hex_chars )
						IntToHexStringInsert(__out_arr, __src_arr, _i, __sz);
			return __output;
		}
		public static unsafe char[] IntToDecString(int _i) {
			if ( _i == 0 ) return ___zca;
			byte __sz = GetDecStringLength(_i);
			char[] __output = new char[__sz];
			fixed ( char* __out_arr = __output )
				fixed ( char* __src_arr = _hex_chars )
						IntToDecStringInsert(__out_arr, _i, __sz);
			return __output;
		}
		/*real engine*/
		public static unsafe char[] RandomUTFURLEncodeString(int _len) {
			char[] __output = new char[_len];
			fixed ( char* out_pointer = __output )
				RandomUTFURLEncodeStringInsert(out_pointer, _len);
			return __output;
		}
		public static unsafe char[] RandomASCII(int _len, char[] _source, int _startindex, int _maxindex) {
			char[] __output = new char[_len];
			fixed ( char* __out_pointer = __output )
				fixed ( char* __source = _source )
					RandomASCIIInsert(__out_pointer, _len, __source + _startindex, _maxindex - _startindex);
			return __output;
		}
		/*same but with bytes*/
		public static unsafe byte[] IntToHexStringBytes(long _i) {
			if ( _i == 0 ) return ___zba;
			byte __sz = GetHexStringLength(_i);
			byte[] __output = new byte[__sz];
			fixed ( byte* __out_arr = __output )
				fixed ( byte* __src_arr = _hex_chars_bytes )
					IntToHexStringBytesInsert(__out_arr, __src_arr, _i, __sz);
			return __output;
		}
		public static unsafe byte[] IntToDecStringBytes(long _i) {
			if ( _i == 0 ) return ___zba;
			byte __sz = GetDecStringLength(_i);
			byte[] __output = new byte[__sz];
			fixed ( byte* __out_arr = __output )
				fixed ( byte* __src_arr = _hex_chars_bytes )
					IntToDecStringBytesInsert(__out_arr, _i, __sz);
			return __output;
		}
		public static unsafe byte[] IntToHexStringBytes(int _i) {
			if ( _i == 0 ) return ___zba;
			byte __sz = GetHexStringLength(_i);
			byte[] __output = new byte[__sz];
			fixed ( byte* __out_arr = __output )
				fixed ( byte* __src_arr = _hex_chars_bytes )
					IntToHexStringBytesInsert(__out_arr, __src_arr, _i, __sz);
			return __output;
		}
		public static unsafe byte[] IntToDecStringBytes(int _i) {
			if ( _i == 0 ) return ___zba;
			byte __sz = GetDecStringLength(_i);
			byte[] __output = new byte[__sz];
			fixed ( byte* __out_arr = __output )
				fixed ( byte* __src_arr = _hex_chars_bytes )
					IntToDecStringBytesInsert(__out_arr, _i, __sz);
			return __output;
		}
		/*real generators*/
		public static unsafe byte[] RandomUTFURLEncodeStringBytes(int _real_len) {
			byte[] __output = new byte[_real_len];
			fixed ( byte* __out_pointer = __output )
				RandomUTFURLEncodeStringBytesInsert(__out_pointer, _real_len);
			return __output;
		}
		public static unsafe byte[] RandomASCIIBytes(int _len, byte[] _source, int _startindex, int _maxindex) {
			byte[] __output = new byte[_len];
			fixed ( byte* __out_pointer = __output )
				fixed ( byte* __source = _source )
					RandomASCIIBytesInsert(__out_pointer, _len, __source + _startindex, _maxindex - _startindex);
			return __output;
		}
		#endregion
		#region Real Generators
		/*parsers*/
		public static unsafe int QIntParse(char* _input, int _count) {
			int __sum = 0;//, __cnt = 0;
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
		public static unsafe long QLongParse(char* _input, int _count) {
			long __sum = 0;//, __cnt = 0;
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
		/*generators with pointers*/
		public static unsafe void RandomUTFURLEncodeStringBytesInsert(byte* _ptr, int _real_len) {
			byte* __end = ( _ptr + _real_len * 6 );
			ushort __rnd = 0;
			byte __pc = (byte)'%';
			fixed ( byte* __hex_chars = _hex_chars_bytes ) {
				while ( _ptr < __end ) {
                    __rnd = (ushort)random.Next(65535);
					*_ptr++ = __pc;
					*_ptr++ = *( __hex_chars + ( __rnd >> 12 ) );
					*_ptr++ = *( __hex_chars + ( ( __rnd >> 8 ) & 0xf ) );
					*_ptr++ = __pc;
					*_ptr++ = *( __hex_chars + ( ( __rnd >> 4 ) & 0xf ) );
					*_ptr++ = *( __hex_chars + ( __rnd & 0xf ) );
				}
			}
		}
		public static unsafe void RandomASCIIBytesInsert(byte* _ptr, int _len, byte* _source, int _maxindex) {
			_maxindex++;
			byte* __end = ( _ptr + _len );
			while ( _ptr < __end ) *_ptr++ = *( _source + random.Next(_maxindex) );
		}
		public static unsafe void RandomUTFURLEncodeStringInsert(char* _ptr, int _real_len) {
			char* __end = ( _ptr + _real_len * 6 );
			ushort __rnd = 0;
			char __pc = '%';
			fixed ( char* __hex_chars = _hex_chars ) {
				while ( _ptr < __end ) {
					__rnd = (ushort)random.Next(65535);
					*_ptr++ = __pc;
					*_ptr++ = *(__hex_chars+(__rnd >> 12));
					*_ptr++ = *(__hex_chars+(( __rnd >> 8 ) & 0xf));
					*_ptr++ = __pc;
					*_ptr++ = *(__hex_chars+(( __rnd >> 4 ) & 0xf));
					*_ptr++ = *(__hex_chars+(__rnd & 0xf));
				}
			}
		}
		public static unsafe void RandomASCIIInsert(char* _ptr, int _len, char* _source, int _maxindex) {
			_maxindex++;
			char* __end = ( _ptr + _len );
			while ( _ptr < __end )
                *_ptr++ = *( _source + random.Next(_maxindex) );
		}
		/*get_to_string_size*/
		public static byte GetDecStringLength(int _i) {
			byte __sz = 1;
			if ( _i < 0 ) { __sz++; _i = -_i; }
			while ( ( _i /= 10 ) > 0 ) __sz++;
			return __sz;
		}
		public static byte GetDecStringLength(long _i) {
			byte __sz = 1;
			if ( _i < 0 ) { __sz++; _i = -_i; }
			while ( ( _i /= 10 ) > 0 ) __sz++;
			return __sz;
		}
		public static byte GetHexStringLength(int _i) {
			byte __sz = 1;
			if ( _i < 0 ) { __sz++; _i = -_i; }
			while ( ( _i >>= 4 ) != 0 ) __sz++;
			return __sz;
		}
		public static byte GetHexStringLength(long _i) {
			byte __sz = 1;
			if ( _i < 0 ) { __sz++; _i = -_i; }
			while ( ( _i >>= 4 ) > 0 ) __sz++;
			return __sz;
		}
		/*to_strings*/
		public static unsafe void IntToHexStringBytesInsert(byte* _outarr, byte* _source_arr, int _i, byte _sz) {
			if ( _i < 0 ) _i = -_i;
			*_outarr = ___b;
			_outarr += _sz;
			do *--_outarr = *( _source_arr + ( _i & 0x0f ) ); while ( ( _i >>= 4 ) != 0 );
		}
		public static unsafe void IntToHexStringBytesInsert(byte* _outarr, byte* _source_arr, long _i, byte _sz) {
			if ( _i < 0 ) _i = -_i;
			*_outarr = ___b;
			_outarr += _sz;
			do *--_outarr = *( _source_arr + ( _i & 0x0f ) ); while ( ( _i >>= 4 ) != 0 );
		}
		public static unsafe void IntToHexStringInsert(char* _outarr, char* _source_arr, long _i, byte _sz) {
			if ( _i < 0 ) _i = -_i;
			*_outarr = ___c;
			_outarr += _sz;
			do *--_outarr = *( _source_arr + ( _i & 0x0f ) ); while ( ( _i >>= 4 ) != 0 );
		}
		public static unsafe void IntToHexStringInsert(char* _outarr, char* _source_arr, int _i, byte _sz) {
			if ( _i < 0 ) _i = -_i;
			*_outarr = ___c;
			_outarr += _sz;
			do *--_outarr = *( _source_arr + ( _i & 0x0f ) ); while ( ( _i >>= 4 ) != 0 );
		}
		public static unsafe void IntToDecStringBytesInsert(byte* _outarr, long _i, byte _sz) {
			if ( _i < 0 ) _i = -_i;
			*_outarr = ___b;
			_outarr += _sz;
			do *--_outarr = (byte)( _i % 10 + 48 ); while ( ( _i /= 10 ) != 0 );
		}
		public static unsafe void IntToDecStringBytesInsert(byte* _outarr, int _i, byte _sz) {
			if ( _i < 0 ) _i = -_i;
			*_outarr = ___b;
			_outarr += _sz;
			do *--_outarr = (byte)( _i % 10 + 48 ); while ( ( _i /= 10 ) != 0 );
		}
		public static unsafe void IntToDecStringInsert(char* _outarr, long _i, byte _sz) {
			if ( _i < 0 ) _i = -_i;
			*_outarr = ___c;
			_outarr += _sz;
			do *--_outarr = (char)( _i % 10 + 48 ); while ( ( _i /= 10 ) != 0 );
		}
		public static unsafe void IntToDecStringInsert(char* _outarr, int _i, byte _sz) {
			if ( _i < 0 ) _i = -_i;
			*_outarr = ___c;
			_outarr += _sz;
			do *--_outarr = (char)( _i % 10 + 48 ); while ( ( _i /= 10 ) != 0 );
		}
		/*FuckingMagic*/
		public static T[] GetT<T>(int _RepeatCount, Func<IExpression, T[]> _GetT, IExpression[] _Expressions) {
			T[] __outbytes;
			T[][] __tmp_bytes;
			int __offset = 0, __tmp_sz = 0, __i = 0, __j = 0, __ex_l = _Expressions.Length;
			__ex_l = _Expressions.Length;
			__tmp_bytes = new T[__ex_l * _RepeatCount][];
			for ( __j = 0; __j < _RepeatCount; __j++ )
				for ( __i = 0; __i < __ex_l; __i++ )
					__tmp_bytes[__j * __ex_l + __i] = _GetT(_Expressions[__i]);
			__j = __tmp_bytes.Length;
			for ( __i = 0; __i < __j; __tmp_sz += __tmp_bytes[__i].Length, __i++ ) ;
			__outbytes = new T[__tmp_sz];
			for ( __i = 0; __i < __j; __i++ ) {
				__tmp_sz = __tmp_bytes[__i].Length;
				Array.Copy(__tmp_bytes[__i], 0, __outbytes, __offset, __tmp_sz);
				__tmp_bytes[__i] = null;
				__offset += __tmp_sz;
			}
			return __outbytes;
		}
		#endregion
	}
}
