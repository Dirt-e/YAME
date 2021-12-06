using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MOTUS.Model
{
    public class RecoveryLogic : MyObject
    {
        Recovery_State _state = Recovery_State.Dormant;
        public Recovery_State State
        { 
            get { return _state; }
            set 
            {
                if (value == _state) return;        //No Change -> Nothing to do.

                //State can only run in circles:
                //Dormant->CrashInformed->Revocering->WaitingForAcknoledgement->Acknoledged->CleanedUp->Dormant
                switch (State)                      
                {
                    case Recovery_State.Dormant:
                        if (value == Recovery_State.Crash_Informed)             _state = Recovery_State.Crash_Informed;
                        break;
                    case Recovery_State.Crash_Informed:
                        if (value == Recovery_State.Recovering)                 _state = Recovery_State.Recovering;
                        break;
                    case Recovery_State.Recovering:
                        if (value == Recovery_State.WaitingForAcknoledgement)   _state = Recovery_State.WaitingForAcknoledgement;
                        break;
                    case Recovery_State.WaitingForAcknoledgement:
                        if (value == Recovery_State.Acknoledged)                _state = Recovery_State.Acknoledged;
                        break;
                    case Recovery_State.Acknoledged:
                        if (value == Recovery_State.Cleaned_Up)                  _state = Recovery_State.Cleaned_Up;
                        break;
                    case Recovery_State.Cleaned_Up:
                        if (value == Recovery_State.Dormant)                    _state = Recovery_State.Dormant; 
                        break;
                    default:
                        throw new ArgumentException("WTF is " + State + "?");
                }
            }
        }

        public RecoveryLogic(Engine e)
        {
            engine = e;
            SetLight(Color.FromArgb(255, 50, 50, 50), Colors.LightGreen, "READY", "for Motion");
        }

        public void Update()
        {
            switch (State)
            {
                case Recovery_State.Dormant:                                        //Do nothing. Will move on when informed about Crash.
                    break;
                case Recovery_State.Crash_Informed:
                    engine.protector.IsLatched = true;
                    SetLight(Colors.Red, Colors.Black, "CRASHED", "Wait for recovery");
                    RecoverRig();
                    State = Recovery_State.Recovering;
                    goto case Recovery_State.Recovering;                            //Move on.
                case Recovery_State.Recovering:
                    if (engine.integrator.Lerp_3Way.State == Lerp3_State.Pause || 
                        engine.integrator.Lerp_3Way.State == Lerp3_State.Park ||
                        engine.integrator.Lerp_3Way.State == Lerp3_State.Transit_Park2Pause ||
                        engine.integrator.Lerp_3Way.State == Lerp3_State.Transit_Pause2Park)      //Do nothing. Will move on
                    {
                        SetLight(Colors.Orange, Colors.Black, "RIG RECOVERED", "Press to reset");
                        State = Recovery_State.WaitingForAcknoledgement;
                        goto case Recovery_State.WaitingForAcknoledgement;          //...move on.
                    }
                    break;
                case Recovery_State.WaitingForAcknoledgement:                       //Do nothing. Will move on when Button is pushed
                    break;
                case Recovery_State.Acknoledged:
                    SetLight(Color.FromArgb(255,50,50,50), Colors.LightGreen, "READY", "for Motion");
                    //Reset all Filters (equilibrium)
                    engine.protector.IsLatched = false;                             //Clean up.
                    State = Recovery_State.Cleaned_Up;
                    goto case Recovery_State.Cleaned_Up;                            //Move on.
                case Recovery_State.Cleaned_Up:
                    State = Recovery_State.Dormant;                                 //Move on.
                    break;
                default:
                    break;
            }
        }

        //Helpers:
        void RecoverRig()
        {
            if (engine.integrator.Lerp_3Way.State == Lerp3_State.Motion)
                engine.integrator.Lerp_3Way.Command = Lerp3_Command.Pause;      //Soft option
            if (engine.integrator.Lerp_3Way.State == Lerp3_State.Transit_Pause2Motion)
                engine.integrator.Lerp_3Way.EMERGENCY_OnCrashDetected();        //Hard option
        }
        void SetLight(Color lgt_col, Color txt_col, string line1, string line2)
        {
            Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback_color(UpdateViewModel_Light_Color), lgt_col);
            Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback_color(UpdateViewModel_Text_Color), txt_col);
            Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback_string(UpdateViewModel_Light_Text), line1, line2);
        }

        #region UI_Callbacks
        private delegate void UpdateViewModel_Callback_color(Color c);
        private delegate void UpdateViewModel_Callback_string(string s1, string s2);
        //These functions run on the Main thread:
        private void UpdateViewModel_Light_Color(Color c)
        {
            if (Application.Current.MainWindow is MainWindow mainwindow)
            {
                mainwindow.engine.VM_CrashDetector.LightColor = new SolidColorBrush(c);
            }
        }
        private void UpdateViewModel_Light_Text(string s1, string s2)
        {
            if (Application.Current.MainWindow is MainWindow mainwindow)
            {
                mainwindow.engine.VM_CrashDetector.Line1 = s1;
                mainwindow.engine.VM_CrashDetector.Line2 = s2;
            }
        }
        private void UpdateViewModel_Text_Color(Color c)
        {
            if (Application.Current.MainWindow is MainWindow mainwindow)
            {
                mainwindow.engine.VM_CrashDetector.TextColor = new SolidColorBrush(c);
            }
        }
        #endregion
    }

    public enum Recovery_State
    {
        Dormant,
        Crash_Informed,
        Recovering,
        WaitingForAcknoledgement,
        Acknoledged,
        Cleaned_Up
    }
}
