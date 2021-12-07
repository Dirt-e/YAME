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
        string _tilte_string;
        public string TitleString
        {
            get { return _tilte_string; }
            set { _tilte_string = value; OnPropertyChanged(nameof(TitleString)); }
        }

        int _framerate;
        public int FrameRate
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
                //float adoption = 1.0f;
                _deltatime_processing = value * adoption + _deltatime_processing * (1-adoption);  //Slight LP filtering :-)
                FrameRate = (int)(1000 / DeltaTime_Processing);
                TitleString = $"YAME running @{FrameRate} FPS";
                OnPropertyChanged(nameof(DeltaTime_Processing)); 
            }
        }

        public ViewModel_MainWindow(Engine e)
        {
            base.engine = e;
        }
    }
}
