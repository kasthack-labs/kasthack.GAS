using GAS.Core;
using GAS.Core.Strings;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SharpNeatLib.Maths;
namespace DBG.Solution
{
	class Program
	{
		static void Main(string[] _args) {
			//UnitTests();
			//bug: int.MinValue, long.MaxValue
			//non-std: to hex -
			//CheckTree();
			//CheckPointers();
            //MemBEnch();
            //Console.ReadLine();
            UTFTest();
		}
        static unsafe void UTFTest() {
            int len = 32;
            int cnt = 10000000;
            byte[] bytes = new byte[len * 6];
            Stopwatch s = new Stopwatch();
            s.Start();
            fixed(byte* ptr = bytes)
            {
                for ( int i = 0; i < cnt; i++ )
                    Functions.RandomUTFURLEncodeStringBytesInsert(ptr, len);
            }
            s.Stop();
            Console.WriteLine(s.Elapsed);
        }
        static void MemBEnch() {
            int G = 1024 * 1024 * 1024;
            Stopwatch s = new Stopwatch();
            int R = 5;
            int[] sizes = new int[] {
                256,
                1024,
                8 * 1024,
                16 * 1024,
                1024 * 32,
                1024 * 64,
                1024 * 512,
                1024 * 768,
                1024 * 1024,
                1024 * 1536,
                1024 * 1024*2,
                1024 * 1024 * 128
            };
            foreach ( int SZ in sizes ) {
                Console.WriteLine("Benching {0}G by {1} bytes ", R, SZ);
                s.Start();
                RndBench(G / SZ * R, SZ);
                s.Stop();
                Console.WriteLine("Elapsed: {0} for {1}G by {2} bytes ", s.Elapsed, R, SZ);
                s.Reset();
            }
            //100000 loops/0.5M per loop

            
        }
        static void RndBench( int count, int size ) {
            FastRandom r = new FastRandom();
            byte[] bytes = new byte[size];
            for ( int i = 0; i < count; i++ ) {
                r.NextBytes(bytes);
            }
        }
        static void CheckPointers() {
			FormattedStringGenerator Expression = ExpressionParser.Parse(
				"{R:{{S:a:5:10}={S:a:1:4}}:1:15}"
				//{S:a:5:10}={S:a:1:4}"
				);
				//"GET {R:{/{S:S:5:10}}:1:20} Http1.1\r\nHost: {R:{{S:a:1:10}.}:1:3}free.fr\r\nAccept: image/gif, image/x-xbitmap, image/jpeg, image/pjpeg" +
					//"\r\nAccept-Language: {S:a:2:2}\r\nAccept-Encoding: gzip, deflate\r\n" +
					//"User-Agent: Mozilla/{I:D:1:5}.{I:D:1:9} (compatible; MSIE {I:D:1:10}.5; Windows NT {I:D:3:6}.0)\r\nConnection: Keep-Alive");
			//Console.WriteLine(new ASCIIEncoding().GetString(Expression._GetPointedBytes()));
		}
		static void UnitTests() {
			int[] __int_test_values = new int[] { 0, -1, 0, int.MaxValue, int.MinValue + 1 };
			long[] __long_test_values = __int_test_values.Select(a => (long)a).ToArray();
			var tr = new char[] { '0', 'O' };
			foreach ( int __i in __int_test_values )
				Debug.Assert(Functions.GetDecStringLength(__i) == __i.ToString().Length, "Bad length " + __i);
			foreach ( int __i in __int_test_values )
				Debug.Assert(new string(Functions.IntToDecString(__i)) == __i.ToString(), "Bad ToString " + __i);
			foreach ( int __i in __int_test_values )
				Debug.Assert(new string(Functions.IntToHexString(__i)).TrimStart(tr) == __i.ToString("x2").TrimStart(tr), "Bad ToString " + __i + " Real " + __i.ToString("x2").TrimStart(tr));
		}
		static void CheckTree() {
			//while ( true ) {
				try {
					string ExpressionString = "";
					IExpression Expression;
					int cnt;
					Stopwatch timer;
					byte[] s;
					timer = new Stopwatch();
					Console.WriteLine("Type in formatted string");
					//ExpressionString = Console.ReadLine();
					ExpressionString = String.IsNullOrEmpty(ExpressionString) ? //77860 last
						//"{R:{{S:a:5:10}={S:a:1:4}&}:0:2}{S:a:5:10}={S:a:1:4}":
					"GET {R:{/{S:S:5:10}}:1:20} Http1.1\r\nHost: {R:{{S:a:1:10}.}:1:3}free.fr\r\nAccept: image/gif, image/x-xbitmap, image/jpeg, image/pjpeg" +
					"\r\nAccept-Language: {S:a:2:2}\r\nAccept-Encoding: gzip, deflate\r\n" +
					"User-Agent: Mozilla/{I:D:1:5}.{I:D:1:9} (compatible; MSIE {I:D:1:10}.5; Windows NT {I:D:3:6}.0)\r\nConnection: Keep-Alive" :
					ExpressionString;
					Expression = ExpressionParser.Parse(ExpressionString);
					//Console.WriteLine("Expression: {0}{1}Result: {2}{1}Enter loops", ExpressionString, Environment.NewLine, new String(Functions.GetT<char>(1, Functions.GetCharsF, ( (FormattedStringGenerator)Expression ).Expressions)));
					cnt = 2500000;//int.Parse(Console.ReadLine());
					Console.WriteLine("Benching {0}", cnt);
					timer.Start();
					for ( int i = 0; i < cnt; i++ )
						s = Expression.GetAsciiBytes();
					timer.Stop();
					Console.WriteLine("Finished: {0}", timer.Elapsed);
				}
				catch ( Exception ex ) {
					var bc = Console.BackgroundColor;
					var fc = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(ex);
					Console.ForegroundColor = fc;
				}
			//}
		}
	}
}
