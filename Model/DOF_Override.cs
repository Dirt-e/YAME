using System.Diagnostics;
using YAME.DataFomats;
using static System.Math;
using static Utility;

namespace YAME.Model
{
    public class DOF_Override : MyObject
    {
        public DOF_Data Input       = new DOF_Data();
        public DOF_Data Output      = new DOF_Data();
        public DOF_Data Slider_LP3  = new DOF_Data();

        LowPassNthOrder LP_sldr_RollHFC     = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldr_YawHFC      = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldrPitchlHFC    = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldr_SurgelHFC   = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldr_PitchLFC    = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldr_HeaveHFC    = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldr_SwayHFC     = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldr_RollLFC     = new LowPassNthOrder(3);

        Lerp lerp = new Lerp(2000);

        #region ViewModel General
        bool _isOverride;
        public bool IsOverride
        {
            get { return _isOverride; }
            set
            {
                _isOverride = value;
                OnPropertyChanged(nameof(IsOverride));
                lerp.Run(value);
            }
        }
        #endregion
        #region ViewModel SliderValuesRaw
        float _sldr_rollHFC;
        public float sldr_RollHFC
        {
            get { return _sldr_rollHFC; }
            set { _sldr_rollHFC = value; OnPropertyChanged(nameof(sldr_RollHFC)); }
        }
        float _sldr_yawHFC;
        public float sldr_YawHFC
        {
            get { return _sldr_yawHFC; }
            set { _sldr_yawHFC = value; OnPropertyChanged(nameof(sldr_YawHFC)); }
        }
        float _sldr_pitchHFC;
        public float sldr_PitchHFC
        {
            get { return _sldr_pitchHFC; }
            set { _sldr_pitchHFC = value; OnPropertyChanged(nameof(sldr_PitchHFC)); }
        }

        float _sldr_surgeHFC;
        public float sldr_SurgeHFC
        {
            get { return _sldr_surgeHFC; }
            set { _sldr_surgeHFC = value; OnPropertyChanged(nameof(sldr_SurgeHFC)); }
        }
        float _sldr_pitchLFC;
        public float sldr_PitchLFC
        {
            get { return _sldr_pitchLFC; }
            set { _sldr_pitchLFC = value; OnPropertyChanged(nameof(sldr_PitchLFC)); }
        }

        float _sldr_heaveHFC;
        public float sldr_HeaveHFC
        {
            get { return _sldr_heaveHFC; }
            set { _sldr_heaveHFC = value; OnPropertyChanged(nameof(sldr_HeaveHFC)); }
        }

        float _sldr_swayHFC;
        public float sldr_SwayHFC
        {
            get { return _sldr_swayHFC; }
            set { _sldr_swayHFC = value; OnPropertyChanged(nameof(sldr_SwayHFC)); }
        }
        float _sldr_rollLFC;
        public float sldr_RollLFC
        {
            get { return _sldr_rollLFC; }
            set { _sldr_rollLFC = value; OnPropertyChanged(nameof(sldr_RollLFC)); }
        }
        #endregion
        #region ViewModel Ranges
        //------------- Pos------------------
        float _range_rollHFC;
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
        float _range_yawHFC;
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
        float _range_pitchHFC;
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

        float _range_surgeHFC;
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
        float _range_pitchLFC;
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

        float _range_heaveHFC;
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

