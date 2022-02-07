using YAME.Model;
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
    public class ActuatorstatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return Colors.Gray; }

            var status = (ActuatorStatus)value;

            switch (status)
            {
                case ActuatorStatus.TooLong:
                    return new SolidColorBrush(Colors.Red);
                case ActuatorStatus.TooShort:
                    return new SolidColorBrush(Colors.Blue);
                case ActuatorStatus.FullyRetracted:
                    return new SolidColorBrush(Colors.SkyBlue);
                case ActuatorStatus.FullyExtended:
                    return new SolidColorBrush(Colors.Orange);
                case ActuatorStatus.Centered:
                    return new SolidColorBrush(Colors.LightGreen);
                case ActuatorStatus.InBetween:
                    return new SolidColorBrush(Colors.WhiteSmoke);
                default:
                    throw new ArgumentException("Unknown Actuatorstatus: " + status.ToString());
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
