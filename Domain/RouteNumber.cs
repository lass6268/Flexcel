using System.Collections.Generic;

namespace Domain
{
    public class RouteNumber
    {
        public List<Offer> offers;

       
        public int RouteID { get; set; }
        public int RequiredVehicleType { get; set; }
        public int HverdagsTimer { get; set; }
        public int WeekendsTimer { get; set; }
        public int HellingdagsTimer { get; set; }
        public int LukkeUger { get; set; }
        public int Lukkedage { get; set; }
        public int Totalkøretimer { 
            get
            {
                Days days = new Days(LukkeUger);
                return ((days.Weekdays(Lukkedage) * HverdagsTimer) + (days.Weekenddays() * WeekendsTimer) + (days.Helligdage() * HellingdagsTimer) + Rest());      
            }    
        }

        public RouteNumber()
        {
            offers = new List<Offer>(); 
        }
        public RouteNumber(int routeID, int requiredVehicleType, int hverdagstimer, int weekendstimer, int helligdagstimer, int lukkeruger, int lukkedage) : this()
        {          
            this.RouteID = routeID;
            this.RequiredVehicleType = requiredVehicleType;
            this.HverdagsTimer = hverdagstimer;
            this.WeekendsTimer = weekendstimer;
            this.HellingdagsTimer = helligdagstimer;
            this.LukkeUger = lukkeruger;
            this.Lukkedage = Lukkededage(lukkedage);
        }

        private int Lukkededage(int dage)
        {
            int returdage;

            if(dage>= 7)
            {
                returdage = dage-7;
                LukkeUger++;
            }
            else
            {
                returdage = dage;
            }
            return returdage;

        }
        private int Rest()
        {
            Days days = new Days(Lukkedage);
            int rest = 0;

        
            

            switch (days.Isleepyear())
            {
                case 0:
                    if(days.Weekday() == false)
                    {
                        rest = 1 * WeekendsTimer;
                    }
                    else
                    {
                        rest = 1 * HverdagsTimer;
                    }
                    return rest;
                case 1:
                    if (days.Weekday() == false)
                    {
                        rest = 2 * WeekendsTimer;
                    }
                    else
                    {
                        rest = 2 * HverdagsTimer;
                    }
                    return rest;
                case 2:
                    rest = (1 * HverdagsTimer) + (1 * WeekendsTimer);
                    return rest;
                default:
                    break;
            }
            
        
            return rest;
        }
        
    }
}
