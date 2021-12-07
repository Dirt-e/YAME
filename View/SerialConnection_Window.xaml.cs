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

namespace MOTUS.View
{
    public partial class SerialConnection_Window : Window
    {
        public SerialConnection_Window()
        {
            InitializeComponent();
        }

        private void cmbbx_Ports_DropDownOpened(object sender, EventArgs e)
        {

        }

        private void tgl_Active_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //LastClosed values
            Left = Properties.Settings.Default.Window_SerialConnection_Position_X;
            Top = Properties.Settings.Default.Window_SerialConnection_Position_Y;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //LastClosed values
            Properties.Settings.Default.Window_SerialConnection_Position_X = (float)Left;
            Properties.Settings.Default.Window_SerialConnection_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }
    }
}
