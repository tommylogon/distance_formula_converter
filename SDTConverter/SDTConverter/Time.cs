using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDTConverter
{
    internal class Time
    {
        public double Value
        {
            get;
            set;
        }

        public string Format { get; set; }

        public Time(double value, string format)
        {
            Value = value;
            Format = format;
        }

        public Time()
        {
        }
    }
}