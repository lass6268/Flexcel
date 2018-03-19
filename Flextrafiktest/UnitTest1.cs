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
        public void TestMethod1()
        {
            Assert.AreEqual(2608, rn.Totalkøretimer);
        }

        [TestMethod]
        public void TestMethod2()
        {

            Assert.AreEqual(1800, day.Weekdays(rn.Lukkedage) * rn.HverdagsTimer);
        }
        [TestMethod]
        public void TestMethod3()
        {

            Assert.AreEqual(704160, offer.OperationPrice);
        }
        [TestMethod]
        public void TestDifferentRoutes()
        {
            RouteNumber route2502 = new RouteNumber(2502, 2,8,8,8,5,4)
            {
                HverdagsTimer = 8,
                WeekendsTimer = 8,
                HellingdagsTimer = 8,
                LukkeUger = 5,
                Lukkedage = 4
            };
            RouteNumber route2503 = new RouteNumber(2503, 2,8,0,0,5,4)
            {
                HverdagsTimer = 8,
                WeekendsTimer = 0,
                HellingdagsTimer = 0,
                LukkeUger = 5,
                Lukkedage = 4
            };
            RouteNumber route2504 = new RouteNumber(2504, 2,8,0,8,5,4)
            {
                HverdagsTimer = 8,
                WeekendsTimer = 0,
                HellingdagsTimer = 8,
                LukkeUger = 5,
                Lukkedage = 4
            };
            RouteNumber route2505 = new RouteNumber(2505, 2,8,0,0,5,4)
            {
                HverdagsTimer = 8,
                WeekendsTimer = 0,
                HellingdagsTimer = 0,
                LukkeUger = 5,
                Lukkedage = 4
            };
            RouteNumber route2506 = new RouteNumber(2506, 2,8,0,0,4,4)
            {
                HverdagsTimer = 8,
                WeekendsTimer = 0,
                HellingdagsTimer = 0,
                LukkeUger = 4,
                Lukkedage = 4
            };
            RouteNumber route2558 = new RouteNumber(2558, 6,9,9,9,3,7)
            {
                HverdagsTimer = 9,
                WeekendsTimer = 9,
                HellingdagsTimer = 9,
                LukkeUger = 3,
                Lukkedage = 7
            };
            Assert.AreEqual(2608, route2502.Totalkøretimer);
            Assert.AreEqual(1808, route2503.Totalkøretimer);
            Assert.AreEqual(1856, route2504.Totalkøretimer);
            Assert.AreEqual(1808, route2505.Totalkøretimer);
            Assert.AreEqual(1848, route2506.Totalkøretimer);
            Assert.AreEqual(3033, route2558.Totalkøretimer);
        }
    }
}
