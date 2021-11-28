using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.DataFomats
{
    class SaveObject
    {
        public float Crashdetector_Trigger_Ax { get; set; } 
        public float Crashdetector_Trigger_Ay { get; set; } 
        public float Crashdetector_Trigger_Az { get; set; }

        public float    PositionOffsetCorrector_Delta_X { get; set; }
        public float    PositionOffsetCorrector_Delta_Y { get; set; }
        public float    PositionOffsetCorrector_Delta_Z { get; set; }
        public bool     PositionOffsetCorrector_IsActive { get; set; }

        public float AlphaCompensator_AOA_Zero { get; set; }    
        public float AlphaCompensator_FadeIn_Start { get; set; }    
        public float AlphaCompensator_FadeIn_Done { get; set; }    
        public float AlphaCompensator_CompensationPercentage { get; set; }    


    }
}