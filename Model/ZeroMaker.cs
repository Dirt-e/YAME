using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class ZeroMaker : INotifyPropertyChanged
    {
        public DOF_Data source = new DOF_Data();
        public DOF_Data Output = new DOF_Data();

        #region values to bind to
        float _rollHFC = 0.5f;
        public float RollHFC
        {
            get { return _rollHFC; }
            set { _rollHFC = value; OnPropertyChanged("RollHFC"); }
        }
        float _yawHFC = 0.1f;
        public float YawHFC
        {
            get { return _yawHFC; }
            set { _yawHFC = value; OnPropertyChanged("YawHFC"); }
        }
        float _pitchHFC = 0.1f;
        public float PitchHFC
        {
            get { return _pitchHFC; }
            set { _pitchHFC = value; OnPropertyChanged("PitchHFC"); }
        }

        float _surgeHFC = 0.1f;
        public float SurgeHFC
        {
            get { return _surgeHFC; }
            set { _surgeHFC = value; OnPropertyChanged("SurgeHFC"); }
        }
        float _pitchLFC = 0.1f;
        public float PitchLFC
        {
            get { return _pitchLFC; }
            set { _pitchLFC = value; OnPropertyChanged("PitchLFC"); }
        }

        float _heaveHFC = 0.1f;
        public float HeaveHFC
        {
            get { return _heaveHFC; }
            set { _heaveHFC = value; OnPropertyChanged("HeaveHFC"); }
        }

        float _swayHFC = 0.1f;
        public float SwayHFC
        {
            get { return _swayHFC; }
            set { _swayHFC = value; OnPropertyChanged("SwayHFC"); }
        }
        float _rollLFC = 0.1f;
        public float RollLFC
        {
            get { return _rollLFC; }
            set { _rollLFC = value; OnPropertyChanged("RollLFC"); }
        }
        #endregion

        #region bool-switches
        bool _zero_rollHFC;
        public bool Zero_RollHFC
        {
            get { return _zero_rollHFC; }
            set
            {
                _zero_rollHFC = value;
                OnPropertyChanged("Zero_RollHFC");
            }
        }
        bool _zero_PitchHFC;
        public bool Zero_PitchHFC
        {
            get { return _zero_PitchHFC; }
            set
            {
                _zero_PitchHFC = value;
                OnPropertyChanged("Zero_PitchHFC");
            }
        }
        bool _zero_yawHFC;
        public bool Zero_YawHFC
        {
            get { return _zero_yawHFC; }
            set
            {
                _zero_yawHFC = value;
                OnPropertyChanged("Zero_YawHFC");
            }
        }

        bool _zero_surgeHFC;
        public bool Zero_SurgeHFC
        {
            get { return _zero_surgeHFC; }
            set
            {
                _zero_surgeHFC = value;
                OnPropertyChanged("Zero_SurgeHFC");
            }
        }
        bool _zero_pitchLFC;
        public bool Zero_PitchLFC
        {
            get { return _zero_pitchLFC; }
            set
            {
                _zero_pitchLFC = value;
                OnPropertyChanged("Zero_PitchLFC");
            }
        }

        bool _zero_heaveHFC;
        public bool Zero_HeavelHFC
        {
            get { return _zero_heaveHFC; }
            set
            {
                _zero_heaveHFC = value;
                OnPropertyChanged("Zero_HeavelHFC");
            }
        }

        bool _zero_swayHFC;
        public bool Zero_SwayHFC
        {
            get { return _zero_swayHFC; }
            set
            {
                _zero_swayHFC = value;
                OnPropertyChanged("Zero_SwayHFC");
            }
        }
        bool _zero_rollLFC;
        public bool Zero_RollLFC
        {
            get { return _zero_rollLFC; }
            set
            {
                _zero_rollLFC = value;
                OnPropertyChanged("Zero_RollLFC");
            }
        }
        #endregion

        public override void Start()
        {
            //NIL
        }
        public override void Update()
        {
            GrabSourceData();
            NullDataAsNeeded();
        }

        private void GrabSourceData()
        {
            source = new DOF_Data(mainwindow.mainprocessor.scalersystem.Output);
        }

        private void NullDataAsNeeded()
        {
            Output = new DOF_Data(source);

            RollHFC = source.HFC_Roll;
            YawHFC = source.HFC_Yaw;
            PitchHFC = source.HFC_Pitch;

            SurgeHFC = source.HFC_Surge;
            PitchLFC = source.LFC_Pitch;

            HeaveHFC = source.HFC_Heave;

            SwayHFC = source.HFC_Sway;
            RollLFC = source.LFC_Roll;

            //And then modify some
            if (Zero_RollHFC) { Output.HFC_Roll = 0; }
            if (Zero_YawHFC) { Output.HFC_Yaw = 0; }
            if (Zero_PitchHFC) { Output.HFC_Pitch = 0; }

            if (Zero_SurgeHFC) { Output.HFC_Surge = 0; }
            if (Zero_PitchLFC) { Output.LFC_Pitch = 0; }

            if (Zero_HeavelHFC) { Output.HFC_Heave = 0; }

            if (Zero_SwayHFC) { Output.HFC_Sway = 0; }
            if (Zero_RollLFC) { Output.LFC_Roll = 0; }
        }


        //INotifyPropertyChanged:
        public event PropertyChangedEventHandler PropertyChanged;
        private protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
