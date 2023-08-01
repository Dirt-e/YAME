#if !DEBUG 
#define RELEASE
#endif

using Microsoft.Win32;
using YAME.Model;
using YAME.View;
using YAME.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.IO.Compression;
using YAME.DataFomats;
using System.Windows.Media.Media3D;
using System.Diagnostics;

namespace YAME
{
    public partial class MainWindow : Window
    {
        public Engine engine = new Engine();
        Mutex single_instance_mutex;
        SnappyDragger snappydragger;

        RawDataWindow               rawDataWindow;
        CrashDetectorWindow         crashDetectorWindow;
        PositionCorrector_Window    positionCorrectorWindow;
        AlphaCompensationWindow     alphaCompensationWindow;
        FiltersWindow               filtersWindow;
        DOF_Window                  dof_window;
        Actuator_Override_Window    actuatorOverrideWindow;
        SceneViewWindow             sceneViewWindow;
        RigConfigWindow             rigConfigWindow;
        MotionControl_Window        motionControlWindow;
        OutputAASD_Window           outputAASD_Window;
        OutputODrive_Window         outputODrive_Window;
        SourceSelect_Window         sourceSelectWindow;
        AboutWindow                 aboutWindow;

        

        public MainWindow()
        {
            InitializeComponent();
            DataContext = engine.VM_MainWindow;

            MoveWindowToLowerRight();
        }

        //Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnforceSingleInstance();

            snappydragger = new SnappyDragger(this);

            engine.StartEngine();
            Thread.Sleep(100);
            engine.loadersaver.Load_Application();

            SetDataContexts();
            
            Configurator.ProcessConfigFile();

            if (Properties.Settings.Default.ShowAboutWindowOnStartup)
            {
                ShowAboutWindowOnAppStart(2000);
            }
            
            OpenChildWindows();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            RememberWhichChildWindowsWereOpen();
            CloseAllOpenChildWindows();
            KillAllExporterRelays();
            
            engine.loadersaver.Save_Application();
            engine.StopEngine();
        }

        private void SetDataContexts()
        {
            txtblk_profile.DataContext      = engine.loadersaver;
            txtblk_simulator.DataContext    = engine.chopper;
            rct_OnAirLight.DataContext      = engine.getter;
        }

