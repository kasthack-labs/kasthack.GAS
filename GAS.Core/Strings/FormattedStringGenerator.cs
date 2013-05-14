using System;
using System.Linq;
using System.Text;
namespace GAS.Core.Strings
{
	public class FormattedStringGenerator : IExpression
	{
		public IExpression[] Expressions;
		/// <summary>
		/// Get string representation of expression execution result
		/// </summary>
		/// <returns>string result</returns>
		public string GetString() {
			if ( Expressions.Length == 1 )
				return Expressions[0].GetString();
			return new string(GetChars());
		}
		/// <summary>
		/// Get char array representation of expression execution result
		/// </summary>
		/// <returns>char[] result</returns>
		public unsafe char[] GetChars() {
			if ( Expressions.Length == 1 )
				return Expressions[0].GetChars();
			char* __b;
			char[] __buffer;
			int __outsize = 0;
			int* __s;
			int[] __size_buf = new int[this.ComputeMaxLenForSize()];//buffer 4 sizes
			long __rcount = 0;
			//get generation data
			fixed ( int* __szb = __size_buf ) {
				__s = __szb;
				ComputeStringLength(ref __s);
				__rcount = __s - __szb;
			}
			//compute output length
			for ( int __i = 0; __i < __rcount; __outsize += __size_buf[__i++] ) ;
			__buffer = new char[__outsize];
			//gen!
			fixed ( int* __szb = __size_buf ) {
				fixed ( char* __outb = __buffer ) {
					__s = __szb;
					__b = __outb;
					GetAsciiInsert(ref __s, ref __b);
				}
			}
			return __buffer;
		}
		/// <summary>
		/// Get native representation of expression execution result
		/// </summary>
		/// <returns>ascii bytes</returns>		
		/*public byte[] GetAsciiBytes() {
			if ( Expressions.Length == 1 )
				return Expressions[0].GetAsciiBytes();
			return Functions.GetT<byte>(1, Functions.GetBytesF, Expressions);
		}*/
		/// <summary>
		/// Get bytes of result encoded with encoding
		/// DON'T USE IT.
		/// </summary>
		/// <param name="_enc">encoding for encoding, lol</param>
		/// <returns>bytes</returns>
		public byte[] GetEncodingBytes(Encoding _enc) {
			return this.Expressions.SelectMany( a => a.GetEncodingBytes( _enc ) ).ToArray();
			//return Functions.GetT<byte>(1, a => a.GetEncodingBytes(_enc), this.Expressions);

		}
		/// <summary>
		/// alias 4 GetString. 4 debugging
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return GetString();
		}
		public System.Collections.Generic.IEnumerable<byte[]> EnumAsciiBuffers() {
			return Expressions.SelectMany(a => a.EnumAsciiBuffers());
		}
		public System.Collections.Generic.IEnumerable<string> EnumStrings() {
			return Expressions.SelectMany(a => a.EnumStrings());
		}
		public unsafe void ComputeStringLength(ref int* _outputdata) {
			int __len = Expressions.Length;
			for ( int __i = 0; __i < __len; __i++ )
				Expressions[__i].ComputeStringLength(ref _outputdata);
		}
		public int ComputeMaxLenForSize() {
			int __sum = 0, __len = Expressions.Length;
			for ( int __i = 0; __i < __len; __i++ )
				__sum += Expressions[__i].ComputeMaxLenForSize();
			return __sum;
		}
		public unsafe byte[] GetAsciiBytes() {//_GetPointedBytes() {
			if ( Expressions.Length == 1 )
				return Expressions[0].GetAsciiBytes();
			byte* __b;
			byte[] __buffer;
			int __outsize=0;
			int* __s;
			int[] __size_buf = new int[this.ComputeMaxLenForSize()];//buffer 4 sizes
			long __rcount = 0;
			//get generation data
			fixed ( int* __szb = __size_buf ) {
				__s = __szb;
				ComputeStringLength(ref __s);
				__rcount = __s - __szb;
			}
			//compute output length
			for ( int __i = 0; __i < __rcount; __outsize += __size_buf[__i++] ) ;
			__buffer = new byte[__outsize];
			//gen!
			fixed ( int* __szb = __size_buf ) {
				fixed ( byte* __outb = __buffer ) {
					__s = __szb;
					__b = __outb;
					GetAsciiBytesInsert(ref __s,ref __b);
				}
			}
			return __buffer;
		}
		public unsafe void GetAsciiBytesInsert(ref int* _Size, ref byte* _OutputBuffer) {
			int __len = Expressions.Length;
			for ( int __i = 0; __i < __len; __i++ )
				Expressions[__i].GetAsciiBytesInsert(ref _Size, ref _OutputBuffer);
		}
		public unsafe void GetAsciiInsert(ref int* _Size, ref char* _OutputBuffer) {
			int __len = Expressions.Length;
			for ( int __i = 0; __i < __len; Expressions[__i++].GetAsciiInsert(ref _Size, ref _OutputBuffer) );
		}

	}
}
