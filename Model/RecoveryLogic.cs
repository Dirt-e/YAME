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
            set     //State can only run in circles
            {
                if (value == _state) return;        //Nothing to do

                switch (State)
                {
                    case Recovery_State.Dormant:
                        if (value == Recovery_State.Crash_Informed) _state = Recovery_State.Crash_Informed;
                        break;
                    case Recovery_State.Crash_Informed:
                        if (value == Recovery_State.Recovering) _state = Recovery_State.Recovering;
                        break;
                    case Recovery_State.Recovering:
                        if (value == Recovery_State.Dormant) _state = Recovery_State.Dormant;
                        break;
                    default:
                        throw new ArgumentException("WTF is " + State + "?");
                }
            }
        }

        public void Update()
        {
            if (State == Recovery_State.Recovering)
            {
                dfghgd
            }
        }

        public void Recover()
        {

        }

    }

    public enum Recovery_State
    {
        Dormant,
        Crash_Informed,
        Recovering
    }
}
