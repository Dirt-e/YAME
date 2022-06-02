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
    public partial class PositionCorrector_Window : Window
    {
        SnappyDragger snappyDragger;

        public PositionCorrector_Window()
        {
            InitializeComponent();
            SetDatacontext();
        }

        private void SetDatacontext()
        {
            DataContext = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine.positionoffsetcorrector;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            snappyDragger = new SnappyDragger(this);

            Left    = Properties.Settings.Default.Window_PositionCorrection_Position_X;
            Top     = Properties.Settings.Default.Window_PositionCorrection_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Window_PositionCorrection_Position_X    = (float)Left;
            Properties.Settings.Default.Window_PositionCorrection_Position_Y    = (float)Top;

            Properties.Settings.Default.Save();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            snappyDragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            snappyDragger.StopDrag();
        }

        private void Red_X_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.mnuPositionCorrection.IsChecked = false;
        }
    }
}
