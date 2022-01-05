using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class ScalerModule : INotifyPropertyChanged
    {
        private float _input;
        public float Input
        {
            get { return _input; }
            set { _input = value; OnPropertyChanged("Input"); }
        }
        private float _gain = 1;
        public float Gain
        {
            get { return _gain; }
            set { _gain = value; OnPropertyChanged("Gain"); }
        }
        private float _output;
        public float Output
        {
            get { return _output; }
            set { _output = value; OnPropertyChanged("Output"); }
        }

        public void Push(float val)
        {
            Input = val;
            Output = val * Gain;
        }

        //INotifyPropertyChanged:
        public event PropertyChangedEventHandler PropertyChanged;
        private protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
