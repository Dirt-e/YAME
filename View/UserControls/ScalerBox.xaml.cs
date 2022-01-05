using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class ScalerBox : UserControl
    {
        public ScalerBox()
        {
            InitializeComponent();
        }

        private void txtbx_scalar_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            txtbx_scalar.Focus();                                                           //...to be able to have a LostFocus event in the end

            float f = Convert.ToSingle(txtbx_scalar.Text, CultureInfo.InvariantCulture);

            if (e.Delta > 0) f *= 1.05f;
            else if (e.Delta < 0) f /= 1.05f;

            txtbx_scalar.Text = f.ToString(CultureInfo.InvariantCulture);

            rct_background.Focus();                                                         //This triggers the LostFocus event
        }
    }
}
