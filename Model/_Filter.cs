using MOTUS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    abstract public class Filter
    {
        public FilterOrder  Order { get; set; }
        public float        InValue { get; set; }
        public float        FilterVariable { get; set; }
        public float        OutValue { get; set; }
        public string       Code { get; set; }

        public Filter()
        {
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
