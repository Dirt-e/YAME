using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class Server
    {
        public string RawDatastring { get; set; }
        public int Port { get; set; } = 31090;

        public string defaultDataString = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0";        //The 1 is for vertical
        Stopwatch stopwatch = new Stopwatch();                                          //To determine the timeout for the default values

        IPEndPoint MyEndPoint;
        UdpClient Client;
        
        public void StartServer()
        {
            MyEndPoint = new IPEndPoint(IPAddress.Any, Port);
            Client = new UdpClient(MyEndPoint);

            RawDatastring = defaultDataString;
        }
        public void Read()
        {
            while (Client.Available > 0)
            {
                Byte[] bytes = Client.Receive(ref MyEndPoint);
                RawDatastring = Encoding.ASCII.GetString(bytes);
                stopwatch.Restart();
            }
            
            if (stopwatch.ElapsedMilliseconds >= 500)
            {
                RawDatastring = defaultDataString;
            }
        }
        public void StopServer()
        {
            Client.Close();
        }
    }

    public enum Serverstatus
    {
        Off,
        Ready,
        Connected
    }
}