        //Window_Loaded:
        private void OpenChildWindows()
        {
            //Open all the windows that were open when you last closed the application:
            //To-Do: Change syntax to --> "mnuAlphaCompensation.IsChecked  = Properties.Settings.Default.Window_AlphaCompensation_IsOpen;"
            if (Properties.Settings.Default.Window_AlphaCompensation_IsOpen)    mnuAlphaCompensation.IsChecked  = true;
            if (Properties.Settings.Default.Window_CrashDetector_IsOpen)        mnuCrashDetector.IsChecked      = true;
            if (Properties.Settings.Default.Window_DOFs_IsOpen)                 mnuDOFs.IsChecked               = true;
            if (Properties.Settings.Default.Window_ActuatorOverride_IsOpen)     mnuActuatorOverride.IsChecked   = true;
            if (Properties.Settings.Default.Window_Filters_IsOpen)              mnuFilters.IsChecked            = true;
            if (Properties.Settings.Default.Window_MotionControl_IsOpen)        mnuMotionControl.IsChecked      = true;
            if (Properties.Settings.Default.Window_PositionCorrection_IsOpen)   mnuPositionCorrection.IsChecked = true;
            if (Properties.Settings.Default.Window_RawData_IsOpen)              mnuRawData.IsChecked            = true;
            if (Properties.Settings.Default.Window_RigConfig_IsOpen)            mnuRigConfig.IsChecked          = true;
            if (Properties.Settings.Default.Window_SceneView_IsOpen)            mnuSceneView.IsChecked          = true;
            if (Properties.Settings.Default.Window_OutputAASD_IsOpen)           mnuOutputAASD.IsChecked         = true;
            if (Properties.Settings.Default.Window_OutputODrive_IsOpen)         mnuOutputODrive.IsChecked       = true;
            if (Properties.Settings.Default.Window_SourceSelect_IsOpen)         mnuHdr_Source_Click(this, new EventArgs() as RoutedEventArgs);
        }
        //Window_Closing:
        private void RememberWhichChildWindowsWereOpen()
        {
            //First set 'em all to false...
            Properties.Settings.Default.Window_RawData_IsOpen               = false;
            Properties.Settings.Default.Window_CrashDetector_IsOpen         = false;
            Properties.Settings.Default.Window_PositionCorrection_IsOpen    = false;
            Properties.Settings.Default.Window_AlphaCompensation_IsOpen     = false;
            Properties.Settings.Default.Window_Filters_IsOpen               = false;
            Properties.Settings.Default.Window_Graphs_IsOpen                = false;
            Properties.Settings.Default.Window_DOFs_IsOpen                  = false;
            Properties.Settings.Default.Window_ActuatorOverride_IsOpen      = false;
            Properties.Settings.Default.Window_SceneView_IsOpen             = false;
            Properties.Settings.Default.Window_RigConfig_IsOpen             = false;
            Properties.Settings.Default.Window_MotionControl_IsOpen         = false;
            Properties.Settings.Default.Window_OutputAASD_IsOpen            = false;
            Properties.Settings.Default.Window_OutputODrive_IsOpen          = false;
            Properties.Settings.Default.Window_SourceSelect_IsOpen          = false;

            //Then set only the ones that were open to "true"
            foreach (Window w in this.OwnedWindows)
            {
                switch (w.Name)
                {
                    case nameof(rawDataWindow):
                        Properties.Settings.Default.Window_RawData_IsOpen               = true;
                        break;
                    case nameof(crashDetectorWindow):
                        Properties.Settings.Default.Window_CrashDetector_IsOpen         = true;
                        break;
                    case nameof(positionCorrectorWindow):
                        Properties.Settings.Default.Window_PositionCorrection_IsOpen    = true;
                        break;
                    case nameof(alphaCompensationWindow):
                        Properties.Settings.Default.Window_AlphaCompensation_IsOpen     = true;
                        break;
                    case nameof(filtersWindow):
                        Properties.Settings.Default.Window_Filters_IsOpen               = true;
                        break;
                    //case nameof(GraphsWindow):
                    //    Properties.Settings.Default.Window_Graphs_IsOpen                = true;
                    //    break;
                    case nameof(dof_window):
                        Properties.Settings.Default.Window_DOFs_IsOpen                  = true;
                        break;
                    case nameof(actuatorOverrideWindow):
                        Properties.Settings.Default.Window_ActuatorOverride_IsOpen      = true;
                        break;
                    case nameof(sceneViewWindow):
                        Properties.Settings.Default.Window_SceneView_IsOpen             = true;
                        break;
                    case nameof(rigConfigWindow):
                        Properties.Settings.Default.Window_RigConfig_IsOpen             = true;
                        break;
                    case nameof(motionControlWindow):
                        Properties.Settings.Default.Window_MotionControl_IsOpen         = true;
                        break;
                    case nameof(outputAASD_Window):
                        Properties.Settings.Default.Window_OutputAASD_IsOpen      = true;
                        break;
                    case nameof(outputODrive_Window):
                        Properties.Settings.Default.Window_OutputODrive_IsOpen     = true;
                        break;
                    case nameof(sourceSelectWindow):
                        Properties.Settings.Default.Window_SourceSelect_IsOpen               = true;
                        break;
                    case nameof(aboutWindow):       //AboutWindow
                        //Do nothing, this does not need to be remembered
                        break;
                    default:
                        throw new Exception("Unknown Window Name: " + w.Name);    
                        ;
                }
            }

        }
        private void CloseAllOpenChildWindows()
        {
            foreach (Window w in this.OwnedWindows)
            {
                w.Close();
            }
        }
        private void KillAllExporterRelays()
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.patcher.KillAllMotionExporters();
        }

        //Menu/File:
        private void OnClick_Load(object sender, RoutedEventArgs e)
        {
            engine.loadersaver.Load_Profile();
        }
        private void OnClick_Save(object sender, RoutedEventArgs e)
        {
            engine.loadersaver.Save_Profile();
        }
        private void OnClick_SaveAs(object sender, RoutedEventArgs e)
        {
            engine.loadersaver.Save_Profile(true);
        }
        private void OnClick_Quit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Menu/Windows:
        private void mnuRawData_Checked(object sender, RoutedEventArgs e)
        {
            rawDataWindow = new RawDataWindow();
            rawDataWindow.Owner = this;
            rawDataWindow.Name = nameof(rawDataWindow);
            rawDataWindow.Show();
        }
        private void mnuRawData_Unchecked(object sender, RoutedEventArgs e)
        {
            rawDataWindow.Close();
        }

        private void mnuCrashDetector_Checked(object sender, RoutedEventArgs e)
        {
            crashDetectorWindow = new CrashDetectorWindow();
            crashDetectorWindow.Owner = this;
            crashDetectorWindow.Name = nameof(crashDetectorWindow);
            crashDetectorWindow.Show();
        }
        private void mnuCrashDetector_Unchecked(object sender, RoutedEventArgs e)
        {
            crashDetectorWindow.Close();
        }

        private void mnuPositionCorrection_Checked(object sender, RoutedEventArgs e)
        {
            positionCorrectorWindow = new PositionCorrector_Window();
            positionCorrectorWindow.Owner = this;
            positionCorrectorWindow.Name = nameof(positionCorrectorWindow);
            positionCorrectorWindow.Show();
        }
        private void mnuPositionCorrection_Unchecked(object sender, RoutedEventArgs e)
        {
            positionCorrectorWindow.Close();
        }
        
        private void mnuAlphaCompensation_Checked(object sender, RoutedEventArgs e)
        {
            alphaCompensationWindow = new AlphaCompensationWindow();
            alphaCompensationWindow.Owner = this;
            alphaCompensationWindow.Name = nameof(alphaCompensationWindow);
            alphaCompensationWindow.Show();
        }
        private void mnuAlphaCompensation_Unchecked(object sender, RoutedEventArgs e)
        {
            alphaCompensationWindow.Close();
        }

        private void mnuFilters_Checked(object sender, RoutedEventArgs e)
        {
            filtersWindow = new FiltersWindow();
            filtersWindow.Owner = this;
            filtersWindow.Name = nameof(filtersWindow);
            filtersWindow.Show();
        }
        private void mnuFilters_Unchecked(object sender, RoutedEventArgs e)
        {
            filtersWindow.Close();
        }

        private void mnuGraphs_Checked(object sender, RoutedEventArgs e)
        {
            //graphWindow = new GraphWindow();
            //graphWindow.Owner = this;
            //graphWindow.name = "GraphsWindow";
            //graphWindow.Show();
        }
        private void mnuGraphs_Unchecked(object sender, RoutedEventArgs e)
        {
            //graphWindow.Close();
        }

        private void mnuDOFs_Checked(object sender, RoutedEventArgs e)
        {
            dof_window = new DOF_Window();
            dof_window.Owner = this;
            dof_window.Name = nameof(dof_window);
            dof_window.Show();
        }
        private void mnuDOFs_Unchecked(object sender, RoutedEventArgs e)
        {
            dof_window.Close();
        }

        private void mnuActuatorOverride_Checked(object sender, RoutedEventArgs e)
        {
            actuatorOverrideWindow = new Actuator_Override_Window();
            actuatorOverrideWindow.Owner = this;
            actuatorOverrideWindow.Name = nameof(actuatorOverrideWindow);
            actuatorOverrideWindow.Show();
        }
        private void mnuActuatorOverride_Unchecked(object sender, RoutedEventArgs e)
        {
            actuatorOverrideWindow.Close(); 
        }

        private void mnuSceneView_Checked(object sender, RoutedEventArgs e)
        {
            sceneViewWindow = new SceneViewWindow();
            sceneViewWindow.Owner = this;
            sceneViewWindow.Name = nameof(sceneViewWindow);
            sceneViewWindow.Show();
        }
        private void mnuSceneView_Unchecked(object sender, RoutedEventArgs e)
        {
            sceneViewWindow.Close();
        }

        private void mnuRigConfig_Checked(object sender, RoutedEventArgs e)
        {
            rigConfigWindow = new RigConfigWindow();
            rigConfigWindow.Owner = this;
            rigConfigWindow.Name = nameof(rigConfigWindow);
            rigConfigWindow.Show();
        }
        private void mnuRigConfig_Unchecked(object sender, RoutedEventArgs e)
        {
            rigConfigWindow.Close();
        }

        private void mnuMotionControl_Checked(object sender, RoutedEventArgs e)
        {
            motionControlWindow = new MotionControl_Window();
            motionControlWindow.Owner = this;
            motionControlWindow.Name = nameof(motionControlWindow);
            motionControlWindow.Show();
        }
        private void mnuMotionControl_Unchecked(object sender, RoutedEventArgs e)
        {
            motionControlWindow?.Close();
        }

        private void mnu_OutputAASD_Checked(object sender, RoutedEventArgs e)
        {
            outputAASD_Window = new OutputAASD_Window();
            outputAASD_Window.Owner = this;
            outputAASD_Window.Name = nameof(outputAASD_Window);
            outputAASD_Window.Show();
        }
        private void mnu_OutputAASD_Unchecked(object sender, RoutedEventArgs e)
        {
            outputAASD_Window?.Close();
        }

        private void mnu_OutputOdrive_Checked(object sender, RoutedEventArgs e)
        {
            outputODrive_Window = new OutputODrive_Window();
            outputODrive_Window.Owner = this;
            outputODrive_Window.Name = nameof(outputODrive_Window);
            outputODrive_Window.Show();
        }
        private void mnu_OutputODrive_Unchecked(object sender, RoutedEventArgs e)
        {
            outputODrive_Window?.Close();
        }
        
        private void mnuOpenAll_Click(object sender, RoutedEventArgs e)
        {
            mnuAlphaCompensation.IsChecked      = true;
            mnuCrashDetector.IsChecked          = true;
            mnuDOFs.IsChecked                   = true;
            mnuActuatorOverride.IsChecked       = true;
            mnuFilters.IsChecked                = true;
            //mnuGraphs.IsChecked                 = true;
            mnuMotionControl.IsChecked          = true;
            mnuPositionCorrection.IsChecked     = true;
            mnuRawData.IsChecked                = true;
            mnuRigConfig.IsChecked              = true;
            mnuSceneView.IsChecked              = true;
            mnuOutputAASD.IsChecked       = true;
            mnuOutputODrive.IsChecked      = true;
        }
        private void mnuCloseAll_Click(object sender, RoutedEventArgs e)
        {
            mnuAlphaCompensation.IsChecked  = false;
            mnuCrashDetector.IsChecked      = false;
            mnuDOFs.IsChecked               = false;
            mnuActuatorOverride.IsChecked   = false;
            mnuFilters.IsChecked            = false;
            //mnuGraphs.IsChecked             = false;
            mnuMotionControl.IsChecked      = false;
            mnuPositionCorrection.IsChecked = false;
            mnuRawData.IsChecked            = false;
            mnuRigConfig.IsChecked          = false;
            mnuSceneView.IsChecked          = false;
            mnuOutputAASD.IsChecked         = false;
            mnuOutputODrive.IsChecked       = false;
        }
        
        //---------- ? ------------
        private void mnuHdr_QM_Click(object sender, RoutedEventArgs e)
        {
            if (aboutWindow != null && aboutWindow.IsOpen) return;
            
            CreateAboutWindow();
        }
        void CreateAboutWindow()
        {
            aboutWindow = new AboutWindow();
            aboutWindow.Owner = this;
            aboutWindow.Name = nameof(aboutWindow);
            aboutWindow.Show();
        }
        void CloseAboutWindow()
        {
            aboutWindow.Close();
        }

        //---------- Source ---------- 
        private void mnuHdr_Source_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in OwnedWindows)
            {
                if (w.Name == nameof(sourceSelectWindow))      //If it's open already!
                {
                    w.Close();                      //Close it!
                    return;      
                }
            }

            sourceSelectWindow = new SourceSelect_Window();
            sourceSelectWindow.Owner = this;
            sourceSelectWindow.Name = nameof(sourceSelectWindow);
            sourceSelectWindow.Show();
        }

        //---------- Helpers ------------
        private void EnforceSingleInstance()
        {
            string mutexname = "YAME_SingleInstanceMutex";
            try
            {
                single_instance_mutex = Mutex.OpenExisting(mutexname);
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                single_instance_mutex = new Mutex(false, mutexname);
            }

            if (!single_instance_mutex.WaitOne(0))
            {
                MessageBox.Show(    "Hmmm,.... It looks like you are trying to run two instances of YAME.exe! " +
                                    "We can't let you do that, because it would screw up all that neat network " +
                                    "code that we wrote.\n\n" +
                                    "Check in Task Manager for a process called \"YAME.exe\" or reboot your " +
                                    "computer to have certainty that there is no other instance of YAME running.",
                                    "Multiple Instances detected",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation     );

                Application.Current.Shutdown();
            }
        }
        private void MoveWindowToLowerRight(int x = 0, int y = 0)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - x;
            this.Top = desktopWorkingArea.Bottom - this.Height - y;
        }
        [Conditional("RELEASE")]
        private void ShowAboutWindowOnAppStart(int ms)
        {
            this.Hide();

            CreateAboutWindow();
            Thread.Sleep(ms);
            CloseAboutWindow();

            this.Show();
        }

        // --------- Mouse Events ---------
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DragMove();
            snappydragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StopDrag();
        }
        //---------- Buttons -----------
        private void btn_Test_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
