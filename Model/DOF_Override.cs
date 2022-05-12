using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace YAME.Model
{
    public class DOF_Override : MyObject
    {
        public DOF_Data Input = new DOF_Data();
        public DOF_Data Output = new DOF_Data();

        //public Lerp lerp = new Lerp();

        #region ViewModel General
        bool _isOverride;
        public bool IsOverride
        {
            get { return _isOverride; }
            set 
            { 
                _isOverride = value; 
                OnPropertyChanged(nameof(IsOverride)); 

                //lerp.Run(IsOverride);
            }
        }
        #endregion
        #region ViewModel SliderValues
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
        #region ViewModel Ranges
        //------------- Pos------------------
        float _range_rollHFC = 20;
        public float RangeRollHFC
        {
            get { return _range_rollHFC; }
            set
            { 
                _range_rollHFC = Abs(value);
                RangeRollHFC_neg = -_range_rollHFC;
                OnPropertyChanged(nameof(RangeRollHFC)); 
            }
        }
        float _range_yawHFC = 20;
        public float RangeYawHFC
        {
            get { return _range_yawHFC; }
            set 
            { 
                _range_yawHFC = Abs(value);
                RangeYawHFC_neg = -_range_yawHFC;
                OnPropertyChanged(nameof(RangeYawHFC)); 
            }
        }
        float _range_pitchHFC = 20;
        public float RangePitchHFC
        {
            get { return _range_pitchHFC; }
            set 
            { 
                _range_pitchHFC = Abs(value);
                RangePitchHFC_neg = -_range_pitchHFC;
                OnPropertyChanged(nameof(RangePitchHFC)); 
            }
        }

        float _range_surgeHFC = 200;
        public float RangeSurgeHFC
        {
            get { return _range_surgeHFC; }
            set 
            { 
                _range_surgeHFC = Abs(value);
                RangeSurgeHFC_neg = -_range_surgeHFC;
                OnPropertyChanged(nameof(RangeSurgeHFC)); 
            }
        }
        float _range_pitchLFC = 20;
        public float RangePitchLFC
        {
            get { return _range_pitchLFC; }
            set 
            { 
                _range_pitchLFC = Abs(value);
                RangePitchLFC_neg = -_range_pitchLFC;
                OnPropertyChanged(nameof(RangePitchLFC)); 
            }
        }

        float _range_heaveHFC = 200;
        public float RangeHeaveHFC
        {
            get { return _range_heaveHFC; }
            set 
            { 
                _range_heaveHFC = Abs(value);
                RangeHeaveHFC_neg = -_range_heaveHFC;    
                OnPropertyChanged(nameof(RangeHeaveHFC)); 
            }
        }

        float _range_swayHFC = 200;
        public float RangeSwayHFC
        {
            get { return _range_swayHFC; }
            set 
            { 
                _range_swayHFC = Abs(value);
                RangeSwayHFC_neg = -_range_swayHFC; 
                OnPropertyChanged(nameof(RangeSwayHFC)); 
            }
        }
        float _range_rollLFC = 20;
        public float RangeRollLFC
        {
            get { return _range_rollLFC; }
            set 
            { 
                _range_rollLFC = Abs(value);
                RangeRollLFC_neg = -_range_rollLFC;
                OnPropertyChanged(nameof(RangeRollLFC)); 
            }
        }
        //----------- neg ---------------
        float _range_rollHFC_neg = -20;
        public float RangeRollHFC_neg
        {
            get { return _range_rollHFC_neg; }
            set { _range_rollHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeRollHFC_neg)); }
        }
        float _range_yawHFC_neg = -20;
        public float RangeYawHFC_neg
        {
            get { return _range_yawHFC_neg; }
            set { _range_yawHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeYawHFC_neg)); }
        }
        float _range_pitchHFC_neg = -20;
        public float RangePitchHFC_neg
        {
            get { return _range_pitchHFC_neg; }
            set { _range_pitchHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangePitchHFC_neg)); }
        }

        float _range_surgeHFC_neg = -200;
        public float RangeSurgeHFC_neg
        {
            get { return _range_surgeHFC_neg; }
            set { _range_surgeHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeSurgeHFC_neg)); }
        }
        float _range_pitchLFC_neg = -20;
        public float RangePitchLFC_neg
        {
            get { return _range_pitchLFC_neg; }
            set { _range_pitchLFC_neg = -Abs(value); OnPropertyChanged(nameof(RangePitchLFC_neg)); }
        }

        float _range_heaveHFC_neg = -200;
        public float RangeHeaveHFC_neg
        {
            get { return _range_heaveHFC_neg; }
            set { _range_heaveHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeHeaveHFC_neg)); }
        }

        float _range_swayHFC_neg = -200;
        public float RangeSwayHFC_neg
        {
            get { return _range_swayHFC_neg; }
            set { _range_swayHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeSwayHFC_neg)); }
        }
        float _range_rollLFC_neg = -20;
        public float RangeRollLFC_neg
        {
            get { return _range_rollLFC_neg; }
            set { _range_rollLFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeRollLFC_neg)); }
        }
        #endregion

        public DOF_Override()
        {

        }

        public void Process(DOF_Data data)
        {
            Input = new DOF_Data(data);

            //lerp.Update();

            if (IsOverride)
            {
                Output.HFC_Roll = RollHFC;
                Output.HFC_Yaw = YawHFC;
                Output.HFC_Pitch = PitchHFC;

                Output.HFC_Surge = SurgeHFC;
                Output.LFC_Pitch = PitchLFC;

                Output.HFC_Heave = HeaveHFC;

                Output.HFC_Sway = SwayHFC;
                Output.LFC_Roll = RollLFC;
            }
            else
            {
                PassThrough();
                PositionSliders();
            }
        }
        private void PositionSliders()
        {
            RollHFC = Output.HFC_Roll;
            YawHFC = Output.HFC_Yaw;
            PitchHFC = Output.HFC_Pitch;

            SurgeHFC = Output.HFC_Surge;
            PitchLFC = Output.LFC_Pitch;

            HeaveHFC = Output.HFC_Heave;

            SwayHFC = Output.HFC_Sway;
            RollLFC = Output.LFC_Roll;
        }
        private void PassThrough()
        {
            Output = new DOF_Data(Input);
        }
    }
}
