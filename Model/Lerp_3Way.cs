using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MOTUS.Model
{
    public class Lerp_3Way
    {
        public Transform3D Output { get; set; }
        public Lerp Lerp_ParkPause = new Lerp();
        public Lerp Lerp_PauseMotion = new Lerp();

        Lerp3_State _state;
        public Lerp3_State State 
        { 
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    UpdateUI_ViaDispatcherInvoke();
                }
            }
        }

        Lerp3_Command _command;
        public Lerp3_Command Command
        {
            get { return _command; }
            set
            {
                {
                    _command = value;
                    switch (value)
                    {
                        case Lerp3_Command.Park:
                            if (State == Lerp3_State.Pause) { Lerp_ParkPause.Run(); }
                            break;

                        case Lerp3_Command.Pause:
                            if (State == Lerp3_State.Park) { Lerp_ParkPause.Run(); }
                            else if (State == Lerp3_State.Motion) { Lerp_PauseMotion.Run(); }
                            break;

                        case Lerp3_Command.Motion:
                            if (State == Lerp3_State.Pause)
                            { 
                                //To-Do: Make sure no crash is detected, only then...
                                Lerp_PauseMotion.Run(); 
                            }
                            break;

                        default:
                            throw new Exception();
                    }
                }
            }
        }

        public Lerp_3Way()
        {
            Lerp_ParkPause.Duration = TimeSpan.FromSeconds(5);
            Lerp_PauseMotion.Duration = TimeSpan.FromSeconds(5);
            SetToA();
            //Just switch a cycle to make sure the State gets Updated at first Startup
            State = Lerp3_State.Dummy;
            State = Lerp3_State.Park;
        }
        public Lerp_3Way(TimeSpan time_AB, TimeSpan time_BC)
        {
            Lerp_ParkPause.Duration = time_AB;
            Lerp_PauseMotion.Duration = time_BC;
            SetToA();
            //Just switch a cycle to make sure the State gets Updated at first Startup
            State = Lerp3_State.Dummy;
            State = Lerp3_State.Park;
        }

        public void SetToA()
        {
            Lerp_ParkPause.ResetTo_Zero();
            Lerp_PauseMotion.ResetTo_Zero();
        }
        public void SetToB()
        {
            Lerp_ParkPause.ResetTo_One();
            Lerp_PauseMotion.ResetTo_Zero();
        }
        public void SetToC()
        {
            Lerp_ParkPause.ResetTo_One();
            Lerp_PauseMotion.ResetTo_One();
        }

        public void EMERGENCY_OnCrashDetected()
        {
            if (State == Lerp3_State.Motion) Command = Lerp3_Command.Pause;                     //Just like pushing the button
            if (State == Lerp3_State.Transit_Pause2Motion) Lerp_PauseMotion.Reverse();          //Hard option!!! To-do: give the Lerp a 3rd order LP transition 
        }


        public void LerpBetween(Transform3D TF1, Transform3D TF2, Transform3D TF3)
        {
            Lerp_ParkPause.Update();
            Lerp_PauseMotion.Update();

            State = DetermineState();
            Output = CreateInterpolation(TF1, TF2, TF3);
        }
        private Lerp3_State DetermineState()
        {
            //Steady state:
            if (Lerp_ParkPause.IsFullDown && Lerp_PauseMotion.IsFullDown)                   return Lerp3_State.Park;
            if (Lerp_ParkPause.IsFullUp && Lerp_PauseMotion.IsFullDown)                     return Lerp3_State.Pause;
            if (Lerp_ParkPause.IsFullUp && Lerp_PauseMotion.IsFullUp)                       return Lerp3_State.Motion;

            //All other cases must be Transit states
            if (Lerp_ParkPause.IsMovingUpwards && Lerp_PauseMotion.IsFullDown)              return Lerp3_State.Transit_Park2Pause;
            if (Lerp_ParkPause.IsMovingDownwards)   return Lerp3_State.Transit_Pause2Park;
            
            if (Lerp_PauseMotion.IsMovingUpwards)   return Lerp3_State.Transit_Pause2Motion;
            if (Lerp_PauseMotion.IsMovingDownwards) return Lerp3_State.Transit_Motion2Pause;
            

            //You should never get here!
            throw new Exception("Unhandled State! ");
            
        }
        private Transform3D CreateInterpolation(Transform3D tf1, Transform3D tf2, Transform3D tf3)
        {                                               //Park              Pause           Motion
            Transform3D TF_AB = Utility.Lerp(tf1, tf2, Lerp_ParkPause.Ratio_external);
            Transform3D TF_ABC = Utility.Lerp(TF_AB, tf3, Lerp_PauseMotion.Ratio_external);

            return TF_ABC;
        }
        
        //-----------------------------------------------------------------------------------------------
        private void UpdateUI_ViaDispatcherInvoke()
        {
            Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback(UpdateViewModel), State);
        }

        #region Callback to update UI
        private delegate void UpdateViewModel_Callback(Lerp3_State st);
        private void UpdateViewModel(Lerp3_State st)
        {
            //This code runs on the Main thread!
            var mainwindow = Application.Current.MainWindow as MainWindow;
            if (mainwindow != null)
            {
                mainwindow.engine.VM_MotionControlWindow.State = st;    
            }
        }
        #endregion
    }

    public enum Lerp3_State
    {
        Park,
        Pause,
        Motion,
        Dummy,                          //This is necessary to switch a "dummy-cycle" during the construction (see constructor above)
        Transit_Park2Pause,
        Transit_Pause2Park,
        Transit_Pause2Motion,
        Transit_Motion2Pause
    }
    public enum Lerp3_Command
    {
        Park,
        Pause,
        Motion
    }
}
