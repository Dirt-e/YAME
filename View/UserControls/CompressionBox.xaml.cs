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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YAME.View.UserControls
{
    public partial class CompressionBox : UserControl
    {
        public CompressionBox()
        {
            InitializeComponent();
        }

        private void Cmbbx_Method_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetLabelsAndVisibilities();
        }

        private void SetLabelsAndVisibilities()
        {
            switch (Cmbbx_Method.SelectedItem)
            {
                case CompressionMethod.None:
                    Txtbx_Parameter.Visibility = Visibility.Hidden;
                    Txtbx_Limit.Visibility = Visibility.Hidden;
                    lbl_1.Text = "";
                    lbl_2.Text = "";
                    break;
                case CompressionMethod.Crop:
                    Txtbx_Parameter.Visibility = Visibility.Hidden;
                    Txtbx_Limit.Visibility = Visibility.Visible;
                    lbl_1.Text = "";
                    lbl_2.Text = "Cutoff";
                    break;
                case CompressionMethod.ATan:
                    Txtbx_Parameter.Visibility = Visibility.Visible;
                    Txtbx_Limit.Visibility = Visibility.Visible;
                    lbl_1.Text = "Gradient";
                    lbl_2.Text = "Anchor";
                    break;
                case CompressionMethod.TanH:
                    Txtbx_Parameter.Visibility = Visibility.Visible;
                    Txtbx_Limit.Visibility = Visibility.Visible;
                    lbl_1.Text = "Gradient";
                    lbl_2.Text = "Limit";
                    break;
                case CompressionMethod.Logist:
                    Txtbx_Parameter.Visibility = Visibility.Hidden;
                    Txtbx_Limit.Visibility = Visibility.Hidden;
                    lbl_1.Text = "Not";
                    lbl_2.Text = "implem.";
                    break;

                default:
                    throw new Exception("The CompressionMethod " + Cmbbx_Method.SelectedItem.ToString() +
                        " is unknown. Make sure there is a case in the switch-statement that handles this method.");
            }
        }
    }
}
