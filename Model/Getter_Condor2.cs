using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Matrix4x4 = System.Numerics.Matrix4x4;
using Vector3D = System.Windows.Media.Media3D.Vector3D;
using System.Globalization;

namespace YAME.Model
{
    public class Getter_Condor2
    {
        public string RawDatastring;
        string prev_RawString;

        double vx;
        double vy;
        double vz;

        double prev_vx;
        double prev_vy;
        double prev_vz;

        double ax;
        double ay;
        double az;

        double ax_LP1;
        double ay_LP1;
        double az_LP1;

        double ax_LP2;
        double ay_LP2;
        double az_LP2;

        double ax_LP3;
        double ay_LP3;
        double az_LP3;

        const double FilterVar = 3;

        int counter;
        double deltatime;
        double prev_time;

        public void Update(string rawString)
        {
            if (rawString != prev_RawString)
            {
                RawDatastring = ConvertFromCondor(rawString);
                prev_RawString = rawString;
            }
            
        }

        //Helpers:
        public string ConvertFromCondor(string CondorRawString)
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

            //Choose wisely :-)
            //double[] Axyz_prop = CalculateProperAccelerations_FromAccels(data);
            double[] Axyz_prop = CalculateProperAccelerations_FromVelocities(data);

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
        Dictionary<string, double> CreateDictionaryFromCondor(string CondorRawString)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();

            string[] lines = CondorRawString.Replace("\r", "").Split('\n');

            foreach (string line in lines)
            {
                if (line == "")                     continue;       //ignore
                if (line.StartsWith("hudmessages")) continue;       //ignore

                string[] tokens = line.Split('=');

                if (tokens[1] == "NAN")
                {
                    tokens[1] = "0";
                }

                string key = tokens[0];
                double value = double.Parse(tokens[1], CultureInfo.InvariantCulture);

                result.Add(key, value);
            }

            return result;
        }
        private double[] CalculateProperAccelerations_FromVelocities(Dictionary<string, double> data)
        {
            /* 
            Condor2 provides velocities in the world coordinate system (vx,vy,vz).
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

            vx = data["vx"];
            vy = data["vy"];
            vz = data["vz"];

            
            if (deltatime != 0)
            {       
                ax = 1000 * (vx - prev_vx) / deltatime;             //Factor 1000 to convert units/ms into units/s
                ay = 1000 * (vy - prev_vy) / deltatime;             //Factor 1000 to convert units/ms into units/s
                az = 1000 * (vz - prev_vz) / deltatime;             //Factor 1000 to convert units/ms into units/s
            }

            prev_vx = vx;
            prev_vy = vy;
            prev_vz = vz;

            //Create a quaternion...
            Quaternion q = new Quaternion(  (float)data["quaternionx"],
                                            (float)data["quaterniony"],
                                            (float)data["quaternionz"],
                                            (float)data["quaternionw"]  );

            //...and convert it into Matrix representation
            Matrix4x4 m = Matrix4x4.CreateFromQuaternion(q);

            //Create all relevant vectors
            Vector3D Vec_Accel_world_No_Gravity = new Vector3D(ax, ay, az);
            Vector3D Vec_Gravity                = new Vector3D(0, 0, 9.806f);
            Vector3D Vec_nose_unit              = new Vector3D(m.M11, m.M21, m.M31);
            Vector3D Vec_rightwing_unit         = new Vector3D(m.M12, m.M22, m.M32);
            Vector3D Vec_head_unit              = new Vector3D(m.M13, m.M23, m.M33);

            //Add gravity
            Vector3D Vec_Accel_World_With_Gravity = Vec_Accel_world_No_Gravity + Vec_Gravity;

            //Calculate component along vehicle axises using DotProduct().
            double Alon = Vector3D.DotProduct(Vec_Accel_World_With_Gravity, Vec_nose_unit);
            double Alat = Vector3D.DotProduct(Vec_Accel_World_With_Gravity, Vec_rightwing_unit);
            double Avrt = Vector3D.DotProduct(Vec_Accel_World_With_Gravity, Vec_head_unit);

            return new double[] { Alon, Alat, Avrt };
        }
        private double[] CalculateProperAccelerations_FromAccels(Dictionary<string, double> data)
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

            //smoothen out jittery data
            ax = data["ax"];
            ay = data["ay"];
            az = data["az"];

            ax_LP1 += (ax - ax_LP1) / FilterVar;
            ay_LP1 += (ay - ay_LP1) / FilterVar;
            az_LP1 += (az - az_LP1) / FilterVar;

            ax_LP2 += (ax_LP1 - ax_LP2) / FilterVar;
            ay_LP2 += (ay_LP1 - ay_LP2) / FilterVar;
            az_LP2 += (az_LP1 - az_LP2) / FilterVar;

            ax_LP3 += (ax_LP2 - ax_LP3) / FilterVar;
            ay_LP3 += (ay_LP2 - ay_LP3) / FilterVar;
            az_LP3 += (az_LP2 - az_LP3) / FilterVar;

            //Create a quaternion...
            Quaternion q = new Quaternion(  (float)data["quaternionx"],
                                            (float)data["quaterniony"],
                                            (float)data["quaternionz"],
                                            (float)data["quaternionw"]      );

            //...and convert it into Matrix representation
            Matrix4x4 m = Matrix4x4.CreateFromQuaternion(q);

            //Create all relevant vectors
            Vector3D Vec_Accel_world_No_Gravity = new Vector3D(ax, ay, az);
            Vector3D Vec_Gravity = new Vector3D(0, 0, 9.806f);
            Vector3D Vec_nose_unit = new Vector3D(m.M11, m.M21, m.M31);
            Vector3D Vec_rightwing_unit = new Vector3D(m.M12, m.M22, m.M32);
            Vector3D Vec_head_unit = new Vector3D(m.M13, m.M23, m.M33);

            //Add gravity
            Vector3D Vec_Accel_World_With_Gravity = Vec_Accel_world_No_Gravity + Vec_Gravity;

            //Calculate component along vehicle axises using DotProduct().
            double Alon = Vector3D.DotProduct(Vec_Accel_World_With_Gravity, Vec_nose_unit);
            double Alat = Vector3D.DotProduct(Vec_Accel_World_With_Gravity, Vec_rightwing_unit);
            double Avrt = Vector3D.DotProduct(Vec_Accel_World_With_Gravity, Vec_head_unit);

            return new double[] { Alon, Alat, Avrt };
        }
    }
}
