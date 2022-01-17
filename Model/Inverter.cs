using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class Inverter : MyObject
    {
        public PreprocessorData Output = new PreprocessorData();

        bool _invert_wx;
        public bool InvertWx
        {
            get { return _invert_wx; }
            set { _invert_wx = value; OnPropertyChanged(nameof(InvertWx)); }
        }
        bool _invert_wy;
        public bool InvertWy
        {
            get { return _invert_wy; }
            set { _invert_wy = value; OnPropertyChanged(nameof(InvertWy)); }
        }
        bool _invert_wz;
        public bool InvertWz
        {
            get { return _invert_wz; }
            set { _invert_wz = value; OnPropertyChanged(nameof(InvertWz)); }
        }
        bool _invert_ax;
        public bool InvertAx
        {
            get { return _invert_ax; }
            set { _invert_ax = value; OnPropertyChanged(nameof(InvertAx)); }
        }
        bool _invert_ay;
        public bool InvertAy
        {
            get { return _invert_ay; }
            set { _invert_ay = value; OnPropertyChanged(nameof(InvertAy)); }
        }
        bool _invert_az;
        public bool InvertAz
        {
            get { return _invert_az; }
            set { _invert_az = value; OnPropertyChanged(nameof(InvertAz)); }
        }

        public void InvertDataAsNeeded(PreprocessorData data)
        {
            Output = data;
            //And then modify some
            if (InvertAx) { Output.AX = data.AX * -1.0f; }
            if (InvertAy) { Output.AY = data.AY * -1.0f; }
            if (InvertAz) { Output.AZ = data.AZ * -1.0f; }

            if (InvertWx) { Output.WX = data.WX * -1.0f; }
            if (InvertWy) { Output.WY = data.WY * -1.0f; }
            if (InvertWz) { Output.WZ = data.WZ * -1.0f; }
        }
    }
}
