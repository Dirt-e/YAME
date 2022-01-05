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
    public partial class ToggleSwitch : UserControl
    {
        readonly Thickness Offset_Right = new Thickness(50, 0, 0, 0);
        readonly Thickness Offset_Left = new Thickness(-50, 0, 0, 0);
        readonly SolidColorBrush Brush_ON = new SolidColorBrush(Colors.PaleGreen);
        readonly SolidColorBrush Brush_OFF = new SolidColorBrush(Colors.DarkGray);

        public static readonly DependencyProperty IsOnProperty = DependencyProperty.Register(
            "IsOn", typeof(bool), typeof(ToggleSwitch),
            new FrameworkPropertyMetadata(false,
            FrameworkPropertyMetadataOptions.AffectsRender,
            (o, e) => ((ToggleSwitch)o).OnIsOnChanged()));

        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }
        private void OnIsOnChanged()
        {
            if (IsOn)
            {
                Nipple.Margin = Offset_Right;
                Body.Fill = Brush_ON;
            }
            else
            {
                Nipple.Margin = Offset_Left;
                Body.Fill = Brush_OFF;
            }
        }

        public ToggleSwitch()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsOn = !IsOn;       //Invert the switch
        }
    }
}
