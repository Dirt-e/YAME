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
                if (value != _isanyexceedancepresent)                   //Only when it changes...
                {
                    _isanyexceedancepresent = value;
                    OnPropertyChanged(nameof(IsAnyExceedancePresent));

                    if (value)                                          //...to the positive.
                    {
                        LatchProtector();
                        InformRecoveryLogic();
                    }
                }
            }
        }

        #region Trigger thresholds (VM)
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
        #endregion
        #region Individual Exceedancees
        public bool IsExceedance_Ax { get; set; }
        public bool IsExceedance_Ay { get; set; }
        public bool IsExceedance_Az { get; set; }
        public bool IsExceedance_Wx { get; set; }
        public bool IsExceedance_Wy { get; set; }
        public bool IsExceedance_Wz { get; set; }
        #endregion

        public ExceedanceDetector(Engine e)
        {
            engine = e;
        }

        public void Process(PreprocessorData data)
        {
            CheckLimits(data);
            Output = data;                              //We always pass the data on. This is only a DETECTOR!
        }

        void CheckLimits(PreprocessorData data)
        {
            if (Math.Abs(data.AX) >= AX_CrashTrigger)   IsExceedance_Ax = true; 
            else                                        IsExceedance_Ax = false;
            if (Math.Abs(data.AY) >= AY_CrashTrigger)   IsExceedance_Ay = true;
            else                                        IsExceedance_Ay = false;
            if (Math.Abs(data.AZ) >= AZ_CrashTrigger)   IsExceedance_Az = true; 
            else                                        IsExceedance_Az = false;
            if (Math.Abs(data.WX) >= WX_CrashTrigger)   IsExceedance_Wx = true;
            else                                        IsExceedance_Wx = false;
            if (Math.Abs(data.WY) >= WY_CrashTrigger)   IsExceedance_Wy = true;
            else                                        IsExceedance_Wy = false;
            if (Math.Abs(data.WZ) >= WZ_CrashTrigger)   IsExceedance_Wz = true;
            else                                        IsExceedance_Wz = false;
            
            IsAnyExceedancePresent = (  IsExceedance_Ax ||
                                        IsExceedance_Ay ||
                                        IsExceedance_Az ||
                                        IsExceedance_Wx ||
                                        IsExceedance_Wy ||
                                        IsExceedance_Wz     );
        }
        void LatchProtector()
        {
            engine.protector.IsLatched = true;      //This only latches the protector. It will
                                                    //be un-latched by the recovery-logic
        }
        void InformRecoveryLogic()
        {
            engine.recoverylogic.State = Recovery_State.Crash_Informed;
        }
    }
}
