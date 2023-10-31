using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using YAME.Model;

namespace YAME.View
{
    public partial class SourceSelect_Window : Window
    {
        SnappyDragger snappyDragger;

        public SourceSelect_Window()
        {
            InitializeComponent();
            SetDatacontexts();

            snappyDragger = new SnappyDragger(this);
        }

        void SetDatacontexts()
        {
            var mw = Application.Current.MainWindow as MainWindow;
            DataContext                 = mw.engine.patcher;
            cmbbx_Source.DataContext    = mw.engine.getter;
        }

        //---------- DCS ----------
        void btn_Patch_DCS_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.patcher.btn_Patch_DCS_Click();
        }
        void btn_Unpatch_DCS_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.patcher.btn_Unpatch_DCS_Click();
        }

        //---------- DCS openbeta ----------
        void btn_Patch_DCS_openbeta_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.patcher.btn_Patch_DCS_openbeta_Click();
        }
        void btn_Unpatch_DCS_openbeta_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.patcher.btn_Unpatch_DCS_openbeta_Click();
        }

        //---------- X-Plane ----------
        void btn_Patch_XPlane_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.patcher.btn_Patch_XPlane_Click();
        }
        void btn_Unpatch_XPlane_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.patcher.btn_Unpatch_XPlane_Click();
        }

        //---------- Condor2 ---------
        private void btn_Patch_Condor2_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.patcher.btn_Patch_Condor2_Click();
        }
        private void btn_Unpatch_Condor2_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.patcher.btn_Unpatch_Condor2_Click();
        }

        //---------- Window ----------
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            snappyDragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            snappyDragger.StopDrag();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Temporary un-patch routine, can be removed in the future.
            //var mw = Application.Current.MainWindow as MainWindow;
            //mw.engine.patcher.btn_Unpatch_FS2020_Click();

            //LastClosed values
            Left = Properties.Settings.Default.Window_SourceSelect_Position_X;
            Top  = Properties.Settings.Default.Window_SourceSelect_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //LastClosed values
            Properties.Settings.Default.Window_SourceSelect_Position_X = (float)Left;
            Properties.Settings.Default.Window_SourceSelect_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }

        private void cmbbx_Source_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            var aasd_talker = mw.engine.aasd_talker;
            var odrive_system = mw.engine.odrivesystem;

            if (aasd_talker.IsOpen || odrive_system.IsAnyPortOpen)
            {
                MessageBox.Show("You are trying to select a new motion data source while a " +
                    "serial connection to your rig is open!?! That could cause huge jolts!\n" +
                    "1. Close all serial connections.(Output Module)\n " +
                    "2. Select your new motion data source.\n" +
                    "3. Reconnect serial connection.(Output Module)",
                    "Serial Connection Open!!!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Red_X_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
