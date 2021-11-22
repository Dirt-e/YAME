using MOTUS.Model;
using MOTUS.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace MOTUS.ViewModel
{
    public class ViewModel_MotionControlWindow : _ViewModel
    {
        public ViewModel_MotionControlWindow(Engine e)
        {
            engine = e;
        }

        SolidColorBrush _btnMotionColor = new SolidColorBrush(Colors.DimGray);
        public SolidColorBrush BtnMotionColor
        {
            get { return _btnMotionColor; }
            set { _btnMotionColor = value; OnPropertyChanged("BtnMotionColor"); }
        }
        SolidColorBrush _btnPauseColor = new SolidColorBrush(Colors.DimGray);
        public SolidColorBrush BtnPauseColor
        {
            get { return _btnPauseColor; }
            set { _btnPauseColor = value; OnPropertyChanged("BtnPauseColor"); }
        }
        SolidColorBrush _btnParkColor = new SolidColorBrush(Colors.DimGray);
        public SolidColorBrush BtnParkColor
        {
            get { return _btnParkColor; }
            set { _btnParkColor = value; OnPropertyChanged("BtnParkColor"); }
        }

        SolidColorBrush _btnMotion_ForegroundColor = new SolidColorBrush(Colors.DimGray);
        public SolidColorBrush BtnMotion_ForegroundColor
        {
            get { return _btnMotion_ForegroundColor; }
            set { _btnMotion_ForegroundColor = value; OnPropertyChanged("BtnMotion_ForegroundColor"); }
        }
        SolidColorBrush _btnPause_ForegroundColor = new SolidColorBrush(Colors.Black);
        public SolidColorBrush BtnPause_ForegroundColor
        {
            get { return _btnPause_ForegroundColor; }
            set { _btnPause_ForegroundColor = value; OnPropertyChanged("BtnPause_ForegroundColor"); }
        }
        SolidColorBrush _btnPark_ForegroundColor = new SolidColorBrush(Colors.LightGreen);
        public SolidColorBrush BtnPark_ForegroundColor
        {
            get { return _btnPark_ForegroundColor; }
            set { _btnPark_ForegroundColor = value; OnPropertyChanged("BtnPark_ForegroundColor"); }
        }

        Cursor _cursor_motion;
        public Cursor CursorMotion
        {
            get { return _cursor_motion; }
            set
            {
                _cursor_motion = value;
                OnPropertyChanged("CursorMotion");
            }
        }
        Cursor _cursor_pause;
        public Cursor CursorPause
        {
            get { return _cursor_pause; }
            set
            {
                _cursor_pause = value;
                OnPropertyChanged("CursorPause");
            }
        }
        Cursor _cursor_park;
        public Cursor CursorPark
        {
            get { return _cursor_park; }
            set
            {
                _cursor_park = value;
                OnPropertyChanged("CursorPark");
            }
        }

    }
}
