using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class RecoveryLogic : MyObject
    {
        Recovery_State _state = Recovery_State.Dormant;
        public Recovery_State State
        { 
            get { return _state; }
            set { _state = value; OnPropertyChanged(nameof(State)); }
        }
    }

    public enum Recovery_State
    {
        Dormant,
        Crash_Informed,
        Recovering,
        Resetting
    }
}
