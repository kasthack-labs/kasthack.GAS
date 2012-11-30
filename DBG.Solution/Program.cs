using GAS.Core;
using GAS.Core.Strings;
using System;
namespace DBG.Solution
{
    class Program
    {
        static unsafe void Main(string[] args) {
            while ( true ) {
                string s = "";
                Console.WriteLine("Type in formatted string");
                s = Console.ReadLine();
                s =// "{I:D:1:2345}";
                "{R:{{I:D:1:2345}}:7:8}ABC";
                //ExpressionParser
                IExpression ex = ExpressionParser.Parse(s);
                Console.WriteLine(s);
                Console.ReadLine();
            }
        }
    }
}
