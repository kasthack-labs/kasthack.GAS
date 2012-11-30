using GAS.Core;
using GAS.Core.Strings;
using System;
using System.Diagnostics;
namespace DBG.Solution
{
    class Program
    {
        static void Main(string[] args) {
            while ( true ) {
                try {
                    string ExpressionStrring = "";
                    Console.WriteLine("Type in formatted string");
                    ExpressionStrring = Console.ReadLine();
                    ExpressionStrring =String.IsNullOrEmpty(ExpressionStrring)?
                        "{R:{{S:a:5:10}={S:a:1:4}&}:0:2}{S:a:5:10}={S:a:1:4}" :
                        ExpressionStrring;
                    IExpression Expression = ExpressionParser.Parse(ExpressionStrring);
                    Console.WriteLine("Expression: {0}",ExpressionStrring);
                    Console.WriteLine("Result: {0}", Expression);
                    Console.WriteLine("Enter loops");
                    int cnt = int.Parse(Console.ReadLine());
                    Console.WriteLine("Benching {0}",cnt);
                    var v = new Stopwatch();
                    v.Start();
                    string s = "";
                    for ( int i = 0; i <cnt; i++ )
                        s=Expression.GetString();
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
