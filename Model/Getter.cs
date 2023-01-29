using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YAME.Model
{
    public class Getter : MyObject
    {
        public UDP_Server udp_Server = new UDP_Server();
        public Getter_Condor2 getter_Condor2 = new Getter_Condor2();

        MotionSource _source;
        public MotionSource Source 
        { 
            get { return _source; }
            set { _source = value; OnPropertyChanged(nameof(Source)); }
        }

        public string RawDatastring { get; set; } = Properties.Settings.Default.Getter_DefaulDataString;

        bool _data_flowing;
        public bool DataFlowing
        {
            get { return _data_flowing; }
            set { _data_flowing = value; OnPropertyChanged(nameof(DataFlowing)); }
        }

        Stopwatch stopwatch = Stopwatch.StartNew();     //To determine the timeout for the default values
        private readonly int Timeout = Properties.Settings.Default.Getter_Timeout;             //[ms]

        public void StartGetter()
        {
            udp_Server.StartServer();
        }
        public void GetData()
        {
            switch (Source)
            {
                case MotionSource.None:
                    break;
                case MotionSource.DCS:
                case MotionSource.DCS_openbeta:
                    udp_Server.Read();
                    if (udp_Server.dataAvailable && IsDCSFormat(udp_Server.RawString))
                    {
                        RawDatastring = udp_Server.RawString;
                        DataFlowing = true;
                        stopwatch.Restart();
                    }
                    break;
                case MotionSource.FS2020:
                    udp_Server.Read();
                    if (udp_Server.dataAvailable && IsFS2020Format(udp_Server.RawString))
                    {
                        RawDatastring = udp_Server.RawString;
                        DataFlowing = true;
                        stopwatch.Restart();
                    }
                    break;
                case MotionSource.XPlane:
                    udp_Server.Read();
                    if (udp_Server.dataAvailable && IsXPlaneFormat(udp_Server.RawString))
                    {
                        RawDatastring = udp_Server.RawString;
                        DataFlowing = true;
                        stopwatch.Restart();
                    }
                    break;
                case MotionSource.iRacing:
                    udp_Server.Read();
                    if (udp_Server.dataAvailable && IsiRacingFormat(udp_Server.RawString))
                    {
                        RawDatastring = udp_Server.RawString;
                        DataFlowing = true;
                        stopwatch.Restart();
                    }
                    break;
                case MotionSource.Condor2:
                    udp_Server.Read();
                    if (udp_Server.dataAvailable && IsCondorFormat(udp_Server.RawString))
                    {
                        getter_Condor2.Update(udp_Server.RawString);
                        RawDatastring = getter_Condor2.RawDatastring;
                        DataFlowing = true;
                        stopwatch.Restart();
                    }
                    break;
                default:
                    break;
            }

            if (stopwatch.ElapsedMilliseconds >= Timeout)
            {
                RawDatastring = Properties.Settings.Default.Getter_DefaulDataString;
                DataFlowing = false;
            }
        }

        bool IsDCSFormat(string rawstring)
        {
            return rawstring.Contains("DCS");
        }
        bool IsFS2020Format(string rawstring)
        {
            return rawstring.Contains("FS2020");
        }
        bool IsXPlaneFormat(string rawstring)
        {
            return rawstring.Contains("X-Plane");
        }
        bool IsiRacingFormat(string rawstring)
        {
            return rawstring.Contains("iRacing");
        }
        bool IsCondorFormat(string rawstring)
        {
            return rawstring.Contains("surfaceroughness") && rawstring.Contains("turbulencestrength");
        }
    }

    public enum MotionSource
    {
        None,
        DCS,
        DCS_openbeta,
        FS2020,
        XPlane,
        iRacing,
        Condor2,
    }
}
