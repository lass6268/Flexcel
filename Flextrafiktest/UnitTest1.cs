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

            rn = new RouteNumber(2502, 2);
           
            rn.HverdagsTimer = 8;
            rn.WeekendsTimer = 8;
            rn.HellingdagsTimer = 8;
            rn.LukkeUger = 5;
            rn.Lukkedage = 4;
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
    }
}
