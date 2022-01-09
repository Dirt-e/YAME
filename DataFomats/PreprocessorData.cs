using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.DataFomats
{
    public class PreprocessorData
    {
        //Airdata
        public float IAS { get; set; }
        public float MACH { get; set; }
        public float TAS { get; set; }
        public float GS { get; set; }
        public float AOA { get; set; }
        public float VS { get; set; }
        public float HGT { get; set; }
        //Euler
        public float HDG { get; set; }
        public float PITCH { get; set; }
        public float BANK { get; set; }
        //Rates
        public float WX { get; set; }
        public float WY { get; set; }
        public float WZ { get; set; }
        //Acccels
        public float AX { get; set; }
        public float AY { get; set; }
        public float AZ { get; set; }
        //Meta
        public float TIME { get; set; }
        public float COUNTER { get; set; }
        public string SIM { get; set; }

        //Constructor
        public PreprocessorData() { }
        public PreprocessorData(PreprocessorData p)
        {
            //AirData
            IAS = p.IAS;
            MACH = p.MACH;
            TAS = p.TAS;
            GS = p.GS;
            AOA = p.AOA;
            VS = p.VS;
            HGT = p.HGT;
            //Euler
            HDG = p.HDG;
            PITCH = p.PITCH;
            BANK = p.BANK;
            //Rates
            WX = p.WX;
            WY = p.WY;
            WZ = p.WZ;
            //Accels
            AX = p.AX;
            AY = p.AY;
            AZ = p.AZ;
            //Meta
            TIME = p.TIME;
            COUNTER = p.COUNTER;
            SIM = p.SIM;
        }
    }
}
