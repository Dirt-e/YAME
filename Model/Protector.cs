using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class Protector
    {
        public PreprocessorData Input           = new PreprocessorData();
        public PreprocessorData Output          = new PreprocessorData();
        public PreprocessorData LastGoodValues  = new PreprocessorData();

        public bool IsLatched { get; set; } = false;

        public void Process(PreprocessorData data)
        {
            Input = data;

            if (!IsLatched)                 //Normal Path
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
            Output = LastGoodValues;
        }
    }

    
}
