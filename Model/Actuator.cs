﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                MaxLength = MinLength + Stroke;
                OnPropertyChanged("MinLength");
            }
        }
        float _stroke;
        public float Stroke
        {
            get { return _stroke; }
            set
            { 
                _stroke = value;
                MaxLength = MinLength + Stroke; 
                OnPropertyChanged("Stroke"); 
            }
        }
        float _currentlength;
        public float CurrentLength           //This is essentially driving the whole object by calling "redraw()"
        {
            get { return _currentlength; }
            set
            {
                _currentlength = value;
                OnPropertyChanged("Currentlength");

                redraw();
            }
        }
        float _speed;
        public float Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                OnPropertyChanged("Speed");
            }
        }
        //Internal:
        float _maxlength;
        public float MaxLength
        {
            get { return _maxlength; }
            private set
            {
                _maxlength = value;
                OnPropertyChanged("MaxLength");
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

        public bool InLimits
        { 
            get 
            {
                return (Status != ActuatorStatus.TooLong &&
                        Status != ActuatorStatus.TooShort);
            } 
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

            Utilisation = DetermineUtilisation();
            Status = DetermineStatus();
        }

        
        //Helpers
        float DetermineUtilisation()
        {
            if (MinLength <= CurrentLength && CurrentLength <= MaxLength)   return (Extension / Stroke);
            else if (CurrentLength <= MinLength)                            return 0;
            else                                                            return 1;
        }
        ActuatorStatus DetermineStatus()
        {
            float triggerzone = Properties.Settings.Default.Actuator_TriggerZone;

            if (Utilisation == 0)                                           return ActuatorStatus.TooShort;
            if (Utilisation == 1)                                           return ActuatorStatus.TooLong;
            
            float Lower_max = 0.0f + triggerzone;
            if (Utilisation < Lower_max)                                    return ActuatorStatus.FullyRetracted;

            float Upper_min = 1.0f - triggerzone;
            if (Utilisation > Upper_min)                                    return ActuatorStatus.FullyExtended;

            float Center_min = 0.5f - triggerzone;
            float Center_max = 0.5f + triggerzone;
            if (Center_min <= Utilisation && Utilisation <= Center_max)     return ActuatorStatus.Centered;

            else                                                            return ActuatorStatus.InBetween;
        }
    }

    public enum ActuatorStatus
    {
        TooLong,
        TooShort,
        FullyRetracted,
        FullyExtended,
        Centered,
        InBetween
    }
}
