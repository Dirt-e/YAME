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
        public MessageGenerator_Odrive(Engine e)
        {
            engine = e;
        }

        public string ComposeMessageFrom(string formatstring, float[] revolutions)
        {   
            //Use 'p' for real time position streaming. See here: https://docs.odriverobotics.com/v/beta/ascii-protocol.html#motor-position
            //Example FormatString: "p 0 [Act1]"
            //Example FormatString: "p 0 [Act1][newline]p 1 [Act2]"

            StringBuilder sb = new StringBuilder(formatstring);

            sb.Replace("[Act1]", revolutions[0].ToString("F6", CultureInfo.InvariantCulture));
            sb.Replace("[Act2]", revolutions[1].ToString("F6", CultureInfo.InvariantCulture));
            sb.Replace("[Act3]", revolutions[2].ToString("F6", CultureInfo.InvariantCulture));
            sb.Replace("[Act4]", revolutions[3].ToString("F6", CultureInfo.InvariantCulture));
            sb.Replace("[Act5]", revolutions[4].ToString("F6", CultureInfo.InvariantCulture));
            sb.Replace("[Act6]", revolutions[5].ToString("F6", CultureInfo.InvariantCulture));
            sb.Replace("[newline]", "\n");

            return sb.ToString();
        }
    }
}
