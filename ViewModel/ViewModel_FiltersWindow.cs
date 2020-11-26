using MOTUS.Model;
using MOTUS.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.ViewModel
{
    public class ViewModel_FiltersWindow : _ViewModel
    {
        #region Inverters
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
        #endregion

        #region Filter WX_HP
        //public VM_FilterBox Wx_HP;
        //public VM_FilterBox Wx_HP_LP;
        //public VM_FilterBox Wy_HP;
        //public VM_FilterBox Wy_HP_LP;
        //public VM_FilterBox Wz_HP;
        //public VM_FilterBox Wz_HP_LP;

        //public VM_FilterBox Ax_HP;
        //public VM_FilterBox Ax_HP_LP2;
        //public VM_FilterBox Ax_LP3;

        //public VM_FilterBox Ay_HP;
        //public VM_FilterBox Ay_HP_LP2;

        //public VM_FilterBox Az_HP;
        //public VM_FilterBox Az_HP_LP2;
        //public VM_FilterBox Az_LP3;
        #endregion

        public ViewModel_FiltersWindow(Engine e)
        {
            base.engine = e;
        }
    }

    public class VM_FilterBox_WX_HP : _ViewModel
    {
        private float _invalue;
        public float InValue
        {
            get { return _invalue; }
            set { _invalue = value; OnPropertyChanged("InValue"); }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged("Code"); }
        }
        
        private float _filtervariable;
        public float FilterVariable
        {
            get { return _filtervariable; }
            set 
            { 
                _filtervariable = value;
                engine.VM_FilterBox_WX_HP.FilterVariable = value;
                OnPropertyChanged("FilterVariable"); 
            }
        }

        private float _outvalue;
        public float OutValue
        {
            get { return _outvalue; }
            set { _outvalue = value; OnPropertyChanged("OutValue"); }
        }

        public VM_FilterBox_WX_HP(Engine e)
        {
            base.engine = e;
        }

    }
}
