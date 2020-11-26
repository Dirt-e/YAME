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

namespace MOTUS.View.UserControls
{
    public partial class FilterBox : UserControl
    {
        public static readonly DependencyProperty InValueProperty = DependencyProperty.Register(
            "InValue", typeof(float), typeof(FilterBox));
        public static readonly DependencyProperty CodeProperty = DependencyProperty.Register(
            "Code", typeof(string), typeof(FilterBox));
        public static readonly DependencyProperty FilterVariableProperty = DependencyProperty.Register(
            "FilterVariable", typeof(float), typeof(FilterBox));
        public static readonly DependencyProperty OutValueProperty = DependencyProperty.Register(
            "OutValue", typeof(float), typeof(FilterBox));


        





        public float InValue
        {
            get { return (float)GetValue(InValueProperty); }
            set { SetValue(InValueProperty, value); }
        }
        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }
        public float FilterVariable
        {
            get { return (float)GetValue(FilterVariableProperty); }
            set { SetValue(FilterVariableProperty, value); }
        }
        public float OutValue
        {
            get { return (float)GetValue(OutValueProperty); }
            set { SetValue(OutValueProperty, value); }
        }




        public FilterBox()
        {
            InitializeComponent();
        }
    }
}
