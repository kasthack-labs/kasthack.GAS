using System.Text;
using System.Linq;
using System;
namespace GAS.Core.Strings
{
	public class RepeatExpression : IExpression
	{
		int _Min, _Max;
		public int Min {
			get {
				return _Min;
			}
			set {
				_Min = value + 1;
			}
		}
		public int Max {
			get {
				return _Max;
			}
			set {
				_Max = value + 1;
			}
		}
		public IExpression[] Expressions;
		public RepeatExpression() {
		}
		public string GetString() {
			return new string(GetChars());
		}
		public byte[] GetAsciiBytes() {
			return GetAsciiBytes(Functions.random.Next(_Min, _Max));
		}
		public unsafe byte[] GetAsciiBytes(int _RepeatCount) {
			if ( _RepeatCount == 0 )
				return new byte[] { };
			if ( Expressions.Length == 1 && _RepeatCount==1)
				return Expressions[0].GetAsciiBytes();
			byte* __b;
			byte[] __buffer;
			int __outsize = 0;
			int __size_len = CompLen() * _RepeatCount + 2;
			int* __s;
			int[] __size_buf = new int[__size_len];//buffer 4 sizes
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
					GetAsciiBytesInsert(ref __s, ref __b);
				}
			}
			return __buffer;
		}
		public char[] GetChars() {
			return GetChars(Functions.random.Next(_Min, _Max));
		}
		public unsafe char[] GetChars(int _RepeatCount) {
			//same as get ascii bytes but with chars
			if ( _RepeatCount == 0 )
				return new char[] { };
			if ( Expressions.Length == 1 && _RepeatCount == 1 )
				return Expressions[0].GetChars();
			char* __b;
			char[] __buffer;
			int __outsize = 0;
			int __size_len = CompLen() * _RepeatCount + 2;
			int* __s;
			int[] __size_buf = new int[__size_len];//buffer 4 sizes
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
		public byte[] GetEncodingBytes(Encoding _enc) {
			return GetEncodingBytes(_enc, Functions.random.Next(_Min, _Max));
		}
		public unsafe byte[] GetEncodingBytes(Encoding _enc, int _RepeatCount) {
			//return Functions.GetT<byte>(_RepeatCount, a => a.GetEncodingBytes(_enc), this.Expressions);
			return this.Expressions.SelectMany( a => a.GetEncodingBytes( _enc ) ).ToArray();
		}
		int CompLen() {
			int __sum = 0, __len = Expressions.Length;
			for ( int __i = 0; __i < __len; __i++ )
				__sum += Expressions[__i].ComputeMaxLenForSize();
			return __sum;
		}
		public override string ToString() {
			return GetString();
		}
		public System.Collections.Generic.IEnumerable<byte[]> EnumAsciiBuffers() {
			return Enumerable.Range(0, Functions.random.Next(_Min, _Max)).SelectMany(a => Expressions.SelectMany(b => b.EnumAsciiBuffers())).ToArray();
		}
		public System.Collections.Generic.IEnumerable<string> EnumStrings() {
			return Enumerable.Range(0, Functions.random.Next(_Min, _Max)).
			SelectMany(
			 a => Expressions.SelectMany(b => b.EnumStrings())
			);
		}
		public unsafe void ComputeStringLength(ref int* _outputdata) {
			int __len = Expressions.Length, __value = Functions.random.Next(_Min, _Max);
			*_outputdata++ = __value;
			*_outputdata++ = -__value;
			for (int __j=0;__j<__value;__j++)
				for ( int __i = 0; __i < __len; __i++ )
					Expressions[__i].ComputeStringLength(ref _outputdata);
		}
		public int ComputeMaxLenForSize() {
			int __sum = 0,__len=Expressions.Length;
			for ( int i = 0; i < __len; i++ )
				__sum += Expressions[i].ComputeMaxLenForSize();
			return __sum * _Max + 2;//inner expressions+repeat count 
		}
		public unsafe void GetAsciiBytesInsert(ref int* _Size, ref byte* _OutputBuffer) {
			int __len = Expressions.Length, __rpt =*_Size++;
			_Size++;
			for (int __j =0;__j<__rpt;__j++)
				for ( int __i = 0; __i < __len; __i++ )
					Expressions[__i].GetAsciiBytesInsert(ref _Size, ref _OutputBuffer);
		}
		public unsafe void GetAsciiInsert(ref int* _Size, ref char* _OutputBuffer) {
			int __len = Expressions.Length, __rpt = *_Size++;
			_Size++;
			for ( int __j = 0; __j < __rpt; __j++ )
				for ( int __i = 0; __i < __len; __i++ )
					Expressions[__i].GetAsciiInsert(ref _Size, ref _OutputBuffer);
		}
	}
}
