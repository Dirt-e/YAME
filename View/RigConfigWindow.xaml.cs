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
    public partial class RigConfigWindow : Window
    {
        SnappyDragger snappydragger;

        public RigConfigWindow()
        {
            InitializeComponent();
            SetDataContexts();
            
            snappydragger = new SnappyDragger(this);
        }
        
        private void SetDataContexts()
        {
            var engine = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine;
            border_UpperPlatform.DataContext    = engine.integrator;
            border_LowerPlatform.DataContext    = engine.integrator;
            border_Actuators.DataContext        = engine.actuatorsystem;
            txtbx_OffsetPark.DataContext        = engine.integrator;
            txtbx_OffsetPause.DataContext       = engine.integrator;
            txtbx_OffsetCoR.DataContext         = engine.integrator;
            //img_warning.DataContext             = engine.serialtalker;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Window_RigConfig_Position_X = (float)Left;
            Properties.Settings.Default.Window_RigConfig_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left = Properties.Settings.Default.Window_RigConfig_Position_X;
            Top = Properties.Settings.Default.Window_RigConfig_Position_Y;
        }
        
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StopDrag();
        }

        private void CalibPark_Click(object sender, RoutedEventArgs e)
        {
            PhantomRig pr = new PhantomRig();

            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.integrator.Offset_Park = pr.ParkPos_Ideal;
        }
        private void CalibPause_Click(object sender, RoutedEventArgs e)
        {
            PhantomRig pr = new PhantomRig();

            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.integrator.Offset_Pause = pr.PausePos_Ideal;
        }
    }
}
