using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDTConverter
{
    internal class Distance
    {
        public double Value { get; set; }
        public string Format { get; set; }

        public Distance(double value, string format)
        {
            Value = value;
            Format = format;
        }

        public Distance()
        {
        }
    }
}