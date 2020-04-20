using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace easysched.Controllers
{
    public class DropDownTime
    {
        public int[]  Hours()
        {
            int[] hours = new int[12];
            for (int i =0; i<12; i++)
            {
                hours[i] = i + 1;
            }
            return hours;
        }
        public int[] Minutes()
        {
            int[] minutes = new int[] { 0, 30 };
            return minutes;
        }
        public string[] Period()
        {
            string[] meridian = new string[] { "AM", "PM" };
            return meridian;
        }
    }
}
