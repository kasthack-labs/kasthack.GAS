using System.Text;
using System;
using System.Runtime.InteropServices;
namespace GAS.Core.Strings
{
    public class StaticASCIIStringExpression : IExpression
    {
        ASCIIEncoding enc;
        byte[] buf;
        public override string ToString() {
            return GetString();
        }
        public StaticASCIIStringExpression(string _str, ASCIIEncoding _enc = null) {
            enc = _enc == null ? new ASCIIEncoding() : _enc;
            buf = enc.GetBytes(_str);
        }
        public StaticASCIIStringExpression(char[] _str, ASCIIEncoding _enc = null) {
            enc = _enc == null ? new ASCIIEncoding() : _enc;
            buf = enc.GetBytes(_str);
        }
        public StaticASCIIStringExpression(byte[] _str, ASCIIEncoding _enc = null) {
            enc = _enc == null ? new ASCIIEncoding() : _enc;
            buf = _str;
        }
        public string GetString() {
            return enc.GetString(buf);
        }
        public char[] GetChars() {
            return enc.GetChars(buf);
        }
        public byte[] GetAsciiBytes() {
            return buf;
        }
        public byte[] GetEncodingBytes(Encoding _enc) {
            return _enc.GetBytes(enc.GetChars(buf));
        }
        public System.Collections.Generic.IEnumerable<byte[]> EnumAsciiBuffers() {
            return new byte[][] { GetAsciiBytes() };
        }
        public System.Collections.Generic.IEnumerable<string> EnumStrings() {
            return new string[] { GetString() };
        }
        public unsafe void ComputeLen(ref int* outputdata) {
			*outputdata++ = buf.Length;
        }
        public int ComputeMaxLenForSize() {
            return 1;
        }
		public unsafe void GetAsciiBytesInsert(ref int* _Size, ref byte* _OutputBuffer) {
			IntPtr __p = new IntPtr(_OutputBuffer);
			Marshal.Copy(buf, 0, __p, *_Size);
			_OutputBuffer += *_Size++;
		}
	}
}