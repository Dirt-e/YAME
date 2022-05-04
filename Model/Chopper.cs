using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class Chopper : MyObject
    {
        public PreprocessorData Output = new PreprocessorData();

        #region ViewModel
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
        //Angular Accelerations
        float _wx_dot;
        public float WX_dot
        {
            get
            {
                return _wx_dot;
            }
            set
            {
                _wx_dot = value;
                OnPropertyChanged(nameof(WX_dot));
            }
        }
        float _wy_dot;
        public float WY_dot
        {
            get
            {
                return _wy_dot;
            }
            set
            {
                _wy_dot = value;
                OnPropertyChanged(nameof(WY_dot));
            }
        }
        float _wz_dot;
        public float WZ_dot
        {
            get
            {
                return _wz_dot;
            }
            set
            {
                _wz_dot = value;
                OnPropertyChanged(nameof(WZ_dot));
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
        float _deltatime;
        public float DELTATIME
        {
            get
            {
                return _deltatime;
            }
            set
            {
                _deltatime = value;
                OnPropertyChanged(nameof(DELTATIME));
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
        #endregion

        private string[] Chunks = new string[19];

        public void ChopParseAndPackage(string rawdatastring)
        {
            ChopStringIntoChunks(rawdatastring);
            ParseChunks();
            PackIntoData();
        }

        private void ChopStringIntoChunks( string s)
        {
            Chunks = s.Split(',');
        }
        private void ParseChunks()
        {
            //Airdata
            IAS         = Convert.ToSingle(Chunks[0], GlobalVars.myNumberFormat(7));
            MACH        = Convert.ToSingle(Chunks[1], GlobalVars.myNumberFormat(7));
            TAS         = Convert.ToSingle(Chunks[2], GlobalVars.myNumberFormat(7));
            GS          = Convert.ToSingle(Chunks[3], GlobalVars.myNumberFormat(7));
            AOA         = Convert.ToSingle(Chunks[4], GlobalVars.myNumberFormat(7));
            VS          = Convert.ToSingle(Chunks[5], GlobalVars.myNumberFormat(7));
            HGT         = Convert.ToSingle(Chunks[6], GlobalVars.myNumberFormat(7));

            //Euler
            BANK        = Convert.ToSingle(Chunks[7], GlobalVars.myNumberFormat(7));
            HDG         = Convert.ToSingle(Chunks[8], GlobalVars.myNumberFormat(7));
            PITCH       = Convert.ToSingle(Chunks[9], GlobalVars.myNumberFormat(7));

            //Rates
            WX          = Convert.ToSingle(Chunks[10], GlobalVars.myNumberFormat(7));
            WY          = Convert.ToSingle(Chunks[11], GlobalVars.myNumberFormat(7));
            WZ          = Convert.ToSingle(Chunks[12], GlobalVars.myNumberFormat(7));

            //Angular Acceleration
            WX_dot      = Convert.ToSingle(Chunks[13], GlobalVars.myNumberFormat(7));
            WY_dot      = Convert.ToSingle(Chunks[14], GlobalVars.myNumberFormat(7));
            WZ_dot      = Convert.ToSingle(Chunks[15], GlobalVars.myNumberFormat(7));

            //Accels
            AX           = Convert.ToSingle(Chunks[16], GlobalVars.myNumberFormat(7));
            AY           = Convert.ToSingle(Chunks[17], GlobalVars.myNumberFormat(7));
            AZ           = Convert.ToSingle(Chunks[18], GlobalVars.myNumberFormat(7));

            //Meta
            TIME        = Convert.ToSingle(Chunks[19], GlobalVars.myNumberFormat(7));
            DELTATIME   = Convert.ToSingle(Chunks[20], GlobalVars.myNumberFormat(7));
            COUNTER     = Convert.ToSingle(Chunks[21], GlobalVars.myNumberFormat(7));

            SIM         = Chunks[22];

        }
        private void PackIntoData()
        {
            //Airdata
            Output.IAS      = IAS;
            Output.MACH     = MACH;
            Output.TAS      = TAS;
            Output.GS       = GS;
            Output.AOA      = AOA;
            Output.VS       = VS;
            Output.HGT      = HGT;
            //Euler                 
            Output.BANK     = BANK;
            Output.HDG      = HDG;
            Output.PITCH    = PITCH;

            //Rates
            Output.WX       = WX;
            Output.WY       = WY;
            Output.WZ       = WZ;

            //Angular Acceleration
            Output.WX_dot = WX_dot;
            Output.WY_dot = WY_dot;
            Output.WZ_dot = WZ_dot;

            //Accels          
            Output.AX       = AX;
            Output.AY       = AY;
            Output.AZ       = AZ;
            //Meta
            Output.TIME     = TIME;
            Output.COUNTER  = COUNTER;
            Output.SIM      = SIM; 
        }
    }

}
