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
    public partial class MotionControl_Window : Window
    {
        SnappyDragger SnappyDragger;
        MainWindow mainwindow = Application.Current.MainWindow as MainWindow;

        public MotionControl_Window()
        {
            InitializeComponent();
            DataContext = mainwindow.engine.VM_MotionControlWindow;
        }

        private void btn_motion_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.engine.integrator.Lerp_3Way.Command = Lerp3_Command.Motion;
        }
        private void btn_pause_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.engine.integrator.Lerp_3Way.Command = Lerp3_Command.Pause;
        }
        private void btn_park_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.engine.integrator.Lerp_3Way.Command = Lerp3_Command.Park;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SnappyDragger = new SnappyDragger(this);

            Left = Properties.Settings.Default.Window_MotionControl_Position_X;
            Top = Properties.Settings.Default.Window_MotionControl_Position_Y;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Window_MotionControl_Position_X = (float)Left;
            Properties.Settings.Default.Window_MotionControl_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SnappyDragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SnappyDragger.StopDrag();
        }
    }
}
