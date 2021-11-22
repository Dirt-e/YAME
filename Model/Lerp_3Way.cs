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
                if (value != _command)
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
                            if (State == Lerp3_State.Pause) { Lerp_PauseMotion.Run(); }
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
        }
        public Lerp_3Way(TimeSpan time_AB, TimeSpan time_BC)
        {
            Lerp_ParkPause.Duration = time_AB;
            Lerp_PauseMotion.Duration = time_BC;
            SetToA();
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
            if (Lerp_ParkPause.Ratio_external == 0 && Lerp_PauseMotion.Ratio_external == 0)         { return Lerp3_State.Park; }
            else if (Lerp_ParkPause.Ratio_external == 1 && Lerp_PauseMotion.Ratio_external == 0)    { return Lerp3_State.Pause; }
            else if (Lerp_ParkPause.Ratio_external == 1 && Lerp_PauseMotion.Ratio_external == 1)    { return Lerp3_State.Motion; }

            //All other cases must be Transit states
            else if (Lerp_PauseMotion.Ratio_external == 0)  { return Lerp3_State.Transit_ParkPause; }
            else if (Lerp_ParkPause.Ratio_external == 1)    { return Lerp3_State.Transit_PauseMotion; }

            //This should never happen:
            else { throw new Exception(); }
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
        Transit,
        Transit_ParkPause,
        Transit_PauseMotion
    }
    public enum Lerp3_Command
    {
        Park,
        Pause,
        Motion
    }
}
