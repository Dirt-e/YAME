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
    public partial class SerialConnection2_Window : Window
    {
        SnappyDragger snappydragger;

        public SerialConnection2_Window()
        {
            InitializeComponent();
            //SetDataContext();

            snappydragger = new SnappyDragger(this);
        }

        private void cmbbx_Port1_DropDownOpened(object sender, EventArgs e)
        {

        }
        private void cmbbx_Port2_DropDownOpened(object sender, EventArgs e)
        {

        }
        private void cmbbx_Port3_DropDownOpened(object sender, EventArgs e)
        {

        }

        private void cmbbx_Controller1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void cmbbx_Controller2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void cmbbx_Controller3_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void tgl_Active1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StopDrag();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left = Properties.Settings.Default.Window_SerialConnection2_Position_X;
            Top = Properties.Settings.Default.Window_SerialConnection2_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Window_SerialConnection2_Position_X = (float)Left;
            Properties.Settings.Default.Window_SerialConnection2_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }

        private void tgl_Active1_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }
        private void tgl_Active2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void tgl_Active3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
