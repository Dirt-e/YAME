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
        //public List<Actuator> Actuators;
        Actuator A1 = new Actuator();   
        Actuator A2 = new Actuator();   
        Actuator A3 = new Actuator();   
        Actuator A4 = new Actuator();   
        Actuator A5 = new Actuator();   
        Actuator A6 = new Actuator();   

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
                    //Debug:
                    Console.WriteLine("Status: " + AllInLimits.ToString());
                }
            }
        }

        public ActuatorSystem(ref IK_Module ikm)
        {
            IK_Module = ikm;
            
            A1 = new Actuator();
            A2 = new Actuator();
            A3 = new Actuator();
            A4 = new Actuator();
            A5 = new Actuator();
            A6 = new Actuator();

            Output = new SixSisters();

            invoke_timer.Start();       //to determine the time to update the "VM_SceneView" (33ms)
        }

        public void Update()
        {   
             A1.MinLength = MinLength;
            A2.MinLength = MinLength;
            A3.MinLength = MinLength;
            A4.MinLength = MinLength;
            A5.MinLength = MinLength;
            A6.MinLength = MinLength;

            A1.MaxLength = MaxLength;
            A2.MaxLength = MaxLength;
            A3.MaxLength = MaxLength;
            A4.MaxLength = MaxLength;
            A5.MaxLength = MaxLength;
            A6.MaxLength = MaxLength;
            
            
            A1.CurrentLength = IK_Module.Lengths[0];
            A2.CurrentLength = IK_Module.Lengths[1];
            A3.CurrentLength = IK_Module.Lengths[2];
            A4.CurrentLength = IK_Module.Lengths[3];
            A5.CurrentLength = IK_Module.Lengths[4];
            A6.CurrentLength = IK_Module.Lengths[5];

            AllInLimits =  DetermineSystemStatus();
            CreateOutput();

            UpdateUI_ViaDispatcherInvoke();
        }


        //Helpers:
        bool DetermineSystemStatus()
        {
            return (A1.Status == ActuatorStatus.Inlimits &&
                    A2.Status == ActuatorStatus.Inlimits &&
                    A3.Status == ActuatorStatus.Inlimits &&
                    A4.Status == ActuatorStatus.Inlimits &&
                    A5.Status == ActuatorStatus.Inlimits &&
                    A6.Status == ActuatorStatus.Inlimits    );
        }
        void CreateOutput()
        {
            Output.values[0] = A1.Utilisation;
            Output.values[1] = A2.Utilisation;
            Output.values[2] = A3.Utilisation;
            Output.values[3] = A4.Utilisation;
            Output.values[4] = A5.Utilisation;
            Output.values[5] = A6.Utilisation;
        }


        private void UpdateUI_ViaDispatcherInvoke()
        {
            if (invoke_timer.ElapsedMilliseconds > 33)      //Update the UI only at ~30fps
            {
                ActuatorStatus_Struct ass = new ActuatorStatus_Struct()
                {
                    Act1_Status = A1.Status,
                    Act2_Status = A2.Status,
                    Act3_Status = A3.Status,
                    Act4_Status = A4.Status,
                    Act5_Status = A5.Status,
                    Act6_Status = A6.Status
                };
                Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback(UpdateViewModel), ass);

                invoke_timer.Restart();
            }

        }

        #region Callback
        private delegate void UpdateViewModel_Callback(ActuatorStatus_Struct ass);
        private void UpdateViewModel(ActuatorStatus_Struct ass)
        {
            //This code runs on the Main thread!
            var mainwindow = Application.Current.MainWindow as MainWindow;
            if (mainwindow != null)
            {
                mainwindow.engine.VM_SceneView.Act1_Status = ass.Act1_Status; 
                mainwindow.engine.VM_SceneView.Act2_Status = ass.Act2_Status; 
                mainwindow.engine.VM_SceneView.Act3_Status = ass.Act3_Status; 
                mainwindow.engine.VM_SceneView.Act4_Status = ass.Act4_Status; 
                mainwindow.engine.VM_SceneView.Act5_Status = ass.Act5_Status; 
                mainwindow.engine.VM_SceneView.Act6_Status = ass.Act6_Status; 
            }
        }
        #endregion
    }
}
