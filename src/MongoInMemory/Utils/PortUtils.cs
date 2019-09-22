using System.Net;
using System.Net.Sockets;

namespace MongoInMemory.Utils
{
    public static class PortUtils
    {
        public static int GetFreePortNumber()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int resultPort = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return resultPort;
        }
    }
}
