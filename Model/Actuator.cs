using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class Actuator : MyObject
    {
        //External:
        float _minlength;
        public float MinLength
        {
            get { return _minlength; }
            set
            {
                _minlength = value;
                Stroke = MaxLength - MinLength;
                OnPropertyChanged("MinLength");
            }
        }
        float _maxlength;
        public float MaxLength
        {
            get { return _maxlength; }
            set
            {
                _maxlength = value;
                Stroke = MaxLength - MinLength;
                OnPropertyChanged("MaxLength");
            }
        }
        float _currentlength;
        public float CurrentLength           //This is essentially driving the whole object by calling "redraw()"
        {
            get { return _currentlength; }
            set
            {
                _currentlength = value;
                redraw();
                OnPropertyChanged("Currentlength");
            }
        }
        //Internal:
        float _stroke;
        public float Stroke
        {
            get { return _stroke; }
            private set { _stroke = value; OnPropertyChanged("Stroke"); }
        }
        float _extension;
        public float Extension
        {
            get { return _extension; }
            private set { _extension = value; OnPropertyChanged("Extension"); }
        }
        float _utilisation;
        public float Utilisation
        {
            get { return _utilisation; }
            private set { _utilisation = value; OnPropertyChanged("Utilisation"); }
        }
        ActuatorStatus _status;
        public ActuatorStatus Status
        {
            get { return _status; }
            private set { _status = value; OnPropertyChanged("Status"); }
        }

        void redraw()
        {
            Extension = CurrentLength - MinLength;
            Status = DetermineStatus();
            Utilisation = DetermineUtilisation();
        }

        ActuatorStatus DetermineStatus()
        {
            if (MinLength <= CurrentLength && CurrentLength <= MaxLength)   return ActuatorStatus.Inlimits;
            else if (CurrentLength <= MinLength)                            return ActuatorStatus.TooShort;
            else                                                            return ActuatorStatus.TooLong;
            
        }
        float DetermineUtilisation()
        {
            if (Status == ActuatorStatus.Inlimits)          return (Extension / Stroke);
            else if (Status == ActuatorStatus.TooShort)     return 0;
            else if ( Status == ActuatorStatus.TooLong)     return 1;

            //This should never happen:
            throw new Exception("Unkown ActuatorStatus: " + Status);
        }
    }

    public enum ActuatorStatus
    {
        TooLong,
        TooShort,
        Inlimits
    }
}
