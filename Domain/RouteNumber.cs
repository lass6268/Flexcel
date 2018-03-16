using System.Collections.Generic;

namespace Domain
{
    public class RouteNumber
    {
        public List<Offer> offers;

        int _totalkøretimer;
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
                return _totalkøretimer;
            }
            set
            {
                Days days = new Days(Lukkedage);
                _totalkøretimer = (days.Weekdays(Lukkedage)+HverdagsTimer) + (days.Weekenddays()+WeekendsTimer) + (days.Helligdage()+HellingdagsTimer);
            }
            
        }

        public RouteNumber()
        {
            offers = new List<Offer>(); 
        }
        public RouteNumber(int routeID, int requiredVehicleType) : this()
        {          
            this.RouteID = routeID;
            this.RequiredVehicleType = requiredVehicleType;
        }
        
    }
}
