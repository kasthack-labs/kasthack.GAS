using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAS.Core.Strings;
using GAS.Core;
namespace DBG.Solution
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            while (true)
            {
                string s = "";
                Console.WriteLine("Type in formatted string");
                s = Console.ReadLine();
                s = "{I:D:0:1000}";
                int len = s.Length;
                fixed (char* _p = s)
                {
                    char* p = _p;
                    var Expression = GAS.Core.Strings.FormattedStringGenerator.Parse(ref p, ref len, new ASCIIEncoding(), new Random());
                    Console.WriteLine(Expression);
                }
            }
        }
    }
}
