using MOTUS.Model;
using MOTUS.View;
using MOTUS.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MOTUS
{
    public partial class MainWindow : Window
    {
        public Engine engine = new Engine();

        RawDataWindow           rawDataWindow;
        CrashDetectorWindow     crashDetectorWindow;
        PositionCorrector_Window positionCorrectorWindow;
        AlphaCompensationWindow alphaCompensationWindow;
        FiltersWindow           filtersWindow;
        DOF_Window              dof_window;
        SceneViewWindow         sceneViewWindow;
        RigConfigWindow         rigConfigWindow;
        MotionControl_Window    motionControlWindow;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = engine.VM_MainWindow;
        }

        //Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            engine.StartEngine();
            Thread.Sleep(10);
            engine.loadersaver.LoadEngineSettingsFromApplication();
            OpenDefaultChildWindows();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            RememberWhichChildWindowsWereOpen();
            CloseAllOpenChildWindows();
            
            engine.loadersaver.SaveSettingsInApplication();
            engine.StopEngine();
        }

        //Window_Loaded:
        private void OpenDefaultChildWindows()
        {
            if (Properties.Settings.Default.Window_RawData_IsOpen)              mnuRawData.IsChecked = true;
            if (Properties.Settings.Default.Window_CrashDetector_IsOpen)        mnuCrashDetector.IsChecked = true;
            if (Properties.Settings.Default.Window_PositionCorrection_IsOpen)   mnuPositionCorrection.IsChecked = true;
            if (Properties.Settings.Default.Window_Filters_IsOpen)              mnuFilters.IsChecked = true;
            if (Properties.Settings.Default.Window_AlphaCompensation_IsOpen)    mnuAlphaCompensation.IsChecked = true;
            if (Properties.Settings.Default.Window_Graphs_IsOpen)               mnuGraphs.IsChecked = true;
            if (Properties.Settings.Default.Window_DOFs_IsOpen)                 mnuDOFs.IsChecked = true;
            if (Properties.Settings.Default.Window_SceneView_IsOpen)            mnuSceneView.IsChecked = true;
            if (Properties.Settings.Default.Window_RigConfig_IsOpen)            mnuRigConfig.IsChecked = true;
            if (Properties.Settings.Default.Window_MotionControl_IsOpen)        mnuMotionControl.IsChecked = true;
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
            Properties.Settings.Default.Window_SceneView_IsOpen             = false;
            Properties.Settings.Default.Window_RigConfig_IsOpen             = false;
            Properties.Settings.Default.Window_MotionControl_IsOpen         = false;

            //Then set only the ones that were open to "true"
            foreach (Window w in this.OwnedWindows)
            {
                switch (w.Name)
                {
                    case "RawDataWindow":
                        Properties.Settings.Default.Window_RawData_IsOpen               = true;
                        break;
                    case "CrashDetectorWindow":
                        Properties.Settings.Default.Window_CrashDetector_IsOpen         = true;
                        break;
                    case "PositionCorrectorWindow":
                        Properties.Settings.Default.Window_PositionCorrection_IsOpen    = true;
                        break;
                    case "AlphaCompensationWindow":
                        Properties.Settings.Default.Window_AlphaCompensation_IsOpen     = true;
                        break;
                    case "FiltersWindow":
                        Properties.Settings.Default.Window_Filters_IsOpen               = true;
                        break;
                    case "GraphsWindow":
                        Properties.Settings.Default.Window_Graphs_IsOpen                = true;
                        break;
                    case "DOF_Window":
                        Properties.Settings.Default.Window_DOFs_IsOpen                  = true;
                        break;
                    case "SceneViewWindow":
                        Properties.Settings.Default.Window_SceneView_IsOpen             = true;
                        break;
                    case "RigConfigWindow":
                        Properties.Settings.Default.Window_RigConfig_IsOpen             = true;
                        break;
                    case "MotionControlWindow":
                        Properties.Settings.Default.Window_MotionControl_IsOpen         = true;
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

        //Menu:
        private void mnuRawData_Checked(object sender, RoutedEventArgs e)
        {
            rawDataWindow = new RawDataWindow();
            rawDataWindow.Owner = this;
            rawDataWindow.Name = "RawDataWindow";
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
            crashDetectorWindow.Name = "CrashDetectorWindow";
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
            positionCorrectorWindow.Name = "PositionCorrectorWindow";
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
            alphaCompensationWindow.Name = "AlphaCompensationWindow";
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
            filtersWindow.Name = "FiltersWindow";
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
            dof_window.Name = "DOF_Window";
            dof_window.Show();
        }
        private void mnuDOFs_Unchecked(object sender, RoutedEventArgs e)
        {
            dof_window.Close();
        }

        private void mnuSceneView_Checked(object sender, RoutedEventArgs e)
        {
            sceneViewWindow = new SceneViewWindow();
            sceneViewWindow.Owner = this;
            sceneViewWindow.Name = "SceneViewWindow";
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
            rigConfigWindow.Name = "RigConfigWindow";
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
            motionControlWindow.Name = "MotionControlWindow";
            motionControlWindow.Show();
        }
        private void mnuMotionControl_Unchecked(object sender, RoutedEventArgs e)
        {
            motionControlWindow.Close();
        }

        private void btn_Test_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
