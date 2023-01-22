using System;
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

    public static double Deg2Rad = Math.PI / 180;
    public static double Rad2Deg = 180 / Math.PI;
    
    public static double Mps2Kts = 1.94384;
    public static double Kts2Mps = 1 / Mps2Kts;
}