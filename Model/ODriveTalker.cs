using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Utility;
using System.Threading;

namespace YAME.Model
{
    public class ODriveTalker : MyObject
    {
        public SerialPort serialport = new SerialPort();
        public MessageGenerator_Odrive messageGenerator_Odrive;

        private const int BaudRate = 115200;
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

        OdriveNumber _odrive_number;
        public OdriveNumber OdriveNumber
        {
            get { return _odrive_number; }
            set
            {
                _odrive_number = value;
                OnPropertyChanged(nameof(OdriveNumber));
            }
        }

        string _ui_message;
        public string UI_Message
        {
            get
            { 
                return _ui_message;  }
            set
            {
                if (value != _ui_message)
                {
                    _ui_message = value; OnPropertyChanged(nameof(UI_Message));
                }
            }
        }

        string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value; OnPropertyChanged(nameof(Message));
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
                        MessageBox.Show("You are trying to open a serial connection that is already open! " +
                            "That is very strange. If you can consistently reproduce this, definitely shoot us an " +
                            "email to bugs@hexago-motion.com.",
                            "Serial port already open :-/",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    if (COM_Port == null)
                    {
                        MessageBox.Show("You have to select a COM port from the dropdown list before you can open it.",
                            "No COM port selected!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation);
                        _isopen = false;
                        return;
                    }

                    if (!engine.actuatorsystem.Is_AllActuatorsFullyRetracted || engine.actuatoroverride.IsOverride)
                    {
                        MessageBoxResult result = MessageBox.Show(
                            "You want to send data to the motion controller. However, the data you're about to send " +
                            "is commanding the rig into motion RIGHT NOW! Your rig could potentially make a huge jolt! " +
                            "Chances are that this is caused by one or both of these two conditions:\n" +
                            "1. Your rig is not in the park position\n" +
                            "2. You are using the actuator override\n" +
                            "\n" +
                            "To fix this you should move the rig down into the park position using the \"Motion Control\" module until you see the actuators turn pale blue in "+
                            "the 3D view. On top make sure that you are not using the actuator override feature." +
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
                        MessageBox.Show($"Unable to open {COM_Port}. Are you sure that this is the controller you're looking for? " +
                            $"Could it be that this controller is already in use?",
                            "Unable to open COM port",
                            MessageBoxButton.OK, 
                            MessageBoxImage.Error,
                            MessageBoxResult.OK,
                            MessageBoxOptions.DefaultDesktopOnly);
                        serialport.Close();
                        _isopen = false;
                    }

                }
                else            //Wanna switch it off?
                {
                    try
                    {
                        var mw = Application.Current.MainWindow as MainWindow;
                        bool isParked = mw.engine.integrator.Lerp_3Way.State == Lerp3_State.Park;

                        if (serialport.IsOpen && isParked)
                        {
                            serialport.Close();
                            _isopen = false;        //The End :-)
                        }
                        else if (!isParked)
                        {
                            ShowWarningToUser();
                            _isopen = true;        //The End :-)
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show($"Uhmm,... cough-cough, I am no longer able to talk to " +
                            $"{_odrive_number} on {COM_Port}. Did you just unplug it?",
                            $"What happened to {_odrive_number}?",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error,
                            MessageBoxResult.OK,
                            MessageBoxOptions.DefaultDesktopOnly);
                    }
                }

                engine.odrivesystem.IsAnyPortOpen = _isopen;        //This doesn't really SET the variable! It's just a trigger to get the OdriveSystem to Update its state.
                OnPropertyChanged(nameof(IsOpen));
            }
        }
        #endregion

        public ODriveTalker(Engine e, OdriveNumber odrive_number)
        {
            engine = e;
            messageGenerator_Odrive = new MessageGenerator_Odrive(engine);
            OdriveNumber = odrive_number;
    }

        public void Update(string formatstring, float[] revolutions)
        {
            if (serialport.IsOpen)
            {
                Message = messageGenerator_Odrive.ComposeMessageFrom(formatstring, revolutions);
                
                UI_Message = Message;
                Write(Message);
            }
            else
            {
                UI_Message = "- - Serial port closed - -";
            }

        }
        void Write(string msg)
        {
            if (serialport.IsOpen)
            {
                try
                {
                    serialport.WriteLine(msg);
                }
                catch(TimeoutException)
                {
                    IsOpen = false;
                    UI_Message = "Conection timed out";

                    MessageBox.Show($"Write timeout after {WriteTimeout}ms.\n" +
                        $"Looks like {OdriveNumber} on {COM_Port} is not responding.",
                        "Write Timeout",
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation,
                        MessageBoxResult.OK,
                        MessageBoxOptions.DefaultDesktopOnly);
                }
                catch (Exception)
                {
                    IsOpen = false;
                    UI_Message = "Conection Lost";

                    MessageBox.Show($"I lost connection with {OdriveNumber} on {COM_Port}.",
                        "Lost Connection",
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation,
                        MessageBoxResult.OK,
                        MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            else
            {
                throw new Exception("Cannot write. Serialport not open.");
            }
        }

        private void ShowWarningToUser()
        {
            Thread thread = new Thread(new ThreadStart(Worker));
            thread.Start();
        }
        void Worker()
        {
            //different thread:
            MessageBox.Show($"You are trying to close the connection to " +
                            $"{_odrive_number} on {COM_Port}. That is asking for trouble! \n\n" +
                            $"Move the rig into the -Park- position, then close the COM port.",
                            $"Unable to close {COM_Port} while rig is not parked",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error,
                            MessageBoxResult.OK,
                            MessageBoxOptions.DefaultDesktopOnly);
        }
    }

    public enum OdriveNumber
    {
        ODrive_1,
        ODrive_2,
        ODrive_3,
    }
}
