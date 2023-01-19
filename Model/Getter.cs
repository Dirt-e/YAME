using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YAME.Model
{
    public class Getter : MyObject
    {
        public MotionSource Source { get; set; } = MotionSource.None;
        public UDP_Server udp_Server = new UDP_Server();

        public string RawDatastring { get; set; } = Properties.Settings.Default.Server_DefaulDataString;

        bool _data_flowing;
        public bool DataFlowing
        {
            get { return _data_flowing; }
            set { _data_flowing = value; OnPropertyChanged(nameof(DataFlowing)); }
        }

        int counter;
        double deltatime;
        double prev_time;

        Stopwatch stopwatch = Stopwatch.StartNew();     //To determine the timeout for the default values
        private readonly int Timeout = Properties.Settings.Default.Getter_Timeout;             //[ms]

        public void StartGetter()
        {
            udp_Server.StartServer();
        }
        public void GetData()
        {
            //Do your  thing!!!
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
                        RawDatastring = ConvertFromCondor(udp_Server.RawString);
                        DataFlowing = true;
                        stopwatch.Restart();
                    }
                    break;
                case MotionSource.Condor2:
                    udp_Server.Read();
                    if (udp_Server.dataAvailable && IsCondorFormat(udp_Server.RawString))
                    {
                        RawDatastring = ConvertFromCondor(udp_Server.RawString);
                        DataFlowing = true;
                        stopwatch.Restart();
                    }
                    break;
                default:
                    break;
            }

            if (stopwatch.ElapsedMilliseconds >= Timeout)
            {
                RawDatastring = Properties.Settings.Default.Server_DefaulDataString;
                DataFlowing = false;
            }
        }

        //Helpers:
        private string ConvertFromCondor(string rawString)
        {
            //This function takes the raw string from Condor2 and returns the
            //rawDataString the Chopper can process
            string[] lines = rawString.Replace("\r", "").Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {   
                if (lines[i].Contains("NAN"))
                {   
                    lines[i] = "0";
                }
                else
                {
                    lines[i] = lines[i].Split('=').Last();
                }
            }

            //Conversions here:
            double Hdng     = double.Parse(lines[12],   CultureInfo.InvariantCulture);
            double Bank     = double.Parse(lines[14],   CultureInfo.InvariantCulture);
            double Ptch     = double.Parse(lines[13],   CultureInfo.InvariantCulture);
            double W_lon    = double.Parse(lines[25],   CultureInfo.InvariantCulture);
            double W_Vrt    = double.Parse(lines[27],   CultureInfo.InvariantCulture);
            double W_lat    = double.Parse(lines[26],   CultureInfo.InvariantCulture);
            double Time     = double.Parse(lines[0],    CultureInfo.InvariantCulture);

            Hdng    *= GlobalVars.Rad2Deg;
            Bank    *= -GlobalVars.Rad2Deg;    //direction reversal by minus 1!
            Ptch    *= GlobalVars.Rad2Deg;
            W_lon   *= GlobalVars.Rad2Deg;
            W_Vrt   *= GlobalVars.Rad2Deg;
            W_lat   *= GlobalVars.Rad2Deg;
            Time    *= 60 * 60 * 1000;

            //calculations here:
            counter++;
            deltatime = Time - prev_time;
            prev_time = Time;

            //Put string together:
            string result = lines[1] + ", " +                                           //IAS
                            "0" + ", " +                                                //Mach
                            "0" + ", " +                                                //TAS
                            "0" + ", " +                                                //GS
                            "0" + ", " +                                                //AOA
                            lines[24] + ", " +                                          //VS
                            "0" + ", " +                                                //HGT

                            Bank.ToString(GlobalVars.myNumberFormat(7)) + ", " +        //BANK
                            Hdng.ToString(GlobalVars.myNumberFormat(7)) + ", " +        //HDG
                            Ptch.ToString(GlobalVars.myNumberFormat(7)) + ", " +        //PITCH

                            W_lon.ToString(GlobalVars.myNumberFormat(7)) + ", " +       //W_lon
                            W_Vrt.ToString(GlobalVars.myNumberFormat(7)) + ", " +       //W_vert
                            W_lat.ToString(GlobalVars.myNumberFormat(7)) + ", " +       //W_lat

                            "0" + ", " +                                                //W_lon_dot
                            "0" + ", " +                                                //W_vert_dot
                            "0" + ", " +                                                //W_lat_dot

                            "0" + ", " +                                                //A_lon
                            "0" + ", " +                                                //A_vert
                            "0" + ", " +                                                //A_lat

                            Time.ToString(GlobalVars.myNumberFormat(7)) + ", " +        //Time [ms]
                            deltatime.ToString(GlobalVars.myNumberFormat(7)) + ", " +   //DeltaTime
                            counter + ", " +                                            //Counter

                            "Condor2"                                                   //Sim
                            ;

            return result;
        }
        private bool IsDCSFormat(string rawstring)
        {
            return rawstring.Contains("DCS");
        }
        private bool IsFS2020Format(string rawstring)
        {
            return rawstring.Contains("FS2020");
        }
        private bool IsXPlaneFormat(string rawstring)
        {
            return rawstring.Contains("X-Plane");
        }
        private bool IsiRacingFormat(string rawstring)
        {
            return rawstring.Contains("iRacing");
        }
        private bool IsCondorFormat(string rawstring)
        {
            return rawstring.Contains("surfaceroughness");
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
