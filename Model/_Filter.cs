using YAME.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    abstract public class Filter : MyObject
    {
        protected FilterOrder  Order { get; set; }
        protected int DefaultFilterVariable = 100;

        float _invalue;
        public float InValue
        {
            get { return _invalue; } 
            set { _invalue = value; OnPropertyChanged("InValue"); } 
        }

        protected float _filtervariable;
        public float FilterVariable
        {
            get { return _filtervariable; }
            set { _filtervariable = value; OnPropertyChanged("FilterVariable"); }
        }

        float _outvalue;
        public float OutValue 
        { 
            get { return _outvalue; } 
            set { _outvalue = value; OnPropertyChanged("OutValue"); }
        }

        string _code;
        public string Code 
        { 
            get { return _code; } 
            set { _code = value; OnPropertyChanged("Code"); }
        }

        public Filter()
        {
            FilterVariable = DefaultFilterVariable;
            UpdateCode();
        }

        public void Push(float f)
        {
            InValue = f;
            CreateOutput();
        }
        public void UpdateCode()
        {
            char Letter = ' ';
            if (this is HighPassNthOrder)   Letter = 'H';
            if (this is LowPassNthOrder)    Letter = 'L';

            int number = (int)Order;

            Code = Letter.ToString() + number.ToString();
        }

        public abstract void CreateOutput();
    }




    public enum FilterOrder
    {
        First = 1,
        Second,
        Third,
        Forth
    }
}
