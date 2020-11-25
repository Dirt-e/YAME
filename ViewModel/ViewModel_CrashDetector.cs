using MOTUS.Model;
using MOTUS.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MOTUS.ViewModel
{
    public class ViewModel_CrashDetector : _ViewModel
    {
        float _ax_crashtrigger;
        public float AX_CrashTrigger
        {
            get
            {
                return _ax_crashtrigger;
            }
            set
            {
                _ax_crashtrigger = value;
                engine.crashdetector.Ax_Crashtrigger = value;
                OnPropertyChanged("AX_CrashTrigger");
            }
        }
        float _ay_crashtrigger;
        public float AY_CrashTrigger
        {
            get
            {
                return _ay_crashtrigger;
            }
            set
            {
                _ay_crashtrigger = value;
                engine.crashdetector.Ay_Crashtrigger = value;
                OnPropertyChanged("AY_CrashTrigger");
            }
        }
        float _az_crashtrigger;
        public float AZ_CrashTrigger
        {
            get
            {
                return _az_crashtrigger;
            }
            set
            {
                _az_crashtrigger = value;
                engine.crashdetector.Az_Crashtrigger = value;
                OnPropertyChanged("AZ_CrashTrigger");
            }
        }

        float _wx_crashtrigger;
        public float WX_CrashTrigger
        {
            get
            {
                return _wx_crashtrigger;
            }
            set
            {
                _wx_crashtrigger = value;
                engine.crashdetector.Wx_Crashtrigger = value;
                OnPropertyChanged("WX_CrashTrigger");
            }
        }
        float _wy_crashtrigger;
        public float WY_CrashTrigger
        {
            get
            {
                return _wy_crashtrigger;
            }
            set
            {
                _wy_crashtrigger = value;
                engine.crashdetector.Wy_Crashtrigger = value;
                OnPropertyChanged("WY_CrashTrigger");
            }
        }
        float _wz_crashtrigger;
        public float WZ_CrashTrigger
        {
            get
            {
                return _wz_crashtrigger;
            }
            set
            {
                _wz_crashtrigger = value;
                engine.crashdetector.Wz_Crashtrigger = value;
                OnPropertyChanged("WZ_CrashTrigger");
            }
        }

        SolidColorBrush _textcolor ;
        public SolidColorBrush TextColor
        {
            get { return _textcolor; }
            set
            {
                _textcolor = value;
                OnPropertyChanged("TextColor");
            }
        }
        SolidColorBrush _lightcolor;
        public SolidColorBrush LightColor
        {
            get  { return _lightcolor; }
            set
            {
                _lightcolor = value;
                OnPropertyChanged("LightColor");
            }
        }

        public ViewModel_CrashDetector(Engine e)
        {
            base.engine = e;
        }
    }
}
