using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class ActuatorSystem : MyObject
    {
        public List<Actuator> Actuators;
        public SixSisters Output;
        IK_Module IK_Module;

        //External:
        float _minlength;
        public float MinLength
        {
            get { return _minlength; }
            set
            {
                _minlength = Math.Min(MaxLength, value);
                //redraw();
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
                //redraw();
                OnPropertyChanged("MaxLength");
            }
        }
        //Internal:
        bool _allinlimits;
        public bool AllInLimits
        {
            get { return _allinlimits; }
            private set
            {
                if (_allinlimits != value)
                {
                    _allinlimits = value; 
                    OnPropertyChanged("AllInLimits"); 
                }
            }
        }

        public ActuatorSystem(ref IK_Module ikm)
        {
            IK_Module = ikm;
            Actuators = new List<Actuator>() {  new Actuator(),
                                                new Actuator(),
                                                new Actuator(),
                                                new Actuator(),
                                                new Actuator(),
                                                new Actuator()  };
        Output = new SixSisters();
        }

        public void Update()
        {   
            foreach (Actuator act in Actuators)
            {
                act.MinLength = MinLength;
                act.MaxLength = MaxLength;
            }

            for (int i = 0; i < 6; i++)
            {
                Actuators[i].CurrentLength = IK_Module.Lengths[i];
            }
            AllInLimits =  DetermineSystemStatus();
            CreateOutput();
        }


        //Helpers:
        bool DetermineSystemStatus()
        {
            foreach (Actuator act in Actuators)                             //...but let each actuator speak.
            {
                if (act.Status != ActuatorStatus.Inlimits)  return false;   //A single vote is enough to spoil the party!
            }
            return true;                                                    //No objections!
        }

        void CreateOutput()
        {
            for (int i = 0; i < 6; i++)
            {
                Output.values[i] = Actuators[i].Utilisation;
            }
        }
    }
}
