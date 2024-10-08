﻿using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YAME.Model
{
    public class AASD_Talker : MyObject
    {
        public SerialPort serialport = new SerialPort();
        public MessageGenerator_AMC_AASD15A messagegenerator_amc = new MessageGenerator_AMC_AASD15A();
        public Messagegenerator_M4S messagegenerator_m4s = new Messagegenerator_M4S();

        private const int BaudRate = 250000;
        private const int WriteTimeout = 2000;

        #region ViewModel
        string _com_port;
        public string COM_Port
        {
            get { return _com_port; } 
            set
            {
                _com_port = value; 
                OnPropertyChanged(nameof(COM_Port));
            }
        }
        ControllerType _controller;
        public ControllerType Controller
        {
            get { return _controller; }
            set
            {
                _controller = value;
                OnPropertyChanged(nameof(Controller));
            }
        }
        string _ui_message;
        public string UI_Message
        {
            get { return _ui_message; }
            set
            {
                if(value != _ui_message)
                {
                    _ui_message = value; OnPropertyChanged(nameof(UI_Message));
                }
            }
        }
        bool _isopen;
        public bool IsOpen              //Heavy lifter!!!
        {
            get { return _isopen; }
            set
            {
                if (value)      //Wanna switch it on?
                {
                    if (serialport.IsOpen)
                    {
                        MessageBox.Show(    "You are trying to open a serial connection that is already open! " +
                                            "That is very strange. If you can consistently reproduce this, definitely shoot us an " +
                                            "email to bugs@hexago-motion.com.",
                                            "Serial port already open :-/",
                                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    if (COM_Port == null)
                    {
                        MessageBox.Show(    "You have to select a COM port from the dropdown list before you can open it.",
                                            "No COM port selected!",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Exclamation);
                        _isopen = false;
                        return;
                    }
                    if (!engine.actuatorsystem.Is_AllActuatorsFullyRetracted && !Properties.Settings.Default.Automator_ForceHotRigWarningSilent )
                    {
                        MessageBoxResult result = MessageBox.Show(  
                                            "You're trying to send data to the motion controller. However, the data you're about to send " +
                                            "is telling the controller to move the rig to a position where it most probably isn't " +
                                            "at this moment. If your controller is active and online YOUR RIG COULD POTENTIALLY MAKE A HUGE JOLT!\n" +
                                            "\n" +
                                            "Do you really know what you're doing?",
                                            "HOT RIG WARNING",
                                            MessageBoxButton.YesNo, MessageBoxImage.Stop);
                        if (result == MessageBoxResult.No)
                        {
                            _isopen = false;
                            return;
                        }
                    }

                    //If you made it here, you really deserve it to be opened
                    try
                    {
                        serialport.PortName = COM_Port;
                        serialport.BaudRate = BaudRate;
                        serialport.WriteTimeout = WriteTimeout;

                        serialport.Open();
                        _isopen = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(    $"Unable to open {COM_Port}. Are you sure that this is the controller you're looking for?",
                                            "Unable to open COM port",
                                            MessageBoxButton.OK, MessageBoxImage.Error);
                        serialport.Close();
                        _isopen = false;
                    }
                    
                }
                else            //Wanna switch it off?
                {
                    if (serialport.IsOpen) serialport.Close();
                    _isopen = false;
                }

                OnPropertyChanged(nameof(IsOpen));
            }
        }
        #endregion

        public AASD_Talker(Engine e)
        {
            engine = e;
        }

        public void Update(SixSisters ss)
        {
            if (serialport.IsOpen)
            {
                switch (Controller)
                {
                    case ControllerType.Motion4Sim:
                        byte[] bytes_M4S = messagegenerator_m4s.ComposeMessageFrom(ss);
                        UI_Message = messagegenerator_m4s.UI_Message;
                        Write(bytes_M4S);
                        break;
                    case ControllerType.Thanos_AMC:
                        byte[] bytes_AMC = messagegenerator_amc.ComposeMessageFrom(ss);
                        UI_Message = messagegenerator_amc.UI_Message;
                        Write(bytes_AMC);
                        break;
                    default:
                        throw new Exception("Unknown controller type: " + Controller.ToString());
                }
            }
            else
            {
                UI_Message = "- - Serial port closed - -";
            }

        }
        void Write(byte[] msg)
        {
            if (serialport.IsOpen)
            {
                try
                {
                    serialport.Write(msg, 0, msg.Length);
                }
                catch (Exception)
                {
                    IsOpen = false;
                    UI_Message = "Conection timed out";
                    
                    MessageBox.Show(    $"Write timeout after {WriteTimeout}ms.\n" +
                                        $"Looks like the device on {COM_Port} is not responding.",
                                        "Write Timeout",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Exclamation);
                }
            }
            else
            {
                throw new Exception("Cannot write. Serialport not open.");
            }
        } 
    }

    public enum ControllerType
    {
        Motion4Sim,
        Thanos_AMC,
    }
}
