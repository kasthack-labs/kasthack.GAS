using System;
using System.Text;
namespace GAS.Core.Strings
{
public class StringExpression : IExpression
{
public StringFormat Format;
public int Min, Max;
Random rnd;
[System.Diagnostics.DebuggerNonUserCode]
public StringExpression(Random _rnd) {
rnd = _rnd == null ? new Random() : _rnd;
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
throw new NotImplementedException();
}
public int ComputeMaxLenForSize() {
return 1;
}
}
}
