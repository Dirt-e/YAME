using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace YAME.Model
{
    public class SnappyDragger
    {
        Window window = null;
        int SnapDist = 10;

        DispatcherTimer timer = new DispatcherTimer
        {
            Interval = new TimeSpan(0, 0, 0, 0, 10),
        };

        Point MouseStartPoint   = new Point();
        Point MouseCurrentPoint = new Point();
        Point WindowStartPoint  = new Point();
        Vector delta            = new Vector();                         //In screen resolution pixels!

        //Virtual positions of dragged window:
        double vLEFT;
        double vTOP;
        double vRIGHT;
        double vBOTTOM;

        public SnappyDragger(Window w)
        {
            window = w;

            timer.Tick += delegate
            {
                UpdateWindow();
            };
        }

        public void StartDrag()
        {
            MouseStartPoint = GetMousePosition_ScreenRes();             //In screen resolution pixels!
            WindowStartPoint = new Point(window.Left, window.Top);      //In screen resolution pixels!

            timer.Start();
        }
        public void StopDrag()
        {
            timer.Stop();   
        }

        void UpdateWindow()
        {
            MouseCurrentPoint = GetMousePosition_ScreenRes();     
            delta = (MouseCurrentPoint - MouseStartPoint);              //In screen resolution pixels!
            
            UpdateVirtualWindowPosition();

            double x = closestLateralSnap();
            double y = closestVerticalSnap();

            if (Math.Abs(x) < SnapDist) window.Left = vLEFT + x;
            else                        window.Left = vLEFT;

            if (Math.Abs(y) < SnapDist) window.Top = vTOP + y;
            else                        window.Top = vTOP;
        }

        //------- Helpers -------
        Point GetMousePosition_ScreenRes()
        {
            var point = Control.MousePosition;                          //In physical pixels (int)
            double x = (double)point.X;                                 //In physical pixels (double)
            double y = (double)point.Y;

            DpiScale scale = VisualTreeHelper.GetDpi(window);

            x /= scale.DpiScaleX;
            y /= scale.DpiScaleY;

            return new Point(x, y);                                     //In screen pixels (double)
        }
        private void UpdateVirtualWindowPosition()
        {
            vLEFT   = WindowStartPoint.X + delta.X;
            vTOP    = WindowStartPoint.Y + delta.Y;
            vRIGHT  = vLEFT + window.Width;
            vBOTTOM = vTOP + window.Height;
        }

        List<Window> WindowsWithLateralOverlap()
        {
            List<Window> WindowsWithLateralOverlap = new List<Window>();

            MainWindow mw = System.Windows.Application.Current.MainWindow as MainWindow;

            foreach (Window w in mw.OwnedWindows)
            {
                if (HasLateralOverlapWith(w) && w.Name != window.Name)
                {
                    WindowsWithLateralOverlap.Add(w);
                }
            }

            return WindowsWithLateralOverlap;
        }
        List<Window> WindowsWithVerticalOverlap()
        {
            List<Window> WindowsWithVerticalOverlap = new List<Window>();

            MainWindow mw = System.Windows.Application.Current.MainWindow as MainWindow;

            foreach (Window w in mw.OwnedWindows)
            {
                if (HasVerticalOverlapWith(w) && w.Name != window.Name)
                {
                    WindowsWithVerticalOverlap.Add(w);
                }
            }

            return WindowsWithVerticalOverlap;
        }

        bool HasLateralOverlapWith(Window w)
        {
            if (w.Left + w.Width >= window.Left && window.Left + window.Width >= w.Left)    return true;
            return false;
        }
        bool HasVerticalOverlapWith(Window w)
        {
            if (w.Top + w.Height >= window.Top && window.Top + window.Height >= w.Top) return true;
            return false;
        }

        List<double> LateralSnapPoints()
        {
            List<double> lateralsnappoints = new List<double>();

            List<Window> WinList_LateralSnap = WindowsWithVerticalOverlap();
            foreach (Window w in WinList_LateralSnap)
            {
                lateralsnappoints.Add(w.Left);  
                lateralsnappoints.Add(w.Left + w.Width);
            }

            //Add left & right edges of screen
            var resolution = Screen.PrimaryScreen.Bounds;
            DpiScale scale = VisualTreeHelper.GetDpi(window);
            lateralsnappoints.Add(0);
            lateralsnappoints.Add(resolution.Width / scale.DpiScaleX);

            return lateralsnappoints.Distinct().ToList();
        }
        List<double> VerticallSnapPoints()
        {
            List<double> verticalsnappoints = new List<double>();

            List<Window> WinList_VerticalSnap = WindowsWithLateralOverlap();
            foreach (Window w in WinList_VerticalSnap)
            {
                verticalsnappoints.Add(w.Top);
                verticalsnappoints.Add(w.Top + w.Height);
            }

            //Add top & bottom edges of screen
            var resolution = Screen.PrimaryScreen.Bounds;
            DpiScale scale = VisualTreeHelper.GetDpi(window);
            verticalsnappoints.Add(0);
            verticalsnappoints.Add(resolution.Height / scale.DpiScaleY);

            return verticalsnappoints.Distinct().ToList();
        }

        double closestLateralSnap()
        {
            List<double> distances = new List<double>();

            foreach (double d in LateralSnapPoints())           //Which of those numbers is closest to the window (vLEFT & vRIGHT) & in which direction?
            {
                distances.Add(d - vLEFT);
                distances.Add(d - vRIGHT);
            }

            double dist_abs = 123456789;
            double dist = 123456789;

            for (int i = 0; i < distances.Count; i++)
            {
                if (Math.Abs(distances[i]) < dist_abs)
                {
                    dist_abs = Math.Abs(distances[i]);
                    dist = distances[i];
                }
            }

            return dist;
        }
        double closestVerticalSnap()
        {
            List<double> distances = new List<double>();

            foreach (double d in VerticallSnapPoints())           //Which of those numbers is closest to the window (vLEFT & vRIGHT) & in which direction?
            {
                distances.Add(d - vTOP);
                distances.Add(d - vBOTTOM);
            }

            double dist_abs = 123456789;
            double dist = 123456789;

            for (int i = 0; i < distances.Count; i++)
            {
                if (Math.Abs(distances[i]) < dist_abs)
                {
                    dist_abs = Math.Abs(distances[i]);
                    dist = distances[i];
                }
            }

            return dist;
        }
    }
}
