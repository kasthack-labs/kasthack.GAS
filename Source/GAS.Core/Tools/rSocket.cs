using System;
using System.Net.Sockets;

namespace GAS.Core.Tools {
    class RSocket : Socket, IDisposable {
        public RSocket( SocketInformation socketinformation ) : base( socketinformation ) { }
        public RSocket( AddressFamily adressfamily, SocketType sockettype, ProtocolType protocoltype ) : base( adressfamily, sockettype, protocoltype ) { }
        public new void Dispose() {
            this.Dispose( true );
        }
    }
}
