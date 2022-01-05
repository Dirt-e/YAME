using YAME.Model;
using YAME.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace YAME.ViewModel
{
    public class ViewModel_CrashDetector : _ViewModel
    {
        float _excceedance_value_displayed_ax = float.NaN;
        public float ExceedanceValue_displayed_Ax
        {
            get { return _excceedance_value_displayed_ax; }
            set
            {
                _excceedance_value_displayed_ax = value;
                OnPropertyChanged(nameof(ExceedanceValue_displayed_Ax));
            }
        }
        float _excceedance_value_displayed_ay = float.NaN;
        public float ExceedanceValue_displayed_Ay
        {
            get { return _excceedance_value_displayed_ay; }
            set
            {
                _excceedance_value_displayed_ay = value;
                OnPropertyChanged(nameof(ExceedanceValue_displayed_Ay));
            }
        }
        float _excceedance_value_displayed_az = float.NaN;
        public float ExceedanceValue_displayed_Az
        {
            get { return _excceedance_value_displayed_az; }
            set
            {
                _excceedance_value_displayed_az = value;
                OnPropertyChanged(nameof(ExceedanceValue_displayed_Az));
            }
        }
        float _excceedance_value_displayed_wx = float.NaN;
        public float ExceedanceValue_displayed_Wx
        {
            get { return _excceedance_value_displayed_wx; }
            set
            {
                _excceedance_value_displayed_wx = value;
                OnPropertyChanged(nameof(ExceedanceValue_displayed_Wx));
            }
        }
        float _excceedance_value_displayed_wy = float.NaN;
        public float ExceedanceValue_displayed_Wy
        {
            get { return _excceedance_value_displayed_wy; }
            set
            {
                _excceedance_value_displayed_wy = value;
                OnPropertyChanged(nameof(ExceedanceValue_displayed_Wy));
            }
        }
        float _excceedance_value_displayed_wz = float.NaN;
        public float ExceedanceValue_displayed_Wz
        {
            get { return _excceedance_value_displayed_wz; }
            set
            {
                _excceedance_value_displayed_wz = value;
                OnPropertyChanged(nameof(ExceedanceValue_displayed_Wz));
            }
        }

        bool _visible_exc_ax;
        public bool Visible_exc_ax
        {
            get { return _visible_exc_ax; }
            set
            {
                _visible_exc_ax = value;
                OnPropertyChanged(nameof(Visible_exc_ax));
            }
        }
        bool _visible_exc_ay;
        public bool Visible_exc_ay
        {
            get { return _visible_exc_ay; }
            set
            {
                _visible_exc_ay = value;
                OnPropertyChanged(nameof(Visible_exc_ay));
            }
        }
        bool _visible_exc_az;
        public bool Visible_exc_az
        {
            get { return _visible_exc_az; }
            set
            {
                _visible_exc_az = value;
                OnPropertyChanged(nameof(Visible_exc_az));
            }
        }
        bool _visible_exc_wx;
        public bool Visible_exc_wx
        {
            get { return _visible_exc_wx; }
            set
            {
                _visible_exc_wx = value;
                OnPropertyChanged(nameof(Visible_exc_wx));
            }
        }
        bool _visible_exc_wy;
        public bool Visible_exc_wy
        {
            get { return _visible_exc_wy; }
            set
            {
                _visible_exc_wy = value;
                OnPropertyChanged(nameof(Visible_exc_wy));
            }
        }
        bool _visible_exc_wz;
        public bool Visible_exc_wz
        {
            get { return _visible_exc_wz; }
            set
            {
                _visible_exc_wz = value;
                OnPropertyChanged(nameof(Visible_exc_wz));
            }
        }

        //"CRASHED" light
        SolidColorBrush _textcolor;
        public SolidColorBrush TextColor
        {
            get { return _textcolor; }
            set { _textcolor = value; OnPropertyChanged(nameof(TextColor)); }
        }
        SolidColorBrush _lightcolor;
        public SolidColorBrush LightColor
        {
            get { return _lightcolor; }
            set { _lightcolor = value; OnPropertyChanged(nameof(LightColor)); }
        }

        string _line1;
        public string Line1
        {
            get { return _line1; }
            set { _line1 = value; OnPropertyChanged(nameof(Line1)); }
        }
        string _line2;
        public string Line2
        {
            get { return _line2; }
            set { _line2 = value; OnPropertyChanged(nameof(Line2)); }
        }

        public ViewModel_CrashDetector(Engine e)
        {
            base.engine = e;
        }
    }
}
