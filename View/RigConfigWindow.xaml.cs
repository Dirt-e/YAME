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
    public partial class RigConfigWindow : Window
    {
        public RigConfigWindow()
        {
            InitializeComponent();
            SetDataContexts();
        }
        
        private void SetDataContexts()
        {
            var engine = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine;
            txtbx_OffsetPark.DataContext        = engine.integrator;
            txtbx_OffsetPause.DataContext       = engine.integrator;
            txtbx_OffsetCoR.DataContext         = engine.integrator;
            border_UpperPlatform.DataContext    = engine.integrator;
            border_LowerPlatform.DataContext    = engine.integrator;
            //border_Actuators.DataContext        = engine.integrator.ActuatorSystem;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
    }
}
