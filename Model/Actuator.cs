using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
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
                _maxlength = value; ;
                redraw();
                OnPropertyChanged("MaxLength");
            }
        }
        float _currentlength;
        public float CurrentLength
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
            private set
            {
                _stroke = value;
                OnPropertyChanged("Stroke");
            }
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
            Stroke = MaxLength - MinLength;
            Extension = CurrentLength - MinLength;
            CalculateUtilisation();
            DetermineStatus();
        }

        private void CalculateUtilisation()
        {
            if (0 <= Stroke)
            {
                Utilisation = Extension / Stroke;
            }
            else
            {
                throw new Exception("Actuator may not have a stroke of zero or less");
            }
        }
        void DetermineStatus()
        {
            if (MinLength <= CurrentLength && CurrentLength <= MaxLength)
            {
                Status = ActuatorStatus.Inlimits;
                return;
            }
            else if (CurrentLength <= MinLength)
            {
                Status = ActuatorStatus.TooShort;
                return;
            }
            else
            {
                Status = ActuatorStatus.TooLong;
                return;
            }
        }
    }

    public enum ActuatorStatus
    {
        TooLong,
        TooShort,
        Inlimits
    }
}
