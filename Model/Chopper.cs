using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class Chopper
    {
        public PreprocessorData Output= new PreprocessorData();
        public string Simulator { get; set; }
        
        private string[] Chunks = new string[18];
        private float[] Floats = new float[18];

        public void ChopParseAndPackage(string rawdatastring)
        {
            ChopStringIntoChunks(rawdatastring);
            ParseChunksIntoFloats();
            PackFloatsIntoData();
        }

        private void ChopStringIntoChunks( string s)
        {
            Chunks = s.Split(',');
            Simulator = Chunks[18];
            Chunks = Chunks.Take(18).ToArray();     //We don't need the last element anymore.
        }
        private void ParseChunksIntoFloats()
        {
            for (int i = 0; i < Chunks.Length; i++)
            {
                Floats[i] = Convert.ToSingle(Chunks[i], GlobalVars.myNumberFormat(7));
            }
        }
        private void PackFloatsIntoData()
        {
            //Airdata
            Output.IAS      = Floats[0];
            Output.MACH     = Floats[1];
            Output.TAS      = Floats[2];
            Output.GS       = Floats[3];
            Output.AOA      = Floats[4];
            Output.VS       = Floats[5];
            Output.HGT      = Floats[6];
            //Euler
            Output.BANK     = Floats[7];
            Output.HDG      = Floats[8];
            Output.PITCH    = Floats[9];

            //Rates
            Output.WX       = Floats[10];
            Output.WY       = Floats[11];
            Output.WZ       = Floats[12];
            //Accels
            Output.AX       = Floats[13];
            Output.AY       = Floats[14];
            Output.AZ       = Floats[15];
            //Meta
            Output.TIME     = Floats[16];
            Output.COUNTER  = Floats[17];
        }
    }

}
