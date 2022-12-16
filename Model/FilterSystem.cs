using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class FilterSystem
    {
        public FilterData Input = new FilterData();
        public DOF_Data Output = new DOF_Data();

        //Defaults:
        float ax_default = 0;
        float ay_default = 9.81f;
        float az_default = 0;

        float wx_default = 0;
        float wy_default = 0;
        float wz_default = 0;

        //Filters omegas:
        public HighPassNthOrder Wx_HP = new HighPassNthOrder(1);
            public LowPassNthOrder Wx_HP_LP = new LowPassNthOrder(1);
        public HighPassNthOrder Wy_HP = new HighPassNthOrder(1);
            public LowPassNthOrder Wy_HP_LP = new LowPassNthOrder(1);
        public HighPassNthOrder Wz_HP = new HighPassNthOrder(1);
            public LowPassNthOrder Wz_HP_LP = new LowPassNthOrder(1);

        //Filters accels:
        public HighPassNthOrder Ax_HP = new HighPassNthOrder(1);
            public LowPassNthOrder Ax_HP_LP2 = new LowPassNthOrder(2);
        public LowPassNthOrder Ax_LP3 = new LowPassNthOrder(3);

        public HighPassNthOrder Ay_HP = new HighPassNthOrder(1);
            public LowPassNthOrder Ay_HP_LP2 = new LowPassNthOrder(2);

        public HighPassNthOrder Az_HP = new HighPassNthOrder(1);
            public LowPassNthOrder Az_HP_LP2 = new LowPassNthOrder(2);
        public LowPassNthOrder Az_LP3 = new LowPassNthOrder(3);

        public FilterSystem()
        {
            Wx_HP.UpdateCode();
            Wx_HP_LP.UpdateCode();
            Wy_HP.UpdateCode();
            Wy_HP_LP.UpdateCode();
            Wz_HP.UpdateCode();
            Wz_HP_LP.UpdateCode();

            Ax_HP.UpdateCode();
            Ax_HP_LP2.UpdateCode();
            Ax_LP3.UpdateCode();

            Ay_HP.UpdateCode();
            Ay_HP_LP2.UpdateCode();

            Az_HP.UpdateCode();
            Az_HP_LP2.UpdateCode();
            Az_LP3.UpdateCode();

            ResetToDefaults();
        }

        public void Process(FilterData data)
        {
            Input = data;
            DriveFilters();
            WriteOutputData();
        }

        private void DriveFilters()
        {
            Wx_HP.Push(Input.WX);
                Wx_HP_LP.Push(Wx_HP.OutValue);
            Wy_HP.Push(Input.WY);
                Wy_HP_LP.Push(Wy_HP.OutValue);
            Wz_HP.Push(Input.WZ);
                Wz_HP_LP.Push(Wz_HP.OutValue);

            Ax_HP.Push(Input.AX_alphacomp);
                Ax_HP_LP2.Push(Ax_HP.OutValue);
            Ax_LP3.Push(Input.AX);

            Ay_HP.Push(Input.AY);
                Ay_HP_LP2.Push(Ay_HP.OutValue);

            Az_HP.Push(Input.AZ);
                Az_HP_LP2.Push(Az_HP.OutValue);
            Az_LP3.Push(Input.AZ);
        }
        private void WriteOutputData()
        {
            Output.HFC_Roll     = Wx_HP_LP.OutValue;
            Output.HFC_Yaw      = Wy_HP_LP.OutValue;
            Output.HFC_Pitch    = Wz_HP_LP.OutValue;

            Output.HFC_Surge    = Ax_HP_LP2.OutValue;
            Output.HFC_Heave    = Ay_HP_LP2.OutValue;
            Output.HFC_Sway     = Az_HP_LP2.OutValue;

            Output.LFC_Pitch    = Ax_LP3.OutValue;
            Output.LFC_Roll     = Az_LP3.OutValue;
        }

        //Convienience functions:
        void ResetToDefaults()
        {
            //This functions sets all Filters to the default values. It also makes sure that all filters are in a balanced state.
            Ax_HP.Set(ax_default);
            Ax_HP_LP2.Set(Ax_HP.OutValue);
            Ax_LP3.Set(ax_default);

            Ay_HP.Set(ay_default);
            Ay_HP_LP2.Set(Ay_HP.OutValue);

            Az_HP.Set(az_default);
            Az_HP_LP2.Set(Az_HP.OutValue);
            Az_LP3.Set(az_default);

            Wx_HP.Set(wx_default);
            Wx_HP_LP.Set(Wx_HP.OutValue);

            Wy_HP.Set(wy_default);
            Wy_HP_LP.Set(Wz_HP.OutValue);

            Wz_HP.Set(wz_default);
            Wz_HP_LP.Set(Wz_HP.OutValue);
        }
    }
}
