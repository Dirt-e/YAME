using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;

public class GlobalVars
{
    public static NumberFormatInfo myNumberFormat(int decimals)
    {
        var format = new NumberFormatInfo();
        format.NegativeSign = "-";
        format.NumberDecimalSeparator = ".";
        format.NumberDecimalDigits = decimals;

        return format;
    }

}