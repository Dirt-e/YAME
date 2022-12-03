using YAME.DataFomats;
using YAME.Model.Structs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YAME.Model
{
    public class ActuatorSystem : MyObject
    {
        public Actuator A1 = new Actuator();
        public Actuator A2 = new Actuator();   
        public Actuator A3 = new Actuator();   
        public Actuator A4 = new Actuator();   
        public Actuator A5 = new Actuator();   
        public Actuator A6 = new Actuator();   

        public SixSisters Output;
        Stopwatch invoke_timer = Stopwatch.StartNew();      //to determine the time to update the "VM_SceneView" (33ms)
        IK_Module IK_Module;

        //ViewModel:
        float _minlength;
        public float MinLength
        {
            get { return _minlength; }
            set
            {
                if (value < 0)  _minlength = 0;
                else            _minlength = value;
                
                A1.MinLength = value;
                A2.MinLength = value;
                A3.MinLength = value;
                A4.MinLength = value;
                A5.MinLength = value;
                A6.MinLength = value;

                OnPropertyChanged("MinLength");
            }
        }
        float _stroke;
        public float Stroke
        {
            get { return _stroke; }
            set
            {
                if (value > 0)  _stroke = value;
                else            _stroke = 1;
                
                A1.Stroke = Stroke;
                A2.Stroke = Stroke;
                A3.Stroke = Stroke;
                A4.Stroke = Stroke;
                A5.Stroke = Stroke;
                A6.Stroke = Stroke;

                OnPropertyChanged("Stroke");
            }
        }
        //Non-UI:
        public List<float> UtilisationList
        {
            get
            {
                List<float> UtilList = new List<float>
                {
                    A1.Utilisation,
                    A2.Utilisation,
                    A3.Utilisation,
                    A4.Utilisation,
                    A5.Utilisation,
                    A6.Utilisation
                };

                return UtilList;
            }
        }
        public bool IsPrettymuchLevel
        {
            get
            {
                //deternmine std. deviation...
                double StdDev = Utility.getStandardDeviation(UtilisationList);

                throw new NotImplementedException();
                //return StdDev < 0.1f;
            }
        }
        public bool AllInLimits
        {
            get
            {
                return (A1.InLimits &&
                        A2.InLimits &&
                        A3.InLimits &&
                        A4.InLimits &&
                        A5.InLimits &&
                        A6.InLimits);
            }
        }
        public bool Is_AllActuatorsFullyRetracted
        {
            get
            {
                return  A1.Status == ActuatorStatus.FullyRetracted &&
                        A2.Status == ActuatorStatus.FullyRetracted &&
                        A3.Status == ActuatorStatus.FullyRetracted &&
                        A4.Status == ActuatorStatus.FullyRetracted &&
                        A5.Status == ActuatorStatus.FullyRetracted &&
                        A6.Status == ActuatorStatus.FullyRetracted;
            }
        }

        public ActuatorSystem(IK_Module ikm)
        {
            IK_Module = ikm;
            
            A1 = new Actuator();
            A2 = new Actuator();
            A3 = new Actuator();
            A4 = new Actuator();
            A5 = new Actuator();
            A6 = new Actuator();

            Output = new SixSisters();     
        }

        public void Update()
        {   
            A1.CurrentLength = IK_Module.Lengths[0];
            A2.CurrentLength = IK_Module.Lengths[1];
            A3.CurrentLength = IK_Module.Lengths[2];
            A4.CurrentLength = IK_Module.Lengths[3];
            A5.CurrentLength = IK_Module.Lengths[4];
            A6.CurrentLength = IK_Module.Lengths[5];

            CreateOutput();
            UpdateUI_ViaDispatcherInvoke();
        }

        //Helpers:
        void CreateOutput()
        {
            Output.Values[0] = A1.Utilisation;
            Output.Values[1] = A2.Utilisation;
            Output.Values[2] = A3.Utilisation;
            Output.Values[3] = A4.Utilisation;
            Output.Values[4] = A5.Utilisation;
            Output.Values[5] = A6.Utilisation;
        }
        
        #region Callback
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
