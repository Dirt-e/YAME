using System;
using System.Collections.Generic;
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

        public ODriveSystem(Engine e)
        {
            engine = e;

            oDriveTalkers[0] = new ODriveTalker(engine, OdriveNumber.ODrive_1);
            oDriveTalkers[1] = new ODriveTalker(engine, OdriveNumber.ODrive_2);
            oDriveTalkers[2] = new ODriveTalker(engine, OdriveNumber.ODrive_3);
        }

        public void Process(SixSisters ss)
        {
            float revolutionsPerFullStroke = engine.actuatorsystem.Stroke / Lead;
            
            float[] revolutions = new float[6];
            for (int i = 0; i < revolutions.Count(); i++)
            {
                revolutions[i] = ss.values[i] * revolutionsPerFullStroke;
            }

            oDriveTalkers[0].Update(revolutions[0], revolutions[1]);
            oDriveTalkers[1].Update(revolutions[2], revolutions[3]);
            oDriveTalkers[2].Update(revolutions[4], revolutions[5]);
        }
    }
}
