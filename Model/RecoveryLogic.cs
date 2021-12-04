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
            set     //State can only run in circles  Dormant->Informed->Revocering->Dormant
            {
                if (value == _state) return;        //Nothing to do

                switch (State)                      //These run only ONCE when the State CHANGES!
                {
                    case Recovery_State.Dormant:
                        if (value == Recovery_State.Crash_Informed)
                        {
                            _state = Recovery_State.Crash_Informed;
                            SetLight(Colors.Red);
                            SetLight("CRASHED", "Press to reset");
                            SetText(Colors.Black);
                            MoveRigToPause();
                        }
                        break;
                    case Recovery_State.Crash_Informed:
                        if (value == Recovery_State.Recovering)
                        {
                            _state = Recovery_State.Recovering;
                            
                        }
                        break;
                    case Recovery_State.Recovering:
                        if (value == Recovery_State.Dormant) _state = Recovery_State.Dormant;
                        break;
                    default:
                        throw new ArgumentException("WTF is " + State + "?");
                }
            }
        }

        public RecoveryLogic(Engine e)
        {
            engine = e;
        }

        public void Update()
        {
            if (State == Recovery_State.Recovering)
            {
                if (engine.integrator.Lerp_3Way.State == Lerp3_State.Pause)     //When the rig reaches PAUSE...
                {
                    CleanUp();                                                  //Clean
                }
            }
        }

        //Helpers:
        void MoveRigToPause()
        {
            engine.integrator.Lerp_3Way.Command = Lerp3_Command.Pause;
        }
        void CleanUp()
        {
            //Switch all lights off
        }
        void SetLight(Color col)
        {
            Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback_color(UpdateViewModel_Light_Color), col);
        }
        void SetLight(string s1,string s2)
        {
            Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback_string(UpdateViewModel_Light_Text), s1,s2);
        }
        void SetText(Color col)
        {
            Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback_color(UpdateViewModel_Text_Color), col);
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
        Acknoledged,
        Recovering
    }
}
