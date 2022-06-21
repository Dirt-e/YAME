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

namespace YAME.Model
{
    public class ODriveTalker : MyObject
    {
        public SerialPort serialport = new SerialPort();
        public MessageGenerator_Odrive messageGenerator_Odrive = new MessageGenerator_Odrive();

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
            get { return _ui_message; }
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

                    if (!engine.actuatorsystem.Is_AllActuatorsFullyRetracted)
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
                        if (serialport.IsOpen)
                        {
                            serialport.Close();
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
                    _isopen = false;
                }

                engine.odrivesystem.IsAnyPortOpen = _isopen;
                OnPropertyChanged(nameof(IsOpen));
            }
        }
        #endregion

        public ODriveTalker(Engine e, OdriveNumber odrive_number)
        {
            engine = e;
            OdriveNumber = odrive_number;
        }

        public void Update(float revs1, float revs2, string formatstring)
        {
            if (serialport.IsOpen)
            {
                Message = messageGenerator_Odrive.ComposeMessageFrom(revs1, revs2, formatstring);
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
    }

    public enum OdriveNumber
    {
        ODrive_1,
        ODrive_2,
        ODrive_3,
    }
}
