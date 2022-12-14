using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace YAME.Model
{
    public class Inverter : MyObject
    {
        public PreprocessorData Output = new PreprocessorData();

        bool _invertWx;
        public bool InvertWx
        {
            get { return _invertWx; }
            set
            {
                _invertWx = value;
                lerp_Wx.Run(value);
                OnPropertyChanged("InvertWx");
            }
        }
        bool _invertWy;
        public bool InvertWy
        {
            get { return _invertWy; }
            set
            {
                _invertWy = value;
                lerp_Wy.Run(value);
                OnPropertyChanged("InvertWy");
            }
        }
        bool _invertWz;
        public bool InvertWz
        {
            get { return _invertWz; }
            set
            {
                _invertWz = value;
                lerp_Wz.Run(value);
                OnPropertyChanged("InvertWz");
            }
        }
        bool _invertAx;
        public bool InvertAx
        {
            get { return _invertAx; }
            set
            {
                _invertAx = value;
                lerp_Ax.Run(value);
                OnPropertyChanged("InvertAx");
            }
        }
        bool _invertAy;
        public bool InvertAy
        {
            get { return _invertAy; }
            set
            {
                _invertAy = value;
                lerp_Ay.Run(value);
                OnPropertyChanged("InvertAy");
            }
        }
        bool _invertAz;
        public bool InvertAz
        {
            get { return _invertAz; }
            set
            {
                _invertAz = value;
                lerp_Az.Run(value);
                OnPropertyChanged("InvertAz");
            }
        }

        Lerp lerp_Wx = new Lerp(2000,LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_Wy = new Lerp(2000,LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_Wz = new Lerp(2000,LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_Ax = new Lerp(2000, LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_Ay = new Lerp(2000, LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_Az = new Lerp(2000, LerpOverMethod.LowPass3rdOrder);

        public void InvertDataAsNeeded(PreprocessorData data)
        {
            Output = data;

            //And then modify some...
            lerp_Wx.Update();
            lerp_Wy.Update();
            lerp_Wz.Update();
            lerp_Ax.Update();
            lerp_Ay.Update();
            lerp_Az.Update();

            Output.WX *= (float)Utility.map(lerp_Wx.Ratio_external, 0, 1, 1, -1.0);
            Output.WY *= (float)Utility.map(lerp_Wy.Ratio_external, 0, 1, 1, -1.0);
            Output.WZ *= (float)Utility.map(lerp_Wz.Ratio_external, 0, 1, 1, -1.0);
            Output.AX *= (float)Utility.map(lerp_Ax.Ratio_external, 0, 1, 1, -1.0);
            Output.AY *= (float)Utility.map(lerp_Ay.Ratio_external, 0, 1, 1, -1.0);
            Output.AZ *= (float)Utility.map(lerp_Az.Ratio_external, 0, 1, 1, -1.0);
        }
    }
}
