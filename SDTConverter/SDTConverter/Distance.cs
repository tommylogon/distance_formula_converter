using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDTConverter
{
    internal class Distance
    {
        private double meters;
        private double Km;

        public double GetMeters()
        {
            return meters;
        }

        public double GetKm()
        {
            return Km;
        }

        public void SetMeters(double value)
        {
            meters = value;
            Km = value / 1000;
        }

        public void SetKm(double value)
        {
            Km = value;
            meters = Km * 1000;
        }

        public string Format { get; set; }

        public Distance(double value, string format)
        {
            SetMeters(value);
            Format = format;
        }

        public Distance()
        {
        }
    }
}