using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class Inverter
    {
        public PreprocessorData Output = new PreprocessorData();

        public bool Invert_Ax { get; set; }
        public bool Invert_Ay { get; set; }
        public bool Invert_Az { get; set; }
        public bool Invert_Wx { get; set; }
        public bool Invert_Wy { get; set; }
        public bool Invert_Wz { get; set; }

        public void InvertDataAsNeeded(PreprocessorData data)
        {
            Output = data;
            //And then modify some
            if (Invert_Ax) { Output.AX = data.AX * -1.0f; }
            if (Invert_Ay) { Output.AY = data.AY * -1.0f; }
            if (Invert_Az) { Output.AZ = data.AZ * -1.0f; }

            if (Invert_Wx) { Output.WX = data.WX * -1.0f; }
            if (Invert_Wy) { Output.WY = data.WY * -1.0f; }
            if (Invert_Wz) { Output.WZ = data.WZ * -1.0f; }
        }
    }
}
