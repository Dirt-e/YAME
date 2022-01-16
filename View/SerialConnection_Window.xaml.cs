﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
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
    public partial class SerialConnection_Window : Window
    {
        SnappyDragger snappydragger;

        public SerialConnection_Window()
        {
            InitializeComponent();
            SetDataContext();

            snappydragger = new SnappyDragger(this);
        }

        void SetDataContext()
        {
            var engine = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine;
            DataContext = engine.serialtalker;
        }

        private void tgl_Active_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (cmbbx_Ports.SelectedItem == null)
            {
                tgl_Active.IsOn = false;
            }
            e.Handled = true;       //Do nothing else with this mouse click.
        }
        private void cmbbx_Controller_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var serialtalker =  Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine.serialtalker;
            if (serialtalker.IsOpen)
            {
                MessageBox.Show(    "You are trying to select another controller while a serial " +
                    "connection is open!?! Close the serial connection, THEN select your controller.",
                           "Serial Connection Open!!!",
                           MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void cmbbx_Ports_DropDownOpened(object sender, EventArgs e)
        {
            if (tgl_Active.IsOn)
            {
                MessageBoxResult result = MessageBox.Show(  $"You cannot change the COM port " +
                                                            "while a connection is open!\n" +
                                                            "First close the serial connection, then try again.", 
                                                            "WTF",
                                                            MessageBoxButton.OK,
                                                            MessageBoxImage.Exclamation);
            }
            else
            {
                PopulateDropdownList_Ports();
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //LastClosed values
            TryLoad_LastUsedComPort_Application();

            Left = Properties.Settings.Default.Window_SerialConnection_Position_X;
            Top = Properties.Settings.Default.Window_SerialConnection_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //LastClosed values
            SaveLastUsedComPort_Application();

            Properties.Settings.Default.Window_SerialConnection_Position_X = (float)Left;
            Properties.Settings.Default.Window_SerialConnection_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }

        //---------- Helpers ----------
        private void PopulateDropdownList_Ports()
        {
            cmbbx_Ports.Items.Clear();

            var ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cmbbx_Ports.Items.Add(port);
            }
        }
        private void SaveLastUsedComPort_Application()
        {
            //Save the state of the SerialTalker so that it has the same COM port preselected on next start
            var SelectedItem = cmbbx_Ports.SelectedItem;
            
            if (SelectedItem != null)
            {
                Properties.Settings.Default.SerialTalker_LastUsedComPort = SelectedItem.ToString();
            }
            else
            {
                Properties.Settings.Default.SerialTalker_LastUsedComPort = String.Empty;
            }
        }
        private void TryLoad_LastUsedComPort_Application()
        {
            PopulateDropdownList_Ports();

            string LastUsedComPort = Properties.Settings.Default.SerialTalker_LastUsedComPort;
            int index = cmbbx_Ports.Items.IndexOf(LastUsedComPort);             //-1 indicates a NoFind!

            if (index >= 0)  cmbbx_Ports.SelectedItem = cmbbx_Ports.Items[index];
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StopDrag();   
        }
    }
}
