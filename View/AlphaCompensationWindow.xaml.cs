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
    public partial class AlphaCompensationWindow : Window
    {
        public AlphaCompensationWindow()
        {
            InitializeComponent();
            DataContext = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine.alphacompensator;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left    = Properties.Settings.Default.Window_AlphaCompensation_Position_X;
            Top     = Properties.Settings.Default.Window_AlphaCompensation_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Window_AlphaCompensation_Position_X = (float)Left;
            Properties.Settings.Default.Window_AlphaCompensation_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }
    }
}
