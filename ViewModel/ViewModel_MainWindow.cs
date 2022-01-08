using YAME.Model;
using YAME.Viewmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.ViewModel
{
    public class ViewModel_MainWindow : _ViewModel
    {
         string _tilte_string = "Main Window";
        public string TitleString
        {
            get { return _tilte_string; }
            set { _tilte_string = value; OnPropertyChanged(nameof(TitleString)); }
        }

        const string _version = "v0.04_alpha_";
        public string Version
        {
            get { return _version; }
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
                //float adoption = 0.1f;
                float adoption = 1.0f;
                _deltatime_processing = value * adoption + _deltatime_processing * (1-adoption);  //Slight LP filtering :-)
                FrameRate = (int)(1000 / DeltaTime_Processing);
                OnPropertyChanged(nameof(DeltaTime_Processing)); 
            }
        }

        public ViewModel_MainWindow(Engine e)
        {
            base.engine = e;
        }
    }
}
