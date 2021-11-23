using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class LoaderSaver
    {
        Engine engine;

        public LoaderSaver(Engine e)
        {
            engine = e;
        }
        //------------------Load stuff:------------------------
        public void LoadEngineSettingsFromApplication()
        {
            LoadCrashDetectorThresholds();
            LoadPositionOffsetCorrectionSettings();
            LoadAlphaCompensationValues();
            LoadFilterSettings();
            LoadCompressionSettings();
            LoadScalerSettings();
            LoadZeromakerSettings();
            LoadRigConfiguration();
        }

        private void LoadCrashDetectorThresholds()
        {
            var defaults = Properties.Settings.Default;
            var Crashdetector = engine.crashdetector;

            Crashdetector.Ax_Crashtrigger = defaults.CrashDetector_Threshold_Ax;
            Crashdetector.Ay_Crashtrigger = defaults.CrashDetector_Threshold_Ay;
            Crashdetector.Az_Crashtrigger = defaults.CrashDetector_Threshold_Az;
            Crashdetector.Wx_Crashtrigger = defaults.CrashDetector_Threshold_Wx;
            Crashdetector.Wy_Crashtrigger = defaults.CrashDetector_Threshold_Wy;
            Crashdetector.Wz_Crashtrigger = defaults.CrashDetector_Threshold_Wz;
        }
        private void LoadPositionOffsetCorrectionSettings()
        {
            var defaults = Properties.Settings.Default;
            var PosCorr = engine.positionoffsetcorrector;

            PosCorr.Delta_X = defaults.PositionOffsetCorrection_DeltaX;
            PosCorr.Delta_Y = defaults.PositionOffsetCorrection_DeltaY;
            PosCorr.Delta_Z = defaults.PositionOffsetCorrection_DeltaZ;
            PosCorr.IsActive = defaults.PositionOffsetCorrection_IsActive;
        }
        private void LoadAlphaCompensationValues()
        {
            var defaults = Properties.Settings.Default;
            var alphacomp = engine.alphacompensator;

            alphacomp.AoA_zero = defaults.AlphaCompensation_AlphaZero;
            alphacomp.AlphaCompensationPercentage = defaults.AlphaCompensation_CompensationPercentage;
            alphacomp.IAS_FadeIn_StartSpeed = defaults.AlphaCompensation_FadeIn_Start;
            alphacomp.IAS_FadeIn_DoneSpeed = defaults.AlphaCompensation_FadeIn_Done;
            alphacomp.IsActive = defaults.AlphaCompensation_IsActive;
        }
        private void LoadFilterSettings()
        {
            var defaults = Properties.Settings.Default;
            var filters = engine.filtersystem;

            filters.Wx_HP.FilterVariable        = defaults.Filtervariable_Wx_HP;
            filters.Wx_HP_LP.FilterVariable     = defaults.Filtervariable_Wx_HP_LP;
            filters.Wy_HP.FilterVariable        = defaults.FilterVariable_Wy_HP;
            filters.Wy_HP_LP.FilterVariable     = defaults.Filtervariable_Wy_HP_LP;
            filters.Wz_HP.FilterVariable        = defaults.Filtervariable_Wz_HP;
            filters.Wz_HP_LP.FilterVariable     = defaults.Filtervariable_Wz_HP_LP;

            filters.Ax_HP.FilterVariable        = defaults.Filtervariable_Ax_HP;
            filters.Ax_HP_LP2.FilterVariable    = defaults.Filtervariable_Ax_HP_LP2;
            filters.Ay_HP.FilterVariable        = defaults.Filtervariable_Ay_HP;
            filters.Ay_HP_LP2.FilterVariable    = defaults.Filtervariable_Ay_HP_LP2;
            filters.Az_HP.FilterVariable        = defaults.Filtervariable_Az_HP;
            filters.Az_HP_LP2.FilterVariable    = defaults.Filtervariable_Az_HP_LP2;

            filters.Ax_LP3.FilterVariable       = defaults.Filtervariable_Ax_LP3;
            filters.Az_LP3.FilterVariable       = defaults.Filtervariable_Az_LP3;
        }
        private void LoadCompressionSettings()
        {
            var defaults = Properties.Settings.Default;
            var compression = engine.compressorsystem;

            //Dropdowns
            compression.CMP_Roll_HFC.Method     = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Roll_HFC);
            compression.CMP_Yaw_HFC.Method      = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Yaw_HFC);
            compression.CMP_Pitch_HFC.Method    = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Pitch_HFC);
            compression.CMP_Surge_HFC.Method    = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Surge_HFC);
            compression.CMP_Heave_HFC.Method    = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Heave_HFC);
            compression.CMP_Sway_HFC.Method     = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Sway_HFC);
            compression.CMP_Pitch_LFC.Method    = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Pitch_LFC);
            compression.CMP_Roll_LFC.Method     = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Roll_LFC);
            //Parameters:
            compression.CMP_Roll_HFC.Parameter = defaults.CompressionParameter_Roll_HFC;
            compression.CMP_Yaw_HFC.Parameter = defaults.CompressionParameter_Yaw_HFC;
            compression.CMP_Pitch_HFC.Parameter = defaults.CompressionParameter_Pitch_HFC;
            compression.CMP_Surge_HFC.Parameter = defaults.CompressionParameter_Surge_HFC;
            compression.CMP_Heave_HFC.Parameter = defaults.CompressionParameter_Heave_HFC;
            compression.CMP_Sway_HFC.Parameter = defaults.CompressionParameter_Sway_HFC;
            compression.CMP_Pitch_LFC.Parameter = defaults.CompressionParameter_Pitch_LFC;
            compression.CMP_Roll_LFC.Parameter = defaults.CompressionParameter_Roll_LFC;
            //Limits:
            compression.CMP_Roll_HFC.Limit = defaults.CompressionLimit_Roll_HFC;
            compression.CMP_Yaw_HFC.Limit = defaults.CompressionLimit_Yaw_HFC;
            compression.CMP_Pitch_HFC.Limit = defaults.CompressionLimit_Pitch_HFC;
            compression.CMP_Surge_HFC.Limit = defaults.CompressionLimit_Surge_HFC;
            compression.CMP_Heave_HFC.Limit = defaults.CompressionLimit_Heave_HFC;
            compression.CMP_Sway_HFC.Limit = defaults.CompressionLimit_Sway_HFC;
            compression.CMP_Pitch_LFC.Limit = defaults.CompressionLimit_Pitch_LFC;
            compression.CMP_Roll_LFC.Limit = defaults.CompressionLimit_Roll_LFC;


        }
        private void LoadScalerSettings()
        {
            var defaults = Properties.Settings.Default;
            var scaler = engine.scalersystem;

            scaler.SCL_Roll_HFC.Gain = defaults.Scaler_Gain_RollHFC;
            scaler.SCL_Yaw_HFC.Gain = defaults.Scaler_Gain_YawHFC;
            scaler.SCL_Pitch_HFC.Gain = defaults.Scaler_Gain_PitchHFC;
            scaler.SCL_Surge_HFC.Gain = defaults.Scaler_Gain_SurgeHFC;
            scaler.SCL_Heave_HFC.Gain = defaults.Scaler_Gain_HeaveHFC;
            scaler.SCL_Sway_HFC.Gain = defaults.Scaler_Gain_SwayHFC;

            scaler.SCL_Roll_LFC.Gain = defaults.Scaler_Gain_RollLFC;
            scaler.SCL_Pitch_LFC.Gain = defaults.Scaler_Gain_PitchLFC;
        }
        private void LoadZeromakerSettings()
        {
            var defaults = Properties.Settings.Default;
            var zeromaker = engine.zeromaker;

            zeromaker.Zero_RollHFC  = defaults.Zeromaker_Zero_RollHFC;
            zeromaker.Zero_YawHFC   = defaults.Zeromaker_Zero_YawHFC;
            zeromaker.Zero_PitchHFC = defaults.Zeromaker_Zero_PitchHFC;

            zeromaker.Zero_SurgeHFC = defaults.Zeromaker_Zero_SurgeHFC;
            zeromaker.Zero_PitchLFC = defaults.Zeromaker_Zero_PitchLFC;

            zeromaker.Zero_HeaveHFC = defaults.Zeromaker_Zero_HeaveHFC;

            zeromaker.Zero_SwayHFC  = defaults.Zeromaker_Zero_SwayHFC;
            zeromaker.Zero_RollLFC  = defaults.Zeromaker_Zero_RollLFC;
        }
        private void LoadRigConfiguration()
        {
            var defaults = Properties.Settings.Default;

            engine.integrator.Offset_Park   = defaults.Integrator_OffsetPark;
            engine.integrator.Offset_Pause  = defaults.Integrator_OffsetPause;
            engine.integrator.Offset_CoR    = defaults.Integrator_OffsetCoR;
            engine.integrator.Dist_A_Upper  = defaults.Dist_A_Upper;
            engine.integrator.Dist_B_Upper  = defaults.Dist_B_Upper;
            engine.integrator.Dist_A_Lower  = defaults.Dist_A_Lower;
            engine.integrator.Dist_B_Lower  = defaults.Dist_B_Lower;
            engine.actuatorsystem.MaxLength = defaults.ActuatorSystem_MaxLength;
            engine.actuatorsystem.MinLength = defaults.ActuatorSystem_MinLength;
        }

        //----------------Save stuff:-----------------------
        public void SaveSettingsInApplication()
        {
            SaveCrashDetectorThresholds();
            SavePositionCorrectionOffsets();
            SaveAlphaCompensationValues();
            SaveFilterSettings();
            SaveCompressionSettings();
            SaveScalerSettings();
            SaveZeromakerSettings();
            SaveRigConfiguration();

            Properties.Settings.Default.Save();
        }

        private void SaveCrashDetectorThresholds()
        {
            var defaults = Properties.Settings.Default;
            var Crashdetector = engine.crashdetector;

            defaults.CrashDetector_Threshold_Ax = Crashdetector.Ax_Crashtrigger;
            defaults.CrashDetector_Threshold_Ay = Crashdetector.Ay_Crashtrigger;
            defaults.CrashDetector_Threshold_Az = Crashdetector.Az_Crashtrigger;
            defaults.CrashDetector_Threshold_Wx = Crashdetector.Wx_Crashtrigger;
            defaults.CrashDetector_Threshold_Wy = Crashdetector.Wy_Crashtrigger;
            defaults.CrashDetector_Threshold_Wz = Crashdetector.Wz_Crashtrigger;
        }
        private void SavePositionCorrectionOffsets()
        {
            var defaults = Properties.Settings.Default;
            var PosCorr = engine.positionoffsetcorrector;

            defaults.PositionOffsetCorrection_DeltaX = PosCorr.Delta_X;
            defaults.PositionOffsetCorrection_DeltaY = PosCorr.Delta_Y;
            defaults.PositionOffsetCorrection_DeltaZ = PosCorr.Delta_Z;
            defaults.PositionOffsetCorrection_IsActive = PosCorr.IsActive;
        }
        private void SaveAlphaCompensationValues()
        {
            var defaults = Properties.Settings.Default;
            var alphacomp = engine.alphacompensator;

            defaults.AlphaCompensation_AlphaZero = alphacomp.AoA_zero;
            defaults.AlphaCompensation_CompensationPercentage = alphacomp.AlphaCompensationPercentage;
            defaults.AlphaCompensation_FadeIn_Start = alphacomp.IAS_FadeIn_StartSpeed;
            defaults.AlphaCompensation_FadeIn_Done = alphacomp.IAS_FadeIn_DoneSpeed;
            defaults.AlphaCompensation_IsActive = alphacomp.IsActive;
        }
        private void SaveFilterSettings()
        {
            var defaults = Properties.Settings.Default;
            var filters = engine.filtersystem;

            defaults.Filtervariable_Wx_HP = filters.Wx_HP.FilterVariable;
            defaults.Filtervariable_Wx_HP_LP = filters.Wx_HP_LP.FilterVariable;
            defaults.FilterVariable_Wy_HP = filters.Wy_HP.FilterVariable;
            defaults.Filtervariable_Wy_HP_LP = filters.Wy_HP_LP.FilterVariable;
            defaults.Filtervariable_Wz_HP = filters.Wz_HP.FilterVariable;
            defaults.Filtervariable_Wz_HP_LP = filters.Wz_HP_LP.FilterVariable;

            defaults.Filtervariable_Ax_HP = filters.Ax_HP.FilterVariable;
            defaults.Filtervariable_Ax_HP_LP2 = filters.Ax_HP_LP2.FilterVariable;
            defaults.Filtervariable_Ay_HP = filters.Ay_HP.FilterVariable;
            defaults.Filtervariable_Ay_HP_LP2 = filters.Ay_HP_LP2.FilterVariable;
            defaults.Filtervariable_Az_HP = filters.Az_HP.FilterVariable;
            defaults.Filtervariable_Az_HP_LP2 = filters.Az_HP_LP2.FilterVariable;

            defaults.Filtervariable_Ax_LP3 = filters.Ax_LP3.FilterVariable;
            defaults.Filtervariable_Az_LP3 = filters.Az_LP3.FilterVariable;
        }
        private void SaveCompressionSettings()
        {
            var defaults = Properties.Settings.Default;
            var compression = engine.compressorsystem;

            //Dropdowns:
            defaults.CompressionMethod_Roll_HFC = compression.CMP_Roll_HFC.Method.ToString();
            defaults.CompressionMethod_Yaw_HFC = compression.CMP_Yaw_HFC.Method.ToString();
            defaults.CompressionMethod_Pitch_HFC = compression.CMP_Pitch_HFC.Method.ToString();
            defaults.CompressionMethod_Surge_HFC = compression.CMP_Surge_HFC.Method.ToString();
            defaults.CompressionMethod_Heave_HFC = compression.CMP_Heave_HFC.Method.ToString();
            defaults.CompressionMethod_Sway_HFC = compression.CMP_Sway_HFC.Method.ToString();
            defaults.CompressionMethod_Pitch_LFC = compression.CMP_Pitch_LFC.Method.ToString();
            defaults.CompressionMethod_Roll_LFC = compression.CMP_Roll_LFC.Method.ToString();
            //Parameters:
            defaults.CompressionParameter_Roll_HFC = compression.CMP_Roll_HFC.Parameter;
            defaults.CompressionParameter_Yaw_HFC = compression.CMP_Yaw_HFC.Parameter;
            defaults.CompressionParameter_Pitch_HFC = compression.CMP_Pitch_HFC.Parameter;
            defaults.CompressionParameter_Surge_HFC = compression.CMP_Surge_HFC.Parameter;
            defaults.CompressionParameter_Heave_HFC = compression.CMP_Heave_HFC.Parameter;
            defaults.CompressionParameter_Sway_HFC = compression.CMP_Sway_HFC.Parameter;
            defaults.CompressionParameter_Pitch_LFC = compression.CMP_Pitch_LFC.Parameter;
            defaults.CompressionParameter_Roll_LFC = compression.CMP_Roll_LFC.Parameter;
            //Limits:
            defaults.CompressionLimit_Roll_HFC = compression.CMP_Roll_HFC.Limit;
            defaults.CompressionLimit_Yaw_HFC = compression.CMP_Yaw_HFC.Limit;
            defaults.CompressionLimit_Pitch_HFC = compression.CMP_Pitch_HFC.Limit;
            defaults.CompressionLimit_Surge_HFC = compression.CMP_Surge_HFC.Limit;
            defaults.CompressionLimit_Heave_HFC = compression.CMP_Heave_HFC.Limit;
            defaults.CompressionLimit_Sway_HFC = compression.CMP_Sway_HFC.Limit;
            defaults.CompressionLimit_Pitch_LFC = compression.CMP_Pitch_LFC.Limit;
            defaults.CompressionLimit_Roll_LFC = compression.CMP_Roll_LFC.Limit;
        }
        private void SaveScalerSettings()
        {
            var defaults = Properties.Settings.Default;
            var scaler = engine.scalersystem;

            defaults.Scaler_Gain_RollHFC = scaler.SCL_Roll_HFC.Gain;
            defaults.Scaler_Gain_YawHFC = scaler.SCL_Yaw_HFC.Gain;
            defaults.Scaler_Gain_PitchHFC = scaler.SCL_Pitch_HFC.Gain;
            defaults.Scaler_Gain_SurgeHFC = scaler.SCL_Surge_HFC.Gain;
            defaults.Scaler_Gain_HeaveHFC = scaler.SCL_Heave_HFC.Gain;
            defaults.Scaler_Gain_SwayHFC = scaler.SCL_Sway_HFC.Gain;

            defaults.Scaler_Gain_PitchLFC = scaler.SCL_Pitch_LFC.Gain;
            defaults.Scaler_Gain_RollLFC = scaler.SCL_Roll_LFC.Gain;
        }
        private void SaveZeromakerSettings()
        {
            var defaults = Properties.Settings.Default;
            var zeromaker = engine.zeromaker;

            defaults.Zeromaker_Zero_RollHFC = zeromaker.Zero_RollHFC;
            defaults.Zeromaker_Zero_YawHFC = zeromaker.Zero_YawHFC;
            defaults.Zeromaker_Zero_PitchHFC = zeromaker.Zero_PitchHFC;

            defaults.Zeromaker_Zero_SurgeHFC = zeromaker.Zero_SurgeHFC;
            defaults.Zeromaker_Zero_PitchLFC = zeromaker.Zero_PitchLFC;

            defaults.Zeromaker_Zero_HeaveHFC = zeromaker.Zero_HeaveHFC;

            defaults.Zeromaker_Zero_SwayHFC = zeromaker.Zero_SwayHFC;
            defaults.Zeromaker_Zero_RollLFC = zeromaker.Zero_RollLFC;
        }
        private void SaveRigConfiguration()
        {
            var defaults = Properties.Settings.Default;
            var integrator = engine.integrator;

            defaults.Integrator_OffsetPark      = engine.integrator.Offset_Park;
            defaults.Integrator_OffsetPause     = engine.integrator.Offset_Pause;
            defaults.Integrator_OffsetCoR       = engine.integrator.Offset_CoR;
            defaults.Dist_A_Upper = engine.integrator.Dist_A_Upper;
            defaults.Dist_B_Upper = engine.integrator.Dist_B_Upper;
            defaults.Dist_A_Lower = engine.integrator.Dist_A_Lower;
            defaults.Dist_B_Lower = engine.integrator.Dist_B_Lower;
            defaults.ActuatorSystem_MaxLength = engine.actuatorsystem.MaxLength;
            defaults.ActuatorSystem_MinLength = engine.actuatorsystem.MinLength;

        }
    }
}
