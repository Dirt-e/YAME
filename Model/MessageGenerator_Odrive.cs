using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utility;
using System.Windows;

namespace YAME.Model
{
    public class MessageGenerator_Odrive : MyObject
    {
        //Use 'p' for real time position streaming. See here: https://docs.odriverobotics.com/v/beta/ascii-protocol.html#motor-position
        string Command = "p";       //Can eventually be replaced by a Binding!
        
        public string ComposeMessageFrom(float f1, float f2)
        {
            return $"{Command} 0 {f1}\n{Command} 1 {f2}";
        }
    }
}
