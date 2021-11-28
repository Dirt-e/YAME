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
            DataContext = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine.VM_CrashDetector;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left = Properties.Settings.Default.Window_CrashDetector_Position_X;
            Top = Properties.Settings.Default.Window_CrashDetector_Position_Y;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Window_CrashDetector_Position_X = (float)Left;
            Properties.Settings.Default.Window_CrashDetector_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }
    }
}
