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
