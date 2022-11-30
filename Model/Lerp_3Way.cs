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

namespace YAME.Model
{
    public class Lerp_3Way : MyObject
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
                _command = value;
                switch (value)
                {
                    case Lerp3_Command.Park:
                        //if (State == Lerp3_State.Pause) { Lerp_ParkPause.Run(); }
                        Lerp_PauseMotion.Run(false);         //Experimental!
                        Lerp_ParkPause.Run(false);           //Experimental!
                        break;
             
                    case Lerp3_Command.Pause:
                        //if (State == Lerp3_State.Park) { Lerp_ParkPause.Run(); }
                        //else if (State == Lerp3_State.Motion) { Lerp_PauseMotion.Run(); }
                        Lerp_PauseMotion.Run(false);         //Experimental!
                        Lerp_ParkPause.Run(true);           //Experimental!
                        break;
             
                    case Lerp3_Command.Motion:
                        bool ExceedancePresent = engine.exceedancedetector.IsAnyExceedancePresent;
                        bool AcknoledgementOpen = engine.recoverylogic.State == Recovery_State.WaitingForAcknoledgement;
                        bool InPauseState = State == Lerp3_State.Pause;
                        //if ((!ExceedancePresent && !AcknoledgementOpen) && InPauseState)
                        //{ 
                        //    Lerp_PauseMotion.Run();
                        //}

                        if (!ExceedancePresent && !AcknoledgementOpen)            //Experimental
                        {
                            Lerp_PauseMotion.Run(true);         //Experimental!
                            Lerp_ParkPause.Run(true);           //Experimental!
                        }
                        break;
             
                    default:
                        throw new Exception();
                }
            }
        }

        public Lerp_3Way(Engine e)
        {
            engine = e;     //To access other objects

            Lerp_ParkPause.Duration = TimeSpan.FromSeconds(5);
            Lerp_PauseMotion.Duration = TimeSpan.FromSeconds(5);

            Lerp_ParkPause.Method = LerpOverMethod.LowPass3rdOrder;
            Lerp_PauseMotion.Method = LerpOverMethod.LowPass3rdOrder;

            SetToA();
            //Just switch a cycle to make sure the State gets Updated at first Startup
            State = Lerp3_State.Dummy;
            State = Lerp3_State.Park;
        }
        public Lerp_3Way(TimeSpan time_AB, TimeSpan time_BC, Engine e)
        {
            engine = e;     //To access other objects

            Lerp_ParkPause.Duration = time_AB;
            Lerp_PauseMotion.Duration = time_BC;

            Lerp_ParkPause.Method = LerpOverMethod.LowPass3rdOrder;
            Lerp_PauseMotion.Method = LerpOverMethod.LowPass3rdOrder;

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
            if (State == Lerp3_State.Motion ||
                State == Lerp3_State.TransitTowards_Motion)     Command = Lerp3_Command.Pause;                     //Just like pushing the button
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

            //All other cases must be Transit states:
            bool TraTo_Motion   = (Lerp_ParkPause.IsMovingUpwards   || Lerp_ParkPause.IsFullUp)     && (Lerp_PauseMotion.IsMovingUpwards    || Lerp_PauseMotion.IsFullUp);
            bool TraTo_Pause    = (Lerp_ParkPause.IsMovingUpwards   || Lerp_ParkPause.IsFullUp)     && (Lerp_PauseMotion.IsMovingDownwards  || Lerp_PauseMotion.IsFullDown);
            bool TraTo_Park     = (Lerp_ParkPause.IsMovingDownwards || Lerp_ParkPause.IsFullDown)   && (Lerp_PauseMotion.IsMovingDownwards  || Lerp_PauseMotion.IsFullDown);

            if (TraTo_Motion)   return Lerp3_State.TransitTowards_Motion;
            if (TraTo_Pause)    return Lerp3_State.TransitTowards_Pause;
            if (TraTo_Park)     return Lerp3_State.TransitTowards_Park;


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
        //Transit_Park2Pause,
        //Transit_Pause2Park,
        //Transit_Pause2Motion,
        //Transit_Motion2Pause
        TransitTowards_Motion,
        TransitTowards_Park,
        TransitTowards_Pause,
    }
    public enum Lerp3_Command
    {
        Park,
        Pause,
        Motion
    }
}
