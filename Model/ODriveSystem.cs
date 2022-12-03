using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YAME.DataFomats;
using static Utility;

namespace YAME.Model
{
    public class ODriveSystem : MyObject
    {
        public ODriveTalker[] oDriveTalkers = new ODriveTalker[3];

        int _lead;
        public int Lead
        {
            get { return _lead; }
            set
            {
                _lead = Clamp(value, 1, 50);
                OnPropertyChanged(nameof(Lead));
            }
        }

        bool _isanyportopen;
        public bool IsAnyPortOpen
        {
            get { return    oDriveTalkers[0].IsOpen ||
                            oDriveTalkers[1].IsOpen ||
                            oDriveTalkers[2].IsOpen; }
            set 
            { 
                _isanyportopen =    oDriveTalkers[0].IsOpen ||
                                    oDriveTalkers[1].IsOpen ||
                                    oDriveTalkers[2].IsOpen; ; 
                
                OnPropertyChanged(nameof(IsAnyPortOpen)); 
            }
        }

        string _formatstring_1;
        public string FormatString_1
        { 
            get { return _formatstring_1; } 
            set { _formatstring_1 = value; OnPropertyChanged(nameof(FormatString_1)); } 
        }

        string _formatstring_2;
        public string FormatString_2
        {
            get { return _formatstring_2; }
            set { _formatstring_2 = value; OnPropertyChanged(nameof(FormatString_2)); }
        }

        string _formatstring_3;
        public string FormatString_3
        {
            get { return _formatstring_3; }
            set { _formatstring_3 = value; OnPropertyChanged(nameof(FormatString_3)); }
        }

        public ODriveSystem(Engine e)
        {
            engine = e;

            oDriveTalkers[0] = new ODriveTalker(engine, OdriveNumber.ODrive_1);
            oDriveTalkers[1] = new ODriveTalker(engine, OdriveNumber.ODrive_2);
            oDriveTalkers[2] = new ODriveTalker(engine, OdriveNumber.ODrive_3);
        }

        public void Process(SixSisters ss)
        {
            if(IsAnyPortOpen)
            {
                float revolutionsPerFullStroke = engine.actuatorsystem.Stroke / Lead;
            
                float[] revolutions = new float[6];
                for (int i = 0; i < revolutions.Count(); i++)
                {
                    revolutions[i] = ss.values[i] * revolutionsPerFullStroke;
                }

                oDriveTalkers[0].Update(FormatString_1, revolutions);
                oDriveTalkers[1].Update(FormatString_2, revolutions);
                oDriveTalkers[2].Update(FormatString_3, revolutions);
            }
            
        }
    }
}
