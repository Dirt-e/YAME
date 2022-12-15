using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class ZeroMaker : MyObject
    {
        public DOF_Data Output = new DOF_Data();

        const int fadeOverTime = 2000;              //ms

        #region Viewmodel for tickmarks
        bool _zero_RollHFC;
        public bool Zero_RollHFC
        {
            get { return _zero_RollHFC; }
            set
            {
                _zero_RollHFC = value;
                lerp_HFC_roll.Run(value);
                OnPropertyChanged(nameof(Zero_RollHFC));
            }
        }
        bool _zero_YawHFC;
        public bool Zero_YawHFC
        {
            get { return _zero_YawHFC; }
            set
            {
                _zero_YawHFC = value;
                lerp_HFC_yaw.Run(value);
                OnPropertyChanged(nameof(Zero_YawHFC));
            }
        }
        bool _zero_PitchHFC;
        public bool Zero_PitchHFC
        {
            get { return _zero_PitchHFC; }
            set
            {
                _zero_PitchHFC = value;
                lerp_HFC_pitch.Run(value);
                OnPropertyChanged(nameof(Zero_PitchHFC));
            }
        }
        
        bool _zero_SurgeHFC;
        public bool Zero_SurgeHFC
        {
            get { return _zero_SurgeHFC; }
            set
            {
                _zero_SurgeHFC = value;
                lerp_HFC_surge.Run(value);
                OnPropertyChanged(nameof(Zero_SurgeHFC));
            }
        }
        bool _zero_PitchLFC;
        public bool Zero_PitchLFC
        {
            get { return _zero_PitchLFC; }
            set
            {
                _zero_PitchLFC = value;
                lerp_LFC_pitch.Run(value);
                OnPropertyChanged(nameof(Zero_PitchLFC));
            }
        }

        bool _zero_HeaveHFC;
        public bool Zero_HeaveHFC
        {
            get { return _zero_HeaveHFC; }
            set
            {
                _zero_HeaveHFC = value;
                lerp_HFC_heave.Run(value);
                OnPropertyChanged(nameof(Zero_HeaveHFC));
            }
        }

        bool _zero_SwayHFC;
        public bool Zero_SwayHFC
        {
            get { return _zero_SwayHFC; }
            set
            {
                _zero_SwayHFC = value;
                lerp_HFC_sway.Run(value);
                OnPropertyChanged(nameof(Zero_SwayHFC));
            }
        }
        bool _zero_RollLFC;
        public bool Zero_RollLFC
        {
            get { return _zero_RollLFC; }
            set
            {
                _zero_RollLFC = value;
                lerp_LFC_roll.Run(value);
                OnPropertyChanged(nameof(Zero_RollLFC));
            }
        }
        #endregion
        #region Viewmodel for indication
        float _rollHFC;
        public float RollHFC
        {
            get { return _rollHFC; }
            set { _rollHFC = value; OnPropertyChanged(nameof(RollHFC)); }
        }
        float _yawHFC;
        public float YawHFC
        {
            get { return _yawHFC; }
            set { _yawHFC = value; OnPropertyChanged(nameof(YawHFC)); }
        }
        float _pitchHFC;
        public float PitchHFC
        {
            get { return _pitchHFC; }
            set { _pitchHFC = value; OnPropertyChanged(nameof(PitchHFC)); }
        }

        float _surgeHFC;
        public float SurgeHFC
        {
            get { return _surgeHFC; }
            set { _surgeHFC = value; OnPropertyChanged(nameof(SurgeHFC)); }
        }
        float _pitchLFC;
        public float PitchLFC
        {
            get { return _pitchLFC; }
            set { _pitchLFC = value; OnPropertyChanged(nameof(PitchLFC)); }
        }

        float _heaveHFC;
        public float HeaveHFC
        {
            get { return _heaveHFC; }
            set { _heaveHFC = value; OnPropertyChanged(nameof(HeaveHFC)); }
        }

        float _swayHFC;
        public float SwayHFC
        {
            get { return _swayHFC; }
            set { _swayHFC = value; OnPropertyChanged(nameof(SwayHFC)); }
        }
        float _rollLFC;
        public float RollLFC
        {
            get { return _rollLFC; }
            set { _rollLFC = value; OnPropertyChanged(nameof(RollLFC)); }
        }
        #endregion

        Lerp lerp_HFC_roll  = new Lerp(fadeOverTime, LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_HFC_yaw   = new Lerp(fadeOverTime, LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_HFC_pitch = new Lerp(fadeOverTime, LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_HFC_surge = new Lerp(fadeOverTime, LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_LFC_pitch = new Lerp(fadeOverTime, LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_HFC_heave = new Lerp(fadeOverTime, LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_HFC_sway  = new Lerp(fadeOverTime, LerpOverMethod.LowPass3rdOrder);
        Lerp lerp_LFC_roll  = new Lerp(fadeOverTime, LerpOverMethod.LowPass3rdOrder);

        public void ZeroDataAsNeeded(DOF_Data data)
        {
            Output = data;

            //And then modify some...
            lerp_HFC_roll.Update();
            lerp_HFC_yaw.Update();
            lerp_HFC_pitch.Update();
            lerp_HFC_surge.Update();
            lerp_LFC_pitch.Update();
            lerp_HFC_heave.Update();
            lerp_HFC_sway.Update();
            lerp_LFC_roll.Update();

            Output.HFC_Roll     *= 1-lerp_HFC_roll.Ratio_external;
            Output.HFC_Yaw      *= 1-lerp_HFC_yaw.Ratio_external;
            Output.HFC_Pitch    *= 1-lerp_HFC_pitch.Ratio_external;
            Output.HFC_Surge    *= 1-lerp_HFC_surge.Ratio_external;
            Output.LFC_Pitch    *= 1-lerp_LFC_pitch.Ratio_external;
            Output.HFC_Heave    *= 1-lerp_HFC_heave.Ratio_external;
            Output.HFC_Sway     *= 1-lerp_HFC_sway.Ratio_external;
            Output.LFC_Roll     *= 1-lerp_LFC_roll.Ratio_external;

            //show values in UI
            RollHFC     = Output.HFC_Roll;
            YawHFC      = Output.HFC_Yaw;
            PitchHFC    = Output.HFC_Pitch;
            SurgeHFC    = Output.HFC_Surge;
            PitchLFC    = Output.LFC_Pitch;
            HeaveHFC    = Output.HFC_Heave;
            SwayHFC     = Output.HFC_Sway;
            RollLFC     = Output.LFC_Roll;
        }
    }
}
