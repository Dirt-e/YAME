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
using YAME.View.UserControls;

namespace YAME.View
{
    public partial class OutputODrive_Window : Window
    {
        SnappyDragger snappydragger;

        public OutputODrive_Window()
        {
            InitializeComponent();
            SetDataContext();

            snappydragger = new SnappyDragger(this);
        }

        private void SetDataContext()
        {
            var mw = Application.Current.MainWindow as MainWindow;

            cmbbx_Port1.DataContext = mw.engine.odrivesystem.oDriveTalkers[0];
            cmbbx_Port2.DataContext = mw.engine.odrivesystem.oDriveTalkers[1];
            cmbbx_Port3.DataContext = mw.engine.odrivesystem.oDriveTalkers[2];

            txtbx_Lead1.DataContext = mw.engine.odrivesystem;
            txtbx_Lead2.DataContext = mw.engine.odrivesystem;
            txtbx_Lead3.DataContext = mw.engine.odrivesystem;

            tgl_Active1.DataContext = mw.engine.odrivesystem.oDriveTalkers[0];
            tgl_Active2.DataContext = mw.engine.odrivesystem.oDriveTalkers[1];
            tgl_Active3.DataContext = mw.engine.odrivesystem.oDriveTalkers[2];

            txtblk_OpenClose1.DataContext = mw.engine.odrivesystem.oDriveTalkers[0];
            txtblk_OpenClose2.DataContext = mw.engine.odrivesystem.oDriveTalkers[1];
            txtblk_OpenClose3.DataContext = mw.engine.odrivesystem.oDriveTalkers[2];

            txtbx_Format_1.DataContext = mw.engine.odrivesystem;
            txtbx_Format_2.DataContext = mw.engine.odrivesystem;
            txtbx_Format_3.DataContext = mw.engine.odrivesystem;

            txtblk_Message1.DataContext = mw.engine.odrivesystem.oDriveTalkers[0];
            txtblk_Message2.DataContext = mw.engine.odrivesystem.oDriveTalkers[1];
            txtblk_Message3.DataContext = mw.engine.odrivesystem.oDriveTalkers[2];
        }

        private void tgl_Active123_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            ToggleSwitch ts = sender as ToggleSwitch;
            ComboBox cbx;

            switch (ts.Name)
            {
                case nameof(tgl_Active1):
                    cbx = cmbbx_Port1;
                    break;
                case nameof(tgl_Active2):
                    cbx = cmbbx_Port2;
                    break;
                case nameof(tgl_Active3):
                    cbx = cmbbx_Port3;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (cbx.SelectedItem == null)
            {
                ts.IsOn = false;        //Leave the switch Off. An error message will be generated by the SerialTalker anyways
            }
            e.Handled = true;       //Do nothing else with this mouse click.
        }
        private void cmbbx_Port123_PopulateDropdownList_OnClick(object sender, EventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            cbx.Items.Clear();

            var ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                if (!cbx.Items.Contains(port))
                {
                    cbx.Items.Add(port);
                }
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            snappydragger.StopDrag();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // LastClosed values
            TryLoad_LastUsedComPort_Application();

            Left = Properties.Settings.Default.Window_OutputODrive_Position_X;
            Top = Properties.Settings.Default.Window_OutputODrive_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Window_OutputODrive_Position_X = (float)Left;
            Properties.Settings.Default.Window_OutputODrive_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }
        
        //------- Helpers -------
        private void TryLoad_LastUsedComPort_Application()
        {
            PopulateDropdownPorts_OnStart();

            string LastUsedComPort1 = Properties.Settings.Default.ODriveTalker_LastUsedComPort_1;
            string LastUsedComPort2 = Properties.Settings.Default.ODriveTalker_LastUsedComPort_2;
            string LastUsedComPort3 = Properties.Settings.Default.ODriveTalker_LastUsedComPort_3;
            
            int index1 = cmbbx_Port1.Items.IndexOf(LastUsedComPort1);             //-1 indicates a NoFind!
            int index2 = cmbbx_Port2.Items.IndexOf(LastUsedComPort2);             //-1 indicates a NoFind!
            int index3 = cmbbx_Port3.Items.IndexOf(LastUsedComPort3);             //-1 indicates a NoFind!

            if (index1 >= 0) cmbbx_Port1.SelectedItem = cmbbx_Port1.Items[index1];
            if (index2 >= 0) cmbbx_Port2.SelectedItem = cmbbx_Port2.Items[index2];
            if (index3 >= 0) cmbbx_Port3.SelectedItem = cmbbx_Port3.Items[index3];
        }
        private void PopulateDropdownPorts_OnStart()
        {
            var ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                if (!cmbbx_Port1.Items.Contains(port)) cmbbx_Port1.Items.Add(port);
                if (!cmbbx_Port2.Items.Contains(port)) cmbbx_Port2.Items.Add(port);
                if (!cmbbx_Port3.Items.Contains(port)) cmbbx_Port3.Items.Add(port);
                
            }
        }

        private void Red_X_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw.mnuOutputODrive.IsChecked = false;
        }
    }
}
