using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Numerics;
using Matrix4x4 = System.Numerics.Matrix4x4;
using Vector3D = System.Windows.Media.Media3D.Vector3D;

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
            if (data.ContainsKey("yaw"))        data["yaw"]         *= GlobalVars.Rad2Deg;
            if (data.ContainsKey("pitch"))      data["pitch"]       *= GlobalVars.Rad2Deg;
            if (data.ContainsKey("bank"))       data["bank"]        *= -GlobalVars.Rad2Deg;     //direction reversal by minus 1!
            if (data.ContainsKey("rollrate"))   data["rollrate"]    *= GlobalVars.Rad2Deg;
            if (data.ContainsKey("pitchrate"))  data["pitchrate"]   *= GlobalVars.Rad2Deg;
            if (data.ContainsKey("yawrate"))    data["yawrate"]     *= GlobalVars.Rad2Deg;
            if (data.ContainsKey("time"))       data["time"]        *= 60 * 60 * 1000;          //to convert from hrs to milliseconds

            //calculations here:
            counter++;
            deltatime = data["time"] - prev_time;
            prev_time = data["time"];
            double[] Axyz_prop = CalculateProperAccelerations_Condor2(data);
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

                            Axyz_prop[0].ToString(GlobalVars.myNumberFormat(7)) + ", " +            //A_lon
                            Axyz_prop[2].ToString(GlobalVars.myNumberFormat(7)) + ", " +            //A_vrt
                            Axyz_prop[1].ToString(GlobalVars.myNumberFormat(7)) + ", " +            //A_lat

                            data["time"].ToString(GlobalVars.myNumberFormat(7)) + ", " +            //Time [ms]
                            deltatime.ToString(GlobalVars.myNumberFormat(7)) + ", " +               //DeltaTime
                            counter + ", " +                                                        //Counter

                            "Condor2"                                                               //Sim
                            ;

            return result;
        }
        private double[] CalculateProperAccelerations_Condor2(Dictionary<string, double> data)
        {
            /* 
            Condor2 only provides coordinate accelerations in the world coordinate system (ax,ay,az).
            This function calculates the PROPER(!) accelerations in the vehicle reference system.
            
            World coordinate system:
            X = West
            Y = North
            Z = Up

            Vehicle reference system:
            X = Forward
            Y = Right wing
            Z = Up

            */

            //Grab world accelerations
            float ax = (float)data["ax"];
            float ay = (float)data["ay"];
            float az = (float)data["az"];
            Vector3D Vec_Accel_world_No_Gravity = new Vector3D(ax, ay, az);
            
            //Grab quaternion
            float Qx = (float)data["quaternionx"];
            float Qy = (float)data["quaterniony"];
            float Qz = (float)data["quaternionz"];
            float Qw = (float)data["quaternionw"];
            Quaternion q = new Quaternion(Qx, Qy, Qz, Qw);
            
            //Convert it into Matrix representation
            Matrix4x4 m = Matrix4x4.CreateFromQuaternion(q);

            //Create all relevant vectors
            Vector3D Vec_Gravity        = new Vector3D(0, 0, 9.806f);
            Vector3D Vec_nose_unit      = new Vector3D(m.M11, m.M21, m.M31);
            Vector3D Vec_rightwing_unit = new Vector3D(m.M12, m.M22, m.M32);
            Vector3D Vec_head_unit      = new Vector3D(m.M13, m.M23, m.M33);

            //Add gravity
            Vector3D Vec_Accel_World_With_Gravity = Vec_Accel_world_No_Gravity + Vec_Gravity;

            //Calculate component along vehicle axises using DotProduct().
            double Alon = Vector3D.DotProduct(Vec_Accel_World_With_Gravity, Vec_nose_unit);
            double Alat = Vector3D.DotProduct(Vec_Accel_World_With_Gravity, Vec_rightwing_unit);
            double Avrt = Vector3D.DotProduct(Vec_Accel_World_With_Gravity, Vec_head_unit);
    
            return new double[] { Alon, Alat, Avrt };
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
