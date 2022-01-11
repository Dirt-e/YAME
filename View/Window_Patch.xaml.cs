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
    public partial class Patch_Window : Window
    {
        public Patch_Window()
        {
            InitializeComponent();
        }

        private void btn_Patch_DCS_Click(object sender, RoutedEventArgs e)
        {
            Patcher.Patch_DCS();
        }
        private void btn_Patch_DCSopenbeta_Click(object sender, RoutedEventArgs e)
        {
            Patcher.Patch_DCSopenbeta();
        }
        
        private void btn_Unpatch_DCS_Click(object sender, RoutedEventArgs e)
        {
            Patcher.UnPatch_DCS();
        }
        private void btn_Unpatch_DCSopenbeta_Click(object sender, RoutedEventArgs e)
        {
            Patcher.UnPatch_DCSopenbeta();
        }
        
    }
}
