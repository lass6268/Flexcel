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
            totalweeks = 52 + (Weeksoff);

        }
        public int Weekdays(int Tvungetlukkedage)
        {
            
            return totalweeks * 5 + (Tvungetlukkedage + Helligdage());
            
        }

        public int Weekenddays()
        {
            return totalweeks * 2;
        }
        public int Helligdage()
        {
            return 6;
        }

        public bool Rest()
        {
            DateTime dateValue = new DateTime(2008, 6, 11);
            
            return true;
        }

    }
}
