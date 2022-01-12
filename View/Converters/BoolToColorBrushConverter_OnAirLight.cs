using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace YAME.View.Converters
{
    public class BoolToColorBrushConverter_OnAirLight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (bool)value;

            //if (b) return new LinearGradientBrush(Colors.Red, Colors.DarkOrange, 90);
            //else return new LinearGradientBrush(Color.FromRgb(66, 74, 87), Color.FromRgb(32, 36, 43), -90);

            if (b)  return new RadialGradientBrush(Colors.Red, Colors.OrangeRed);
            else    return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
