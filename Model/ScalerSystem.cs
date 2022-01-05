using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class ScalerSystem
    {
        public DOF_Data Input = new DOF_Data();
        public DOF_Data Output = new DOF_Data();

        //HFC:
        public ScalerModule SCL_Surge_HFC = new ScalerModule();
        public ScalerModule SCL_Heave_HFC = new ScalerModule();
        public ScalerModule SCL_Sway_HFC = new ScalerModule();

        public ScalerModule SCL_Yaw_HFC = new ScalerModule();
        public ScalerModule SCL_Pitch_HFC = new ScalerModule();
        public ScalerModule SCL_Roll_HFC = new ScalerModule();

        //LFC
        public ScalerModule SCL_Pitch_LFC = new ScalerModule();
        public ScalerModule SCL_Roll_LFC = new ScalerModule();

        public void Process(DOF_Data data)
        {
            Input = new DOF_Data(data);
            DriveScalerModules();
            WriteOutputData();
        }

        void DriveScalerModules()
        {
            SCL_Surge_HFC.Push(Input.HFC_Surge);
            SCL_Heave_HFC.Push(Input.HFC_Heave);
            SCL_Sway_HFC.Push(Input.HFC_Sway);

            SCL_Yaw_HFC.Push(Input.HFC_Yaw);
            SCL_Pitch_HFC.Push(Input.HFC_Pitch);
            SCL_Roll_HFC.Push(Input.HFC_Roll);

            SCL_Pitch_LFC.Push(Input.LFC_Pitch);
            SCL_Roll_LFC.Push(Input.LFC_Roll);
        }
        private void WriteOutputData()
        {
            Output.HFC_Surge = SCL_Surge_HFC.Output;
            Output.HFC_Heave = SCL_Heave_HFC.Output;
            Output.HFC_Sway = SCL_Sway_HFC.Output;

            Output.HFC_Yaw = SCL_Yaw_HFC.Output;
            Output.HFC_Pitch = SCL_Pitch_HFC.Output;
            Output.HFC_Roll = SCL_Roll_HFC.Output;

            Output.LFC_Pitch = SCL_Pitch_LFC.Output;
            Output.LFC_Roll = SCL_Roll_LFC.Output;
        }
    }
}
