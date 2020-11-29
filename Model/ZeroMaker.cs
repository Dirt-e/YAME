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
        public DOF_Data Input = new DOF_Data();
        public DOF_Data Output = new DOF_Data();

        #region values to bind to
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

        #region Zero switches
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

        //#region Isolate switches
        //bool _isolate_rollHFC;
        //public bool Isolate_RollHFC
        //{
        //    get { return _isolate_rollHFC; }
        //    set
        //    {
        //        _isolate_rollHFC = value;
        //        OnPropertyChanged("Isolate_RollHFC");
        //    }
        //}
        //bool _isolate_PitchHFC;
        //public bool Isolate_PitchHFC
        //{
        //    get { return _isolate_PitchHFC; }
        //    set
        //    {
        //        _isolate_PitchHFC = value;
        //        OnPropertyChanged("Isolate_PitchHFC");
        //    }
        //}
        //bool _isolate_yawHFC;
        //public bool Isolate_YawHFC
        //{
        //    get { return _isolate_yawHFC; }
        //    set
        //    {
        //        _isolate_yawHFC = value;
        //        OnPropertyChanged("Isolate_YawHFC");
        //    }
        //}

        //bool _isolate_surgeHFC;
        //public bool Isolate_SurgeHFC
        //{
        //    get { return _isolate_surgeHFC; }
        //    set
        //    {
        //        _isolate_surgeHFC = value;
        //        OnPropertyChanged("Isolate_SurgeHFC");
        //    }
        //}
        //bool _isolate_pitchLFC;
        //public bool Isolate_PitchLFC
        //{
        //    get { return _isolate_pitchLFC; }
        //    set
        //    {
        //        _isolate_pitchLFC = value;
        //        OnPropertyChanged("Isolate_PitchLFC");
        //    }
        //}

        //bool _isolate_heaveHFC;
        //public bool Isolate_HeavelHFC
        //{
        //    get { return _isolate_heaveHFC; }
        //    set
        //    {
        //        _isolate_heaveHFC = value;
        //        OnPropertyChanged("Isolate_HeavelHFC");
        //    }
        //}

        //bool _isolate_swayHFC;
        //public bool Isolate_SwayHFC
        //{
        //    get { return _isolate_swayHFC; }
        //    set
        //    {
        //        _isolate_swayHFC = value;
        //        OnPropertyChanged("Isolate_SwayHFC");
        //    }
        //}
        //bool _isolate_rollLFC;
        //public bool Isolate_RollLFC
        //{
        //    get { return _isolate_rollLFC; }
        //    set
        //    {
        //        _isolate_rollLFC = value;
        //        OnPropertyChanged("Isolate_RollLFC");
        //    }
        //}
        //#endregion

        public void Process(DOF_Data data)
        {
            Input = new DOF_Data(data);

            NullDataAsNeeded();
        }

        private void NullDataAsNeeded()
        {
            //Copy all values over
            Output = new DOF_Data(Input);

            //And then modify some
            if (Zero_RollHFC) { Output.HFC_Roll = 0; }
            if (Zero_YawHFC) { Output.HFC_Yaw = 0; }
            if (Zero_PitchHFC) { Output.HFC_Pitch = 0; }

            if (Zero_SurgeHFC) { Output.HFC_Surge = 0; }
            if (Zero_PitchLFC) { Output.LFC_Pitch = 0; }

            if (Zero_HeavelHFC) { Output.HFC_Heave = 0; }

            if (Zero_SwayHFC) { Output.HFC_Sway = 0; }
            if (Zero_RollLFC) { Output.LFC_Roll = 0; }

            //Make them show up in the UI
            RollHFC     = Output.HFC_Roll;
            YawHFC      = Output.HFC_Yaw;
            PitchHFC    = Output.HFC_Pitch;

            SurgeHFC    = Output.HFC_Surge;
            PitchLFC    = Output.LFC_Pitch;

            HeaveHFC    = Output.HFC_Heave;

            SwayHFC     = Output.HFC_Sway;
            RollLFC     = Output.LFC_Roll;
        }


        //INotifyPropertyChanged:
        public event PropertyChangedEventHandler PropertyChanged;
        private protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
