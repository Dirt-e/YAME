using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MOTUS.View
{
    public partial class CrashDetectorWindow : Window
    {
        public CrashDetectorWindow()
        {
            InitializeComponent();
            SetDataContext();
        }

        private void SetDataContext()
        {
            var engine = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine;
            DataContext = engine.VM_CrashDetector;

            txtbx_Ax_Threshold.DataContext = engine.exceedancedetector;
            txtbx_Ay_Threshold.DataContext = engine.exceedancedetector;
            txtbx_Az_Threshold.DataContext = engine.exceedancedetector;
            txtbx_Wx_Threshold.DataContext = engine.exceedancedetector;
            txtbx_Wy_Threshold.DataContext = engine.exceedancedetector;
            txtbx_Wz_Threshold.DataContext = engine.exceedancedetector;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //LastClosed values
            Left = Properties.Settings.Default.Window_CrashDetector_Position_X;
            Top = Properties.Settings.Default.Window_CrashDetector_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //LastClosed values
            Properties.Settings.Default.Window_CrashDetector_Position_X = (float)Left;
            Properties.Settings.Default.Window_CrashDetector_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }

    }
}
