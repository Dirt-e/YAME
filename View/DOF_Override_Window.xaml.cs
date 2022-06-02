using YAME.Model;
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

namespace YAME.View
{
    public partial class DOF_Window : Window
    {
        SnappyDragger SnappyDragger;
        Engine engine;

        public DOF_Window()
        {   
            var mw = Application.Current.MainWindow as MainWindow;
            engine = mw.engine;
            
            InitializeComponent();
            DataContext = engine.dof_override;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SnappyDragger = new SnappyDragger(this);

            Left    = Properties.Settings.Default.Window_DOFs_Position_X;
            Top     = Properties.Settings.Default.Window_DOFs_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Window_DOFs_Position_X = (float)Left;
            Properties.Settings.Default.Window_DOFs_Position_Y = (float)Top;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sld_DOF_Roll.Value = 0;
            sld_DOF_Yaw.Value = 0;
            sld_DOF_Pitch.Value = 0;
            sld_DOF_Surge.Value = 0;
            sld_DOF_Pitch_LFC.Value = 0;
            sld_DOF_Heave.Value = 0;
            sld_DOF_Sway.Value = 0;
            sld_DOF_Roll_LFC.Value = 0;
        }
        private void Red_X_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.mnuDOFs.IsChecked = false;
        }
    }
}
