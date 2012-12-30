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
		public byte[] GetAsciiBytes(int _RepeatCount) {
			return Functions.GetT<byte>(_RepeatCount, Functions.GetBytesF, Expressions);
		}
		public char[] GetChars() {
			return GetChars(Functions.random.Next(_Min, _Max));
		}
		public char[] GetChars(int _RepeatCount) {
			//same as get ascii bytes but with chars
			return Functions.GetT<char>(_RepeatCount, Functions.GetCharsF, Expressions);
		}
		public byte[] GetEncodingBytes(Encoding _enc) {
			return GetEncodingBytes(_enc, Functions.random.Next(_Min, _Max));
		}
		public byte[] GetEncodingBytes(Encoding _enc, int _RepeatCount) {
			return Functions.GetT<byte>(_RepeatCount, a => a.GetEncodingBytes(_enc), this.Expressions);
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
		public unsafe void ComputeLen(ref int* _outputdata) {
			int __len = Expressions.Length, __value = Functions.random.Next(_Min, _Max);
			*_outputdata++ = __value;
			*_outputdata++ = -__value;
			for (int __j=0;__j<__value;__j++)
				for ( int __i = 0; __i < __len; __i++ )
					Expressions[__i].ComputeLen(ref _outputdata);
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
	}
}
