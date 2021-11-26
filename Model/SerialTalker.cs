using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class SerialTalker
    {
        public SerialPort serialport = new SerialPort();
        public MessageGenerator_AMC_AASD15A messagegenerator = new MessageGenerator_AMC_AASD15A();

        public string COM_Port { get; set; } = "COM5";
        public string UI_message { get; set; }
        private const int BaudRate = 250000;
        private const int WriteTimeout = 2000;

        StringBuilder sb = new StringBuilder();

        bool _isopen;
        public bool IsOpen
        {
            get { return _isopen; }
            set
            {
                _isopen = value;

                if (value)
                {
                    if (!serialport.IsOpen) { Open(); }
                }
                else
                {
                    if (serialport.IsOpen) { Close(); }
                }
            }
        }


        public void Update(SixSisters ss)
        {
            IsOpen = true;                  //TESTING!!!
            if (serialport.IsOpen)
            {
                byte[] bytes = messagegenerator.ComposeMessageFrom(ss);
                Write(bytes);

                ShowMessageInUI(bytes);
            }
            else
            {
                UI_message = "- - Serial port closed - -";
            }

        }

        void Open()
        {
            if (serialport.IsOpen)
            {
                Console.WriteLine("Serialport already open.");
            }
            else
            {
                serialport.PortName = COM_Port;
                serialport.BaudRate = BaudRate;
                serialport.WriteTimeout = WriteTimeout;

                serialport.Open();
            }

        }
        void Close()
        {
            if (serialport.IsOpen)
            {
                serialport.Close();
            }
            else
            {
                throw new Exception("Serialport alredy closed.");
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
                    throw new Exception();
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
            sb.Clear();             //Tabula rasa!!!

            foreach (byte b in bytes)
            {
                string s3 = ConvertToThreeDigitNumber(b);

                sb.Append("<" + s3 + ">");
            }
            UI_message = sb.ToString();
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
