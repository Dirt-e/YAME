using YAME.Model;
using YAME.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.ViewModel
{
    public class ViewModel_RawData : _ViewModel
    {
        //Airdata
        float _ias;
        public float IAS
        {
            get
            {
                return _ias;
            }
            set
            {
                _ias = value;
                OnPropertyChanged("IAS");
            }
        }
        float _mach;
        public float MACH
        {
            get
            {
                return _mach;
            }
            set
            {
                _mach = value;
                OnPropertyChanged("MACH");
            }
        }
        float _tas;
        public float TAS
        {
            get
            {
                return _tas;
            }
            set
            {
                _tas = value;
                OnPropertyChanged("TAS");
            }
        }
        float _gs;
        public float GS
        {
            get
            {
                return _gs;
            }
            set
            {
                _gs = value;
                OnPropertyChanged("GS");
            }
        }
        float _aoa;
        public float AOA
        {
            get
            {
                return _aoa;
            }
            set
            {
                _aoa = value;
                OnPropertyChanged("AOA");
            }
        }
        float _vs;
        public float VS
        {
            get
            {
                return _vs;
            }
            set
            {
                _vs = value;
                OnPropertyChanged("VS");
            }
        }
        float _hgt;
        public float HGT
        {
            get
            {
                return _hgt;
            }
            set
            {
                _hgt = value;
                OnPropertyChanged("HGT");
            }
        }
        //Euler
        float _hdg;
        public float HDG
        {
            get
            {
                return _hdg;
            }
            set
            {
                _hdg = value;
                OnPropertyChanged("HDG");
            }
        }
        float _pitch;
        public float PITCH
        {
            get
            {
                return _pitch;
            }
            set
            {
                _pitch = value;
                OnPropertyChanged("PITCH");
            }
        }
        float _bank;
        public float BANK
        {
            get
            {
                return _bank;
            }
            set
            {
                _bank = value;
                OnPropertyChanged("BANK");
            }
        }
        //Rates
        float _wx;
        public float WX
        {
            get
            {
                return _wx;
            }
            set
            {
                _wx = value;
                OnPropertyChanged("WX");
            }
        }
        float _wy;
        public float WY
        {
            get
            {
                return _wy;
            }
            set
            {
                _wy = value;
                OnPropertyChanged("WY");
            }
        }
        float _wz;
        public float WZ
        {
            get
            {
                return _wz;
            }
            set
            {
                _wz = value;
                OnPropertyChanged("WZ");
            }
        }
        //Acccels
        float _ax;
        public float AX
        {
            get
            {
                return _ax;
            }
            set
            {
                _ax = value;
                OnPropertyChanged("AX");
            }
        }
        float _ay;
        public float AY
        {
            get
            {
                return _ay;
            }
            set
            {
                _ay = value;
                OnPropertyChanged("AY");
            }
        }
        float _az;
        public float AZ
        {
            get
            {
                return _az;
            }
            set
            {
                _az = value;
                OnPropertyChanged("AZ");
            }
        }
        //Meta
        float _time;
        public float TIME
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged("TIME");
            }
        }
        float _counter;
        public float COUNTER
        {
            get
            {
                return _counter;
            }
            set
            {
                _counter = value;
                OnPropertyChanged("COUNTER");
            }
        }
        string _sim;
        public string SIM
        {
            get
            {
                return _sim;
            }
            set
            {
                _sim = value;
                OnPropertyChanged("SIM");
            }
        }


        public ViewModel_RawData(Engine e)
        {
            base.engine = e;
        }
    }
}
