using MOTUS.Model;
using MOTUS.View;
using MOTUS.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        PositionCorrectorWindow positionCorrectorWindow;
        AlphaCompensationWindow alphaCompensationWindow;
        FiltersWindow           filtersWindow;
        DOF_Window              dof_window;
        SceneViewWindow         sceneViewWindow;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = engine.VM_MainWindow;
        }

        //Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            engine.StartEngine();
            //engine.loadersaver.LoadSettingsFromApplication();
            OpenDefaultChildWindows();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            engine.loadersaver.SaveSettingsInApplication();
            engine.StopEngine();
        }

        private void OpenDefaultChildWindows()
        {
            //Open these ChildWindows on start-up for user convenience.
            
            mnuRawData.IsChecked = true;
            mnuCrashDetector.IsChecked = true;
            mnuPositionCorrection.IsChecked = true;
            mnuFilters.IsChecked = true;
            mnuAlphaCompansation.IsChecked = true;
            //mnuGraphs.IsChecked = true;
            mnuDOFs.IsChecked = true;
            mnuSceneView.IsChecked = true;
            //mnuRigConfig.IsChecked = true;
            //mnuMotionControl.IsChecked = true;
        }

        //Menu:
        private void mnuRawData_Checked(object sender, RoutedEventArgs e)
        {
            rawDataWindow = new RawDataWindow();
            rawDataWindow.Owner = this;
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
            crashDetectorWindow.Show();
        }
        private void mnuCrashDetector_Unchecked(object sender, RoutedEventArgs e)
        {
            crashDetectorWindow.Close();
        }

        private void mnuPositionCorrection_Checked(object sender, RoutedEventArgs e)
        {
            positionCorrectorWindow = new PositionCorrectorWindow();
            positionCorrectorWindow.Owner = this;
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
            sceneViewWindow.Show();
        }
        private void mnuSceneView_Unchecked(object sender, RoutedEventArgs e)
        {
            sceneViewWindow.Close();
        }

        private void mnuRigConfig_Checked(object sender, RoutedEventArgs e)
        {
            //rigConfigWindow = new RigConfigWindow();
            //rigConfigWindow.Owner = this;
            //rigConfigWindow.Show();
        }
        private void mnuRigConfig_Unchecked(object sender, RoutedEventArgs e)
        {
            //rigConfigWindow.Close();
        }

        private void mnuMotionControl_Checked(object sender, RoutedEventArgs e)
        {
            //motionControlWindow = new MotionControlWindow();
            //motionControlWindow.Owner = this;
            //motionControlWindow.Show();
        }
        private void mnuMotionControl_Unchecked(object sender, RoutedEventArgs e)
        {
            //motionControlWindow.Close();
        }

        private void btn_Test_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
