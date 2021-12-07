using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MOTUS.Model
{
    public class SerialTalker : MyObject
    {
        public SerialPort serialport = new SerialPort();
        public MessageGenerator_AMC_AASD15A messagegenerator = new MessageGenerator_AMC_AASD15A();

        #region ViewModel
        string _com_port;
        public string COM_Port
        {
            get { return _com_port; } 
            set
            {
                _com_port = value; OnPropertyChanged(nameof(COM_Port));
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
        public bool IsOpen
        {
            get { return _isopen; }
            set
            {
                
                if (value)      //Wanna switch it on?
                {
                    if (serialport.IsOpen)
                    {
                        Console.WriteLine("Serialport already open.");       //???
                        value = true;
                    }
                    else if (COM_Port == null)
                    {
                        MessageBoxResult result = MessageBox.Show(  "You have to select a COM port from the " +
                                                                    "dropdown list before you can open it.",
                                                                    "No COM port selected!",
                                                                    MessageBoxButton.OK,
                                                                    MessageBoxImage.Exclamation);
                        value = false;
                    }
                    else            //Normal path:
                    {
                        try
                        {
                            serialport.PortName = COM_Port;
                            serialport.BaudRate = BaudRate;
                            serialport.WriteTimeout = WriteTimeout;

                            serialport.Open();
                            value = true;
                        }
                        catch (Exception)
                        {
                            MessageBoxResult result = MessageBox.Show(  $"Unable to open {COM_Port}. Are you " +
                                                                        "sure that this is the " +
                                                                        "controller you're looking for?",
                                                                        "Unable to open COM port",
                                                                        MessageBoxButton.OK,
                                                                        MessageBoxImage.Error);
                            serialport.Close();
                            value = false;
                        }
                    }
                }
                else            //Wanna switch it off?
                {
                    if (serialport.IsOpen) serialport.Close();
                    value = false;
                }

                _isopen = value; OnPropertyChanged(nameof(IsOpen));
            }
        }
        #endregion

        private const int BaudRate = 250000;
        private const int WriteTimeout = 2000;
        
        public void Update(SixSisters ss)
        {
            if (serialport.IsOpen)
            {
                byte[] bytes = messagegenerator.ComposeMessageFrom(ss);
                Write(bytes);

                ShowMessageInUI(bytes);
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
                    
                    MessageBoxResult result = MessageBox.Show(
                        $"Write timeout after {WriteTimeout}ms.\n" +
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

        //Helper functions:
        void ShowMessageInUI(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();             //Tabula rasa!!!

            foreach (byte b in bytes)
            {
                string s3 = ConvertToThreeDigitNumber(b);

                sb.Append("<" + s3 + ">");
            }
            UI_Message = sb.ToString();
        }
        string ConvertToThreeDigitNumber(byte b)
        {
            if (b < 10)
            {
                return "00" + b.ToString(); ;
            }
            else if (b < 100)
            {
                return "0" + b.ToString(); ;
            }
            else
            {
                return b.ToString();
            }
        }
    }
}
