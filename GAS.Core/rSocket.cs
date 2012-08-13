using System;
using System.Net.Sockets;
namespace GAS.Core
{
    class rSocket : Socket
    {
        public rSocket(SocketInformation socketinformation) : base(socketinformation) { }
        public rSocket(AddressFamily adressfamily, SocketType sockettype, ProtocolType protocoltype) : base(adressfamily, sockettype, protocoltype) { }
        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
