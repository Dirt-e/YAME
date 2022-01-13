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
    public class Server : MyObject
    {
        public string RawDatastring { get; set; }
        public int Port { get; set; } = 31090;
        bool _data_flowing;
        public bool DataFlowing
        { 
            get { return _data_flowing; }
            set { _data_flowing = value; OnPropertyChanged(nameof(DataFlowing)); }
        }

        public string defaultDataString = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,9.806,0,0,0,nil";        //The 9.806 is for vertical acceleration (1G)
        Stopwatch stopwatch = new Stopwatch();                                                  //To determine the timeout for the default values

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
                                    $"(Port {Properties.Settings.Default.Server_Port_UDP}) is in use already.\n\n" +
                                    "Shoot us an email to software@hexago-motion.com. We will get you a fix.",
                                    "Port occupied",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                
                Application.Current.Shutdown();     //THE END!!!!
            }
            

            RawDatastring = defaultDataString;
        }
        public void Read()
        {
            while (Client.Available > 0)
            {
                Byte[] bytes = Client.Receive(ref MyEndPoint);
                RawDatastring = Encoding.ASCII.GetString(bytes);

                DataFlowing = true;

                stopwatch.Restart();
            }
            
            if (stopwatch.ElapsedMilliseconds >= 500)
            {
                RawDatastring = defaultDataString;
                DataFlowing = false;
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
