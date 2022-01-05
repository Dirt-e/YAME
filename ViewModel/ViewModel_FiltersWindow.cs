using YAME.Model;
using YAME.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.ViewModel
{
    public class ViewModel_FiltersWindow : _ViewModel
    {
        #region Inverters
        bool _invertWx;
        public bool InvertWx
        {
            get { return _invertWx; }
            set
            {
                _invertWx = value;
                OnPropertyChanged("InvertWx");
                engine.inverter.Invert_Wx = value;
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
                engine.inverter.Invert_Wy = value;
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
                engine.inverter.Invert_Wz = value;
            }
        }
        bool _invertAx;
        public bool InvertAx
        {
            get { return _invertAx; }
            set 
            { 
                _invertAx = value;
                engine.inverter.Invert_Ax = value;
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
                engine.inverter.Invert_Ay = value; 
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
                engine.inverter.Invert_Az = value;
            }
        }
        
        #endregion

        public ViewModel_FiltersWindow(Engine e)
        {
            base.engine = e;
        }
    }
}
