using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class Days
    {
        private int totalweeks = 0;
        public Days()
        {

        }
        public Days(int Weeksoff)
        {
            totalweeks = 52 - (Weeksoff);

        }
        public int Weekdays(int Tvungetlukkedage)
        {
            
            return totalweeks * 5 - (Tvungetlukkedage + Helligdage());
            
        }

        public int Weekenddays()
        {
            return totalweeks * 2;
        }
        public int Helligdage()
        {
            return 6;
        }

        public bool Weekday()
        {
            bool returnvalue = true;
            DateTime dateValue = new DateTime(DateTime.Now.Year+1, 2, 28);
            

            if ((dateValue.DayOfWeek == DayOfWeek.Saturday) && (dateValue.DayOfWeek == DayOfWeek.Sunday))
            {
                returnvalue = false;
            }

            return returnvalue;
        }
        public int Isleepyear()
        {
            int isleepyear = 0;
            DateTime datevalue = new DateTime(DateTime.Now.Year + 1, 2, 28);
            if (DateTime.IsLeapYear(DateTime.Now.Year +1) == true)
            {
                if (datevalue.DayOfWeek == DayOfWeek.Friday || datevalue.DayOfWeek == DayOfWeek.Sunday)
                {
                    isleepyear = 2;
                }
                else
                {
                    isleepyear = 1;
                }
            }
            else
            {
                isleepyear = 0;
            }

            return isleepyear;
        }

    }
}
