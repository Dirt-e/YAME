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

        //These determine everything:
        bool _inmotionposition = false;
        public bool InMotionPosition
        {
            get { return _inmotionposition; }
            set
            {
                _inmotionposition = value;

                BtnMotion_ForegroundColor = new SolidColorBrush(Colors.LightGreen);
                BtnPause_ForegroundColor = new SolidColorBrush(Colors.Black);
                BtnPark_ForegroundColor = new SolidColorBrush(Colors.Black);

                CursorMotion = Cursors.Arrow;
                CursorPause = Cursors.Hand;
                CursorPark = Cursors.No;

                OnPropertyChanged("InMotionPosition");
            }
        }
        bool _inpauseposition = false;
        public bool InPausePosition
        {
            get { return _inpauseposition; }
            set
            {
                _inpauseposition = value;

                BtnMotion_ForegroundColor = new SolidColorBrush(Colors.Black);
                BtnPause_ForegroundColor = new SolidColorBrush(Colors.LightGreen);
                BtnPark_ForegroundColor = new SolidColorBrush(Colors.Black);

                CursorMotion = Cursors.Hand;
                CursorPause = Cursors.Arrow;
                CursorPark = Cursors.Hand;

                OnPropertyChanged("InPausePosition");
            }
        }
        bool _inparkposition = true;
        public bool InParkPosition
        {
            get { return _inparkposition; }
            set
            {
                _inparkposition = value;

                BtnMotion_ForegroundColor = new SolidColorBrush(Colors.Black);
                BtnPause_ForegroundColor = new SolidColorBrush(Colors.Black);
                BtnPark_ForegroundColor = new SolidColorBrush(Colors.LightGreen);

                CursorMotion = Cursors.No;
                CursorPause = Cursors.Hand;
                CursorPark = Cursors.Arrow;

                OnPropertyChanged("InParkPosition");
            }
        }
        bool _intransit = true;
        public bool InTransit
        {
            get { return _intransit; }
            set
            {
                _intransit = value;

                BtnMotion_ForegroundColor = new SolidColorBrush(Colors.DarkOrange);
                BtnPause_ForegroundColor = new SolidColorBrush(Colors.DarkOrange);
                BtnPark_ForegroundColor = new SolidColorBrush(Colors.DarkOrange);

                CursorMotion = Cursors.Wait;
                CursorPause = Cursors.Wait;
                CursorPark = Cursors.Wait;

                OnPropertyChanged("InTransit");
            }
        }

        public ViewModel_MotionControlWindow(Engine e)
        {
            base.engine = e;
        }
    }
}
