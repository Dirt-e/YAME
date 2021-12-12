using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOTUS.DataFomats;
using static Utility;

namespace MOTUS.Model
{
    public class Messagegenerator_M4S
    {
        byte[] Message = new byte[28];
        byte[] StartBlock = new byte[] { 255, 255 };
        byte[] Additionals = new byte[] { 0, 0, 0, 0, 0, 0 };
        byte[] EndBlock = new byte[] { 10, 13 };

        public byte[] ComposeMessageFrom(SixSisters ss)
        {
            byte[] A1_bytes = GenerateBytes_24bit_from(ss.values[0]);
            byte[] A2_bytes = GenerateBytes_24bit_from(ss.values[1]);
            byte[] A3_bytes = GenerateBytes_24bit_from(ss.values[2]);
            byte[] A4_bytes = GenerateBytes_24bit_from(ss.values[3]);
            byte[] A5_bytes = GenerateBytes_24bit_from(ss.values[4]);
            byte[] A6_bytes = GenerateBytes_24bit_from(ss.values[5]);

            Message[0] = StartBlock[0];
            Message[1] = StartBlock[1];

            Message[2] = A1_bytes[0];
            Message[3] = A1_bytes[1];
            Message[4] = A1_bytes[2];
            Message[5] = A2_bytes[0];
            Message[6] = A2_bytes[1];
            Message[7] = A2_bytes[2];
            Message[8] = A3_bytes[0];
            Message[9] = A3_bytes[1];
            Message[10] = A3_bytes[2];
            Message[11] = A4_bytes[0];
            Message[12] = A4_bytes[1];
            Message[13] = A4_bytes[2];
            Message[14] = A5_bytes[0];
            Message[15] = A5_bytes[1];
            Message[16] = A5_bytes[2];
            Message[17] = A6_bytes[0];
            Message[18] = A6_bytes[1];
            Message[19] = A6_bytes[2];

            Message[20] = Additionals[0];
            Message[21] = Additionals[1];
            Message[22] = Additionals[2];
            Message[23] = Additionals[3];
            Message[24] = Additionals[4];
            Message[25] = Additionals[5];

            Message[26] = EndBlock[0];
            Message[27] = EndBlock[1];

            return Message;
        }
    }
}

    

