using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class Inverter : MyObject
    {
        public PreprocessorData Output = new PreprocessorData();

        bool _invertWx;
        public bool InvertWx
        {
            get { return _invertWx; }
            set
            {
                _invertWx = value;
                OnPropertyChanged("InvertWx");
            }
        }
        bool _invertWy;
        public bool InvertWy
        {
            get { return _invertWy; }
            set
            {
                _invertWy = value;
                OnPropertyChanged("InvertWy");
            }
        }
        bool _invertWz;
        public bool InvertWz
        {
            get { return _invertWz; }
            set
            {
                _invertWz = value;
                OnPropertyChanged("InvertWz");
            }
        }
        bool _invertAx;
        public bool InvertAx
        {
            get { return _invertAx; }
            set
            {
                _invertAx = value;
                OnPropertyChanged("InvertAx");
            }
        }
        bool _invertAy;
        public bool InvertAy
        {
            get { return _invertAy; }
            set
            {
                _invertAy = value;
                OnPropertyChanged("InvertAy");
            }
        }
        bool _invertAz;
        public bool InvertAz
        {
            get { return _invertAz; }
            set
            {
                _invertAz = value;
                OnPropertyChanged("InvertAz");
            }
        }

        public void InvertDataAsNeeded(PreprocessorData data)
        {
            Output = data;
            //And then modify some
            if (InvertWx) Output.WX *= -1.0f;
            if (InvertWy) Output.WY *= -1.0f;
            if (InvertWz) Output.WZ *= -1.0f;
                                     
            if (InvertAx) Output.AX *= -1.0f;
            if (InvertAy) Output.AY *= -1.0f;
            if (InvertAz) Output.AZ *= -1.0f;
        }
    }
}
