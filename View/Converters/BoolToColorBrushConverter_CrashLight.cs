using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace YAME.View.Converters
{
    public class BoolToColorBrushConverter_CrashLight : IValueConverter
    {
        //This converter converts a bool (crashdetector.IsCrashed) into the color of the crash-light
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (bool)value;

            if (b) return new SolidColorBrush(Colors.Red);          //IsCrashed = Red
            else return new SolidColorBrush(Colors.LightGreen);     //!IsCrashed = Green
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
