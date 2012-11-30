using GAS.Core;
using GAS.Core.Strings;
using System;
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
                        "{R:{{S:S:5:10}={S:S:1:4}&}:0:2}{S:S:5:10}={S:S:1:4}" : ExpressionStrring; ;
                    //ExpressionParser
                    IExpression Expression = ExpressionParser.Parse(ExpressionStrring);
                    Console.WriteLine("Expression: {0}",ExpressionStrring);
                    Console.WriteLine("Result: {0}", Expression);
                    Console.ReadLine();
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
