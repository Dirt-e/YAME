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
            set     //State can only run in circles
            {
                if (value == _state) return;        //Nothing to do

                switch (State)                      //This runs only ONCE when the State CHANGES!
                {
                    case Recovery_State.Dormant:
                        if (value == Recovery_State.Crash_Informed) _state = Recovery_State.Crash_Informed;
                        SwitchLight(Colors.Red);
                        SwitchText(Colors.Blue);
                        engine.integrator.Lerp_3Way.Command = Lerp3_Command.Pause;
                        break;
                    case Recovery_State.Crash_Informed:
                        if (value == Recovery_State.Recovering)
                        {
                            _state = Recovery_State.Recovering;
                            Recover();
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
                if (engine.integrator.Lerp_3Way.State == Lerp3_State.Pause)
                {
                    CleanUp();
                }
            }
        }

        //Helpers:
        void Recover()
        {
            engine.integrator.Lerp_3Way.Command = Lerp3_Command.Pause;
        }
        void CleanUp()
        {
            //Switch all lights off
        }
        void SwitchLight(Color col)
        {
            Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback(UpdateViewModel_Light), col);
        }
        void SwitchText(Color col)
        {
            Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback(UpdateViewModel_Text), col);
        }

        #region UI_Callbacks
        private delegate void UpdateViewModel_Callback(Color c);
        //These functions run on the Main thread:
        private void UpdateViewModel_Light(Color c)
        {
            if (Application.Current.MainWindow is MainWindow mainwindow)
            {
                mainwindow.engine.VM_CrashDetector.LightColor = new SolidColorBrush(c);
            }
        }
        private void UpdateViewModel_Text(Color c)
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
        Recovering
    }
}
