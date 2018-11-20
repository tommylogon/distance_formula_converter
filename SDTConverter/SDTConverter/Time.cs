using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDTConverter
{
    internal class Time
    {
        private double seconds;
        private double minutes;
        private double hours;

        public double GetSeconds()
        {
            return seconds;
        }

        public double GetMinutes()
        {
            return minutes;
        }

        public double GetHours()
        {
            return hours;
        }

        public void SetSecounds(double value)
        {
            seconds = value;
            minutes = seconds / 60;
            hours = minutes / 60;
        }

        public void Setminutes(double value)
        {
            minutes = value;
            seconds = minutes * 60;
            hours = minutes / 60;
        }

        public void SetHours(double value)
        {
            hours = value;
            minutes = hours * 60;
            seconds = minutes * 60;
        }

        public string Format { get; set; }

        public Time(double value, string format)
        {
            SetSecounds(value);
            Format = format;
        }

        public Time()
        {
        }
    }
}