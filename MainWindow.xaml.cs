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
        AASD_Talker_Window     serialConnectionWindow;
        ODriveTalker_Window         serialConnection2Window;
        Patcher_Window              patcherWindow;
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

            engine.StartEngine();
            Thread.Sleep(100);
            engine.loadersaver.Load_Application();

            SetDataContexts();

            ShowAboutWindowOnAppStart(2000);

            OpenDefaultChildWindows();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            RememberWhichChildWindowsWereOpen();
            CloseAllOpenChildWindows();
            
            engine.loadersaver.Save_Application();
            engine.StopEngine();
        }
        
        private void SetDataContexts()
        {
            txtblk_profile.DataContext      = engine.loadersaver;
            txtblk_simulator.DataContext    = engine.chopper;
            rct_OnAirLight.DataContext      = engine.server;
        }

        //Window_Loaded:
        private void OpenDefaultChildWindows()
        {
            //Open all the windows that were open when you last closed the application:
            if (Properties.Settings.Default.Window_AlphaCompensation_IsOpen)    mnuAlphaCompensation.IsChecked  = true;
            if (Properties.Settings.Default.Window_CrashDetector_IsOpen)        mnuCrashDetector.IsChecked      = true;
            if (Properties.Settings.Default.Window_DOFs_IsOpen)                 mnuDOFs.IsChecked               = true;
            if (Properties.Settings.Default.Window_ActuatorOverride_IsOpen)     mnuActuatorOverride.IsChecked   = true;
            if (Properties.Settings.Default.Window_Filters_IsOpen)              mnuFilters.IsChecked            = true;
            if (Properties.Settings.Default.Window_Graphs_IsOpen)               mnuGraphs.IsChecked             = true;
            if (Properties.Settings.Default.Window_MotionControl_IsOpen)        mnuMotionControl.IsChecked      = true;
            if (Properties.Settings.Default.Window_PositionCorrection_IsOpen)   mnuPositionCorrection.IsChecked = true;
            if (Properties.Settings.Default.Window_RawData_IsOpen)              mnuRawData.IsChecked            = true;
            if (Properties.Settings.Default.Window_RigConfig_IsOpen)            mnuRigConfig.IsChecked          = true;
            if (Properties.Settings.Default.Window_SceneView_IsOpen)            mnuSceneView.IsChecked          = true;
            if (Properties.Settings.Default.Window_SerialConnection_IsOpen)     mnuSerialConnection.IsChecked   = true;
            if (Properties.Settings.Default.Window_SerialConnection2_IsOpen)    mnuSerialConnection2.IsChecked  = true;
            if (Properties.Settings.Default.Window_Patcher_IsOpen)              mnuHdr_Patcher_Click(this, new EventArgs() as RoutedEventArgs);
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
            Properties.Settings.Default.Window_SerialConnection_IsOpen      = false;
            Properties.Settings.Default.Window_SerialConnection2_IsOpen     = false;
            Properties.Settings.Default.Window_Patcher_IsOpen               = false;

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
                    case nameof(serialConnectionWindow):
                        Properties.Settings.Default.Window_SerialConnection_IsOpen      = true;
                        break;
                    case nameof(serialConnection2Window):
                        Properties.Settings.Default.Window_SerialConnection2_IsOpen     = true;
                        break;
                    case nameof(patcherWindow):
                        Properties.Settings.Default.Window_Patcher_IsOpen               = true;
                        break;
                    case nameof(aboutWindow):
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

        private void mnuSerialConnection_Checked(object sender, RoutedEventArgs e)
        {
            serialConnectionWindow = new AASD_Talker_Window();
            serialConnectionWindow.Owner = this;
            serialConnectionWindow.Name = nameof(serialConnectionWindow);
            serialConnectionWindow.Show();
        }
        private void mnuSerialConnection_Unchecked(object sender, RoutedEventArgs e)
        {
            serialConnectionWindow?.Close();
        }

        private void mnuSerialConnection2_Checked(object sender, RoutedEventArgs e)
        {
            serialConnection2Window = new ODriveTalker_Window();
            serialConnection2Window.Owner = this;
            serialConnection2Window.Name = nameof(serialConnection2Window);
            serialConnection2Window.Show();
        }
        private void mnuSerialConnection2_Unchecked(object sender, RoutedEventArgs e)
        {
            serialConnection2Window?.Close();
        }
        
        private void mnuOpenAll_Click(object sender, RoutedEventArgs e)
        {
            mnuAlphaCompensation.IsChecked      = true;
            mnuCrashDetector.IsChecked          = true;
            mnuDOFs.IsChecked                   = true;
            mnuActuatorOverride.IsChecked       = true;
            mnuFilters.IsChecked                = true;
            mnuGraphs.IsChecked                 = true;
            mnuMotionControl.IsChecked          = true;
            mnuPositionCorrection.IsChecked     = true;
            mnuRawData.IsChecked                = true;
            mnuRigConfig.IsChecked              = true;
            mnuSceneView.IsChecked              = true;
            mnuSerialConnection.IsChecked       = true;
            mnuSerialConnection2.IsChecked      = true;
        }
        private void mnuCloseAll_Click(object sender, RoutedEventArgs e)
        {
            mnuAlphaCompensation.IsChecked  = false;
            mnuCrashDetector.IsChecked      = false;
            mnuDOFs.IsChecked               = false;
            mnuActuatorOverride.IsChecked   = false;
            mnuFilters.IsChecked            = false;
            mnuGraphs.IsChecked             = false;
            mnuMotionControl.IsChecked      = false;
            mnuPositionCorrection.IsChecked = false;
            mnuRawData.IsChecked            = false;
            mnuRigConfig.IsChecked          = false;
            mnuSceneView.IsChecked          = false;
            mnuSerialConnection.IsChecked   = false;
            mnuSerialConnection2.IsChecked  = false;
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
            aboutWindow.Name = "AboutWindow";
            aboutWindow.Show();
        }
        void CloseAboutWindow()
        {
            aboutWindow.Close();
        }

        //---------- Patcher ---------- 
        private void mnuHdr_Patcher_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in OwnedWindows)
            {
                if (w.Name == nameof(patcherWindow))      //If it's open already!
                {
                    w.Close();                      //Close it!
                    return;      
                }
            }

            patcherWindow = new Patcher_Window();
            patcherWindow.Owner = this;
            patcherWindow.Name = nameof(patcherWindow);
            patcherWindow.Show();
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
            DragMove();
        }

        //---------- Buttons -----------
        private void btn_Test_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
