using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace MultiPortScan
{
    /// <summary>
    /// A Console type Multi Port TCP Scanner
    /// Author : Philip Murray
    /// </summary>

    class Program
    {
        static void Main(string[] args)
        {
            string host;
            int portStart;
            int portStop;

            int timeout;
            string startPort;
            string endPort;
 
            string tcpTimeout;
           

            startPort = "0";

            endPort = "8000";
            
            tcpTimeout = "2";
            timeout = int.Parse(tcpTimeout) * 1000;

            string[] IpAddress = new string[254];

            string IPLocal = getIPAdressLocal();
            int Index = getIPAdressLocal().ToString().LastIndexOf(".");
            IPLocal = IPLocal.Remove(Index, IPLocal.Length - (Index));
            for (int i = 0; i <= 253; i++)
            {
                IpAddress[i] = IPLocal.Insert(Index, "." + (i).ToString());
            }


            for (int i =0; i<= 253; i++)
            {
                host = IpAddress[i];
                //Console.WriteLine("Scanning the IP: {0}", IpAddress[i]);
                portStart = int.Parse(startPort);
                portStop = int.Parse(endPort);
                PortScanner ps = new PortScanner(host, portStart, portStop, timeout);
                ps.start(10); // 10 Threads
            }
        }
        public static string getIPAdressLocal()
        {
            string ipAddress = "";
            var ni = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface item in ni)
            {
                if (item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork & !IPAddress.IsLoopback(ip.Address))
                        {
                            ipAddress = ip.Address.ToString();

                        }
                    }
                }

            }
            return ipAddress;
        }
    }
    
}
