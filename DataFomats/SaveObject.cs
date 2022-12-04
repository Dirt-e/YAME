using YAME.Model;

namespace YAME.DataFomats
{
    public class SaveObject
    {
        #region Crashdetector
        public float Crashdetector_Trigger_Ax { get; set; }
        public float Crashdetector_Trigger_Ay { get; set; }
        public float Crashdetector_Trigger_Az { get; set; }

        public float Crashdetector_Trigger_Wx { get; set; }
        public float Crashdetector_Trigger_Wy { get; set; }
        public float Crashdetector_Trigger_Wz { get; set; }
        #endregion
        #region RigConfiguration
        public float RigConfig_Upper_DistA { get; set; }
        public float RigConfig_Upper_DistB { get; set; }

        public float RigConfig_Lower_DistA { get; set; }
        public float RigConfig_Lower_DistB { get; set; }

        public float RigConfig_Act_Stroke { get; set; }
        public float RigConfig_Act_Min { get; set; }

        public float RigConfig_Offset_Park { get; set; }
        public float RigConfig_Offset_Pause { get; set; }
        public float RigConfig_Offset_CoR { get; set; }
        #endregion
        #region PositionCorrector
        public float PositionOffsetCorrector_Delta_X { get; set; }
        public float PositionOffsetCorrector_Delta_Y { get; set; }
        public float PositionOffsetCorrector_Delta_Z { get; set; }

        public bool PositionOffsetCorrector_IsActive { get; set; }
        #endregion
        #region AlphaCompensator
        public float AlphaCompensator_AOA_Zero { get; set; }
        public float AlphaCompensator_FadeIn_Start { get; set; }
        public float AlphaCompensator_FadeIn_Done { get; set; }
        public float AlphaCompensator_CompensationPercentage { get; set; }
        #endregion
        #region Inverter
        public bool InvertWx { get; set; }
        public bool InvertWy { get; set; }
        public bool InvertWz { get; set; }
        public bool InvertAx { get; set; }
        public bool InvertAy { get; set; }
        public bool InvertAz { get; set; }
        #endregion
        #region FilterSystem
        public float FilterSystem_Variable_Wx_HP { get; set; }
        public float FilterSystem_Variable_Wx_HP_LP { get; set; }

        public float FilterSystem_Variable_Wy_HP { get; set; }
        public float FilterSystem_Variable_Wy_HP_LP { get; set; }

        public float FilterSystem_Variable_Wz_HP { get; set; }
        public float FilterSystem_Variable_Wz_HP_LP { get; set; }

        public float FilterSystem_Variable_Ax_HP { get; set; }
        public float FilterSystem_Variable_Ax_HP_LP2 { get; set; }
        public float FilterSystem_Variable_Ax_LP3 { get; set; }

        public float FilterSystem_Variable_Ay_HP { get; set; }
        public float FilterSystem_Variable_Ay_HP_LP2 { get; set; }

        public float FilterSystem_Variable_Az_HP { get; set; }
        public float FilterSystem_Variable_Az_HP_LP2 { get; set; }
        public float FilterSystem_Variable_Az_LP3 { get; set; }
        #endregion
        #region CompressionMethod
        public CompressionMethod CompressionMethod_Roll_HFC { get; set; }
        public CompressionMethod CompressionMethod_Yaw_HFC { get; set; }
        public CompressionMethod CompressionMethod_Pitch_HFC { get; set; }

        public CompressionMethod CompressionMethod_Surge_HFC { get; set; }
        public CompressionMethod CompressionMethod_Pitch_LFC { get; set; }

        public CompressionMethod CompressionMethod_Heave_HFC { get; set; }

        public CompressionMethod CompressionMethod_Sway_HFC { get; set; }
        public CompressionMethod CompressionMethod_Roll_LFC { get; set; }
        #endregion
        #region CompressionParameter
        public float CompressionParameter_Roll_HFC { get; set; }
        public float CompressionParameter_Yaw_HFC { get; set; }
        public float CompressionParameter_Pitch_HFC { get; set; }

        public float CompressionParameter_Surge_HFC { get; set; }
        public float CompressionParameter_Pitch_LFC { get; set; }

        public float CompressionParameter_Heave_HFC { get; set; }

        public float CompressionParameter_Sway_HFC { get; set; }
        public float CompressionParameter_Roll_LFC { get; set; }



        public float CompressionLimit_Roll_HFC { get; set; }
        public float CompressionLimit_Yaw_HFC { get; set; }
        public float CompressionLimit_Pitch_HFC { get; set; }

        public float CompressionLimit_Surge_HFC { get; set; }
        public float CompressionLimit_Pitch_LFC { get; set; }

        public float CompressionLimit_Heave_HFC { get; set; }

        public float CompressionLimit_Sway_HFC { get; set; }
        public float CompressionLimit_Roll_LFC { get; set; }
        #endregion
        #region ScalerSystem
        public float Scaler_Gain_Roll_HFC { get; set; }
        public float Scaler_Gain_Yaw_HFC { get; set; }
        public float Scaler_Gain_Pitch_HFC { get; set; }

        public float Scaler_Gain_Surge_HFC { get; set; }
        public float Scaler_Gain_Pitch_LFC { get; set; }

        public float Scaler_Gain_Heave_HFC { get; set; }

        public float Scaler_Gain_Sway_HFC { get; set; }
        public float Scaler_Gain_Roll_LFC { get; set; }
        #endregion
        #region ZeroMaker
        public bool Zero_Roll_HFC   { get; set; }
        public bool Zero_Yaw_HFC    { get; set; }
        public bool Zero_Pitch_HFC  { get; set; }

        public bool Zero_Surge_HFC  { get; set; }
        public bool Zero_Pitch_LFC  { get; set; }

        public bool Zero_Heave_HFC  { get; set; }

        public bool Zero_Sway_HFC   { get; set; }
        public bool Zero_Roll_LFC   { get; set; }
        #endregion
        #region DOF_Override
        public float RangeRollHFC   { get; set; }
        public float RangeYawHFC    { get; set; }
        public float RangePitchHFC  { get; set; }
        public float RangeSurgeHFC  { get; set; }
        public float RangeHeaveHFC  { get; set; }
        public float RangeSwayHFC   { get; set; }
        public float RangePitchLFC  { get; set; }
        public float RangeRollLFC   { get; set; }
        #endregion
        #region AASDlTalker
        public ControllerType AASDTalker_ControllerType { get; set; }
        #endregion
        #region ODriveTalker
        public int Lead { get; set; }
        public string FormatString_1 { get; set; }
        public string FormatString_2 { get; set; }
        public string FormatString_3 { get; set; }
        #endregion
    }
}