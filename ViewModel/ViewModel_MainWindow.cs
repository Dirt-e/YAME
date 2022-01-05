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
                float adoption = 0.1f;
                //float adoption = 1.0f;
                _deltatime_processing = value * adoption + _deltatime_processing * (1-adoption);  //Slight LP filtering :-)
                FrameRate = (int)(1000 / DeltaTime_Processing);
                TitleString = $"YAME running @{FrameRate} FPS";
                OnPropertyChanged(nameof(DeltaTime_Processing)); 
            }
        }
        //bool _is_checked_EventDriven;       //Unused
        //public bool IsChecked_EventDriven
        //{
        //    get { return _is_checked_EventDriven;}
        //    set { _is_checked_EventDriven = value; OnPropertyChanged(nameof(IsChecked_EventDriven)); }
        //}
        //bool _is_checked_short_sleep;
        //public bool IsChecked_ShortSleep
        //{
        //    get { return _is_checked_short_sleep; }
        //    set { _is_checked_short_sleep = value; OnPropertyChanged(nameof(IsChecked_ShortSleep)); }
        //}
        //bool _is_checked_hot_idle = true;
        //public bool IsChecked_HotIdle
        //{
        //    get { return _is_checked_hot_idle; }
        //    set { _is_checked_hot_idle = value; OnPropertyChanged(nameof(IsChecked_HotIdle)); }
        //}

        public ViewModel_MainWindow(Engine e)
        {
            base.engine = e;
        }
    }
}
