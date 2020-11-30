using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MOTUS.Model
{
    public class DOF_Override : INotifyPropertyChanged
    {
        public DOF_Data Input = new DOF_Data();
        public DOF_Data Output = new DOF_Data();

        #region ViewModel Sliders
        float _rollHFC;
        public float RollHFC
        {
            get { return _rollHFC; }
            set { _rollHFC = value; OnPropertyChanged("RollHFC"); }
        }
        float _yawHFC;
        public float YawHFC
        {
            get { return _yawHFC; }
            set { _yawHFC = value; OnPropertyChanged("YawHFC"); }
        }
        float _pitchHFC;
        public float PitchHFC
        {
            get { return _pitchHFC; }
            set { _pitchHFC = value; OnPropertyChanged("PitchHFC"); }
        }

        float _surgeHFC;
        public float SurgeHFC
        {
            get { return _surgeHFC; }
            set { _surgeHFC = value; OnPropertyChanged("SurgeHFC"); }
        }
        float _pitchLFC;
        public float PitchLFC
        {
            get { return _pitchLFC; }
            set { _pitchLFC = value; OnPropertyChanged("PitchLFC"); }
        }

        float _heaveHFC;
        public float HeaveHFC
        {
            get { return _heaveHFC; }
            set { _heaveHFC = value; OnPropertyChanged("HeaveHFC"); }
        }

        float _swayHFC;
        public float SwayHFC
        {
            get { return _swayHFC; }
            set { _swayHFC = value; OnPropertyChanged("SwayHFC"); }
        }
        float _rollLFC;
        public float RollLFC
        {
            get { return _rollLFC; }
            set { _rollLFC = value; OnPropertyChanged("RollLFC"); }
        }
        #endregion
        #region ViewModel MaxValues
        float _max_rollHFC;
        public float MaxRollHFC
        {
            get { return _max_rollHFC; }
            set { _max_rollHFC = value; OnPropertyChanged("MaxRollHFC"); MinRollHFC = -MaxRollHFC; }
        }
        float _max_yawHFC;
        public float MaxYawHFC
        {
            get { return _max_yawHFC; }
            set { _max_yawHFC = value; OnPropertyChanged("MaxYawHFC"); MinYawHFC = -MaxYawHFC; }
        }
        float _max_pitchHFC;
        public float MaxPitchHFC
        {
            get { return _max_pitchHFC; }
            set { _max_pitchHFC = value; OnPropertyChanged("MaxPitchHFC"); MinPitchHFC = -MaxPitchHFC; }
        }

        float _max_surgeHFC;
        public float MaxSurgeHFC
        {
            get { return _max_surgeHFC; }
            set { _max_surgeHFC = value; OnPropertyChanged("MaxSurgeHFC"); MinSurgeHFC = -MaxSurgeHFC; }
        }
        float _max_pitchLFC;
        public float MaxPitchLFC
        {
            get { return _max_pitchLFC; }
            set { _max_pitchLFC = value; OnPropertyChanged("MaxPitchLFC"); MinPitchLFC = -MaxPitchLFC; }
        }

        float _max_heaveHFC;
        public float MaxHeaveHFC
        {
            get { return _max_heaveHFC; }
            set { _max_heaveHFC = value; OnPropertyChanged("MaxHeaveHFC"); MinHeaveHFC = -MaxHeaveHFC; }
        }

        float _max_swayHFC;
        public float MaxSwayHFC
        {
            get { return _max_swayHFC; }
            set { _max_swayHFC = value; OnPropertyChanged("MaxSwayHFC"); MinSwayHFC = -MaxSwayHFC; }
        }
        float _max_rollLFC;
        public float MaxRollLFC
        {
            get { return _max_rollLFC; }
            set { _max_rollLFC = value; OnPropertyChanged("MaxRollLFC"); MinRollLFC = -MaxRollLFC; }
        }
        #endregion
        #region ViewModel MinValues
        float _min_rollHFC;
        public float MinRollHFC
        {
            get { return _min_rollHFC; }
            set { _min_rollHFC = value; OnPropertyChanged("MinRollHFC"); }
        }
        float _min_yawHFC;
        public float MinYawHFC
        {
            get { return _min_yawHFC; }
            set { _min_yawHFC = value; OnPropertyChanged("MinYawHFC"); }
        }
        float _min_pitchHFC;
        public float MinPitchHFC
        {
            get { return _min_pitchHFC; }
            set { _min_pitchHFC = value; OnPropertyChanged("MinPitchHFC"); }
        }

        float _min_surgeHFC;
        public float MinSurgeHFC
        {
            get { return _min_surgeHFC; }
            set { _min_surgeHFC = value; OnPropertyChanged("MinSurgeHFC"); }
        }
        float _min_pitchLFC;
        public float MinPitchLFC
        {
            get { return _min_pitchLFC; }
            set { _min_pitchLFC = value; OnPropertyChanged("MinPitchLFC"); }
        }

        float _min_heaveHFC;
        public float MinHeaveHFC
        {
            get { return _min_heaveHFC; }
            set { _min_heaveHFC = value; OnPropertyChanged("MinHeaveHFC"); }
        }

        float _min_swayHFC;
        public float MinSwayHFC
        {
            get { return _min_swayHFC; }
            set { _min_swayHFC = value; OnPropertyChanged("MinSwayHFC"); }
        }
        float _min_rollLFC;
        public float MinRollLFC
        {
            get { return _min_rollLFC; }
            set { _min_rollLFC = value; OnPropertyChanged("MinRollLFC"); }
        }
        #endregion

        public void Process(DOF_Data data)
        {
            Input = new DOF_Data(data);
            RememberNewMaximumValues();
            SetSliderValues();
            PassThrough();
        }
         
        private void RememberNewMaximumValues()
        {
            MaxRollHFC  = Max(Abs(Input.HFC_Roll), Abs(MaxRollHFC));
            MaxYawHFC   = Max(Abs(Input.HFC_Yaw), Abs(MaxYawHFC));
            MaxPitchHFC = Max(Abs(Input.HFC_Pitch), Abs(MaxPitchHFC));

            MaxSurgeHFC = Max(Abs(Input.HFC_Surge), Abs(MaxSurgeHFC));
            MaxPitchLFC = Max(Abs(Input.LFC_Pitch), Abs(MaxPitchLFC));

            MaxHeaveHFC = Max(Abs(Input.HFC_Heave), Abs(MaxHeaveHFC));

            MaxSwayHFC  = Max(Abs(Input.HFC_Sway), Abs(MaxSwayHFC));
            MaxRollLFC  = Max(Abs(Input.LFC_Roll), Abs(MaxRollLFC));
        }
        private void SetSliderValues()
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

        public event PropertyChangedEventHandler PropertyChanged;
        private protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
