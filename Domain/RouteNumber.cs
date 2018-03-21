using System.Collections.Generic;

namespace Domain
{
    public class RouteNumber
    {
        public List<Offer> offers;
        public int RouteID { get; set; }
        public int RequiredVehicleType { get; set; }
        public double WeekdayHours { get; set; }
        public double WeekendHours { get; set; }
        public double HolidayHours { get; set; }
        public int ClosedWeeks { get; set; }
        public int ClosedDays { get; set; }
        public double TotalYearlyDrivingHours { 
            get
            {
                DaysInYearCalculator days = new DaysInYearCalculator(ClosedWeeks);
                return ((days.Weekdays(ClosedDays) * WeekdayHours) + (days.Weekenddays() * WeekendHours) + (days.HolidayDays * HolidayHours) + LastPartOfYear());      
            }    
        }

        public RouteNumber()
        {
            offers = new List<Offer>(); 
        }
        public RouteNumber(int routeID, int requiredVehicleType, double weekdayHours, double weekendsHours, double holidayHours, int closedWeeks, int closedDays)
            : this()
        {          
            this.RouteID = routeID;
            this.RequiredVehicleType = requiredVehicleType;
            this.WeekdayHours = weekdayHours;
            this.WeekendHours = weekendsHours;
            this.HolidayHours = holidayHours;
            this.ClosedWeeks = closedWeeks;
            this.ClosedDays = CalculateClosedDays(closedDays);
        }
        private int CalculateClosedDays(int days)
        {
            int realClosedDays;
            if(days>= 7)
            {
                realClosedDays = days-7;
                ClosedWeeks++;
            }
            else
            {
                realClosedDays = days;
            }
            return realClosedDays;
        }
        private double LastPartOfYear()
        {
            DaysInYearCalculator days = new DaysInYearCalculator(ClosedWeeks);
            double lastPart = 0;
            switch (days.Isleepyear())
            {
                case 0:
                    if(days.Weekday() == false)
                    {
                        lastPart = 1 * WeekendHours;
                    }
                    else
                    {
                        lastPart = 1 * WeekdayHours;
                    }
                    return lastPart;
                case 1:
                    if (days.Weekday() == false)
                    {
                        lastPart = 2 * WeekendHours;
                    }
                    else
                    {
                        lastPart = 2 * WeekdayHours;
                    }
                    return lastPart;
                case 2:
                    lastPart = (1 * WeekdayHours) + (1 * WeekendHours);
                    return lastPart;
                default:
                    break;
            }
            return lastPart;
        }
    }
}
