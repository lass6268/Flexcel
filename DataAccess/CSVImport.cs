using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Domain;
using System.IO;
using System.Globalization;
namespace DataAccess
{
    public class CSVImport
    {

        Encoding encoding;
        public List<Contractor> listOfContractors;
        public List<RouteNumber> listOfRouteNumbers;
        public List<Offer> listOfOffers;
        public CSVImport()
        {
            listOfContractors = new List<Contractor>();
            listOfRouteNumbers = new List<RouteNumber>();
            listOfOffers = new List<Offer>();
            encoding = Encoding.GetEncoding("iso-8859-1");
        }
        public int TryParseToIntElseZero(string toParse)
        {
            int number;
            toParse = toParse.Replace(" ", "");
            bool tryParse = Int32.TryParse(toParse, out number);
            return number;
        }
        public float TryParseToFloatElseZero(string toParse)
        {
            string CurrentCultureName = Thread.CurrentThread.CurrentCulture.Name;
            CultureInfo cultureInformation = new CultureInfo(CurrentCultureName);
            if (cultureInformation.NumberFormat.NumberDecimalSeparator != ",")
            // Forcing use of decimal separator for numerical values
            {
                cultureInformation.NumberFormat.NumberDecimalSeparator = ",";
                Thread.CurrentThread.CurrentCulture = cultureInformation;
            }
            float number = 0;
            toParse = toParse.Replace(" ", "");
            bool tryParse = float.TryParse(toParse.Replace('.', ','), out number);
            return number;
        }
        public void ImportOffers(string filepath)
        {
            try
            {
                var data = File.ReadAllLines(filepath, encoding)
           .Skip(1)
           .Select(x => x.Split(';'))
           .Select(x => new Offer
           {
               OfferReferenceNumber = x[0],
               RouteID = TryParseToIntElseZero(x[1]),
               OperationPrice = TryParseToFloatElseZero((x[2])),
               UserID = x[5],
               CreateRouteNumberPriority = x[6],
               CreateContractorPriority = x[7],

           });
                foreach (var o in data)
                {

                    if (o.UserID != "" || o.OperationPrice != 0)
                    {          
                        o.RouteNumberPriority = TryParseToIntElseZero(o.CreateRouteNumberPriority);
                        o.ContractorPriority = TryParseToIntElseZero(o.CreateContractorPriority);
                        Contractor contractor = listOfContractors.Find(x => x.UserID == o.UserID);
                        try
                        {
                            o.RequiredVehicleType = (listOfRouteNumbers.Find(r => r.RouteID == o.RouteID)).RequiredVehicleType;
                            double totalcontracthours = (listOfRouteNumbers.Find(r => r.RouteID == o.RouteID)).TotalYearlyDrivingHours;
                            Offer newOffer = new Offer(o.OfferReferenceNumber, o.OperationPrice, o.RouteID, o.UserID, o.RouteNumberPriority, o.ContractorPriority, contractor, totalcontracthours,o.RequiredVehicleType);
                            listOfOffers.Add(newOffer);
                        }
                        catch
                        {
                            // Help for debugging purpose only.
                            string failure = o.RouteID.ToString();
                        }
                    }

                }
                foreach (RouteNumber routeNumber in listOfRouteNumbers)
                {
                    foreach (Offer offer in listOfOffers)
                    {
                        if (offer.RouteID == routeNumber.RouteID)
                        {
                            routeNumber.offers.Add(offer);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Fejl, er du sikker på du har valgt den rigtige fil?");
            }
            catch (FormatException)
            {
                throw new FormatException("Fejl, er du sikker på du har valgt den rigtige fil?");
            }
            catch (Exception)
            {
                throw new Exception("Fejl, filerne blev ikke importeret");
            }
        }
        public void ImportRouteNumbers()
        {
            try
            {
                string filepath = Environment.ExpandEnvironmentVariables("RouteNumbers.csv");
                var data = File.ReadAllLines(filepath, encoding)
                .Skip(1)
                .Select(x => x.Split(';'))
                .Select(x => new RouteNumber
                {
                    RouteID = TryParseToIntElseZero(x[0]),
                    RequiredVehicleType = TryParseToIntElseZero(x[1]),
                    WeekdayHours = CalculateOperatingHours(x[2]) - ParseStringToFloat("00" + x[5]),
                    WeekendHours = CalculateOperatingHours(x[3]) - ParseStringToFloat("00" + x[5]),
                    HolidayHours = CalculateOperatingHours(x[4]) - ParseStringToFloat("00" + x[5]),
                    ClosedWeeks = x[6].Count(s => s == '+')+1,
                    ClosedDays = TryParseToIntElseZero(x[7]),
                });
                foreach (var r in data)
                {
                    bool doesAlreadyContain = listOfRouteNumbers.Any(obj => obj.RouteID == r.RouteID);

                    if (!doesAlreadyContain && r.RouteID != 0 && r.RequiredVehicleType != 0)
                    {
                        listOfRouteNumbers.Add(r);
                    }
                }

            }


            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Fejl, er du sikker på du har valgt den rigtige fil?");
            }
            catch (FormatException)
            {
                throw new FormatException("Fejl, er du sikker på du har valgt den rigtige fil?");
            }
            catch (Exception)
            {
                throw new Exception("Fejl, filerne blev ikke importeret");
            }
        }
        public void ImportContractors(string filepath)
        {
            try
            {
                var data = File.ReadAllLines(filepath, encoding)
                  .Skip(1)
                  .Select(x => x.Split(';'))
                  .Select(x => new Contractor
                  {
                      ReferenceNumberBasicInformationPDF = x[0],
                      ManagerName = x[1],
                      CompanyName = x[2],
                      UserID = x[3],        
                      TryParseValueType2PledgedVehicles = x[4],
                      TryParseValueType3PledgedVehicles = x[5],
                      TryParseValueType5PledgedVehicles = x[6],
                      TryParseValueType6PledgedVehicles = x[7],
                      TryParseValueType7PledgedVehicles = x[8],

                  });

                foreach (var c in data)
                {
                    if (c.UserID != "")
                    {
                        {
                            bool doesAlreadyContain = listOfContractors.Any(obj => obj.UserID == c.UserID);

                            c.NumberOfType2PledgedVehicles = TryParseToIntElseZero(c.TryParseValueType2PledgedVehicles);
                            c.NumberOfType3PledgedVehicles = TryParseToIntElseZero(c.TryParseValueType3PledgedVehicles);
                            c.NumberOfType5PledgedVehicles = TryParseToIntElseZero(c.TryParseValueType5PledgedVehicles);
                            c.NumberOfType6PledgedVehicles = TryParseToIntElseZero(c.TryParseValueType6PledgedVehicles);
                            c.NumberOfType7PledgedVehicles = TryParseToIntElseZero(c.TryParseValueType7PledgedVehicles);

                            Contractor newContractor = new Contractor(c.ReferenceNumberBasicInformationPDF, c.UserID, c.CompanyName, c.ManagerName, c.NumberOfType2PledgedVehicles, c.NumberOfType3PledgedVehicles, c.NumberOfType5PledgedVehicles, c.NumberOfType6PledgedVehicles, c.NumberOfType7PledgedVehicles);
                            listOfContractors.Add(newContractor);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Fejl, er du sikker på du har valgt den rigtige fil?");
            }
            catch (FormatException)
            {
                throw new FormatException("Fejl, er du sikker på du har valgt den rigtige fil?");
            }
            catch (Exception)
            {
                throw new Exception("Fejl, filerne blev ikke importeret");
            }
        }
        public List<Contractor> SendContractorListToContainer()
        {
            return listOfContractors;
        }
        public List<RouteNumber> SendRouteNumberListToContainer()
        {
            return listOfRouteNumbers;
        }
        public float CalculateOperatingHours(string hverdageFeldtIExcel)
        {
            string[] hours = hverdageFeldtIExcel.Split('+');

            float valueToReturn = 0;
            foreach (string operatingHours  in hours)
            {
                string[] workHours = operatingHours.Split('-');
                float first = ParseStringToFloat(workHours[0]);
                float second = ParseStringToFloat(workHours[1]);
                valueToReturn += second - first;
            }
            return valueToReturn;
        }
        private float ParseStringToFloat(string stringToParse)
        {
            string firstpart = stringToParse.Substring(0, stringToParse.Count() - 2);
            string secondpart = stringToParse.Substring(stringToParse.Count() - 2);
            if (secondpart == "30")
            {
                secondpart = "50";
            }
            return float.Parse(firstpart + "," + secondpart);
        }
    }
}
