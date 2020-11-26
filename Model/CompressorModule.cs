using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class CompressorModule
    {
        float _input;
        public float Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                //OnPropertyChanged("Input");
            }
        }
        float _output;
        public float Output
        {
            get
            {
                return _output;
            }
            set
            {
                _output = value;
                //OnPropertyChanged("Output");
            }
        }
        float _parameter = 1;
        public float Parameter
        {
            get
            {
                return _parameter;
            }
            set
            {
                _parameter = value;
                //OnPropertyChanged("Parameter");
            }
        }
        float _limit;
        public float Limit
        {
            get
            {
                return _limit;
            }
            set
            {
                _limit = value;
                //OnPropertyChanged("Limit");
            }
        }

        CompressionMethod _method;
        public CompressionMethod Method
        {
            get { return _method; }
            set
            {
                _method = value;
                //OnPropertyChanged("Method");
            }
        }

        public CompressorModule()
        {
            Method = CompressionMethod.None;
        }
        public void Push(float input)
        {
            Input = input;
            switch (Method)
            {
                case CompressionMethod.None:
                    Output = Input;
                    break;
                case CompressionMethod.Crop:
                    Output = Crop_Compression(Input);
                    break;
                case CompressionMethod.TanH:
                    Output = TangensHyperbolis_Compression(Input);
                    break;
                case CompressionMethod.ATan:
                    Output = ArcusTangens_Compression(Input);
                    break;
                case CompressionMethod.Logist:
                    Output = Logistic_Compression(Input);
                    break;
                default:
                    break;
            }
        }

        private float Crop_Compression(float input)
        {
            float result = Utility.Clamp(input, -Limit, Limit);
            return result;
        }
        private float TangensHyperbolis_Compression(float val)
        {
            if (Limit == 0) return 0;

            double result = Limit * Math.Tanh(val * Parameter / Limit);
            return (float)result;
        }
        private float ArcusTangens_Compression(float val)
        {
            if (Limit == 0) return 0;
            if (Parameter == 0) return val;

            double result = Math.Atan(val * Parameter / Limit) * Limit / Math.Atan(Parameter);
            return (float)result;
        }
        private float Logistic_Compression(float val)
        {
            //Not implemented yet!
            return 0;
        }
    }

    public enum CompressionMethod
    {
        None,
        Crop,
        TanH,
        ATan,
        Logist
    }
}
