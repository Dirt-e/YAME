﻿using System;
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
    public class CharToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return Colors.Gray; }

            var code = (string)value;
            char ch = code.ToCharArray()[0];

            switch (ch)
            {
                case 'H':
                    return Colors.LightSalmon;
                case 'L':
                    return Colors.LightBlue;
                default:
                    string message = "Character " + ch + " was not in the list";
                    Debug.WriteLine(message);
                    throw new Exception(message);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
 