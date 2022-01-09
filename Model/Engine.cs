using YAME.DataFomats;
using YAME.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace YAME.Model
{
    public class Engine : MyObject
    {   
        //UI-thread objects:
        public Server                   server;
        public LoaderSaver              loadersaver;

        //Worker-thread objects:
        public BackgroundWorker         backgroundworker;
        public Chopper                  chopper;
        public Inverter                 inverter;
        public ExceedanceDetector       exceedancedetector;
        public RecoveryLogic            recoverylogic;
        public PositionOffsetCorrector  positionoffsetcorrector;
        public Protector                protector;
        public AlphaCompensator         alphacompensator;
        public FilterSystem             filtersystem;
        public CompressorSystem         compressorsystem;
        public ScalerSystem             scalersystem;
        public ZeroMaker                zeromaker;
        public DOF_Override             dof_override;
        public Integrator               integrator;
        public IK_Module                IK_Module;
        public ActuatorSystem           actuatorsystem;
        public SerialTalker             serialtalker;
        //...
        //...
        //...

        //ViewModels:
        public ViewModel_MainWindow                 VM_MainWindow;
        public ViewModel_CrashDetector              VM_CrashDetector;
        public ViewModel_FiltersWindow              VM_FiltersWindow;
        public ViewModel_MotionControlWindow        VM_MotionControlWindow;
        public ViewModel_Sceneview                  VM_SceneView;
        //...
        //...
        //...


        //Internal properties:

        float _deltatime_processing;
        public float DeltatimeProcessing
        {
            get { return _deltatime_processing; }
            private set
            {
                _deltatime_processing = value; OnPropertyChanged(nameof(DeltatimeProcessing));
                FPS = 1000.0f / DeltatimeProcessing; ;
            }
        }
        float _fps;
        public float FPS
        {
            get { return _fps; }
            set { _fps = value; OnPropertyChanged(nameof(FPS)); }
        }
        Stopwatch stopwatch = Stopwatch.StartNew();

        public Engine()
        {
            backgroundworker    = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
            };
            server              = new Server();
            loadersaver         = new LoaderSaver(this);
            InstantiateViewModels();
        }
        
        private void InstantiateViewModels()
        {
            VM_MainWindow               = new ViewModel_MainWindow(this);
            VM_CrashDetector            = new ViewModel_CrashDetector(this);
            VM_FiltersWindow            = new ViewModel_FiltersWindow(this);
            VM_MotionControlWindow      = new ViewModel_MotionControlWindow();
            VM_SceneView                = new ViewModel_Sceneview(this);
            //...
            //...
            //...
        }
        
        public void StartEngine()
        {
            if (!backgroundworker.IsBusy)
            {
                server.StartServer();

                backgroundworker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    InstatiateObjects();
                    while (!backgroundworker.CancellationPending)
                    {
                        UpdateObjects();
                        WaitForTargetFramerate(500);
                        //if (VM_MainWindow.IsChecked_HotIdle) WaitForTargetFramerate(500);
                        //else if (VM_MainWindow.IsChecked_ShortSleep) ShortSleep(3);
                    }
                };
                backgroundworker.RunWorkerAsync();
            }
            
        }
        public void StopEngine()
        {
            backgroundworker.CancelAsync();
        }

        //------- This happens on the Worker Thread -----------------
        private void InstatiateObjects()
        {
            chopper                 = new Chopper();
            inverter                = new Inverter();
            exceedancedetector      = new ExceedanceDetector(this);
            recoverylogic           = new RecoveryLogic(this);
            positionoffsetcorrector = new PositionOffsetCorrector();
            protector               = new Protector();
            alphacompensator        = new AlphaCompensator();
            filtersystem            = new FilterSystem();
            compressorsystem        = new CompressorSystem();
            scalersystem            = new ScalerSystem();
            zeromaker               = new ZeroMaker();
            dof_override            = new DOF_Override();
            loadersaver             = new LoaderSaver(this);
            integrator              = new Integrator(this);
            IK_Module               = new IK_Module(ref integrator);
            actuatorsystem          = new ActuatorSystem(ref IK_Module);
            serialtalker            = new SerialTalker(this);
            //...
            //...
            //...
        }
        private void UpdateObjects()
        {
            Update_Server();
            Update_Chopper();
            Update_Inverter();
            Update_ExceedanceDetector();
            Update_RecoveryLogic();
            Update_PositionOffsetCorrector();
            Update_Protector();
            Update_Alphacompensator();
            Update_Filtersystem();
            Update_CompressorSytem();
            Update_ScalerSystem();
            Update_ZeroMaker();
            Update_DOF_Override();
            Update_Integrator();
            Update_IK_Module();
            Update_ActuatorSystem();
            Update_SerialTalker();
            //TestCode:
            
            //...
            //...
            //...
        }

        private void Update_Server()
        {
            server.Read();
        }
        private void Update_Chopper()
        {
            chopper.ChopParseAndPackage(server.RawDatastring);
        }
        private void Update_Inverter()
        {
            inverter.InvertDataAsNeeded(chopper.Output);

            #region Update ViewModel
            VM_FiltersWindow.InvertWx = inverter.Invert_Wx;
            VM_FiltersWindow.InvertWy = inverter.Invert_Wy;
            VM_FiltersWindow.InvertWz = inverter.Invert_Wz; 

            VM_FiltersWindow.InvertAx = inverter.Invert_Ax;
            VM_FiltersWindow.InvertAy = inverter.Invert_Ay;
            VM_FiltersWindow.InvertAz = inverter.Invert_Az;
            #endregion

        }
        private void Update_ExceedanceDetector()
        {
            exceedancedetector.Process(inverter.Output);
        }
        private void Update_RecoveryLogic()
        {
            recoverylogic.Update();
        }
        private void Update_PositionOffsetCorrector()
        {
            positionoffsetcorrector.Process(exceedancedetector.Output, DeltatimeProcessing);
        }
        private void Update_Protector()
        {
            protector.Process(positionoffsetcorrector.Output);
        }
        private void Update_Alphacompensator()
        {
            alphacompensator.Process(protector.Output);
        }
        private void Update_Filtersystem()
        {
            filtersystem.Process(alphacompensator.Output);
        }
        private void Update_CompressorSytem()
        {
            compressorsystem.Process(filtersystem.Output);
        }
        private void Update_ScalerSystem()
        {
            scalersystem.Process(compressorsystem.Output);
        }
        private void Update_ZeroMaker()
        {
            zeromaker.Process(scalersystem.Output);
        }
        private void Update_DOF_Override()
        {
            dof_override.Process(zeromaker.Output);
        }
        private void Update_Integrator()
        {
            integrator.Update(dof_override.Output);
        }
        private void Update_IK_Module()
        {
            IK_Module.Update();
        }
        private void Update_ActuatorSystem()
        {
            actuatorsystem.Update();
        }
        private void Update_SerialTalker()
        {
            serialtalker.Update(actuatorsystem.Output);
        }

        //Helpers:
        private void WaitForTargetFramerate(int fps)
        {
            //Hic sunt dracones!
            var targetTicksPerFrame = Stopwatch.Frequency / fps;
            
            while (stopwatch.ElapsedTicks < targetTicksPerFrame)
            {
                //DoNothing();
            }

            DeltatimeProcessing = (float)stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Restart();
        }
    }
}
