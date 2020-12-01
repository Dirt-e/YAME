using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MOTUS.Model
{
    public class Fader_Threeway : MyObject
    {
        public Fader Fader_AB = new Fader();
        public Fader Fader_BC = new Fader();

        FaderState _state;
        public FaderState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }

        public Fader_Threeway(TimeSpan time_AB, TimeSpan time_BC)
        {
            Fader_AB.Duration = time_AB;
            Fader_BC.Duration = time_BC;
            SetToA();
        }

        public void OnPressA()
        {
            if (State == FaderState.B)
            {
                Fader_AB.Run();
                State = FaderState.InTransit;
            }
        }
        public void OnPressB()
        {
            if (State == FaderState.A) { Fader_AB.Run(); }
            else if (State == FaderState.C) { Fader_BC.Run(); }
        }
        public void OnPressC()
        {
            if (State == FaderState.B) { Fader_BC.Run(); }
        }

        public void SetToA()
        {
            Fader_AB.ResetTo_Zero();
            Fader_BC.ResetTo_Zero();
        }
        public void SetToB()
        {
            Fader_AB.ResetTo_One();
            Fader_BC.ResetTo_Zero();
        }
        public void SetToC()
        {
            Fader_AB.ResetTo_One();
            Fader_BC.ResetTo_One();
        }

        public Transform3D CreateInperpolation(Transform3D tf1, Transform3D tf2, Transform3D tf3)
        {                                               //Park              Pause           Motion
            Transform3D TF_AB = Utility.Lerp(tf1, tf2, Fader_AB.Ratio);
            Transform3D TF_ABC = Utility.Lerp(TF_AB, tf3, Fader_BC.Ratio);

            return TF_ABC;
        }

        public void Update()
        {
            DetrmineFaderStatus();
            SetLightsInPanelMotionControl();
        }

        //Helpers:
        private void DetrmineFaderStatus()
        {
            if (Fader_AB.Ratio == 0 && Fader_BC.Ratio == 0) { State = FaderState.A; }
            else if (Fader_AB.Ratio == 1 && Fader_BC.Ratio == 0) { State = FaderState.B; }
            else if (Fader_AB.Ratio == 1 && Fader_BC.Ratio == 1) { State = FaderState.C; }

            //All other cases must be Transit cases
            else { State = FaderState.InTransit; }
        }
        private void SetLightsInPanelMotionControl()
        {
            //var ViewModel = engine.integrator.VM_MotionControl;

            //switch (_state)
            //{
            //    case FaderState.A:
            //        ViewModel.InParkPosition = true;
            //        break;
            //    case FaderState.B:
            //        ViewModel.InPausePosition = true;
            //        break;
            //    case FaderState.C:
            //        ViewModel.InMotionPosition = true;
            //        break;
            //    case FaderState.InTransit:
            //        ViewModel.InTransit = true;
            //        break;
            //    default:
            //        break;
            //}

        }

    }



    public enum FaderState
    {
        A,
        B,
        C,
        InTransit
    }
}
