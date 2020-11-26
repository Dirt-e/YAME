using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.DataFomats
{
    public class DOF_Data
    {
        //HFC:
        public float HFC_Surge { get; set; }
        public float HFC_Heave { get; set; }
        public float HFC_Sway { get; set; }

        public float HFC_Yaw { get; set; }
        public float HFC_Pitch { get; set; }
        public float HFC_Roll { get; set; }

        //LFC
        public float LFC_Pitch { get; set; }
        public float LFC_Roll { get; set; }


        //Constructor
        public DOF_Data() { }
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
    }
}
