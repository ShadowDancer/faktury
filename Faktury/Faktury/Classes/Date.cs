using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Faktury.Classes
{
    [Serializable]
    public class Date
    {
        public static Date GetCurrentDate()
        {
            return new Date(DateTime.Now.Day.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
        }

        public Date()
        {

        }

        public Date(string Day, string Month, string Year)
        {
            this.Day = Convert.ToInt32(Day);
            this.Month = Month;
            this.Year = Convert.ToInt32(Year);
        }

        public int Day { get; set; }
        public String Month { get; set; }
        public int Year { get; set; }

        override public string ToString()
        {
            return Day.ToString() + "." + Month.ToString() + "." + Year.ToString();
        }
    }
}
