using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class ActuatorSystem : MyObject
    {
        public List<Actuator> Actuators = new List<Actuator>()
        {
            new Actuator(),
            new Actuator(),
            new Actuator(),
            new Actuator(),
            new Actuator(),
            new Actuator(),
        };

        //External:
        float _minlength;
        public float MinLength
        {
            get { return _minlength; }
            set
            {
                _minlength = Math.Min(MaxLength, value);
                redraw();
                OnPropertyChanged("MinLength");
            }
        }
        float _maxlength;
        public float MaxLength
        {
            get { return _maxlength; }
            set
            {
                _maxlength = Math.Max(MinLength, value);
                redraw();
                OnPropertyChanged("MaxLength");
            }
        }
        //Internal:
        bool _allinlimits;
        public bool AllInLimits
        {
            get { return _allinlimits; }
            private set { _allinlimits = value; OnPropertyChanged("AllInLimits"); }

        }

        public void Update()
        {
            DetermineStatus();
        }

        public void redraw()
        {
            foreach (Actuator act in Actuators)
            {
                act.MinLength = MinLength;
                act.MaxLength = MaxLength;
            }
            DetermineStatus();
        }

        //Helpers:
        void DetermineStatus()
        {
            bool temp = true;                                       //Assume things are OK,....

            foreach (Actuator act in Actuators)                     //...but let each actuator...
            {
                if (act.Status != ActuatorStatus.Inlimits)         //...speak. 
                {
                    temp = false;                          
                    break;                                          //A single vote is enough to spoil the party!
                }
                
            }
            AllInLimits = temp; ;
        }
    }
}
