using MOTUS.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.ViewModel
{
    public class ViewModel_PositionOffsetCorrector : _ViewModel
    {
        private float _delta_x = 0;
        public float Delta_X
        {
            get { return _delta_x; }
            set
            {
                _delta_x = value;
                OnPropertyChanged("Delta_X");
            }
        }
        private float _delta_y = 0;
        public float Delta_Y
        {
            get { return _delta_y; }
            set
            {
                _delta_y = value;
                OnPropertyChanged("Delta_Y");
            }
        }
        private float _delta_z = 0;
        public float Delta_Z
        {
            get { return _delta_z; }
            set
            {
                _delta_z = value;
                OnPropertyChanged("Delta_Z");
            }
        }

        private float _ax_output;
        public float Ax_output
        {
            get { return _ax_output; }
            set
            {
                _ax_output = value;
                OnPropertyChanged("Ax_output");
            }
        }
        private float _ay_output;
        public float Ay_output
        {
            get { return _ay_output; }
            set
            {
                _ay_output = value;
                OnPropertyChanged("Ay_output");
            }
        }
        private float _az_output;
        public float Az_output
        {
            get { return _az_output; }
            set
            {
                _az_output = value;
                OnPropertyChanged("Az_output");
            }
        }
    }
}
