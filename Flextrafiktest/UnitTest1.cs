using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Flextrafiktest
{
    [TestClass]
    public class UnitTest1
    {
        RouteNumber rn;
        Days day;
        Offer offer;
        [TestInitialize]
        public void Testinit()
        {

            rn = new RouteNumber(2502, 2,8,8,8,5,11);
           
            /*rn.HverdagsTimer = 8;
            rn.WeekendsTimer = 8;
            rn.HellingdagsTimer = 8;
            rn.LukkeUger = 5;
            rn.Lukkedage = 4;*/
            day = new Days(rn.LukkeUger);
            offer = new Offer("123", 270, 2502, "123", 123, 123, null, rn.Totalkøretimer);

        }
        [TestMethod]
        public void TestWeekdayHours()
        {

            Assert.AreEqual(1760, day.Weekdays(rn.Lukkedage) * rn.HverdagsTimer);
        }
        [TestMethod]
        public void TestOperationPrice()
        {

            Assert.AreEqual(689040, offer.OperationPrice);
        }
        [TestMethod]
        public void TestDifferentRoutesDriveHours()
        {
            RouteNumber route2502 = new RouteNumber(2502, 2,8,8,8,5,11);
            RouteNumber route2503 = new RouteNumber(2503, 2,8,0,0,5,11);
            RouteNumber route2504 = new RouteNumber(2504, 2,8,0,8,5,11);
            RouteNumber route2505 = new RouteNumber(2505, 2,8,0,0,5,11);
            RouteNumber route2506 = new RouteNumber(2506, 2,8,0,0,4,11);
            RouteNumber route2558 = new RouteNumber(2558, 6,9,9,9,3,7);
            Assert.AreEqual(2552, route2502.Totalkøretimer);
            Assert.AreEqual(1768, route2503.Totalkøretimer);
            Assert.AreEqual(1816, route2504.Totalkøretimer);
            Assert.AreEqual(1768, route2505.Totalkøretimer);
            Assert.AreEqual(1808, route2506.Totalkøretimer);
            Assert.AreEqual(3033, route2558.Totalkøretimer);
        }
        [TestMethod]
        public void TestSpecificRoute4000DriveHours()
        {
            RouteNumber route4000 = new RouteNumber(4000, 6, 7.5, 6.5, 6.5, 0, 0);
            Assert.AreEqual(2627.5, route4000.Totalkøretimer);
        }
    }
}
