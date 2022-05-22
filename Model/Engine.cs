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
        public BackgroundWorker         backgroundworker;
        public Server                   server;

        //Worker-thread objects:
        public LoaderSaver              loadersaver;
        public Patcher                  patcher;

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
        public Actuator_Override        actuatoroverride;
        public SerialTalker             serialtalker;
        public Logger                   logger;
        //...

        //ViewModels:
        public ViewModel_MainWindow                 VM_MainWindow;
        public ViewModel_CrashDetector              VM_CrashDetector;
        public ViewModel_MotionControlWindow        VM_MotionControlWindow;
        public ViewModel_Sceneview                  VM_SceneView;
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

            InstantiateViewModels();
        }
        
        private void InstantiateViewModels()
        {
            VM_MainWindow               = new ViewModel_MainWindow(this);
            VM_CrashDetector            = new ViewModel_CrashDetector(this);
            VM_MotionControlWindow      = new ViewModel_MotionControlWindow();
            VM_SceneView                = new ViewModel_Sceneview(this);
            //...
        }
        
        public void StartEngine()
        {
            if (!backgroundworker.IsBusy)
            {
                server.StartServer();

                backgroundworker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    InstatiateObjects_OnWorkerThread();
                    while (!backgroundworker.CancellationPending)
                    {
                        UpdateObjects();
                        WaitForTargetFramerate();
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
        void InstatiateObjects_OnWorkerThread()
        {
            loadersaver             = new LoaderSaver(this);
            patcher                 = new Patcher();

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
            integrator              = new Integrator(this);
            IK_Module               = new IK_Module(integrator as Integrator_basic);
            actuatorsystem          = new ActuatorSystem(IK_Module);
            actuatoroverride        = new Actuator_Override() ;
            serialtalker            = new SerialTalker(this);
            logger                  = new Logger(this);
        }
        void UpdateObjects()
        {
            server.Read();
            chopper.ChopParseAndPackage(server.RawDatastring);
            inverter.InvertDataAsNeeded(chopper.Output);
            exceedancedetector.Process(inverter.Output);
            recoverylogic.Update();
            positionoffsetcorrector.Process(exceedancedetector.Output, DeltatimeProcessing);
            protector.Process(positionoffsetcorrector.Output);
            alphacompensator.Process(protector.Output);
            filtersystem.Process(alphacompensator.Output);
            compressorsystem.Process(filtersystem.Output);
            scalersystem.Process(compressorsystem.Output);
            zeromaker.Process(scalersystem.Output);
            dof_override.Process(zeromaker.Output);
            integrator.Update(dof_override.Output);
            IK_Module.Update();
            actuatorsystem.Update();
            actuatoroverride.Process(actuatorsystem.Output);
            serialtalker.Update(actuatoroverride.Output);
            logger.Update();
        }
        void WaitForTargetFramerate()
        {
            //Hic sunt dracones!
            int fps_target = Properties.Settings.Default.Processing_Framerate;
            var TicksPerFrame_target = Stopwatch.Frequency / fps_target;
            
            while (stopwatch.ElapsedTicks < TicksPerFrame_target)
            {
                //DoNothing();
            }

            DeltatimeProcessing = (float)stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Restart();
        }
    }
}
