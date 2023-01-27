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
                        RawDatastring = udp_Server.RawString;
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
                RawDatastring = Properties.Settings.Default.Getter_DefaulDataString;
                DataFlowing = false;
            }
        }

        //Helpers:
        string ConvertFromCondor(string CondorRawString)
        {
            //This function takes the raw string from Condor2 and returns the
            //rawDataString the Chopper can process

            Dictionary<string, double> data = CreateDictionaryFromCondor(CondorRawString);

            //Conversions here:
            if (data.ContainsKey("yaw")) data["yaw"] *= GlobalVars.Rad2Deg;
            if (data.ContainsKey("pitch")) data["pitch"] *= GlobalVars.Rad2Deg;
            if (data.ContainsKey("bank")) data["bank"] *= -GlobalVars.Rad2Deg;     //direction reversal by minus 1!
            if (data.ContainsKey("rollrate")) data["rollrate"] *= GlobalVars.Rad2Deg;
            if (data.ContainsKey("pitchrate")) data["pitchrate"] *= GlobalVars.Rad2Deg;
            if (data.ContainsKey("yawrate")) data["yawrate"] *= GlobalVars.Rad2Deg;
            if (data.ContainsKey("time")) data["time"] *= 60 * 60 * 1000;          //to convert from hrs to milliseconds

            double[] A_xyz_prop = CalculateProperAccelerations(data);

            //calculations here:
            counter++;
            deltatime = data["time"] - prev_time;
            prev_time = data["time"];
            double GS = Math.Sqrt(data["vx"] * data["vx"] + data["vy"] * data["vy"]);

            //Put string together:
            string result = data["airspeed"].ToString(GlobalVars.myNumberFormat(7)) + ", " +        //IAS
                            "0" + ", " +                                                            //Mach
                            "0" + ", " +                                                            //TAS
                            GS.ToString(GlobalVars.myNumberFormat(7)) + ", " +                      //GS
                            "0" + ", " +                                                            //AOA
                            data["vz"].ToString(GlobalVars.myNumberFormat(7)) + ", " +              //VS
                            data["wheelheight"].ToString(GlobalVars.myNumberFormat(7)) + ", " +     //HGT

                            data["bank"].ToString(GlobalVars.myNumberFormat(7)) + ", " +            //BANK
                            data["yaw"].ToString(GlobalVars.myNumberFormat(7)) + ", " +             //HDG
                            data["pitch"].ToString(GlobalVars.myNumberFormat(7)) + ", " +           //PITCH

                            data["rollrate"].ToString(GlobalVars.myNumberFormat(7)) + ", " +        //W_lon
                            data["yawrate"].ToString(GlobalVars.myNumberFormat(7)) + ", " +         //W_vert
                            data["pitchrate"].ToString(GlobalVars.myNumberFormat(7)) + ", " +       //W_lat

                            "0" + ", " +                                                            //W_lon_dot
                            "0" + ", " +                                                            //W_vert_dot
                            "0" + ", " +                                                            //W_lat_dot

                            "0" + ", " +                                                            //A_lon
                            "0" + ", " +                                                            //A_vert
                            "0" + ", " +                                                            //A_lat

                            data["time"].ToString(GlobalVars.myNumberFormat(7)) + ", " +            //Time [ms]
                            deltatime.ToString(GlobalVars.myNumberFormat(7)) + ", " +               //DeltaTime
                            counter + ", " +                                                        //Counter

                            "Condor2"                                                               //Sim
                            ;

            return result;
        }

        private double[] CalculateProperAccelerations(Dictionary<string, double> data)
        {
            double[] result = new double[3];

            double ax = data["ax"];
            double ay = data["ay"];
            double az = data["az"];

            double hdg = data["yaw"];
            double ptc = data["pitch"];
            double bnk = data["bank"];

            //do some magic here!!!

            //do some more magic here!!!

            return result;
        }

        Dictionary<string, double> CreateDictionaryFromCondor(string CondorRawString)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();

            string[] lines = CondorRawString.Replace("\r","").Split('\n');

            foreach (string line in lines)
            {
                if (line == "")                         continue;       //ignore
                if (line.StartsWith("hudmessages"))     continue;       //ignore

                string[] tokens = line.Split('=');

                if (tokens[1] == "NAN")
                {
                    tokens[1] = "0";
                    Console.WriteLine("Alarm");
                    SystemSounds.Exclamation.Play();
                }
                
                string key = tokens[0];
                double value = double.Parse(tokens[1], CultureInfo.InvariantCulture);
                
                result.Add(key,value);
            }

            return result;
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
