﻿using HelixToolkit.Wpf;
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

namespace MOTUS.View
{
    public partial class SceneViewWindow : Window
    {
        public SceneViewWindow()
        {
            InitializeComponent();

            SetDataContext();
            Load_3D_Objects();
            SetCamera();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left = Properties.Settings.Default.Window_SceneView_Position_X;
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
            DataContext = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine.VM_SceneView;
        }
        private void Load_3D_Objects()
        {
            ModelImporter Importer = new ModelImporter();

            #region UpperPoints
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Red));
            //Upper1.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Blue));
            //Upper2.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Green));
            //Upper3.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Yellow));
            //Upper4.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Black));
            //Upper5.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.White));
            //Upper6.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            #endregion

            #region Dynamic platforms
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
            #endregion

            #region BasePoints
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Red));
            //Base1.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Blue));
            //Base2.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Green));
            //Base3.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Yellow));
            //Base4.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Black));
            //Base5.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            //Importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.White));
            //Base6.Content = Importer.Load(@"..\..\Media\Models\Sphere.stl");
            #endregion
        }
        private void SetCamera()
        {   
            Viewport.Camera.LookDirection = new Vector3D(-1000, 2000, -1000);
        }
    }
}