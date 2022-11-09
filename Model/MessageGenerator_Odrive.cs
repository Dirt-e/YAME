using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utility;
using System.Windows;
using System.Globalization;

namespace YAME.Model
{
    public class MessageGenerator_Odrive : MyObject
    {
        //Use 'p' for real time position streaming. See here: https://docs.odriverobotics.com/v/beta/ascii-protocol.html#motor-position

        public string ComposeMessageFrom(float f1, float f2, string formatstring)
        {
            //Example FormatString: "p 0 [value0]"
            //Example FormatString: "p 0 [value0][newline]p 1 [value1]"

            StringBuilder sb = new StringBuilder(formatstring);

            sb.Replace("[value0]", f1.ToString("F6", CultureInfo.InvariantCulture));
            sb.Replace("[newline]", "\n");
            sb.Replace("[value1]", f2.ToString("F6", CultureInfo.InvariantCulture));

            return sb.ToString();
        }
    }
}
