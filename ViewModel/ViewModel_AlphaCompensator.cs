using MOTUS.Model;
using MOTUS.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.ViewModel
{
    public class ViewModel_AlphaCompensator : _ViewModel
    {
        float _aoa;
        public float AoA 
        { 
            get { return _aoa; }
            set
            {
                _aoa = value;
                engine.alphacompensator.AoA = value;
                OnPropertyChanged("AoA");
            }
        }
        
        float _aoa_zero;
        public float AoA_zero
        {
            get { return _aoa_zero; }
            set
            {
                _aoa_zero = value;
                engine.alphacompensator.AoA_zero = value;
                OnPropertyChanged("AoA_zero");

            }
        }
        
        float _alphacompensationpercentage;
        public float AlphaCompensationPercentage
        {
            get { return _alphacompensationpercentage; }
            set
            {
                _alphacompensationpercentage = value;
                engine.alphacompensator.AlphaCompensationPercentage = value;
                OnPropertyChanged("AlphaCompensationPercentage");
            }
        }

        float _ias_fade_in_start_speed;
        public float IAS_FadeIn_StartSpeed
        {
            get { return _ias_fade_in_start_speed; }
            set
            {
                _ias_fade_in_start_speed = value;
                engine.alphacompensator.IAS_FadeIn_StartSpeed = value;
                OnPropertyChanged("IAS_FadeIn_StartSpeed");
            }
        }

        float _ias_fade_in_done_speed;
        public float IAS_FadeIn_DoneSpeed
        {
            get { return _ias_fade_in_done_speed; }
            set
            {
                _ias_fade_in_done_speed = value;
                engine.alphacompensator.IAS_FadeIn_DoneSpeed = value;
                OnPropertyChanged("IAS_FadeIn_DoneSpeed");
            }
        }

        float _fade_in_percentage;
        public float FadeIn_Percentage
        {
            get { return _fade_in_percentage; }
            set
            {
                _fade_in_percentage = value;
                engine.alphacompensator.FadeIn_Percentage = value;
                OnPropertyChanged("FadeIn_Percentage");
            }
        }

        bool _is_active;
        public bool IsActive
        {
            get { return _is_active; }
            set
            {
                _is_active = value;
                engine.alphacompensator.IsActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        public ViewModel_AlphaCompensator(Engine e)
        {
            base.engine = e;
        }
    }
}
