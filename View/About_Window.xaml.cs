using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class AboutWindow : Window
    {
        public bool IsOpen;

        public AboutWindow()
        {
            InitializeComponent();

            MainWindow mw = Application.Current.MainWindow as MainWindow;
            DataContext = mw.engine.VM_MainWindow;
            txtblk_framerate.DataContext = mw.engine;
        }


        private void ButtonClick_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsOpen = true;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            IsOpen=false;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://creativecommons.org/licenses/by-nc-nd/3.0/deed.en");
            Topmost = false;
        }
    }
}
