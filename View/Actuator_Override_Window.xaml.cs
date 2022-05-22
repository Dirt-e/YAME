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
using YAME.Model;

namespace YAME.View
{
    public partial class Actuator_Override_Window : Window
    {
        SnappyDragger snappydragger;

        public Actuator_Override_Window()
        {
            InitializeComponent();
            SetDataContext();
        }

        private void SetDataContext()
        {
            var mw = Application.Current.MainWindow as MainWindow;
            DataContext = mw.engine.actuatoroverride;
        }

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.actuatoroverride.syncSliders();   
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            snappydragger = new SnappyDragger(this);
            //LastClosed values
            Left = Properties.Settings.Default.Window_ActuatorOverride_Position_X;
            Top = Properties.Settings.Default.Window_ActuatorOverride_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //LastClosed values
            Properties.Settings.Default.Window_ActuatorOverride_Position_X = (float)Left;
            Properties.Settings.Default.Window_ActuatorOverride_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StopDrag();
        }
    }
}
