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
    public partial class PositionCorrector_Window : Window
    {
        public PositionCorrector_Window()
        {
            InitializeComponent();
            SetDatacontext();
        }

        private void SetDatacontext()
        {
            this.DataContext = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine.positionoffsetcorrector;
            //var engine = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine;

            //txtbx_DeltaX.DataContext = engine.positionoffsetcorrector;
            //txtbx_DeltaY.DataContext = engine.positionoffsetcorrector;
            //txtbx_DeltaZ.DataContext = engine.positionoffsetcorrector;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left    = Properties.Settings.Default.Window_PositionCorrection_Position_X;
            Top     = Properties.Settings.Default.Window_PositionCorrection_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Window_PositionCorrection_Position_X    = (float)Left;
            Properties.Settings.Default.Window_PositionCorrection_Position_Y    = (float)Top;

            Properties.Settings.Default.Save();
        }
    }
}
