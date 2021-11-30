using MOTUS.DataFomats;
using MOTUS.Model.Structs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MOTUS.Model
{
    public class ActuatorSystem : MyObject
    {
        public List<Actuator> Actuators;
        public SixSisters Output;
        Stopwatch invoke_timer = new Stopwatch();
        IK_Module IK_Module;

        //External:
        float _minlength;
        public float MinLength
        {
            get { return _minlength; }
            set
            {
                _minlength = Math.Min(MaxLength, value);
                //redraw();
                OnPropertyChanged("MinLength");
            }
        }
        float _maxlength;
        public float MaxLength
        {
            get { return _maxlength; }
            set
            {
                _maxlength = Math.Max(MinLength, value);
                //redraw();
                OnPropertyChanged("MaxLength");
            }
        }
        //Internal:
        bool _allinlimits;
        public bool AllInLimits
        {
            get { return _allinlimits; }
            private set
            {
                if (_allinlimits != value)
                {
                    _allinlimits = value; 
                    OnPropertyChanged("AllInLimits"); 
                }
            }
        }

        public ActuatorSystem(ref IK_Module ikm)
        {
            IK_Module = ikm;
            Actuators = new List<Actuator>() {  new Actuator(),
                                                new Actuator(),
                                                new Actuator(),
                                                new Actuator(),
                                                new Actuator(),
                                                new Actuator()  };
            Output = new SixSisters();

            invoke_timer.Start();       //to determine the time to update the "VM_SceneView" (33ms)
        }

        public void Update()
        {   
            foreach (Actuator act in Actuators)
            {
                act.MinLength = MinLength;
                act.MaxLength = MaxLength;
            }

            for (int i = 0; i < 6; i++)
            {
                Actuators[i].CurrentLength = IK_Module.Lengths[i];
            }
            AllInLimits =  DetermineSystemStatus();
            CreateOutput();

            //UpdateUI_ViaDispatcherInvoke();
        }


        //Helpers:
        bool DetermineSystemStatus()
        {
            foreach (Actuator act in Actuators)                             //...but let each actuator speak.
            {
                if (act.Status != ActuatorStatus.Inlimits)  return false;   //A single vote is enough to spoil the party!
            }
            return true;                                                    //No objections!
        }
        void CreateOutput()
        {
            for (int i = 0; i < 6; i++)
            {
                Output.values[i] = Actuators[i].Utilisation;
            }
        }





        //private void UpdateUI_ViaDispatcherInvoke()
        //{
        //    if (invoke_timer.ElapsedMilliseconds > 33)      //Update the UI only at ~30fps
        //    {
        //        ActuatorColors_Struct actColStr = new ActuatorColors_Struct()
        //        {
        //            //Act1_ColorBrush = 
        //            //Act2_ColorBrush = 
        //            //Act3_ColorBrush = 
        //            //Act4_ColorBrush = 
        //            //Act5_ColorBrush = 
        //            //Act6_ColorBrush = 
        //        };
        //        Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback(UpdateViewModel), actColStr);

        //        invoke_timer.Restart();
        //    }

        //}

        //#region Callback
        //private delegate void UpdateViewModel_Callback(Platforms_Struct Mx);
        //private void UpdateViewModel(Platforms_Struct Mx)
        //{
        //    //This code runs on the Main thread!
        //    var mainwindow = Application.Current.MainWindow as MainWindow;
        //    if (mainwindow != null)
        //    {
                
        //    }
        //}
        //#endregion
    }
}
