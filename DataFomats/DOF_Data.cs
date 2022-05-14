using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAME.Model;

namespace YAME.DataFomats
{
    public class DOF_Data : MyObject
    {
        //HFC:
        public float HFC_Surge  { get; set; }
        public float HFC_Heave  { get; set; }
        public float HFC_Sway   { get; set; }

        public float HFC_Yaw    { get; set; }
        public float HFC_Pitch  { get; set; }
        public float HFC_Roll   { get; set; }

        //LFC
        public float LFC_Pitch  { get; set; }
        public float LFC_Roll   { get; set; }

        //Constructor
        public DOF_Data(DOF_Data dof_data)
        {
            //HFC
            HFC_Surge = dof_data.HFC_Surge;
            HFC_Heave = dof_data.HFC_Heave;
            HFC_Sway = dof_data.HFC_Sway;
            HFC_Yaw = dof_data.HFC_Yaw;
            HFC_Pitch = dof_data.HFC_Pitch;
            HFC_Roll = dof_data.HFC_Roll;

            //LFC
            LFC_Pitch = dof_data.LFC_Pitch;
            LFC_Roll = dof_data.LFC_Roll;
        }
        public DOF_Data(float surge = 0, float heave = 0, float sway = 0, float yaw = 0, float pitch = 0, float roll = 0, float pitch_lfc = 0, float roll_lfc = 0)
        {
            //HFC
            HFC_Surge   = surge;
            HFC_Heave   = heave;
            HFC_Sway    = sway;
            HFC_Yaw     = yaw;
            HFC_Pitch   = pitch;
            HFC_Roll    = roll;

            //LFC
            LFC_Pitch   = pitch_lfc;
            LFC_Roll    = roll_lfc;
        }


        public static DOF_Data operator -(DOF_Data d1)
        {
            DOF_Data temp = new DOF_Data();

            temp.HFC_Surge = d1.HFC_Surge   * -1.0f;
            temp.HFC_Heave  = d1.HFC_Heave  * -1.0f;
            temp.HFC_Sway   = d1.HFC_Sway   * -1.0f;
            temp.HFC_Yaw    = d1.HFC_Yaw    * -1.0f;
            temp.HFC_Pitch  = d1.HFC_Pitch  * -1.0f;
            temp.HFC_Roll   = d1.HFC_Roll   * -1.0f;
            temp.LFC_Pitch  = d1.LFC_Pitch  * -1.0f;
            temp.LFC_Roll   = d1.LFC_Roll   * -1.0f;

            return temp;
        }
        public static DOF_Data operator -(DOF_Data d1, DOF_Data d2)
        {
            DOF_Data temp = new DOF_Data();

            temp.HFC_Surge  = d1.HFC_Surge  -d1.HFC_Surge ;
            temp.HFC_Heave  = d1.HFC_Heave  -d1.HFC_Heave ;
            temp.HFC_Sway   = d1.HFC_Sway   -d1.HFC_Sway ;
            temp.HFC_Yaw    = d1.HFC_Yaw    -d1.HFC_Yaw ;
            temp.HFC_Pitch  = d1.HFC_Pitch  -d1.HFC_Pitch ;
            temp.HFC_Roll   = d1.HFC_Roll   -d1.HFC_Roll ;
            temp.LFC_Pitch  = d1.LFC_Pitch  -d1.LFC_Pitch ;
            temp.LFC_Roll   = d1.LFC_Roll   -d1.LFC_Roll ;

            return temp;
        }
        public static DOF_Data operator +(DOF_Data d1, DOF_Data d2)
        {
            DOF_Data temp = new DOF_Data();

            temp.HFC_Surge  = d1.HFC_Surge  + d2.HFC_Surge ;
            temp.HFC_Heave  = d1.HFC_Heave  + d2.HFC_Heave ;
            temp.HFC_Sway   = d1.HFC_Sway   + d2.HFC_Sway ;
            temp.HFC_Yaw    = d1.HFC_Yaw    + d2.HFC_Yaw ;
            temp.HFC_Pitch  = d1.HFC_Pitch  + d2.HFC_Pitch ;
            temp.HFC_Roll   = d1.HFC_Roll   + d2.HFC_Roll ;
            temp.LFC_Pitch  = d1.LFC_Pitch  + d2.LFC_Pitch ;
            temp.LFC_Roll   = d1.LFC_Roll   + d2.LFC_Roll ;

            return temp;
        }
        public static DOF_Data operator *(DOF_Data d1, float f)
        {
            DOF_Data temp = new DOF_Data();

            temp.HFC_Surge  = d1.HFC_Surge * f; 
            temp.HFC_Heave  = d1.HFC_Heave * f; 
            temp.HFC_Sway   = d1.HFC_Sway * f; 
            temp.HFC_Yaw    = d1.HFC_Yaw * f; 
            temp.HFC_Pitch  = d1.HFC_Pitch * f; 
            temp.HFC_Roll   = d1.HFC_Roll * f; 
            temp.LFC_Pitch  = d1.LFC_Pitch * f; 
            temp.LFC_Roll   = d1.LFC_Roll * f;

            return temp;
        }
        public static DOF_Data operator *(float f, DOF_Data d1)
        {
            return d1 * f;
        }

        public float report(DOF dof)
        {
            switch (dof)
            {
                case DOF.surge:
                    return HFC_Surge;
                case DOF.heave:
                    return HFC_Heave;
                case DOF.sway:
                    return HFC_Sway;
                case DOF.yaw:
                    return HFC_Yaw;
                case DOF.pitch:
                    return HFC_Pitch;
                case DOF.roll:
                    return HFC_Roll;
                case DOF.pitch_lfc:
                    return LFC_Pitch;
                case DOF.roll_lfc:
                    return LFC_Roll;
                default:
                    throw new Exception($"Unknown DOF: {dof}");
            }
        }
        public void SetZero()
        {
            //HFC
            HFC_Surge   = 0;
            HFC_Heave   = 0;
            HFC_Sway    = 0;
            HFC_Yaw     = 0;
            HFC_Pitch   = 0;
            HFC_Roll    = 0;
                        
            //LFC        
            LFC_Pitch   = 0;
            LFC_Roll    = 0;
        }
    }
}
