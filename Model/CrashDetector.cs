using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class CrashDetector
    {
        public PreprocessorData Output = new PreprocessorData();

        public bool IsCrashed { get; set; }

        public float Ax_Crashtrigger { get; set; }
        public float Ay_Crashtrigger { get; set; }
        public float Az_Crashtrigger { get; set; }
        public float Wx_Crashtrigger { get; set; }
        public float Wy_Crashtrigger { get; set; }
        public float Wz_Crashtrigger { get; set; }

        public void CheckForCrash(PreprocessorData data)
        {
            if (LimitsExceeded(data)) { IsCrashed = true;  }
            Output = data;                                          //We always pass the data on. This is only a DETECTOR!
        }

        private bool LimitsExceeded(PreprocessorData data)
        {
            if (Math.Abs(data.AX) >= Ax_Crashtrigger) { return true; }
            if (Math.Abs(data.AY) >= Ay_Crashtrigger) { return true; }
            if (Math.Abs(data.AZ) >= Az_Crashtrigger) { return true; }

            if (Math.Abs(data.WX) >= Wx_Crashtrigger) { return true; }
            if (Math.Abs(data.WY) >= Wy_Crashtrigger) { return true; }
            if (Math.Abs(data.WZ) >= Wz_Crashtrigger) { return true; }
            
            return false;
        }

    }
}
