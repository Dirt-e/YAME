using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using YAME.Model;

namespace YAME.View
{
    public partial class SceneViewWindow : Window
    {
        SnappyDragger snappyDragger;

        public SceneViewWindow()
        {
            InitializeComponent();

            snappyDragger = new SnappyDragger(this);

            SetDataContext();
            //LoadPlaneObjectsForDebug();
            SetCamera();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left    = Properties.Settings.Default.Window_SceneView_Position_X;
            Top     = Properties.Settings.Default.Window_SceneView_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Window_SceneView_Position_X = (float)Left;
            Properties.Settings.Default.Window_SceneView_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }

        //Helpers:
        private void SetDataContext()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            DataContext = mainWindow.engine.VM_SceneView;
        }
        private void LoadPlaneObjectsForDebug()
        {
            ModelImporter Importer = new ModelImporter();

            Importer.DefaultMaterial    = Materials.Black;
            Plat_Fix_Park.Content       = Importer.Load(@"..\..\Media\Models\Plane.stl");

            Importer.DefaultMaterial    = Materials.White;
            Plat_Fix_Pause.Content      = Importer.Load(@"..\..\Media\Models\Plane.stl");

            Importer.DefaultMaterial    = Materials.Gold;
            Plat_CoR.Content            = Importer.Load(@"..\..\Media\Models\Plane.stl");

            Importer.DefaultMaterial    = Materials.Blue;
            Plat_LFC.Content            = Importer.Load(@"..\..\Media\Models\Plane.stl");

            Importer.DefaultMaterial    = Materials.Red;
            Plat_HFC.Content             = Importer.Load(@"..\..\Media\Models\Plane.stl");

            Importer.DefaultMaterial    = Materials.Violet;
            Plat_Motion.Content         = Importer.Load(@"..\..\Media\Models\Plane.stl");

            Importer.DefaultMaterial    = Materials.Rainbow;
            Plat_Physical.Content       = Importer.Load(@"..\..\Media\Models\Plane.stl");
        }
        private void SetCamera()
        {   
            Viewport.Camera.LookDirection = new Vector3D(-1000, 2000, -1000);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            snappyDragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            snappyDragger.StopDrag();
        }
    }
}
