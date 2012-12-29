using System;
using System.Text;
namespace GAS.Core.Strings
{
	public class IntExpression : IExpression
	{
		public NumberFormat Format;
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
		public IntExpression() {
		}
		/// <summary>
		/// Get string representation of expression execution result
		/// </summary>
		/// <returns>string result</returns>
		public string GetString() {
			return new string(Format == NumberFormat.Decimal ? Functions.IntToDecString(Functions.random.Next(_Min, _Max)) :
			  Functions.IntToHexString(Functions.random.Next(_Min, _Max)));
		}
		/// <summary>
		/// Get char array representation of expression execution result
		/// </summary>
		/// <returns>char[] result</returns>
		public char[] GetChars() {
			return Format == NumberFormat.Decimal ? Functions.IntToDecString(Functions.random.Next(_Min, _Max)) :
			  Functions.IntToHexString(Functions.random.Next(_Min, _Max));
		}
		/// <summary>
		/// Get native representation of expression execution result
		/// </summary>
		/// <returns>ascii bytes</returns>
		public byte[] GetAsciiBytes() {
			return Format == NumberFormat.Decimal ? Functions.IntToDecStringBytes(Functions.random.Next(_Min, _Max)) :
			  Functions.IntToHexStringBytes(Functions.random.Next(_Min, _Max));
		}
		/// <summary>
		/// Get bytes of result encoded with encoding
		/// </summary>
		/// <param name="_enc">encoding for encoding, lol</param>
		/// <returns>bytes</returns>
		public byte[] GetEncodingBytes(Encoding enc) {
			return enc.GetBytes(Format == NumberFormat.Decimal ? Functions.IntToDecString(Functions.random.Next(_Min, _Max)) :
			  Functions.IntToHexString(Functions.random.Next(_Min, _Max)));
		}
		/// <summary>
		/// alias 4 GetString. 4 debugging
		/// </summary>
		/// <returns></returns>
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
			throw new NotImplementedException();
		}
		public int ComputeMaxLenForSize() {
			return 1;
		}
	}
}
