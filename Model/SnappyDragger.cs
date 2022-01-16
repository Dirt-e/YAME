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
        DispatcherTimer timer = new DispatcherTimer
        {
            Interval = new TimeSpan(0, 0, 0, 0, 10),
        };
        Window window = null;
        int SnapDist = 10;
        SnapToBorder snapBorder = SnapToBorder.none;

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
            
            Window w = WindowToSnapTo();

            if (WindowToSnapTo() == null)
            {
                window.Left = WindowStartPoint.X + delta.X;
                window.Top = WindowStartPoint.Y + delta.Y;              //Just Dragging
                return;
            }

            switch (snapBorder)
            {
                case SnapToBorder.left:
                    window.Left = w.Left - window.Width;
                    window.Top = WindowStartPoint.Y + delta.Y;
                    break;
                case SnapToBorder.top:
                    window.Top = w.Top - window.Height;
                    window.Left = WindowStartPoint.X + delta.X;
                    break;
                case SnapToBorder.right:
                    window.Left = w.Left + w.Width;
                    window.Top = WindowStartPoint.Y + delta.Y;
                    break;
                case SnapToBorder.bottom:
                    window.Top = w.Top + w.Height;
                    window.Left = WindowStartPoint.X + delta.X;
                    break;
                case SnapToBorder.none:
                    break;
                default:
                    break;
            }


        }

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
        Window WindowToSnapTo()
        {
            //Check all windows for proximity...
            MainWindow mw = System.Windows.Application.Current.MainWindow as MainWindow;

            foreach (Window w in mw.OwnedWindows)
            {
                if (w.Name == window.Name)  continue;                //Skip yourself
                if (ShallSnappTo(w))        return w;
            }
            return null;
        }
        bool ShallSnappTo(Window w)
        {
            //Virtual positions:
            vLEFT      = WindowStartPoint.X + delta.X;
            vTOP       = WindowStartPoint.Y + delta.Y;
            vRIGHT     = WindowStartPoint.X + window.Width + delta.X;
            vBOTTOM    = WindowStartPoint.Y + window.Height + delta.Y;

            if (HasVerticalOverlapWith(w))
            {
                if (Math.Abs((w.Left + w.Width) - vLEFT) < SnapDist)            //dragging right to left  <--
                {
                    snapBorder = SnapToBorder.right;
                    return true;
                }
                if (Math.Abs(vRIGHT - w.Left) < SnapDist)                       //dragging left to right  -->
                {
                    snapBorder = SnapToBorder.left;
                    return true;
                }
            }
            if (HasLateralOverlapWith(w))
            {
                if (Math.Abs((w.Top + w.Height) - vTOP) < SnapDist)            //dragging upwards
                {
                    snapBorder = SnapToBorder.bottom;
                    return true;
                }
                if (Math.Abs((vBOTTOM) - w.Top) < SnapDist)                     //dragging downwards
                {
                    snapBorder = SnapToBorder.top;
                    return true;
                }
            }
            return false;
        }
        bool HasLateralOverlapWith(Window w)
        {
            if (w.Left + w.Width > window.Left && window.Left + window.Width > w.Left)    return true;
            return false;
        }
        bool HasVerticalOverlapWith(Window w)
        {
            if (w.Top + w.Height > window.Top && window.Top + window.Height > w.Top) return true;
            return false;
        }

        enum SnapToBorder
        {
            left,
            top,
            right,
            bottom,
            none,
        }
    }
}
