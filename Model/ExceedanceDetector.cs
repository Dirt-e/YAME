using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MOTUS.Model
{
    public class ExceedanceDetector : MyObject
    {
        public PreprocessorData Output = new PreprocessorData();

        bool _isanyexceedancepresent = false;
        public bool IsAnyExceedancePresent
        {
            get { return _isanyexceedancepresent; }
            set
            {
                if (value != _isanyexceedancepresent)                                       //Only when it changes...
                {
                    _isanyexceedancepresent = value;
                    OnPropertyChanged(nameof(IsAnyExceedancePresent));

                    if (value)                                                              //...to the positive...
                    {
                        engine.recoverylogic.State = Recovery_State.Crash_Informed;         //...should you inform the RecoveryLogic
                    }
                }
            }
        }

        #region (VM)
        //Thresholds
        float _ax_crashtrigger = float.PositiveInfinity;
        public float AX_CrashTrigger
        {
            get{ return _ax_crashtrigger; }
            set{ _ax_crashtrigger = value; OnPropertyChanged(nameof(AX_CrashTrigger)); }
        }
        float _ay_crashtrigger = float.PositiveInfinity;
        public float AY_CrashTrigger
        {
            get { return _ay_crashtrigger;  }
            set { _ay_crashtrigger = value; OnPropertyChanged(nameof(AY_CrashTrigger)); }
        }
        float _az_crashtrigger = float.PositiveInfinity;
        public float AZ_CrashTrigger
        {
            get  {  return _az_crashtrigger;  }
            set  {  _az_crashtrigger = value; OnPropertyChanged(nameof(AZ_CrashTrigger)); }
        }
        float _wx_crashtrigger = float.PositiveInfinity;
        public float WX_CrashTrigger
        {
            get { return _wx_crashtrigger; }
            set { _wx_crashtrigger = value; OnPropertyChanged(nameof(WX_CrashTrigger)); }
        }
        float _wy_crashtrigger = float.PositiveInfinity;
        public float WY_CrashTrigger
        {
            get { return _wy_crashtrigger; }
            set { _wy_crashtrigger = value; OnPropertyChanged(nameof(WY_CrashTrigger)); }
        }
        float _wz_crashtrigger = float.PositiveInfinity;
        public float WZ_CrashTrigger
        {
            get { return _wz_crashtrigger; }
            set { _wz_crashtrigger = value; OnPropertyChanged(nameof(WZ_CrashTrigger)); }
        }
        
        //These DON't latch!
        public bool IsExceedance_Ax { get; private set; }
        public bool IsExceedance_Ay { get; private set; }
        public bool IsExceedance_Az { get; private set; }
        public bool IsExceedance_Wx { get; private set; }
        public bool IsExceedance_Wy { get; private set; }
        public bool IsExceedance_Wz { get; private set; }
        
        float _excceedance_value_ax = float.NaN;
        public float ExceedanceValue_Ax
        { 
            get { return _excceedance_value_ax; }
            private set
            {
                _excceedance_value_ax = value;
                OnPropertyChanged(nameof(ExceedanceValue_Ax));
            }
        }
        float _excceedance_value_ay = float.NaN;
        public float ExceedanceValue_Ay
        {
            get { return _excceedance_value_ay; }
            private set
            {
                _excceedance_value_ay = value;
                OnPropertyChanged(nameof(ExceedanceValue_Ay));
            }
        }
        float _excceedance_value_az = float.NaN;
        public float ExceedanceValue_Az
        {
            get { return _excceedance_value_az; }
            private set
            {
                _excceedance_value_az = value;
                OnPropertyChanged(nameof(ExceedanceValue_Az));
            }
        }
        float _excceedance_value_wx = float.NaN;
        public float ExceedanceValue_Wx
        {
            get { return _excceedance_value_wx; }
            private set
            {
                _excceedance_value_wx = value;
                OnPropertyChanged(nameof(ExceedanceValue_Wx));
            }
        }
        float _excceedance_value_wy = float.NaN;
        public float ExceedanceValue_Wy
        {
            get { return _excceedance_value_wy; }
            private set
            {
                _excceedance_value_wy = value;
                OnPropertyChanged(nameof(ExceedanceValue_Wy));
            }
        }
        float _excceedance_value_wz = float.NaN;
        public float ExceedanceValue_Wz
        {
            get { return _excceedance_value_wz; }
            private set
            {
                _excceedance_value_wz = value;
                OnPropertyChanged(nameof(ExceedanceValue_Wz));
            }
        }
        #endregion

        public ExceedanceDetector(Engine e)
        {
            engine = e;
        }

        public void Process(PreprocessorData data)
        {
            CheckLimits(data);
            Output = data;              //We always pass the data on. This is only a DETECTOR!
        }

        void CheckLimits(PreprocessorData data)
        {
            IsExceedance_Ax = Math.Abs(data.AX) >= AX_CrashTrigger;
            IsExceedance_Ay = Math.Abs(data.AY) >= AY_CrashTrigger;
            IsExceedance_Az = Math.Abs(data.AZ) >= AZ_CrashTrigger;
            IsExceedance_Wx = Math.Abs(data.WX) >= WX_CrashTrigger;
            IsExceedance_Wy = Math.Abs(data.WY) >= WY_CrashTrigger;
            IsExceedance_Wz = Math.Abs(data.WZ) >= WZ_CrashTrigger;

            IsAnyExceedancePresent = (  IsExceedance_Ax ||
                                        IsExceedance_Ay ||
                                        IsExceedance_Az ||
                                        IsExceedance_Wx ||
                                        IsExceedance_Wy ||
                                        IsExceedance_Wz     );

            if (IsExceedance_Ax) ExceedanceValue_Ax = data.AX;
            if (IsExceedance_Ay) ExceedanceValue_Ay = data.AY;
            if (IsExceedance_Az) ExceedanceValue_Az = data.AZ;
            if (IsExceedance_Wx) ExceedanceValue_Wx = data.WX;
            if (IsExceedance_Wy) ExceedanceValue_Wy = data.WY;
            if (IsExceedance_Wz) ExceedanceValue_Wz = data.WZ;
        }
    }
}
