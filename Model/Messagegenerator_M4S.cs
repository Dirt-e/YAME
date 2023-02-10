using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAME.DataFomats;
using static Utility;

namespace YAME.Model
{
    public class Messagegenerator_M4S
    {
        byte[] Message = new byte[28];
        static readonly byte[] StartBlock = new byte[] { 255, 255 };
        static readonly byte[] Additionals = new byte[] { 0, 0, 0, 0, 0, 0 };
        static readonly byte[] EndBlock = new byte[] { 10, 13 };

        //public byte[] ComposeMessageFrom(float[] Utilisations)
        //{
        //    byte[] A1_bytes = GenerateBytes_24bit_from(Utilisations[0]);
        //    byte[] A2_bytes = GenerateBytes_24bit_from(Utilisations[1]);
        //    byte[] A3_bytes = GenerateBytes_24bit_from(Utilisations[2]);
        //    byte[] A4_bytes = GenerateBytes_24bit_from(Utilisations[3]);
        //    byte[] A5_bytes = GenerateBytes_24bit_from(Utilisations[4]);
        //    byte[] A6_bytes = GenerateBytes_24bit_from(Utilisations[5]);

        //    Message[0] = StartBlock[0];
        //    Message[1] = StartBlock[1];

        //    Message[2] = A1_bytes[0];
        //    Message[3] = A1_bytes[1];
        //    Message[4] = A1_bytes[2];
        //    Message[5] = A2_bytes[0];
        //    Message[6] = A2_bytes[1];
        //    Message[7] = A2_bytes[2];
        //    Message[8] = A3_bytes[0];
        //    Message[9] = A3_bytes[1];
        //    Message[10] = A3_bytes[2];
        //    Message[11] = A4_bytes[0];
        //    Message[12] = A4_bytes[1];
        //    Message[13] = A4_bytes[2];
        //    Message[14] = A5_bytes[0];
        //    Message[15] = A5_bytes[1];
        //    Message[16] = A5_bytes[2];
        //    Message[17] = A6_bytes[0];
        //    Message[18] = A6_bytes[1];
        //    Message[19] = A6_bytes[2];

        //    Message[20] = Additionals[0];
        //    Message[21] = Additionals[1];
        //    Message[22] = Additionals[2];
        //    Message[23] = Additionals[3];
        //    Message[24] = Additionals[4];
        //    Message[25] = Additionals[5];

        //    Message[26] = EndBlock[0];
        //    Message[27] = EndBlock[1];

        //    return Message;
        //}

        public string UI_Message
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append($"<{ThreeDigitNumber_from(Message[0])}>");        //Start block
                sb.Append($"<{ThreeDigitNumber_from(Message[1])}>");
                sb.Append($"\n");
                sb.Append($"<{ThreeDigitNumber_from(Message[2])}>");        //Actuator1
                sb.Append($"<{ThreeDigitNumber_from(Message[3])}>");
                sb.Append($"<{ThreeDigitNumber_from(Message[4])}>");
                sb.Append($"\n");
                sb.Append($"<{ThreeDigitNumber_from(Message[5])}>");        //Actuator2    
                sb.Append($"<{ThreeDigitNumber_from(Message[6])}>");    
                sb.Append($"<{ThreeDigitNumber_from(Message[7])}>");    
                sb.Append($"\n");
                sb.Append($"<{ThreeDigitNumber_from(Message[8])}>");        //Actuator3     
                sb.Append($"<{ThreeDigitNumber_from(Message[9])}>");      
                sb.Append($"<{ThreeDigitNumber_from(Message[10])}>");     
                sb.Append($"\n");
                sb.Append($"<{ThreeDigitNumber_from(Message[11])}>");       //Actuator4    
                sb.Append($"<{ThreeDigitNumber_from(Message[12])}>");     
                sb.Append($"<{ThreeDigitNumber_from(Message[13])}>");     
                sb.Append($"\n");
                sb.Append($"<{ThreeDigitNumber_from(Message[14])}>");       //Actuator5   
                sb.Append($"<{ThreeDigitNumber_from(Message[15])}>");     
                sb.Append($"<{ThreeDigitNumber_from(Message[16])}>");     
                sb.Append($"\n");
                sb.Append($"<{ThreeDigitNumber_from(Message[17])}>");       //Actuator6   
                sb.Append($"<{ThreeDigitNumber_from(Message[18])}>");     
                sb.Append($"<{ThreeDigitNumber_from(Message[19])}>");     
                sb.Append($"\n");
                sb.Append($"<{ThreeDigitNumber_from(Message[20])}>");       //Additionals1   
                sb.Append($"<{ThreeDigitNumber_from(Message[21])}>");
                sb.Append($"<{ThreeDigitNumber_from(Message[22])}>");
                sb.Append($"\n");
                sb.Append($"<{ThreeDigitNumber_from(Message[23])}>");       //Additionals1     
                sb.Append($"<{ThreeDigitNumber_from(Message[24])}>");
                sb.Append($"<{ThreeDigitNumber_from(Message[25])}>");
                sb.Append($"\n");
                sb.Append($"<{ThreeDigitNumber_from(Message[26])}>");       //End block   
                sb.Append($"<{ThreeDigitNumber_from(Message[27])}>");

                return sb.ToString();
            }
        }

        public byte[] ComposeMessageFrom(SixSisters ss)
        {
            byte[] A1_bytes = GenerateBytes_24bit_from(ss.Values[0]);
            byte[] A2_bytes = GenerateBytes_24bit_from(ss.Values[1]);
            byte[] A3_bytes = GenerateBytes_24bit_from(ss.Values[2]);
            byte[] A4_bytes = GenerateBytes_24bit_from(ss.Values[3]);
            byte[] A5_bytes = GenerateBytes_24bit_from(ss.Values[4]);
            byte[] A6_bytes = GenerateBytes_24bit_from(ss.Values[5]);

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

    

