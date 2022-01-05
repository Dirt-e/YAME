using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class AlphaCompensator : MyObject
    {
        //The AlphaCompensator takes PreprocessorData in and gives FilterData out
        private PreprocessorData Input = new PreprocessorData();
        public FilterData Output = new FilterData();

        #region ViewModel
        float _aoa;
        public float AoA
        {
            get { return _aoa; }
            set { _aoa = value; OnPropertyChanged("AoA"); }
        }

        float _aoa_zero;
        public float AoA_zero
        {
            get { return _aoa_zero; }
            set { _aoa_zero = value; OnPropertyChanged("AoA_zero"); }
        }

        float _alphacompensationpercentage;
        public float AlphaCompensationPercentage
        {
            get { return _alphacompensationpercentage; }
            set { _alphacompensationpercentage = value; OnPropertyChanged("AlphaCompensationPercentage"); }
        }

        float _ias_fade_in_start_speed;
        public float FadeIn_Start_IAS
        {
            get { return _ias_fade_in_start_speed; }
            set { _ias_fade_in_start_speed = value; OnPropertyChanged("IAS_FadeIn_StartSpeed");  }
        }

        float _ias_fade_in_done_speed;
        public float FadeIn_Done_IAS
        {
            get { return _ias_fade_in_done_speed; }
            set { _ias_fade_in_done_speed = value; OnPropertyChanged("IAS_FadeIn_DoneSpeed"); }
        }

        float _fade_in_percentage;
        public float FadeIn_Percentage
        {
            get { return _fade_in_percentage; }
            set { _fade_in_percentage = value; OnPropertyChanged("FadeIn_Percentage"); }
        }

        bool _is_active;
        public bool IsActive
        {
            get { return _is_active; }
            set { _is_active = value; OnPropertyChanged("IsActive"); }
        }
        #endregion
        
        public void Process(PreprocessorData data)
        {
            Input = data;

            FillFilterDataWithUncompensatedValues();
            DetermineFadeInPercentage();
            if (IsActive)
            {
                ApplyAlphaCompensation();
            }
        }

        private void FillFilterDataWithUncompensatedValues()
        {
            AoA = Input.AOA;

            Output.WX = Input.WX;
            Output.WY = Input.WY;
            Output.WZ = Input.WZ;

            Output.AX = Input.AX;
            Output.AY = Input.AY;
            Output.AZ = Input.AZ;

            Output.AX_alphacomp = Input.AX;     //In case we don't apply any compensation.

        }
        private void DetermineFadeInPercentage()
        {
            float IAS = Input.IAS;
            float IAS_range = FadeIn_Done_IAS - FadeIn_Start_IAS;

            if (IAS_range == 0)     //Check this to prevent division by zero later on
            {
                if (IAS < FadeIn_Start_IAS)
                {
                    FadeIn_Percentage = 0;
                }
                else
                {
                    FadeIn_Percentage = 100;
                }
                return;
            }

            float IAS_inRange = Utility.Clamp(IAS, FadeIn_Start_IAS, FadeIn_Done_IAS) - FadeIn_Start_IAS;
            FadeIn_Percentage = (IAS_inRange / IAS_range) * 100;
        }
        private void ApplyAlphaCompensation()
        {
            //Grab the data:
            float AoA_fuselage = Input.AOA - AoA_zero;

            //Calculate the compensation:
            float A_tot = Calculate_TotalAcceleration();      //this can only be positive!
            float AlphaFactor = CalculateAlphaFactor(AoA_fuselage);

            float CorrectionValue = A_tot * AlphaFactor * (AlphaCompensationPercentage / 100) * (FadeIn_Percentage / 100);
            float Ax_corrected = Input.AX - CorrectionValue;

            //Write the new data into OutputData
            Output.AX_alphacomp = Ax_corrected;     //This is the only one that needs to be modified for AoA
        }
        
        //Helpers:
        private float Calculate_TotalAcceleration()
        {
            return (float)Math.Sqrt(Math.Pow(Input.AX, 2) + Math.Pow(Input.AY, 2));      //this can only be positive!
        }
        private static float CalculateAlphaFactor(float AoA_fuselage)
        {
            //the AlphaFactor tells me how much of the total acceleration acts along the longitudinal axis due to AoA
            return (float)Math.Sin(                                    
                        Math.Abs(
                            Utility.RAD_from_DEG(AoA_fuselage)));
        }

    }
}