        float _range_swayHFC;
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
        float _range_rollLFC;
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
        float _range_rollHFC_neg;
        public float RangeRollHFC_neg
        {
            get { return _range_rollHFC_neg; }
            set { _range_rollHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeRollHFC_neg)); }
        }
        float _range_yawHFC_neg;
        public float RangeYawHFC_neg
        {
            get { return _range_yawHFC_neg; }
            set { _range_yawHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeYawHFC_neg)); }
        }
        float _range_pitchHFC_neg;
        public float RangePitchHFC_neg
        {
            get { return _range_pitchHFC_neg; }
            set { _range_pitchHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangePitchHFC_neg)); }
        }

        float _range_surgeHFC_neg;
        public float RangeSurgeHFC_neg
        {
            get { return _range_surgeHFC_neg; }
            set { _range_surgeHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeSurgeHFC_neg)); }
        }
        float _range_pitchLFC_neg;
        public float RangePitchLFC_neg
        {
            get { return _range_pitchLFC_neg; }
            set { _range_pitchLFC_neg = -Abs(value); OnPropertyChanged(nameof(RangePitchLFC_neg)); }
        }

        float _range_heaveHFC_neg;
        public float RangeHeaveHFC_neg
        {
            get { return _range_heaveHFC_neg; }
            set { _range_heaveHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeHeaveHFC_neg)); }
        }

        float _range_swayHFC_neg;
        public float RangeSwayHFC_neg
        {
            get { return _range_swayHFC_neg; }
            set { _range_swayHFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeSwayHFC_neg)); }
        }
        float _range_rollLFC_neg;
        public float RangeRollLFC_neg
        {
            get { return _range_rollLFC_neg; }
            set { _range_rollLFC_neg = -Abs(value); OnPropertyChanged(nameof(RangeRollLFC_neg)); }
        }
        #endregion
        #region ViewModel Blue Bars
        //------------- Max ------------------
        float _sel_rollHFC_max;
        public float SelRollHFC_max
        {
            get { return _sel_rollHFC_max; }
            set
            {
                _sel_rollHFC_max = value;
                OnPropertyChanged(nameof(SelRollHFC_max));
            }
        }
        float _sel_yawHFC_max;
        public float SelYawHFC_max
        {
            get { return _sel_yawHFC_max; }
            set
            {
                _sel_yawHFC_max = value;
                OnPropertyChanged(nameof(SelYawHFC_max));
            }
        }
        float _sel_pitchHFC_max;
        public float SelPitchHFC_max
        {
            get { return _sel_pitchHFC_max; }
            set
            {
                _sel_pitchHFC_max = value;
                OnPropertyChanged(nameof(SelPitchHFC_max));
            }
        }

        float _sel_surgeHFC_max;
        public float SelSurgeHFC_max
        {
            get { return _sel_surgeHFC_max; }
            set
            {
                _sel_surgeHFC_max = value;
                OnPropertyChanged(nameof(SelSurgeHFC_max));
            }
        }
        float _sel_pitchLFC_max;
        public float SelPitchLFC_max
        {
            get { return _sel_pitchLFC_max; }
            set
            {
                _sel_pitchLFC_max = value;
                OnPropertyChanged(nameof(SelPitchLFC_max));
            }
        }

        float _sel_heaveHFC_max;
        public float SelHeaveHFC_max
        {
            get { return _sel_heaveHFC_max; }
            set
            {
                _sel_heaveHFC_max = value;
                OnPropertyChanged(nameof(SelHeaveHFC_max));
            }
        }

        float _sel_swayHFC_max;
        public float SelSwayHFC_max
        {
            get { return _sel_swayHFC_max; }
            set
            {
                _sel_swayHFC_max = value;
                OnPropertyChanged(nameof(SelSwayHFC_max));
            }
        }
        float _sel_rollLFC_max;
        public float SelRollLFC_max
        {
            get { return _sel_rollLFC_max; }
            set
            {
                _sel_rollLFC_max = value;
                OnPropertyChanged(nameof(SelRollLFC_max));
            }
        }
        //----------- Min ---------------
        float _sel_rollHFC_min;
        public float SelRollHFC_min
        {
            get { return _sel_rollHFC_min; }
            set { _sel_rollHFC_min = value; OnPropertyChanged(nameof(SelRollHFC_min)); }
        }
        float _sel_yawHFC_min;
        public float SelYawHFC_min
        {
            get { return _sel_yawHFC_min; }
            set { _sel_yawHFC_min = value; OnPropertyChanged(nameof(SelYawHFC_min)); }
        }
        float _sel_pitchHFC_min;
        public float SelPitchHFC_min
        {
            get { return _sel_pitchHFC_min; }
            set { _sel_pitchHFC_min = value; OnPropertyChanged(nameof(SelPitchHFC_min)); }
        }

        float _sel_surgeHFC_min;
        public float SelSurgeHFC_min
        {
            get { return _sel_surgeHFC_min; }
            set { _sel_surgeHFC_min = value; OnPropertyChanged(nameof(SelSurgeHFC_min)); }
        }
        float _sel_pitchLFC_min;
        public float SelPitchLFC_min
        {
            get { return _sel_pitchLFC_min; }
            set { _sel_pitchLFC_min = value; OnPropertyChanged(nameof(SelPitchLFC_min)); }
        }

        float _sel_heaveHFC_min;
        public float SelHeaveHFC_min
        {
            get { return _sel_heaveHFC_min; }
            set { _sel_heaveHFC_min = value; OnPropertyChanged(nameof(SelHeaveHFC_min)); }
        }

        float _sel_swayHFC_min;
        public float SelSwayHFC_min
        {
            get { return _sel_swayHFC_min; }
            set { _sel_swayHFC_min = value; OnPropertyChanged(nameof(SelSwayHFC_min)); }
        }
        float _sel_rollLFC_min;
        public float SelRollLFC_min
        {
            get { return _sel_rollLFC_min; }
            set { _sel_rollLFC_min = value; OnPropertyChanged(nameof(SelRollLFC_min)); }
        }
        #endregion


        public void Process(DOF_Data data)
        {
            Input = new DOF_Data(data);

            Update_LP_Filters(); 
            lerp.Update();

            DrawBlueBars();

            Output = Input * (1 - lerp.Ratio_external) + Slider_LP3 * lerp.Ratio_external;
        }

        void Update_LP_Filters()
        {
            //Push values in:
            LP_sldr_RollHFC.Push(sldr_RollHFC);
            LP_sldr_YawHFC.Push(sldr_YawHFC);
            LP_sldrPitchlHFC.Push(sldr_PitchHFC);
            LP_sldr_SurgelHFC.Push(sldr_SurgeHFC);
            LP_sldr_PitchLFC.Push(sldr_PitchLFC);
            LP_sldr_HeaveHFC.Push(sldr_HeaveHFC);
            LP_sldr_SwayHFC.Push(sldr_SwayHFC);
            LP_sldr_RollLFC.Push(sldr_RollLFC);


            //...and take them back out:
            Slider_LP3.HFC_Roll     = LP_sldr_RollHFC.OutValue;
            Slider_LP3.HFC_Yaw      = LP_sldr_YawHFC.OutValue;
            Slider_LP3.HFC_Pitch    = LP_sldrPitchlHFC.OutValue;
            Slider_LP3.HFC_Surge    = LP_sldr_SurgelHFC.OutValue;
            Slider_LP3.LFC_Pitch    = LP_sldr_PitchLFC.OutValue;
            Slider_LP3.HFC_Heave    = LP_sldr_HeaveHFC.OutValue;
            Slider_LP3.HFC_Sway     = LP_sldr_SwayHFC.OutValue;
            Slider_LP3.LFC_Roll     = LP_sldr_RollLFC.OutValue;
        }
        void DrawBlueBars()
        {
            SelRollHFC_max  = Max(Input.HFC_Roll, 0.0f);
            SelYawHFC_max   = Max(Input.HFC_Yaw, 0.0f);
            SelPitchHFC_max = Max(Input.HFC_Pitch, 0.0f);
            SelSurgeHFC_max = Max(Input.HFC_Surge, 0.0f);
            SelPitchLFC_max = Max(Input.LFC_Pitch, 0.0f);
            SelHeaveHFC_max = Max(Input.HFC_Heave, 0.0f);
            SelSwayHFC_max  = Max(Input.HFC_Sway, 0.0f);
            SelRollLFC_max  = Max(Input.LFC_Roll, 0.0f);


            SelRollHFC_min  = Min(Input.HFC_Roll, 0.0f);
            SelYawHFC_min   = Min(Input.HFC_Yaw, 0.0f);
            SelPitchHFC_min = Min(Input.HFC_Pitch, 0.0f);
            SelSurgeHFC_min = Min(Input.HFC_Surge, 0.0f);
            SelPitchLFC_min = Min(Input.LFC_Pitch, 0.0f);
            SelHeaveHFC_min = Min(Input.HFC_Heave, 0.0f);
            SelSwayHFC_min  = Min(Input.HFC_Sway, 0.0f);
            SelRollLFC_min  = Min(Input.LFC_Roll, 0.0f);
        }
    }
}
