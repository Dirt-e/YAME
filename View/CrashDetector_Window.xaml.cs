﻿using System;
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
using System.Windows.Shapes;
using YAME.Model;

namespace YAME.View
{
    public partial class CrashDetectorWindow : Window
    {
        SnappyDragger snappydragger;

        public CrashDetectorWindow()
        {
            InitializeComponent();
            SetDataContext();
        }

        private void SetDataContext()
        {
            var engine = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine;
            DataContext = engine.VM_CrashDetector;

            txtbx_Ax_Threshold.DataContext = engine.exceedancedetector;
            txtbx_Ay_Threshold.DataContext = engine.exceedancedetector;
            txtbx_Az_Threshold.DataContext = engine.exceedancedetector;
            txtbx_Wx_Threshold.DataContext = engine.exceedancedetector;
            txtbx_Wy_Threshold.DataContext = engine.exceedancedetector;
            txtbx_Wz_Threshold.DataContext = engine.exceedancedetector;
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            snappydragger = new SnappyDragger(this);
            //LastClosed values
            Left = Properties.Settings.Default.Window_CrashDetector_Position_X;
            Top = Properties.Settings.Default.Window_CrashDetector_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //LastClosed values
            Properties.Settings.Default.Window_CrashDetector_Position_X = (float)Left;
            Properties.Settings.Default.Window_CrashDetector_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }

        private void btn_Indicator_Click(object sender, RoutedEventArgs e)
        {
            var engine = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine;
            engine.recoverylogic.State = Recovery_State.Acknoledged;
        }
        
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StopDrag();
        }

        private void Red_X_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mw = System.Windows.Application.Current.MainWindow as MainWindow;
            mw.mnuCrashDetector.IsChecked = false;
        }
    }
}
