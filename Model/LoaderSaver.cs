using Microsoft.Win32;
using System;
using System.IO;
using System.IO.Ports;
using System.Text.Json;
using System.Windows;
using YAME.DataFomats;

namespace YAME.Model
{
    public class LoaderSaver : MyObject
    {
        SaveObject saveObject;

        private string _fullProfilePath;
        public string FullProfilePath
        {
            get { return _fullProfilePath; }
            set
            {
                _fullProfilePath = value;
                ProfileFileName = Path.GetFileNameWithoutExtension(_fullProfilePath);
                OnPropertyChanged(nameof(FullProfilePath));
            }
        }

        private string _profileFileName;
        public string ProfileFileName
        {
            get { return _profileFileName; }
            set { _profileFileName = value; OnPropertyChanged(nameof(ProfileFileName)); }
        }

        public LoaderSaver(Engine e)
        {
            engine = e;
            saveObject = new SaveObject();
        }

        #region To/from Settings:
        //------------------ Load ------------------------
        public void Load_Application()
        {
            Load_ProfilePath_Application();
            Load_CrashDetectorThresholds_Application();
            Load_RigConfiguration_Application();
            Load_PositionOffsetCorrectionSettings_Application();
            Load_AlphaCompensationValues_Application();
            Load_InverterValues_Application();
            Load_FilterSettings_Application();
            Load_CompressionSettings_Application();
            Load_ScalerSettings_Application();
            Load_ZeromakerSettings_Application();
            Load_DOF_Override_Application();
            Load_AASD_OutputSettings_Application();
            Load_ODrive_OutputSettings_Application();
        }

