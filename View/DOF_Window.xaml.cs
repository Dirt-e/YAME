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
            //dummy.DataContext = engine.dof_override.lerp;
            
        }

        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Draw the bar (SelectionRange) anytime a value changes
            var slider = sender as Slider;

            if (slider.Value > 0)
            {
                slider.SelectionStart = 0;
                slider.SelectionEnd = slider.Value;
            }
            else
            {
                slider.SelectionStart = slider.Value;
                slider.SelectionEnd = 0;
            }
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
    }
}
