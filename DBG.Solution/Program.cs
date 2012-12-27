using GAS.Core;
using GAS.Core.Strings;
using System;
using System.Diagnostics;
using System.Linq;
namespace DBG.Solution
{
    class Program
    {
        static void Main(string[] _args) {
            //UnitTests();
            //bug: int.MinValue, long.MaxValue
            //non-std: to hex -
            CheckTree();
            Console.ReadLine();
        }
        static void UnitTests() {
            int[] __int_test_values = new int[] { 0, -1, 0, int.MaxValue, int.MinValue+1 };
            long[] __long_test_values = __int_test_values.Select(a=>(long)a).ToArray();
            var tr = new char[]{'0', 'O'};

            foreach ( int __i in __int_test_values )
                Debug.Assert(Functions.GetDecStringLength(__i) == __i.ToString().Length, "Bad length " + __i);
            foreach ( int __i in __int_test_values )
                Debug.Assert(new string(Functions.IntToDecString(__i)) == __i.ToString(), "Bad ToString " + __i);
            foreach ( int __i in __int_test_values )
                Debug.Assert(new string(Functions.IntToHexString(__i)).TrimStart(tr) == __i.ToString("x2").TrimStart(tr), "Bad ToString " + __i + " Real " + __i.ToString("x2").TrimStart(tr));

        }
        static void CheckTree() {

            while ( true ) {
                try {
                    string ExpressionString = "";
                    Console.WriteLine("Type in formatted string");
                        ExpressionString = Console.ReadLine();
                        ExpressionString = String.IsNullOrEmpty(ExpressionString) ? 
                            "{R:{{S:a:5:10}={S:a:1:4}&}:0:2}{S:a:5:10}={S:a:1:4}" 
                            //"12{I:D:1:10}"
                            : ExpressionString;
                    IExpression Expression = ExpressionParser.Parse(ExpressionString);
                    Console.WriteLine("Expression: {0}", ExpressionString);
                    Console.WriteLine("Result: {0}", new String(Functions.GetT<char>(1, Functions.GetCharsF,
                        ((FormattedStringGenerator)Expression).Expressions)));
                    Console.WriteLine("Enter loops");
                    int cnt = int.Parse(Console.ReadLine());
                    Console.WriteLine("Benching {0}", cnt);
                    var v = new Stopwatch();
                    v.Start();
                    string s = "";
                    for ( int i = 0; i < cnt; i++ )
                        s = Expression.ToString();
                    v.Stop();
                    Console.WriteLine("Finished: {0}", v.Elapsed);
                }
                catch ( Exception ex ) {
                    var bc = Console.BackgroundColor;
                    var fc = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex);
                    Console.ForegroundColor = fc;
                }
            }
        }
    }
}
