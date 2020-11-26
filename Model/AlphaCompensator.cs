using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class AlphaCompensator
    {
        //The AlphaCompensator takes PreprocessorData in and gives MotionData out
        private PreprocessorData Input = new PreprocessorData();
        public FilterData Output = new FilterData();

        //AoA compensation
        public float AoA { get; set; }
        public float AoA_zero { get; set; }
        public bool IsActive { get; set; }
        public float AlphaCompensationPercentage { get; set; }
        public float IAS_FadeIn_StartSpeed { get; set; }
        public float IAS_FadeIn_DoneSpeed { get; set; }
        public float FadeIn_Percentage { get; set; }

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
            float IAS_range = IAS_FadeIn_DoneSpeed - IAS_FadeIn_StartSpeed;

            if (IAS_range == 0)     //Check this to prevent division by zero later on
            {
                if (IAS < IAS_FadeIn_StartSpeed)
                {
                    FadeIn_Percentage = 0;
                }
                else
                {
                    FadeIn_Percentage = 100;
                }
                return;
            }

            float IAS_inRange = Utility.Clamp(IAS, IAS_FadeIn_StartSpeed, IAS_FadeIn_DoneSpeed) - IAS_FadeIn_StartSpeed;
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
