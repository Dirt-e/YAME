using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.DataFomats
{
    public class FilterData
    {
        //Omegas:
        public float WX { get; set; }
        public float WY { get; set; }
        public float WZ { get; set; }

        //Accels
        public float AX { get; set; }
        public float AY { get; set; }
        public float AZ { get; set; }

        //Alpha compensated
        public float AX_alphacomp { get; set; }

        //Constructor
        public FilterData() { }
        public FilterData(FilterData fid)
        {
            //HFC
            AX = fid.AX;
            AY = fid.AY;
            AZ = fid.AZ;

            WX = fid.WX;
            WY = fid.WY;
            WZ = fid.WZ;

            //AlphaCompensated:
            AX_alphacomp = fid.AX_alphacomp;
        }

        public override string ToString()
        {
            return AX.ToString() + " " +
                    AY.ToString() + " " +
                    AZ.ToString() + " " +
                    WX.ToString() + " " +
                    WY.ToString() + " " +
                    WZ.ToString() + " " +
                    AX_alphacomp.ToString();
        }
    }
}