        void Load_ProfilePath_Application()
        {
            FullProfilePath = Properties.Settings.Default.LoaderSaver_ProfilePath;
        }
        void Load_CrashDetectorThresholds_Application()
        {
            var defaults = Properties.Settings.Default;
            var Crashdetector = engine.exceedancedetector;

            Crashdetector.AX_CrashTrigger = defaults.CrashDetector_Threshold_Ax;
            Crashdetector.AY_CrashTrigger = defaults.CrashDetector_Threshold_Ay;
            Crashdetector.AZ_CrashTrigger = defaults.CrashDetector_Threshold_Az;
            Crashdetector.WX_CrashTrigger = defaults.CrashDetector_Threshold_Wx;
            Crashdetector.WY_CrashTrigger = defaults.CrashDetector_Threshold_Wy;
            Crashdetector.WZ_CrashTrigger = defaults.CrashDetector_Threshold_Wz;
        }
        void Load_RigConfiguration_Application()
        {
            var defaults = Properties.Settings.Default;

            engine.integrator.Offset_Park = defaults.Integrator_OffsetPark;
            engine.integrator.Offset_Pause = defaults.Integrator_OffsetPause;
            engine.integrator.Offset_CoR = defaults.Integrator_OffsetCoR;
            engine.integrator.Dist_A_Upper = defaults.Dist_A_Upper;
            engine.integrator.Dist_B_Upper = defaults.Dist_B_Upper;
            engine.integrator.Dist_A_Lower = defaults.Dist_A_Lower;
            engine.integrator.Dist_B_Lower = defaults.Dist_B_Lower;
            engine.actuatorsystem.Stroke = defaults.ActuatorSystem_Stroke;
            engine.actuatorsystem.MinLength = defaults.ActuatorSystem_MinLength;
        }
        void Load_PositionOffsetCorrectionSettings_Application()
        {
            var defaults = Properties.Settings.Default;
            var PosCorr = engine.positionoffsetcorrector;

            PosCorr.Delta_X = defaults.PositionOffsetCorrection_DeltaX;
            PosCorr.Delta_Y = defaults.PositionOffsetCorrection_DeltaY;
            PosCorr.Delta_Z = defaults.PositionOffsetCorrection_DeltaZ;
            PosCorr.IsActive = defaults.PositionOffsetCorrection_IsActive;
        }
        void Load_AlphaCompensationValues_Application()
        {
            var defaults = Properties.Settings.Default;
            var alphacomp = engine.alphacompensator;

            alphacomp.AoA_zero = defaults.AlphaCompensation_AlphaZero;
            alphacomp.AlphaCompensationPercentage = defaults.AlphaCompensation_CompensationPercentage;
            alphacomp.FadeIn_Start_IAS = defaults.AlphaCompensation_FadeIn_Start;
            alphacomp.FadeIn_Done_IAS = defaults.AlphaCompensation_FadeIn_Done;
            alphacomp.IsActive = defaults.AlphaCompensation_IsActive;
        }
        void Load_InverterValues_Application()
        {
            var defaults = Properties.Settings.Default;

            engine.inverter.InvertWx = defaults.Invert_Wx;
            engine.inverter.InvertWy = defaults.Invert_Wy;
            engine.inverter.InvertWz = defaults.Invert_Wz;
            engine.inverter.InvertAx = defaults.Invert_Ax;
            engine.inverter.InvertAy = defaults.Invert_Ay;
            engine.inverter.InvertAz = defaults.Invert_Az;
        }
        void Load_FilterSettings_Application()
        {
            var defaults = Properties.Settings.Default;
            var filters = engine.filtersystem;

            filters.Wx_HP.FilterVariable = defaults.Filtervariable_Wx_HP;
            filters.Wx_HP_LP.FilterVariable = defaults.Filtervariable_Wx_HP_LP;
            filters.Wy_HP.FilterVariable = defaults.FilterVariable_Wy_HP;
            filters.Wy_HP_LP.FilterVariable = defaults.Filtervariable_Wy_HP_LP;
            filters.Wz_HP.FilterVariable = defaults.Filtervariable_Wz_HP;
            filters.Wz_HP_LP.FilterVariable = defaults.Filtervariable_Wz_HP_LP;

            filters.Ax_HP.FilterVariable = defaults.Filtervariable_Ax_HP;
            filters.Ax_HP_LP2.FilterVariable = defaults.Filtervariable_Ax_HP_LP2;
            filters.Ay_HP.FilterVariable = defaults.Filtervariable_Ay_HP;
            filters.Ay_HP_LP2.FilterVariable = defaults.Filtervariable_Ay_HP_LP2;
            filters.Az_HP.FilterVariable = defaults.Filtervariable_Az_HP;
            filters.Az_HP_LP2.FilterVariable = defaults.Filtervariable_Az_HP_LP2;

            filters.Ax_LP3.FilterVariable = defaults.Filtervariable_Ax_LP3;
            filters.Az_LP3.FilterVariable = defaults.Filtervariable_Az_LP3;
        }
        void Load_CompressionSettings_Application()
        {
            var defaults = Properties.Settings.Default;
            var compression = engine.compressorsystem;

            //Dropdowns
            compression.CMP_Roll_HFC.Method = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Roll_HFC);
            compression.CMP_Yaw_HFC.Method = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Yaw_HFC);
            compression.CMP_Pitch_HFC.Method = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Pitch_HFC);
            compression.CMP_Surge_HFC.Method = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Surge_HFC);
            compression.CMP_Heave_HFC.Method = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Heave_HFC);
            compression.CMP_Sway_HFC.Method = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Sway_HFC);
            compression.CMP_Pitch_LFC.Method = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Pitch_LFC);
            compression.CMP_Roll_LFC.Method = (CompressionMethod)Enum.Parse(typeof(CompressionMethod), defaults.CompressionMethod_Roll_LFC);
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
        void Load_ScalerSettings_Application()
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
        void Load_ZeromakerSettings_Application()
        {
            var defaults = Properties.Settings.Default;
            var zeromaker = engine.zeromaker;

            zeromaker.Zero_RollHFC = defaults.Zeromaker_Zero_RollHFC;
            zeromaker.Zero_YawHFC = defaults.Zeromaker_Zero_YawHFC;
            zeromaker.Zero_PitchHFC = defaults.Zeromaker_Zero_PitchHFC;

            zeromaker.Zero_SurgeHFC = defaults.Zeromaker_Zero_SurgeHFC;
            zeromaker.Zero_PitchLFC = defaults.Zeromaker_Zero_PitchLFC;

            zeromaker.Zero_HeaveHFC = defaults.Zeromaker_Zero_HeaveHFC;

            zeromaker.Zero_SwayHFC = defaults.Zeromaker_Zero_SwayHFC;
            zeromaker.Zero_RollLFC = defaults.Zeromaker_Zero_RollLFC;
        }
        void Load_DOF_Override_Application()
        {
            var defaults = Properties.Settings.Default;
            var dof_override = engine.dof_override;

            dof_override.RangeRollHFC = defaults.DOF_Override_Range_Roll_HFC;
            dof_override.RangeYawHFC = defaults.DOF_Override_Range_Yaw_HFC;
            dof_override.RangePitchHFC = defaults.DOF_Override_Range_Pitch_HFC;
            dof_override.RangeSurgeHFC = defaults.DOF_Override_Range_Surge_HFC;
            dof_override.RangeHeaveHFC = defaults.DOF_Override_Range_Heave_HFC;
            dof_override.RangeSwayHFC = defaults.DOF_Override_Range_Sway_HFC;
            dof_override.RangePitchLFC = defaults.DOF_Override_Range_Pitch_LFC;
            dof_override.RangeRollLFC = defaults.DOF_Override_Range_Roll_LFC;
        }
        void Load_AASD_OutputSettings_Application()
        {
            //COM port is handled inside "SerialConnectionWindow.xaml.cs",
            //because COM ports are assigned during runtime

            string name = Properties.Settings.Default.SerialTalker_LastUsed_HardwareController;
            engine.aasd_talker.Controller = (ControllerType)Enum.Parse(typeof(ControllerType), name);
        }
        void Load_ODrive_OutputSettings_Application()
        {
            //COM port is handled inside "ODriveTalker_Window.xaml.cs" Window_Loaded Event

            engine.odrivesystem.Lead            = Properties.Settings.Default.ODriveSystem_Lead;
            engine.odrivesystem.FormatString_1    = Properties.Settings.Default.ODriveSystem_FormatString_1;
            engine.odrivesystem.FormatString_2    = Properties.Settings.Default.ODriveSystem_FormatString_2;
            engine.odrivesystem.FormatString_3    = Properties.Settings.Default.ODriveSystem_FormatString_3;
        }

        //---------------- Save -----------------------
        public void Save_Application()
        {
            Save_ProfilePath_Application();
            Save_CrashDetectorThresholds_Application();
            Save_RigConfiguration_Application();
            Save_PositionCorrectionOffsets_Application();
            Save_AlphaCompensationValues_Application();
            Save_InverterValues_Application();
            Save_FilterSettings_Application();
            Save_CompressionSettings_Application();
            Save_ScalerSettings_Application();
            Save_ZeromakerSettings_Application();
            Save_DOF_OverrideSettings_Application();
            Save_AASD_OutputSettings_Application();
            Save_ODrive_OutputSettings_Application();

            Properties.Settings.Default.Save();
        }

        void Save_ProfilePath_Application()
        {
            Properties.Settings.Default.LoaderSaver_ProfilePath = FullProfilePath;
        }
        void Save_RigConfiguration_Application()
        {
            var defaults = Properties.Settings.Default;
            var integrator = engine.integrator;

            defaults.Integrator_OffsetPark = engine.integrator.Offset_Park;
            defaults.Integrator_OffsetPause = engine.integrator.Offset_Pause;
            defaults.Integrator_OffsetCoR = engine.integrator.Offset_CoR;
            defaults.Dist_A_Upper = engine.integrator.Dist_A_Upper;
            defaults.Dist_B_Upper = engine.integrator.Dist_B_Upper;
            defaults.Dist_A_Lower = engine.integrator.Dist_A_Lower;
            defaults.Dist_B_Lower = engine.integrator.Dist_B_Lower;
            defaults.ActuatorSystem_Stroke = engine.actuatorsystem.Stroke;
            defaults.ActuatorSystem_MinLength = engine.actuatorsystem.MinLength;
        }
        void Save_CrashDetectorThresholds_Application()
        {
            var defaults = Properties.Settings.Default;
            var Crashdetector = engine.exceedancedetector;

            defaults.CrashDetector_Threshold_Ax = Crashdetector.AX_CrashTrigger;
            defaults.CrashDetector_Threshold_Ay = Crashdetector.AY_CrashTrigger;
            defaults.CrashDetector_Threshold_Az = Crashdetector.AZ_CrashTrigger;
            defaults.CrashDetector_Threshold_Wx = Crashdetector.WX_CrashTrigger;
            defaults.CrashDetector_Threshold_Wy = Crashdetector.WY_CrashTrigger;
            defaults.CrashDetector_Threshold_Wz = Crashdetector.WZ_CrashTrigger;
        }
        void Save_PositionCorrectionOffsets_Application()
        {
            var defaults = Properties.Settings.Default;
            var PosCorr = engine.positionoffsetcorrector;

            defaults.PositionOffsetCorrection_DeltaX = PosCorr.Delta_X;
            defaults.PositionOffsetCorrection_DeltaY = PosCorr.Delta_Y;
            defaults.PositionOffsetCorrection_DeltaZ = PosCorr.Delta_Z;
            defaults.PositionOffsetCorrection_IsActive = PosCorr.IsActive;
        }
        void Save_AlphaCompensationValues_Application()
        {
            var defaults = Properties.Settings.Default;
            var alphacomp = engine.alphacompensator;

            defaults.AlphaCompensation_AlphaZero = alphacomp.AoA_zero;
            defaults.AlphaCompensation_CompensationPercentage = alphacomp.AlphaCompensationPercentage;
            defaults.AlphaCompensation_FadeIn_Start = alphacomp.FadeIn_Start_IAS;
            defaults.AlphaCompensation_FadeIn_Done = alphacomp.FadeIn_Done_IAS;
            defaults.AlphaCompensation_IsActive = alphacomp.IsActive;
        }
        void Save_InverterValues_Application()
        {
            var defaults = Properties.Settings.Default;

            defaults.Invert_Wx = engine.inverter.InvertWx;
            defaults.Invert_Wy = engine.inverter.InvertWy;
            defaults.Invert_Wz = engine.inverter.InvertWz;
            defaults.Invert_Ax = engine.inverter.InvertAx;
            defaults.Invert_Ay = engine.inverter.InvertAy;
            defaults.Invert_Az = engine.inverter.InvertAz;

        }
        void Save_FilterSettings_Application()
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
        void Save_CompressionSettings_Application()
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
        void Save_ScalerSettings_Application()
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
        void Save_ZeromakerSettings_Application()
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
        void Save_DOF_OverrideSettings_Application()
        {
            var defaults = Properties.Settings.Default;
            var dof_override = engine.dof_override;

            defaults.DOF_Override_Range_Roll_HFC = dof_override.RangeRollHFC;
            defaults.DOF_Override_Range_Yaw_HFC = dof_override.RangeYawHFC;
            defaults.DOF_Override_Range_Pitch_HFC = dof_override.RangePitchHFC;
            defaults.DOF_Override_Range_Surge_HFC = dof_override.RangeSurgeHFC;
            defaults.DOF_Override_Range_Heave_HFC = dof_override.RangeHeaveHFC;
            defaults.DOF_Override_Range_Sway_HFC = dof_override.RangeSwayHFC;
            defaults.DOF_Override_Range_Pitch_LFC = dof_override.RangePitchLFC;
            defaults.DOF_Override_Range_Roll_LFC = dof_override.RangeRollLFC;
        }
        void Save_AASD_OutputSettings_Application()
        {
            Properties.Settings.Default.SerialTalker_LastUsed_HardwareController = engine.aasd_talker.Controller.ToString();
        }
        void Save_ODrive_OutputSettings_Application()
        {
            if (engine.odrivesystem.oDriveTalkers[0].COM_Port != null)
            {
                Properties.Settings.Default.ODriveTalker_LastUsedComPort_1 = 
                    engine.odrivesystem.oDriveTalkers[0].COM_Port.ToString();
            }
            if (engine.odrivesystem.oDriveTalkers[1].COM_Port != null)
            {
                Properties.Settings.Default.ODriveTalker_LastUsedComPort_2 =
                    engine.odrivesystem.oDriveTalkers[1].COM_Port.ToString();
            }
            if (engine.odrivesystem.oDriveTalkers[2].COM_Port != null)
            {
                Properties.Settings.Default.ODriveTalker_LastUsedComPort_3 =
                    engine.odrivesystem.oDriveTalkers[2].COM_Port.ToString();
            }

            Properties.Settings.Default.ODriveSystem_Lead               = engine.odrivesystem.Lead;
            Properties.Settings.Default.ODriveSystem_FormatString_1     = engine.odrivesystem.FormatString_1;
            Properties.Settings.Default.ODriveSystem_FormatString_2     = engine.odrivesystem.FormatString_2;
            Properties.Settings.Default.ODriveSystem_FormatString_3     = engine.odrivesystem.FormatString_3;
        }
        #endregion
        #region To/from Profile:
        //------------------ Load ------------------------
        public void Load_Profile()
        {
            if (engine.aasd_talker.IsOpen)
            {
                MessageBox.Show(
                    "You wanna load a profile while the rig is hot? I'm tellin'ya " +
                    "that is not going to end well.\n" +
                    "Move the rig to PARK, then close the serial connection. " +
                    "ONLY THEN should you change the profile.",
                    "HOT RIG WARNING",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            OpenFileDialog ofd = MyOpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                string json = File.ReadAllText(ofd.FileName);
                saveObject = JsonSerializer.Deserialize<SaveObject>(json);

                FullProfilePath = ofd.FileName;
                Load_CrashDetectorThresholds_Profile();
                Load_RigConfiguration_Profile();
                Load_PositionOffsetCorrectionSettings_Profile();
                Load_AlphaCompensationValues_Profile();
                Load_InverterValues_Profile();
                Load_FilterSettings_Profile();
                Load_CompressionSettings_Profile();
                Load_ScalerSettings_Profile();
                Load_ZeromakerSettings_Profile();
                Load_DOF_Override_Profile();
                Load_AASDTalker_Settings_Profile();
                Load_ODriveTalker_Settings_Profile();
            }
        }

        void Load_CrashDetectorThresholds_Profile()
        {
            engine.exceedancedetector.AX_CrashTrigger = saveObject.Crashdetector_Trigger_Ax;
            engine.exceedancedetector.AY_CrashTrigger = saveObject.Crashdetector_Trigger_Ay;
            engine.exceedancedetector.AZ_CrashTrigger = saveObject.Crashdetector_Trigger_Az;

            engine.exceedancedetector.WX_CrashTrigger = saveObject.Crashdetector_Trigger_Wx;
            engine.exceedancedetector.WY_CrashTrigger = saveObject.Crashdetector_Trigger_Wy;
            engine.exceedancedetector.WZ_CrashTrigger = saveObject.Crashdetector_Trigger_Wz;
        }
        void Load_RigConfiguration_Profile()
        {
            engine.integrator.Dist_A_Upper = saveObject.RigConfig_Upper_DistA;
            engine.integrator.Dist_B_Upper = saveObject.RigConfig_Upper_DistB;

            engine.integrator.Dist_A_Lower = saveObject.RigConfig_Lower_DistA;
            engine.integrator.Dist_B_Lower = saveObject.RigConfig_Lower_DistB;

            engine.actuatorsystem.Stroke = saveObject.RigConfig_Act_Stroke;
            engine.actuatorsystem.MinLength = saveObject.RigConfig_Act_Min;

            engine.integrator.Offset_Park = saveObject.RigConfig_Offset_Park;
            engine.integrator.Offset_Pause = saveObject.RigConfig_Offset_Pause;
            engine.integrator.Offset_CoR = saveObject.RigConfig_Offset_CoR;
        }
        void Load_PositionOffsetCorrectionSettings_Profile()
        {
            engine.positionoffsetcorrector.Delta_X = saveObject.PositionOffsetCorrector_Delta_X;
            engine.positionoffsetcorrector.Delta_Y = saveObject.PositionOffsetCorrector_Delta_Y;
            engine.positionoffsetcorrector.Delta_Z = saveObject.PositionOffsetCorrector_Delta_Z;

            engine.positionoffsetcorrector.IsActive = saveObject.PositionOffsetCorrector_IsActive;
        }
        void Load_AlphaCompensationValues_Profile()
        {
            engine.alphacompensator.AoA_zero = saveObject.AlphaCompensator_AOA_Zero;
            engine.alphacompensator.FadeIn_Start_IAS = saveObject.AlphaCompensator_FadeIn_Start;
            engine.alphacompensator.FadeIn_Done_IAS = saveObject.AlphaCompensator_FadeIn_Done;
            engine.alphacompensator.AlphaCompensationPercentage = saveObject.AlphaCompensator_CompensationPercentage;
        }
        void Load_InverterValues_Profile()
        {
            engine.inverter.InvertWx = saveObject.InvertWx;
            engine.inverter.InvertWy = saveObject.InvertWy;
            engine.inverter.InvertWz = saveObject.InvertWz;
            engine.inverter.InvertAx = saveObject.InvertAx;
            engine.inverter.InvertAy = saveObject.InvertAy;
            engine.inverter.InvertAz = saveObject.InvertAz;
        }
        void Load_FilterSettings_Profile()
        {
            engine.filtersystem.Wx_HP.FilterVariable = saveObject.FilterSystem_Variable_Wx_HP;
            engine.filtersystem.Wx_HP_LP.FilterVariable = saveObject.FilterSystem_Variable_Wx_HP_LP;

            engine.filtersystem.Wy_HP.FilterVariable = saveObject.FilterSystem_Variable_Wy_HP;
            engine.filtersystem.Wy_HP_LP.FilterVariable = saveObject.FilterSystem_Variable_Wy_HP_LP;

            engine.filtersystem.Wz_HP.FilterVariable = saveObject.FilterSystem_Variable_Wz_HP;
            engine.filtersystem.Wz_HP_LP.FilterVariable = saveObject.FilterSystem_Variable_Wz_HP_LP;

            engine.filtersystem.Ax_HP.FilterVariable = saveObject.FilterSystem_Variable_Ax_HP;
            engine.filtersystem.Ax_HP_LP2.FilterVariable = saveObject.FilterSystem_Variable_Ax_HP_LP2;
            engine.filtersystem.Ax_LP3.FilterVariable = saveObject.FilterSystem_Variable_Ax_LP3;

            engine.filtersystem.Ay_HP.FilterVariable = saveObject.FilterSystem_Variable_Ay_HP;
            engine.filtersystem.Ay_HP_LP2.FilterVariable = saveObject.FilterSystem_Variable_Ay_HP_LP2;

            engine.filtersystem.Az_HP.FilterVariable = saveObject.FilterSystem_Variable_Az_HP;
            engine.filtersystem.Az_HP_LP2.FilterVariable = saveObject.FilterSystem_Variable_Az_HP_LP2;
            engine.filtersystem.Az_LP3.FilterVariable = saveObject.FilterSystem_Variable_Az_LP3;
        }
        void Load_CompressionSettings_Profile()
        {
            //Dropdowns:
            engine.compressorsystem.CMP_Yaw_HFC.Method = saveObject.CompressionMethod_Yaw_HFC;
            engine.compressorsystem.CMP_Pitch_HFC.Method = saveObject.CompressionMethod_Pitch_HFC;
            engine.compressorsystem.CMP_Roll_HFC.Method = saveObject.CompressionMethod_Roll_HFC;
            engine.compressorsystem.CMP_Surge_HFC.Method = saveObject.CompressionMethod_Surge_HFC;
            engine.compressorsystem.CMP_Heave_HFC.Method = saveObject.CompressionMethod_Heave_HFC;
            engine.compressorsystem.CMP_Sway_HFC.Method = saveObject.CompressionMethod_Sway_HFC;
            engine.compressorsystem.CMP_Pitch_LFC.Method = saveObject.CompressionMethod_Pitch_LFC;
            engine.compressorsystem.CMP_Roll_LFC.Method = saveObject.CompressionMethod_Roll_LFC;
            //Parameters:
            engine.compressorsystem.CMP_Roll_HFC.Parameter = saveObject.CompressionParameter_Roll_HFC;
            engine.compressorsystem.CMP_Yaw_HFC.Parameter = saveObject.CompressionParameter_Yaw_HFC;
            engine.compressorsystem.CMP_Pitch_HFC.Parameter = saveObject.CompressionParameter_Pitch_HFC;
            engine.compressorsystem.CMP_Surge_HFC.Parameter = saveObject.CompressionParameter_Surge_HFC;
            engine.compressorsystem.CMP_Heave_HFC.Parameter = saveObject.CompressionParameter_Heave_HFC;
            engine.compressorsystem.CMP_Sway_HFC.Parameter = saveObject.CompressionParameter_Sway_HFC;
            engine.compressorsystem.CMP_Pitch_LFC.Parameter = saveObject.CompressionParameter_Pitch_LFC;
            engine.compressorsystem.CMP_Roll_LFC.Parameter = saveObject.CompressionParameter_Roll_LFC;
            //Limits:
            engine.compressorsystem.CMP_Roll_HFC.Limit = saveObject.CompressionLimit_Roll_HFC;
            engine.compressorsystem.CMP_Yaw_HFC.Limit = saveObject.CompressionLimit_Yaw_HFC;
            engine.compressorsystem.CMP_Pitch_HFC.Limit = saveObject.CompressionLimit_Pitch_HFC;
            engine.compressorsystem.CMP_Surge_HFC.Limit = saveObject.CompressionLimit_Surge_HFC;
            engine.compressorsystem.CMP_Heave_HFC.Limit = saveObject.CompressionLimit_Heave_HFC;
            engine.compressorsystem.CMP_Sway_HFC.Limit = saveObject.CompressionLimit_Sway_HFC;
            engine.compressorsystem.CMP_Pitch_LFC.Limit = saveObject.CompressionLimit_Pitch_LFC;
            engine.compressorsystem.CMP_Roll_LFC.Limit = saveObject.CompressionLimit_Roll_LFC;
        }
        void Load_ScalerSettings_Profile()
        {
            engine.scalersystem.SCL_Roll_HFC.Gain = saveObject.Scaler_Gain_Roll_HFC;
            engine.scalersystem.SCL_Yaw_HFC.Gain = saveObject.Scaler_Gain_Yaw_HFC;
            engine.scalersystem.SCL_Pitch_HFC.Gain = saveObject.Scaler_Gain_Pitch_HFC;

            engine.scalersystem.SCL_Surge_HFC.Gain = saveObject.Scaler_Gain_Surge_HFC;
            engine.scalersystem.SCL_Pitch_LFC.Gain = saveObject.Scaler_Gain_Pitch_LFC;

            engine.scalersystem.SCL_Heave_HFC.Gain = saveObject.Scaler_Gain_Heave_HFC;

            engine.scalersystem.SCL_Sway_HFC.Gain = saveObject.Scaler_Gain_Sway_HFC;
            engine.scalersystem.SCL_Roll_LFC.Gain = saveObject.Scaler_Gain_Roll_LFC;
        }
        void Load_ZeromakerSettings_Profile()
        {
            engine.zeromaker.Zero_RollHFC = saveObject.Zero_Roll_HFC;
            engine.zeromaker.Zero_YawHFC = saveObject.Zero_Yaw_HFC;
            engine.zeromaker.Zero_PitchHFC = saveObject.Zero_Pitch_HFC;

            engine.zeromaker.Zero_SurgeHFC = saveObject.Zero_Surge_HFC;
            engine.zeromaker.Zero_PitchLFC = saveObject.Zero_Pitch_LFC;

            engine.zeromaker.Zero_HeaveHFC = saveObject.Zero_Heave_HFC;

            engine.zeromaker.Zero_SwayHFC = saveObject.Zero_Sway_HFC;
            engine.zeromaker.Zero_RollLFC = saveObject.Zero_Roll_LFC;
        }
        void Load_DOF_Override_Profile()
        {
            engine.dof_override.RangeRollHFC    = saveObject.RangeRollHFC;
            engine.dof_override.RangeYawHFC     = saveObject.RangeYawHFC;
            engine.dof_override.RangePitchHFC   = saveObject.RangePitchHFC;
            engine.dof_override.RangeSurgeHFC   = saveObject.RangeSurgeHFC;
            engine.dof_override.RangeHeaveHFC   = saveObject.RangeHeaveHFC;
            engine.dof_override.RangeSwayHFC    = saveObject.RangeSwayHFC;
            engine.dof_override.RangePitchLFC   = saveObject.RangePitchLFC;
            engine.dof_override.RangeRollLFC    = saveObject.RangeRollLFC;
        }
        void Load_AASDTalker_Settings_Profile()
        {
            //COM port is handled inside "SerialConnectionWindow.xaml.cs",
            //because COM ports are assigned during runtime

            engine.aasd_talker.Controller = saveObject.AASDTalker_ControllerType;
        }
        void Load_ODriveTalker_Settings_Profile()
        {
            //COM port is handled inside "OutputODrive_Window.xaml.cs",
            //because COM ports are assigned during runtime

            engine.odrivesystem.FormatString_1      = saveObject.FormatString_1;
            engine.odrivesystem.FormatString_2      = saveObject.FormatString_2;
            engine.odrivesystem.FormatString_3      = saveObject.FormatString_3;
            engine.odrivesystem.Lead                = saveObject.Lead;
        }

        //---------------- Save -----------------------
        public void Save_Profile(bool _as = false)
        {
            if (_as || !File.Exists(FullProfilePath))
            {
                SaveFileDialog sfd = MySaveFileDialog();
                if (FullProfilePath != "nil") sfd.FileName = Path.GetFileName(FullProfilePath);
                if (sfd.ShowDialog() == true) FullProfilePath = sfd.FileName;
                else return;
            }
            Save_CrashDetectorThresholds_Profile();
            Save_RigConfiguration_Profile();
            Save_PositionCorrectionOffsets_Profile();
            Save_AlphaCompensationValues_Profile();
            Save_InverterSettings_Profile();
            Save_FilterSettings_Profile();
            Save_CompressionSettings_Profile();
            Save_ScalerSettings_Profile();
            Save_ZeromakerSettings_Profile();
            Save_DOF_Override_Profile();
            Save_AASDTalker_Settings_Profile();
            Save_ODriveTalker_Settings_Profile();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(saveObject, options);

            File.WriteAllText(FullProfilePath, json);
        }

        void Save_CrashDetectorThresholds_Profile()
        {
            saveObject.Crashdetector_Trigger_Ax = engine.exceedancedetector.AX_CrashTrigger;
            saveObject.Crashdetector_Trigger_Ay = engine.exceedancedetector.AY_CrashTrigger;
            saveObject.Crashdetector_Trigger_Az = engine.exceedancedetector.AZ_CrashTrigger;

            saveObject.Crashdetector_Trigger_Wx = engine.exceedancedetector.WX_CrashTrigger;
            saveObject.Crashdetector_Trigger_Wy = engine.exceedancedetector.WY_CrashTrigger;
            saveObject.Crashdetector_Trigger_Wz = engine.exceedancedetector.WZ_CrashTrigger;
        }
        void Save_RigConfiguration_Profile()
        {
            saveObject.RigConfig_Upper_DistA = engine.integrator.Dist_A_Upper;
            saveObject.RigConfig_Upper_DistB = engine.integrator.Dist_B_Upper;

            saveObject.RigConfig_Lower_DistA = engine.integrator.Dist_A_Lower;
            saveObject.RigConfig_Lower_DistB = engine.integrator.Dist_B_Lower;

            saveObject.RigConfig_Act_Stroke = engine.actuatorsystem.Stroke;
            saveObject.RigConfig_Act_Min = engine.actuatorsystem.MinLength;

            saveObject.RigConfig_Offset_Park = engine.integrator.Offset_Park;
            saveObject.RigConfig_Offset_Pause = engine.integrator.Offset_Pause;
            saveObject.RigConfig_Offset_CoR = engine.integrator.Offset_CoR;
        }
        void Save_PositionCorrectionOffsets_Profile()
        {
            saveObject.PositionOffsetCorrector_Delta_X = engine.positionoffsetcorrector.Delta_X;
            saveObject.PositionOffsetCorrector_Delta_Y = engine.positionoffsetcorrector.Delta_Y;
            saveObject.PositionOffsetCorrector_Delta_Z = engine.positionoffsetcorrector.Delta_Z;
            saveObject.PositionOffsetCorrector_IsActive = engine.positionoffsetcorrector.IsActive;
        }
        void Save_AlphaCompensationValues_Profile()
        {
            saveObject.AlphaCompensator_AOA_Zero = engine.alphacompensator.AoA_zero;
            saveObject.AlphaCompensator_FadeIn_Start = engine.alphacompensator.FadeIn_Start_IAS;
            saveObject.AlphaCompensator_FadeIn_Done = engine.alphacompensator.FadeIn_Done_IAS;
            saveObject.AlphaCompensator_CompensationPercentage = engine.alphacompensator.AlphaCompensationPercentage;
        }
        void Save_InverterSettings_Profile()
        {
            saveObject.InvertWx = engine.inverter.InvertWx;
            saveObject.InvertWy = engine.inverter.InvertWy;
            saveObject.InvertWz = engine.inverter.InvertWz;
            saveObject.InvertAx = engine.inverter.InvertAx;
            saveObject.InvertAy = engine.inverter.InvertAy;
            saveObject.InvertAz = engine.inverter.InvertAz;
        }
        void Save_FilterSettings_Profile()
        {
            saveObject.FilterSystem_Variable_Wx_HP = engine.filtersystem.Wx_HP.FilterVariable;
            saveObject.FilterSystem_Variable_Wx_HP_LP = engine.filtersystem.Wx_HP_LP.FilterVariable;

            saveObject.FilterSystem_Variable_Wy_HP = engine.filtersystem.Wy_HP.FilterVariable;
            saveObject.FilterSystem_Variable_Wy_HP_LP = engine.filtersystem.Wy_HP_LP.FilterVariable;

            saveObject.FilterSystem_Variable_Wz_HP = engine.filtersystem.Wz_HP.FilterVariable;
            saveObject.FilterSystem_Variable_Wz_HP_LP = engine.filtersystem.Wz_HP_LP.FilterVariable;

            saveObject.FilterSystem_Variable_Ax_HP = engine.filtersystem.Ax_HP.FilterVariable;
            saveObject.FilterSystem_Variable_Ax_HP_LP2 = engine.filtersystem.Ax_HP_LP2.FilterVariable;
            saveObject.FilterSystem_Variable_Ax_LP3 = engine.filtersystem.Ax_LP3.FilterVariable;

            saveObject.FilterSystem_Variable_Ay_HP = engine.filtersystem.Ay_HP.FilterVariable;
            saveObject.FilterSystem_Variable_Ay_HP_LP2 = engine.filtersystem.Ay_HP_LP2.FilterVariable;

            saveObject.FilterSystem_Variable_Az_HP = engine.filtersystem.Az_HP.FilterVariable;
            saveObject.FilterSystem_Variable_Az_HP_LP2 = engine.filtersystem.Az_HP_LP2.FilterVariable;
            saveObject.FilterSystem_Variable_Az_LP3 = engine.filtersystem.Az_LP3.FilterVariable;
        }
        void Save_CompressionSettings_Profile()
        {
            //Dropdowns:
            saveObject.CompressionMethod_Roll_HFC = engine.compressorsystem.CMP_Roll_HFC.Method;
            saveObject.CompressionMethod_Yaw_HFC = engine.compressorsystem.CMP_Yaw_HFC.Method;
            saveObject.CompressionMethod_Pitch_HFC = engine.compressorsystem.CMP_Pitch_HFC.Method;
            saveObject.CompressionMethod_Surge_HFC = engine.compressorsystem.CMP_Surge_HFC.Method;
            saveObject.CompressionMethod_Heave_HFC = engine.compressorsystem.CMP_Heave_HFC.Method;
            saveObject.CompressionMethod_Sway_HFC = engine.compressorsystem.CMP_Sway_HFC.Method;
            saveObject.CompressionMethod_Pitch_LFC = engine.compressorsystem.CMP_Pitch_LFC.Method;
            saveObject.CompressionMethod_Roll_LFC = engine.compressorsystem.CMP_Roll_LFC.Method;
            //Parameters:
            saveObject.CompressionParameter_Roll_HFC = engine.compressorsystem.CMP_Roll_HFC.Parameter;
            saveObject.CompressionParameter_Yaw_HFC = engine.compressorsystem.CMP_Yaw_HFC.Parameter;
            saveObject.CompressionParameter_Pitch_HFC = engine.compressorsystem.CMP_Pitch_HFC.Parameter;
            saveObject.CompressionParameter_Surge_HFC = engine.compressorsystem.CMP_Surge_HFC.Parameter;
            saveObject.CompressionParameter_Heave_HFC = engine.compressorsystem.CMP_Heave_HFC.Parameter;
            saveObject.CompressionParameter_Sway_HFC = engine.compressorsystem.CMP_Sway_HFC.Parameter;
            saveObject.CompressionParameter_Pitch_LFC = engine.compressorsystem.CMP_Pitch_LFC.Parameter;
            saveObject.CompressionParameter_Roll_LFC = engine.compressorsystem.CMP_Roll_LFC.Parameter;
            //Limits:
            saveObject.CompressionLimit_Roll_HFC = engine.compressorsystem.CMP_Roll_HFC.Limit;
            saveObject.CompressionLimit_Yaw_HFC = engine.compressorsystem.CMP_Yaw_HFC.Limit;
            saveObject.CompressionLimit_Pitch_HFC = engine.compressorsystem.CMP_Pitch_HFC.Limit;
            saveObject.CompressionLimit_Surge_HFC = engine.compressorsystem.CMP_Surge_HFC.Limit;
            saveObject.CompressionLimit_Heave_HFC = engine.compressorsystem.CMP_Heave_HFC.Limit;
            saveObject.CompressionLimit_Sway_HFC = engine.compressorsystem.CMP_Sway_HFC.Limit;
            saveObject.CompressionLimit_Pitch_LFC = engine.compressorsystem.CMP_Pitch_LFC.Limit;
            saveObject.CompressionLimit_Roll_LFC = engine.compressorsystem.CMP_Roll_LFC.Limit;
        }
        void Save_ScalerSettings_Profile()
        {
            saveObject.Scaler_Gain_Roll_HFC = engine.scalersystem.SCL_Roll_HFC.Gain;
            saveObject.Scaler_Gain_Yaw_HFC = engine.scalersystem.SCL_Yaw_HFC.Gain;
            saveObject.Scaler_Gain_Pitch_HFC = engine.scalersystem.SCL_Pitch_HFC.Gain;

            saveObject.Scaler_Gain_Surge_HFC = engine.scalersystem.SCL_Surge_HFC.Gain;
            saveObject.Scaler_Gain_Pitch_LFC = engine.scalersystem.SCL_Pitch_LFC.Gain;

            saveObject.Scaler_Gain_Heave_HFC = engine.scalersystem.SCL_Heave_HFC.Gain;

            saveObject.Scaler_Gain_Sway_HFC = engine.scalersystem.SCL_Sway_HFC.Gain;
            saveObject.Scaler_Gain_Roll_LFC = engine.scalersystem.SCL_Roll_LFC.Gain;
        }
        void Save_ZeromakerSettings_Profile()
        {
            saveObject.Zero_Roll_HFC = engine.zeromaker.Zero_RollHFC;
            saveObject.Zero_Yaw_HFC = engine.zeromaker.Zero_YawHFC;
            saveObject.Zero_Pitch_HFC = engine.zeromaker.Zero_PitchHFC;

            saveObject.Zero_Surge_HFC = engine.zeromaker.Zero_SurgeHFC;
            saveObject.Zero_Pitch_LFC = engine.zeromaker.Zero_PitchLFC;

            saveObject.Zero_Heave_HFC = engine.zeromaker.Zero_HeaveHFC;

            saveObject.Zero_Sway_HFC = engine.zeromaker.Zero_SwayHFC;
            saveObject.Zero_Roll_LFC = engine.zeromaker.Zero_RollLFC;
        }
        void Save_DOF_Override_Profile()
        {
            saveObject.RangeRollHFC     = engine.dof_override.RangeRollHFC;
            saveObject.RangeYawHFC      = engine.dof_override.RangeYawHFC;
            saveObject.RangePitchHFC    = engine.dof_override.RangePitchHFC;
            saveObject.RangeSurgeHFC    = engine.dof_override.RangeSurgeHFC;
            saveObject.RangeHeaveHFC    = engine.dof_override.RangeHeaveHFC;
            saveObject.RangeSwayHFC     = engine.dof_override.RangeSwayHFC;
            saveObject.RangePitchLFC    = engine.dof_override.RangePitchLFC;
            saveObject.RangeRollLFC     = engine.dof_override.RangeRollLFC;
        }
        void Save_AASDTalker_Settings_Profile()
        {
            saveObject.AASDTalker_ControllerType = engine.aasd_talker.Controller;
        }
        void Save_ODriveTalker_Settings_Profile()
        {
            saveObject.FormatString_1   = engine.odrivesystem.FormatString_1;
            saveObject.FormatString_2   = engine.odrivesystem.FormatString_2;
            saveObject.FormatString_3   = engine.odrivesystem.FormatString_3;
            saveObject.Lead             = engine.odrivesystem.Lead;
        }
        #endregion

        //---------- Helpers ----------
        private OpenFileDialog MyOpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "YAME files|*.yame",
                DefaultExt = ".yame",
                AddExtension = true,
                CheckPathExists = true,
                DereferenceLinks = true,
                RestoreDirectory = true,        //Only used if no InitialDirectory is used!
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            };

            //Saved Games:
            ofd.InitialDirectory += @"\Saved Games\YAME Motion Engine\Profiles";
            //APP Data:
            //ofd.InitialDirectory += "\\AppData\\Roaming\\MOTUS";

            if (!Directory.Exists(ofd.InitialDirectory)) Directory.CreateDirectory(ofd.InitialDirectory);

            return ofd;
        }
        private SaveFileDialog MySaveFileDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "YAME files|*.yame",
                DefaultExt = ".yame",
                AddExtension = true,
                OverwritePrompt = true,
                CreatePrompt = false,
                //Saved Games:
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                                    @"\Saved Games\YAME Motion Engine\Profiles",
                //APP Data:
                //InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                //                    "\\AppData\\Local\\HexaGo",
            };

            if (!Directory.Exists(sfd.InitialDirectory)) Directory.CreateDirectory(sfd.InitialDirectory);

            return sfd;
        }
    }
}
