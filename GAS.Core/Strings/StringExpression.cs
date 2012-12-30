using System;
using System.Text;
namespace GAS.Core.Strings
{
	public class StringExpression : IExpression
	{
		public StringFormat Format;
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
		[System.Diagnostics.DebuggerNonUserCode]
		public StringExpression() {
		}
		public byte[] GetAsciiBytes() {
			switch ( Format ) {
				case StringFormat.Decimal:
					return Functions.RandomASCIIBytes(Min, Max, Functions._hex_chars_bytes, 0, 9);
				case StringFormat.Hexadecimal:
					return Functions.RandomASCIIBytes(Min, Max, Functions._hex_chars_bytes, 0, 15);
				case StringFormat.Letters:
					return Functions.RandomASCIIBytes(Min, Max, Functions._ascii_chars_bytes, 10, 61);
				case StringFormat.LowerCase:
					return Functions.RandomASCIIBytes(Min, Max, Functions._ascii_chars_bytes, 10, 35);
				case StringFormat.Random:
					return Functions.RandomASCIIBytes(Min, Max);
				case StringFormat.Std:
					return Functions.RandomASCIIBytes(Min, Max, Functions._ascii_chars_bytes, 0, 61);
				case StringFormat.UpperCase:
					return Functions.RandomASCIIBytes(Min, Max, Functions._ascii_chars_bytes, 36, 61);
				case StringFormat.Urlencode:
					return Functions.RandomUTFURLEncodeStringBytes(Min, Max);
				default:
					throw new ArgumentException("Bad string format");
			}
		}
		public byte[] GetEncodingBytes(Encoding enc) {
			byte[] output;
			switch ( Format ) {
				case StringFormat.Decimal:
					output = enc.GetBytes(Functions.RandomASCII(Min, Max, Functions._hex_chars, 0, 9));
					break;
				case StringFormat.Hexadecimal:
					output = enc.GetBytes(Functions.RandomASCII(Min, Max, Functions._hex_chars, 0, 15));
					break;
				case StringFormat.Letters:
					output = enc.GetBytes(Functions.RandomASCII(Min, Max, Functions._ascii_chars, 10, 61));
					break;
				case StringFormat.LowerCase:
					output = enc.GetBytes(Functions.RandomASCII(Min, Max, Functions._ascii_chars, 10, 35));
					break;
				case StringFormat.Random:
					output = enc.GetBytes(Functions.RandomASCII(Min, Max));
					break;
				case StringFormat.Std:
					output = enc.GetBytes(Functions.RandomASCII(Min, Max, Functions._ascii_chars, 0, 61));
					break;
				case StringFormat.UpperCase:
					output = enc.GetBytes(Functions.RandomASCII(Min, Max, Functions._ascii_chars, 36, 61));
					break;
				case StringFormat.Urlencode:
					output = enc.GetBytes(Functions.RandomUTFURLEncodeString(Min, Max));
					break;
				default:
					throw new ArgumentException("Bad string format");
			}
			return output;
		}
		public char[] GetChars() {
			switch ( Format ) {
				case StringFormat.Decimal:
					return Functions.RandomASCII(Min, Max, Functions._hex_chars, 0, 9);
				case StringFormat.Hexadecimal:
					return Functions.RandomASCII(Min, Max, Functions._hex_chars, 0, 15);
				case StringFormat.Letters:
					return Functions.RandomASCII(Min, Max, Functions._ascii_chars, 10, 61);
				case StringFormat.LowerCase:
					return Functions.RandomASCII(Min, Max, Functions._ascii_chars, 10, 35);
				case StringFormat.Random:
					return Functions.RandomASCII(Min, Max);
				case StringFormat.Std:
					return Functions.RandomASCII(Min, Max, Functions._ascii_chars, 0, 61);
				case StringFormat.UpperCase:
					return Functions.RandomASCII(Min, Max, Functions._ascii_chars, 36, 61);
				case StringFormat.Urlencode:
					return Functions.RandomUTFURLEncodeString(Min, Max);
				default:
					throw new ArgumentException("Bad string format");
			}
		}
		public string GetString() {
			switch ( Format ) {
				case StringFormat.Decimal:
					return new string(Functions.RandomASCII(Min, Max, Functions._hex_chars, 0, 9));
				case StringFormat.Hexadecimal:
					return new string(Functions.RandomASCII(Min, Max, Functions._hex_chars, 0, 15));
				case StringFormat.Letters:
					return new string(Functions.RandomASCII(Min, Max, Functions._ascii_chars, 10, 61));
				case StringFormat.LowerCase:
					return new string(Functions.RandomASCII(Min, Max, Functions._ascii_chars, 10, 35));
				case StringFormat.Random:
					return new string(Functions.RandomASCII(Min, Max));
				case StringFormat.Std:
					return new string(Functions.RandomASCII(Min, Max, Functions._ascii_chars, 0, 61));
				case StringFormat.UpperCase:
					return new string(Functions.RandomASCII(Min, Max, Functions._ascii_chars, 36, 61));
				case StringFormat.Urlencode:
					return new string(Functions.RandomUTFURLEncodeString(Min, Max));
				default:
					throw new ArgumentException("Bad string format");
			}
		}
		public override string ToString() {
			return GetString();
		}
		public System.Collections.Generic.IEnumerable<byte[]> EnumAsciiBuffers() {
			return new byte[][] { GetAsciiBytes() };
		}
		public System.Collections.Generic.IEnumerable<string> EnumStrings() {
			return new string[] { GetString() };
		}
		public unsafe void ComputeLen(ref int* outputdata) {
			*outputdata++ = Functions.random.Next(_Min, _Max) * ( Format == StringFormat.Urlencode ? 6 : 1 );
		}
		public int ComputeMaxLenForSize() {
			return 1;
		}
		public unsafe void GetAsciiBytesInsert(ref int* _Size, ref byte* _OutputBuffer) {
			int __len = *_Size++;
			fixed(byte* __chars = Functions._ascii_chars_bytes)
			switch ( Format ) {
				case StringFormat.Decimal:
					Functions.RandomASCIIBytesInsert(_OutputBuffer, __len, __chars, 9);
					break;
				case StringFormat.Hexadecimal:
					Functions.RandomASCIIBytesInsert(_OutputBuffer, __len, __chars, 15);
					break;
				case StringFormat.Letters:
					Functions.RandomASCIIBytesInsert(_OutputBuffer, __len, __chars+ 10, 51);
					break;
				case StringFormat.LowerCase:
					Functions.RandomASCIIBytesInsert(_OutputBuffer, __len, __chars+ 10, 25);
					break;
				case StringFormat.Random:
					Functions.RandomASCIIBytesInsert(_OutputBuffer, __len, __chars, 93);
					break;
				case StringFormat.Std:
					Functions.RandomASCIIBytesInsert(_OutputBuffer, __len, __chars, 61);
					break;
				case StringFormat.UpperCase:
					Functions.RandomASCIIBytesInsert(_OutputBuffer, __len, __chars+ 36, 25);
					break;
				case StringFormat.Urlencode:
					Functions.RandomUTFURLEncodeStringBytesInsert(_OutputBuffer, __len / 6);
					break;
				default:
					throw new ArgumentException("Bad string format");
			}
			_OutputBuffer += __len;
		}
		public unsafe void GetAsciiInsert(ref int* _Size, ref char* _OutputBuffer) {
			int __len = *_Size++;
			fixed ( char* __chars = Functions._ascii_chars )
				switch ( Format ) {
					case StringFormat.Decimal:
						Functions.RandomASCIIInsert(_OutputBuffer, __len, __chars, 9);
						break;
					case StringFormat.Hexadecimal:
						Functions.RandomASCIIInsert(_OutputBuffer, __len, __chars, 15);
						break;
					case StringFormat.Letters:
						Functions.RandomASCIIInsert(_OutputBuffer, __len, __chars + 10, 51);
						break;
					case StringFormat.LowerCase:
						Functions.RandomASCIIInsert(_OutputBuffer, __len, __chars + 10, 25);
						break;
					case StringFormat.Random:
						Functions.RandomASCIIInsert(_OutputBuffer, __len, __chars, 93);
						break;
					case StringFormat.Std:
						Functions.RandomASCIIInsert(_OutputBuffer, __len, __chars, 61);
						break;
					case StringFormat.UpperCase:
						Functions.RandomASCIIInsert(_OutputBuffer, __len, __chars + 36, 25);
						break;
					case StringFormat.Urlencode:
						Functions.RandomUTFURLEncodeStringInsert(_OutputBuffer, __len / 6);
						break;
					default:
						throw new ArgumentException("Bad string format");
				}
			_OutputBuffer += __len;
		}
	}
}
