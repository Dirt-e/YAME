using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class ScalerModule : MyObject
    {
        private LowPassNthOrder LP = new LowPassNthOrder(3);

        #region Viewmodel
        private float _input;
        public float Input
        {
            get { return _input; }
            set { _input = value; OnPropertyChanged(nameof(Input)); }
        }
        private float _gain_raw;
        public float Gain_raw
        {
            get { return _gain_raw; }
            set { _gain_raw = value; OnPropertyChanged(nameof(Gain_raw)); }
        }
        private float _output;
        public float Output
        {
            get { return _output; }
            set { _output = value; OnPropertyChanged(nameof(Output)); }
        } 
        #endregion

        public void Push(float val)
        {   
            Input = val;
            
            LP.Push(Gain_raw);
            
            Output = Input * LP.OutValue;
        }
    }
}
