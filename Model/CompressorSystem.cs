using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class CompressorSystem
    {
        public DOF_Data Input = new DOF_Data();
        public DOF_Data Output = new DOF_Data();

        //HFC
        public CompressorModule CMP_Yaw_HFC = new CompressorModule();
        public CompressorModule CMP_Pitch_HFC = new CompressorModule();
        public CompressorModule CMP_Roll_HFC = new CompressorModule();

        public CompressorModule CMP_Surge_HFC = new CompressorModule();
        public CompressorModule CMP_Heave_HFC = new CompressorModule();
        public CompressorModule CMP_Sway_HFC = new CompressorModule();

        //LFC
        public CompressorModule CMP_Pitch_LFC = new CompressorModule();
        public CompressorModule CMP_Roll_LFC = new CompressorModule();

        public void Process(DOF_Data data)
        {
            Input = new DOF_Data(data);
            DriveCompressors();
            WriteOutputData();
        }

        private void DriveCompressors()
        {
            CMP_Surge_HFC.Push(Input.HFC_Surge);
            CMP_Heave_HFC.Push(Input.HFC_Heave);
            CMP_Sway_HFC.Push(Input.HFC_Sway);

            CMP_Yaw_HFC.Push(Input.HFC_Yaw);
            CMP_Pitch_HFC.Push(Input.HFC_Pitch);
            CMP_Roll_HFC.Push(Input.HFC_Roll);

            CMP_Pitch_LFC.Push(Input.LFC_Pitch);
            CMP_Roll_LFC.Push(Input.LFC_Roll);
        }
        private void WriteOutputData()
        {
            //HFC:
            Output.HFC_Surge = CMP_Surge_HFC.Output;
            Output.HFC_Heave = CMP_Heave_HFC.Output;
            Output.HFC_Sway = CMP_Sway_HFC.Output;

            Output.HFC_Yaw = CMP_Yaw_HFC.Output;
            Output.HFC_Pitch = CMP_Pitch_HFC.Output;
            Output.HFC_Roll = CMP_Roll_HFC.Output;

            //LFC:
            Output.LFC_Pitch = CMP_Pitch_LFC.Output;
            Output.LFC_Roll = CMP_Roll_LFC.Output;
        }
    }
}
