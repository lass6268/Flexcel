using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DaysInYearCalculator
    {
        private int totalweeks;
        public int HolidayDays { get { return 6; } }
        public DaysInYearCalculator(int WeeksOff)
        {
            totalweeks = 52 - (WeeksOff);
        }
        public int Weekdays(int closedWeekdays)
        {
            return totalweeks * 5 - (closedWeekdays + HolidayDays);
        }
        public int Weekenddays()
        {
            return totalweeks * 2;
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
