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
    public partial class AlphaCompensationWindow : Window
    {
        public AlphaCompensationWindow()
        {
            InitializeComponent();
            DataContext = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine.VM_AlphaCompensator;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
