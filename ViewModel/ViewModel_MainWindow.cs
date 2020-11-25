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
        float _deltatime_processing;
        public float DeltaTime_Processing
        {
            get { return _deltatime_processing; }
            set
            {
                float adoption = 0.01f;
                _deltatime_processing = value * adoption + _deltatime_processing * (1-adoption);  //Slight LP filtering :-)
                OnPropertyChanged("DeltaTime_Processing"); 
            }
        }

        public ViewModel_MainWindow(Engine e)
        {
            base.engine = e;
        }
    }
}
