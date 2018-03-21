﻿using System.Collections.Generic;
using Domain;
using DataAccess;

namespace Logic
{
    public class IOController
    {
        List<RouteNumber> routeNumberList;
        List<Contractor> contractorList;

        public IOController()
        {
            routeNumberList = new List<RouteNumber>();
        }
       
        public void InitializeExportToPublishList(string filePath)
        {
            CSVExportToPublishList ExportToPublishList = new CSVExportToPublishList(filePath);
            ExportToPublishList.CreateFile(); 
        }
        public void InitializeExportToCallingList(string filePath)
        {
            CSVExportToCallList ExportCallList = new CSVExportToCallList(filePath);
            ExportCallList.CreateFile();
        }
        public void InitializeImport(string masterDataFilepath, string routeNumberFilepath,string RNFile)
        {
            CSVImport csvImport = new CSVImport();
            csvImport.ImportContractors(masterDataFilepath);
            csvImport.ImportRouteNumbers(RNFile);
            csvImport.ImportOffers(routeNumberFilepath);
            contractorList = csvImport.SendContractorListToContainer();
            routeNumberList = csvImport.SendRouteNumberListToContainer();
            ListContainer listContainer = ListContainer.GetInstance();
            listContainer.GetLists(routeNumberList, contractorList);
        }
    }
}
