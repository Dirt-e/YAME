using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class Protector
    {
        public PreprocessorData Input;
        public PreprocessorData Output;
        public PreprocessorData LastGoodValues;

        public bool IsLatched { get; set; } = false;

        public void Process(PreprocessorData data)
        {
            Input = data;

            if (!IsLatched)     //Normal Path
            {
                RememberLastGoodValues();
                LetItFlow();
            }
            else
            {
                SendLastGoodValues();
            }
        }

        private void RememberLastGoodValues()
        {
            LastGoodValues = Input;
        }
        private void LetItFlow()
        {
            Output = Input;
        }

        private void SendLastGoodValues()
        {
            OutputData = new PreprocessorData(LastGoodValues);
        }
        public void Latch()
        {
            IsLatched = true;
        }
    }
}
