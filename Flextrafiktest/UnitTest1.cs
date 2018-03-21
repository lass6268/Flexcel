using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using DataAccess;

namespace Flextrafiktest
{
    [TestClass]
    public class UnitTest1
    {
        CSVImport CSVImp;
        RouteNumber rn;
        DaysInYearCalculator day;
        Offer offer;
        [TestInitialize]
        public void Testinit()
        {
            CSVImp = new CSVImport();
            rn = new RouteNumber(2502, 2,8,8,8,5,11);
           
            /*rn.HverdagsTimer = 8;
            rn.WeekendsTimer = 8;
            rn.HellingdagsTimer = 8;
            rn.LukkeUger = 5;
            rn.Lukkedage = 4;*/
            day = new DaysInYearCalculator(rn.ClosedWeeks);
            offer = new Offer("123", 270, 2502, "123", 123, 123, null, rn.TotalYearlyDrivingHours);

        }
        [TestMethod]
        public void TestWeekdayHours()
        {

            Assert.AreEqual(1760, day.Weekdays(rn.ClosedDays) * rn.WeekdayHours);
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
            Assert.AreEqual(2552, route2502.TotalYearlyDrivingHours);
            Assert.AreEqual(1768, route2503.TotalYearlyDrivingHours);
            Assert.AreEqual(1816, route2504.TotalYearlyDrivingHours);
            Assert.AreEqual(1768, route2505.TotalYearlyDrivingHours);
            Assert.AreEqual(1808, route2506.TotalYearlyDrivingHours);
            Assert.AreEqual(3033, route2558.TotalYearlyDrivingHours);
        }
        [TestMethod]
        public void TestSpecificRoute4000DriveHours()
        {
            RouteNumber route4000 = new RouteNumber(4000, 6, 7.5, 6.5, 6.5, 0, 0);
            Assert.AreEqual(2627.5, route4000.TotalYearlyDrivingHours);
        }
        [TestMethod]
        public void TestRoute18()
        {
            RouteNumber route18 = new RouteNumber(18, 2, 8.5, 8.5, 8.5, 5, 0);
            Assert.AreEqual(2805, route18.TotalYearlyDrivingHours);
        }
        [TestMethod]
        public void TestMultipleArgumentsFromExcelArk()
        {
            string hverdageFeldtIExcel = "730-1100+1300-1800";
            Assert.AreEqual(8.5, CSVImp.CalculateOperatingHours(hverdageFeldtIExcel));
        }
    }
}
