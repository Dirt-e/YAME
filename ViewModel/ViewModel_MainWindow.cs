using MOTUS.Model;
using MOTUS.Viewmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.ViewModel
{
    public class ViewModel_MainWindow : _ViewModel
    {
        private float _framerate;

        public float FrameRate
        {
            get { return _framerate; }
            set { _framerate = value; OnPropertyChanged(nameof(FrameRate)); }
        }

        float _deltatime_processing;
        public float DeltaTime_Processing
        {
            get { return _deltatime_processing; }
            set
            {
                float adoption = 0.01f;
                _deltatime_processing = value * adoption + _deltatime_processing * (1-adoption);  //Slight LP filtering :-)
                FrameRate = 1000 / DeltaTime_Processing;
                OnPropertyChanged(nameof(DeltaTime_Processing)); 
            }
        }

        public ViewModel_MainWindow(Engine e)
        {
            base.engine = e;
        }
    }
}
