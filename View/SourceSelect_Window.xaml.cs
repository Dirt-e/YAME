﻿using Microsoft.WindowsAPICodePack.Dialogs;
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
        //DispatcherTimer timer;

        public SourceSelect_Window()
        {
            InitializeComponent();
            SetDatacontexts();
            //UpdatePatchStatusOfAllSims();

            snappyDragger = new SnappyDragger(this);

            //timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(5000);
            //timer.Tick += delegate
            //{
            //    UpdatePatchStatusOfAllSims();
            //};
            //timer.Start();
        }

        void SetDatacontexts()
        {
            var mw = Application.Current.MainWindow as MainWindow;
            DataContext = mw.engine.patcher;
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

        //---------- FS2020 ----------
        void btn_Patch_FS2020_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.patcher.btn_Patch_FS2020_Click();
        }
        void btn_Unpatch_FS2020_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.engine.patcher.btn_Unpatch_FS2020_Click();
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
            //LastClosed values
            Left = Properties.Settings.Default.Window_Patcher_Position_X;
            Top  = Properties.Settings.Default.Window_Patcher_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //LastClosed values
            Properties.Settings.Default.Window_Patcher_Position_X = (float)Left;
            Properties.Settings.Default.Window_Patcher_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }

        private void Red_X_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void RadioButton_ActiveSource_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            var button = sender as RadioButton;

            switch (button.Name)
            {
                case "RdoBtn_DCS":
                    mw.engine.getter.Source = MotionSource.DCS;
                    break;
                case "RdoBtn_DCSopenbeta":
                    mw.engine.getter.Source = MotionSource.DCS_openbeta;
                    break;
                case "RdoBtn_FS2020":
                    mw.engine.getter.Source = MotionSource.FS2020;
                    break;
                case "RdoBtn_XPlane":
                    mw.engine.getter.Source = MotionSource.XPlane;
                    break;
                case "RdoBtn_iRacing":
                    mw.engine.getter.Source = MotionSource.iRacing;
                    break;
                case "RdoBtn_Condor2":
                    mw.engine.getter.Source = MotionSource.Condor2;
                    break;
                default:
                    break;
            }
        }
    }
}
