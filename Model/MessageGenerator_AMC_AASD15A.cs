using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class MessageGenerator_AMC_AASD15A
    {
        byte[] Message = new byte[20];
        byte[] StartBlock = new byte[] { 255, 255 };
        byte[] Additionals = new byte[] { 0, 0, 0, 0 };
        byte[] EndBlock = new byte[] { 10, 13 };

        public byte[] ComposeMessageFrom(SixSisters ss)
        {
            byte[] A1_bytes = GenerateBytes(ss.values[0]);
            byte[] A2_bytes = GenerateBytes(ss.values[1]);
            byte[] A3_bytes = GenerateBytes(ss.values[2]);
            byte[] A4_bytes = GenerateBytes(ss.values[3]);
            byte[] A5_bytes = GenerateBytes(ss.values[4]);
            byte[] A6_bytes = GenerateBytes(ss.values[5]);

            Message[0] = StartBlock[0];
            Message[1] = StartBlock[1];
            Message[2] = A1_bytes[0];
            Message[3] = A1_bytes[1];
            Message[4] = A2_bytes[0];
            Message[5] = A2_bytes[1];
            Message[6] = A3_bytes[0];
            Message[7] = A3_bytes[1];
            Message[8] = A4_bytes[0];
            Message[9] = A4_bytes[1];
            Message[10] = A5_bytes[0];
            Message[11] = A5_bytes[1];
            Message[12] = A6_bytes[0];
            Message[13] = A6_bytes[1];
            Message[14] = Additionals[0];
            Message[15] = Additionals[1];
            Message[16] = Additionals[2];
            Message[17] = Additionals[3];
            Message[18] = EndBlock[0];
            Message[19] = EndBlock[1];

            return Message;
        }
        private byte[] GenerateBytes(float util)
        {
            UInt16 value;

            if (util <= 0.0f)
            {
                return new byte[2] { 0, 0 };            //return a min value
            }
            else if (util >= 1.0f)
            {
                return new byte[2] { 255, 255 };        //return a max value
            }
            else
            {
                value = (UInt16)(UInt16.MaxValue * util);
            }

            Byte[] Bytes = BitConverter.GetBytes(value);
            Array.Reverse(Bytes);

            return Bytes;
        }
    }
}
