using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace YAME.Model
{
    public class UDP_Server : MyObject
    {
        public string RawString { get; set; }
        public bool dataAvailable { get; set; } = false;
       
        readonly int _port = Properties.Settings.Default.Server_Port_UDP;
        public int Port
        {
            get { return _port; }
        }

        IPEndPoint MyEndPoint;
        UdpClient Client;
        
        public void StartServer()
        {
            MyEndPoint = new IPEndPoint(IPAddress.Any, Port);
            try
            {
                Client = new UdpClient(MyEndPoint);
            }
            catch (Exception)
            {
                MessageBox.Show(    "It looks like the port YAME wants to use for communication " +
                                    $"(Port {Port}) is in use already.\n\n" +
                                    "Shoot us an email to software@hexago-motion.com. We will get you a fix.",
                                    "UDP_Server: Port occupied",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                
                Application.Current.Shutdown();     //THE END!!!!
            }
        }
        public void Read()
        {
            Byte[] bytes;
            if (Client.Available == 0)
            {
                dataAvailable = false;
            }
            else
            {
                bytes = Client.Receive(ref MyEndPoint);
                RawString = Encoding.ASCII.GetString(bytes);
            
                dataAvailable = true;
            }
        }
        public void StopServer()
        {
            Client.Close();
        }
    }
}
