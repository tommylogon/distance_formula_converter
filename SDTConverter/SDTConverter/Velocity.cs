using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDTConverter
{
    internal class Velocity
    {
        private double MetSec;

        private double KmH;

        public double GetKmH()
        {
            return KmH;
        }

        public double GetMetSec()
        {
            return MetSec;
        }

        public void SetMetSec(double value)
        {
            MetSec = value;
            KmH = MetSec / 3600;
        }

        public void SetKmH(double value)
        {
            KmH = value;
            MetSec = KmH * 3600;
        }

        public string Format { get; set; }

        public Velocity(double value, string format)
        {
            MetSec = value;
            Format = format;
        }

        public Velocity()
        {
        }
    }
}