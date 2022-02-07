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
            IK_Module               = new IK_Module(integrator as Integrator_basic);
            actuatorsystem          = new ActuatorSystem(IK_Module);
            serialtalker            = new SerialTalker(this);
            //...
            //...
            //...
        }
        private void UpdateObjects()
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
            serialtalker.Update(actuatorsystem.Output);
        }


        //Helpers:
        private void WaitForTargetFramerate()
        {
            //Hic sunt dracones!
            int fps = Properties.Settings.Default.Processing_Framerate;
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
