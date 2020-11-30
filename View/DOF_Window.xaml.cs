using MOTUS.Model;
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
    public partial class DOF_Window : Window
    {
        Engine engine;

        public DOF_Window()
        {   
            engine = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine;
            
            InitializeComponent();
            DataContext = engine.dof_override;
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
    }
}
