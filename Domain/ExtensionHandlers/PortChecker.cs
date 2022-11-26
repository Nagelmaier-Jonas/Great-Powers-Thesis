using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Domain.ExtensionHandlers;

public class PortChecker{
    private static TcpClient tcpClient = new ();
    public bool IsPortOpen(int port){
        bool isAvailable = true;
        
        IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
        TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

        foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
        {
            if (tcpi.LocalEndPoint.Port==port)
            {
                isAvailable = false;
                break;
            }
        }

        return isAvailable;
    }
}