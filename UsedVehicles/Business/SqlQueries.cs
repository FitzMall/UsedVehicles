using System;
using UsedVehicles.Business;
using UsedVehicles.Models;
using System.Collections.Generic;
using System.Web.Caching;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace UsedVehicles.Business
{
    public class SqlQueries
    {
        public static List<UsedVehicle> GetAllUsedVehicles()
        {
            var sqlGet = @"SELECT 
	            V_Stock as StockNumber,
	            V_xrefId as XrefId,
	            UPPER(V_loc) as Location,
	            V_Year as ModelYear,
	            V_MakeName as Make,
	            V_ModelName as Model,	
	            V_Vin as VIN,
	            V_Status as Status,
	            V_int_price as ListAmount,
	            V_daysinv as DaysInventory,
                V_Certified as Certified
                ,A.daysn as 'Days4008'
                ,A.inv_amt as CostAmount
             from  [FITZWAY].[dbo].[Allinventory]  C
                left JOIN [10.254.162.196].[FITZDB].[dbo].[UCMAS] A 
                ON A.vin = C.[V_Vin] and A.loc = C.V_loc
                where V_nu = 'USED'";

            var usedVehicles = SqlMapperUtil.SqlWithParams<UsedVehicle>(sqlGet, new { }, "Rackspace");

            return usedVehicles;

        }

        public static List<UsedVehicle> GetAllUsedTitleVehicles()
        {
            var sqlGet = @"SELECT 
                C.stk as StockNumber,
	            C.location as Location,
	            C.yr as ModelYear,
	            A.make as Make,
	            C.carline as Model,	
				C.color as Color,
	            C.vin as VIN,
				C.miles as Miles,
	            C.status as Status,
                C.MEMO2_CAT as Memo2,
	            A.list_amt as ListAmount,
	            CAST(C.days as INT) as DaysInventory
                ,A.daysn as 'Days4008'
                ,A.inv_amt as CostAmount
             FROM JUNK.[dbo].CSV_vehicleUSED C
                left JOIN [FITZDB].[dbo].[UCMAS] A 
                ON A.vin = C.[Vin] and A.loc = C.loc";

            var usedVehicles = SqlMapperUtil.SqlWithParams<UsedVehicle>(sqlGet, new { }, "SQLServer");

            return usedVehicles;

        }

        public static UsedVehicle GetUsedVehicle(string VIN)
        {
            var sqlGet = @"SELECT 
	            C.stk as StockNumber,
	            C.location as Location,
	            C.yr as ModelYear,
	            A.make as Make,
	            C.carline as Model,	
				C.color as Color,
	            C.vin as VIN,
				C.miles as Miles,
	            C.status as Status,
                C.MEMO2_CAT as Memo2,
	            A.list_amt as ListAmount,
	            CAST(C.days as INT) as DaysInventory
                ,A.daysn as 'Days4008'
                ,A.inv_amt as CostAmount
				,O.XrefID, OD.OPTCODE as Certified
             FROM JUNK.[dbo].CSV_vehicleUSED C 
                left JOIN [FITZDB].[dbo].[UCMAS] A 
                ON A.vin = C.[vin] and A.loc = C.loc
				LEFT join FITZWAY.DBO.optionxreffm O on C.stk = O.locstk
				LEFT JOIN  FITZWAY.DBO.optiondataFM OD on O.XREFID = OD.XREFID
				and (OPTTYPE = 'FEA' and OPTCODE in (
                      'F906','F907','F908','F909','F910','F911','F912','F913','F916','F917','F918','F922','F923'
                      ))
                where C.VIN = @vin";

            var usedVehicles = SqlMapperUtil.SqlWithParams<UsedVehicle>(sqlGet, new { vin = VIN }, "SQLServer");

            if (usedVehicles != null && usedVehicles.Count > 0)
            {
                return usedVehicles[0];
            }
            else
            {
                return new UsedVehicle();
            }

        }

        public static List<UsedVehicle> GetAllUsedAuctionVehicles()
        {
            var sqlGet = @"SELECT 
	            C.stk as StockNumber,
	            C.location as Location,
	            C.yr as ModelYear,
	            A.make as Make,
	            C.carline as Model,	
				C.color as Color,
	            C.vin as VIN,
				C.miles as Miles,
	            C.status as Status,
                C.MEMO2_CAT as Memo2,
	            A.list_amt as ListAmount,
	            CAST(C.days as INT) as DaysInventory
                ,A.daysn as 'Days4008'
                ,A.inv_amt as CostAmount
				,O.XrefID, OD.OPTCODE as Certified
             FROM JUNK.[dbo].CSV_vehicleUSED C 
                left JOIN [FITZDB].[dbo].[UCMAS] A 
                ON A.vin = C.[vin] and A.loc = C.loc
				LEFT join FITZWAY.DBO.optionxreffm O on C.stk = O.locstk
				LEFT JOIN  FITZWAY.DBO.optiondataFM OD on O.XREFID = OD.XREFID
				and (OPTTYPE = 'FEA' and OPTCODE in (
                      'F906','F907','F908','F909','F910','F911','F912','F913','F916','F917','F918','F922','F923'
                      ))
                 where C.status = 8 or C.status = 9";

            var usedVehicles = SqlMapperUtil.SqlWithParams<UsedVehicle>(sqlGet, new { }, "SQLServer");

            return usedVehicles;

        }

        public static List<UsedVehicle> GetNewStatus5Vehicles()
        {
            var sqlGet = @"SELECT 
	            C.STOCKNO as StockNumber,
	            C.loc as Location,
	            'NEW' as ModelYear,
	            C.make as Make,
	            C.ModelDescr as Model,	
	            C.SERIALno as VIN,
				SL.sl_VehicleMiles as Miles,
	            5 as Status,
                '' as Memo2,
	            C.invamt as ListAmount,
				SL.sl_VehicleDaysInStock as DaysInventory,
				SL.sl_VehicleBuyerName as CustomerName,
				SL.sl_SellPrice as SellPrice,
				SL.sl_VehicleDealDate as SoldDate,
				Sl.sl_dealkey as DealNumber
				,O.XrefID, OD.OPTCODE as Certified
                  FROM [JUNK].[dbo].[csv_NUstatus5] C

                LEFT JOIN SalesCommission.dbo.SalesLog SL on SL.sl_VehicleVIN = C.SERIALno and SL.sl_VehicleStockNumber = C.STOCKNO
				LEFT join FITZWAY.DBO.optionxreffm O on C.STOCKNO = O.locstk
				LEFT JOIN  FITZWAY.DBO.optiondataFM OD on O.XREFID = OD.XREFID
				and (OPTTYPE = 'FEA' and OPTCODE in (
                      'F906','F907','F908','F909','F910','F911','F912','F913','F916','F917','F918','F922','F923'
                      ))
                 where C.projname = 'NEW'";

            var usedVehicles = SqlMapperUtil.SqlWithParams<UsedVehicle>(sqlGet, new { }, "SQLServer");

            return usedVehicles;
        }

        public static List<UsedVehicle> GetNewStatus20Vehicles()
        {
            var sqlGet = @"SELECT 
	            C.stk_no as StockNumber,
                C.loc as Location,
                C.year as ModelYear,
                C.mk_code as Make,
                C.carline as Model,	
                C.clr_desc as Color,
                C.vin as VIN,
                0 as Miles,
                C.status as Status,
                '' as Memo2,
                C.int_price as ListAmount,
                CAST(C.days as INT) as DaysInventory
                ,O.XrefID, OD.OPTCODE as Certified
             FROM [JUNK].[dbo].[CSV_vehicleNew_S20] C 
                LEFT join FITZWAY.DBO.optionxreffm O on C.stk_no = O.locstk
				LEFT JOIN  FITZWAY.DBO.optiondataFM OD on O.XREFID = OD.XREFID
				and (OPTTYPE = 'FEA' and OPTCODE in (
                      'F906','F907','F908','F909','F910','F911','F912','F913','F916','F917','F918','F922','F923'
                      ))";
            
        var usedVehicles = SqlMapperUtil.SqlWithParams<UsedVehicle>(sqlGet, new { }, "SQLServer");

            return usedVehicles;
        }

        public static List<UsedVehicle> GetAllOtherStatusVehicles()
        {
            var sqlGet = @"  SELECT 
	            C.stk as StockNumber,
	            C.DRLoc as Location,
	            C.yr as ModelYear,
	            A.make as Make,
	            C.carline as Model,	
				C.color as Color,
	            C.vin as VIN,
				C.miles as Miles,
	            C.status as Status,
                C.MEMO1_CAT as Memo1,
                C.MEMO2_CAT as Memo2,
                A.list_amt as ListAmount,
	            CAST(C.days as INT) as DaysInventory
                ,A.daysn as 'Days4008'
                ,A.inv_amt as CostAmount
                ,U.[svcro] as RONumber
                ,U.[svcrodate] as RODate
                ,U.[svcmiles] as ROMiles,
				SL.sl_VehicleBuyerName as CustomerName,
				SL.sl_SellPrice as SellPrice,
				SL.sl_VehicleDealDate as SoldDate,
				Sl.sl_dealkey as DealNumber
				,O.XrefID, OD.OPTCODE as Certified
             FROM JUNK.[dbo].CSV_vehicleUSED C 
                LEFT JOIN SalesCommission.dbo.SalesLog SL on SL.sl_VehicleVIN = C.VIN and SL.sl_VehicleStockNumber = C.STK
				
                left JOIN [FITZDB].[dbo].[UCMAS] A 
                ON A.vin = C.[vin] and A.loc = C.loc
				LEFT join FITZWAY.DBO.optionxreffm O on C.stk = O.locstk
                LEFT JOIN JUNK.dbo.[UPD_UCDEMO] U on C.stk = U.stk
				LEFT JOIN  FITZWAY.DBO.optiondataFM OD on O.XREFID = OD.XREFID
				and (OPTTYPE = 'FEA' and OPTCODE in (
                      'F906','F907','F908','F909','F910','F911','F912','F913','F916','F917','F918','F922','F923'
                      ))
                 where C.status = 3 or C.status = 4 or C.status = 5 or C.status = 21 or C.status = 14 or C.status = 15 or C.status = 20";

            var usedVehicles = SqlMapperUtil.SqlWithParams<UsedVehicle>(sqlGet, new { }, "SQLServer");

            return usedVehicles;

        }

        public static List<TitleStatus> GetAllTitleStatus()
        {

            var titleStatus = SqlMapperUtil.StoredProcWithParams<TitleStatus>("sp_SalesLogReportGetAllTitleStatus", null, "SalesCommission");

            return titleStatus;

        }

        public static List<OpenRecalls> GetAllOpenRecalls()
        {
            var sqlGet = @"SELECT 
	                    CF.[Loc]
                        ,CF.[StockNo]
                        ,CF.[VIN]
                        ,CF.[Campaign_Code]
	                    ,RA.DESCRIPTION AS [Description]
                        FROM [RecallMaster].[dbo].[CarFax_UVRecallWithRO] CF 
	                    JOIN [RecallMaster].[dbo].CarFax_recall_alert RA on CF.Campaign_Code = RA.CAMPAIGN_CODE and CF.VIN = RA.VIN
                        where ROCompleteDate is NULL
                        order by CF.LastCheckDate";

            var recalls = SqlMapperUtil.SqlWithParams<OpenRecalls>(sqlGet, new { }, "SQLServer");

            return recalls;

        }

        public static VehiclePriceChange GetVehiclePriceChange(string location, string stockNumber, decimal currentPrice)
        {
            var sqlGet = @"SELECT TOP 1 
                [V_SAVEID] as PriceDate      
                  ,[V_stock] as StockNumber       
                  ,[V_loc] as Location
                  ,[V_daysinv] as DaysInventory
                  ,[V_int_price] as ListAmount
                    FROM Fitzway.[dbo].AllInventory_history
                    where V_loc = @location and V_Stock = @stock and V_int_price<> @price  order by V_SAVEID desc";

            var priceChanges = SqlMapperUtil.SqlWithParams<VehiclePriceChange>(sqlGet, new { location = location, stock = stockNumber, price = currentPrice }, "Rackspace");


            var priceChange = new VehiclePriceChange();
            if (priceChanges != null && priceChanges.Count > 0)
            {
                priceChange = priceChanges[0];
            }

            return priceChange;
        }

        public static List<VehiclePriceChange> GetAllVehiclePriceChanges()
        {
            var sqlGet = @"Select V_SaveId as PriceDate,
	                         V_Loc as Location,
	                          V_Stock as StockNumber, 
	                          V_daysinv as DaysInventory,
	                           v_int_Price as ListAmount 
                        FROM Fitzway.[dbo].AllInventory_history  
                        where V_Stock in (Select V_Stock from [FITZWAY].[dbo].[Allinventory] A where A.V_nu = 'USED')";

            var priceChanges = SqlMapperUtil.SqlWithParams<VehiclePriceChange>(sqlGet, new { }, "Rackspace");

            return priceChanges;
        }

        public static List<VehiclePriceChange> GetAllUsedVehiclePriceChanges()
        {
            var sqlGet = @"Select V_SaveId as PriceDate,
	                         V_Loc as Location,
	                          V_Stock as StockNumber, 
	                          V_daysinv as DaysInventory,
	                           v_int_Price as ListAmount 
                        FROM Fitzway.[dbo].AllInventory_history  
                        where V_Stock in (Select stk from [10.254.162.196].[JUNK].[dbo].[CSV_vehicleUSED] A )";

            var priceChanges = SqlMapperUtil.SqlWithParams<VehiclePriceChange>(sqlGet, new { }, "Rackspace");

            return priceChanges;
        }

        public static List<UsedVehicle> GetUsedVehicles(int monthId, int yearId)
        {

            var sqlGet = @" Select A.*,  O.OptCode as Certified from  [REYDATA].[dbo].[AgedUnits] A
                    left JOIN FITZWAY.dbo.OptionDataFM O on A.XrefId = O.XREFID and (OPTTYPE = 'FEA' and OPTCODE in (
                      'F906','F907','F908','F909','F910','F911','F912','F913','F916','F917','F918','F922','F923'
                      ))  where Month = @ReportMonth and Year = @ReportYear";


            var usedVehicles = SqlMapperUtil.SqlWithParams<UsedVehicle>(sqlGet, new { ReportMonth = monthId, ReportYear = yearId }, "ReynoldsData");

            var bRefresh = false;

            var soldVehicles = GetSoldVehicles(monthId, yearId);
            foreach (var vehicle in soldVehicles)
            {
                var usedVehicle = usedVehicles.Find(x => x.StockNumber.Trim() == vehicle.stk_no.Trim());

                if (usedVehicle.IsSold == false)
                {
                    UpdateSoldVehicles(monthId, yearId, vehicle);
                    bRefresh = true;
                }
            }

            if (bRefresh)
            {
                usedVehicles = SqlMapperUtil.SqlWithParams<UsedVehicle>(sqlGet, new { ReportMonth = monthId, ReportYear = yearId }, "ReynoldsData");
            }

            return usedVehicles;
        }

        public static List<VehicleNotes> GetAllVehicleNotes()
        {
            var vehicleNotes = new VehicleNotes();

            var sqlGet = "SELECT [Id],[Location],[VIN] ,[StockNumber],[InventoryStatus],[Notes],[LotLocation],[UpdateDate],[UpdateUser],[VehicleSold],[VehicleSoldDate],[VehicleSoldNotes] FROM [fitzway].[dbo].[InventoryStatusNotes]";

            var notes = SqlMapperUtil.SqlWithParams<VehicleNotes>(sqlGet, new {  }, "SQLServer");

            return notes;
        }

        public static VehicleNotes GetVehicleNotes(string status, string vin)
        {
            var vehicleNotes = new VehicleNotes();

            var sqlGet = "SELECT [Id],[Location],[VIN] ,[StockNumber],[InventoryStatus],[Notes],[LotLocation],[UpdateDate],[UpdateUser],[VehicleSold],[VehicleSoldDate],[VehicleSoldNotes] FROM [fitzway].[dbo].[InventoryStatusNotes] where VIN = @vin and InventoryStatus = @status";

            var  notes = SqlMapperUtil.SqlWithParams<VehicleNotes>(sqlGet, new {vin = vin, status = status }, "SQLServer");

            if (notes != null && notes.Count > 0)
            {
                vehicleNotes = notes[0];
            }
            else
            {
                vehicleNotes.InventoryStatus = Int32.Parse(status);
                vehicleNotes.VIN = vin;
            }

            return vehicleNotes;
        }

        public static int UpdateVehicleNotesById(VehicleNotes vehicleNotes)
        {

            //Now save everything to the database and save the files...
            int saveInputs = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("[FITZWAY].[dbo].[sp_UsedInventoryUpdateNotesById]", vehicleNotes, "SQLServer");
            // sp_CommissionReportsUpdateTitleDueById
            // End database saving

            return saveInputs;
        }

        public static List<InventoryHistoryStatus> GetInventoryHistory()
        {
            //var sqlGet = " Select UCHISTID, days, loc, stk, vin, color, status, updated, list_amt from [FOXPROTABLES].[dbo].[UCHISTFOX] UCH where UCH.vin in (Select vin from[10.254.162.196].junk.dbo.CSV_VehicleUsed) order by stk, uchistid desc";
            var sqlGet = "Select UCHISTID, UCH.days, UCH.loc, UCH.stk, UCH.vin, UCH.color, UCH.status, UCH.updated, UCH.list_amt from [FOXPROTABLES].[dbo].[UCHISTFOX] UCH join [10.254.162.196].junk.dbo.CSV_VehicleUsed VU ON UCH.VIN = VU.VIN AND UCH.list_amt<> vu.list_amt where UCH.vin <> '' and UCH.list_amt > 0  order by stk, uchistid desc";
            //var invHistory = SqlMapperUtil.SqlWithParams<InventoryHistoryStatus>(sqlGet, new { }, "FDServer");

            var x = new Cache();

           List<InventoryHistoryStatus> invHistory = x["InvHistory"] as List<InventoryHistoryStatus>;
            if (invHistory == null) //not in cache
            {
                 invHistory = SqlMapperUtil.SqlWithParams<InventoryHistoryStatus>(sqlGet, new { }, "FDServer");
                x["InvHistory"] = invHistory;
            }
         

            return invHistory;
        }

        public static List<UsedVehicle> GetRepoVehicles(int monthId, int yearId)
        {

            var sqlGet = @" Select A.*,  O.OptCode as Certified from  [REYDATA].[dbo].[RepoUnits] A
                    left JOIN FITZWAY.dbo.OptionDataFM O on A.XrefId = O.XREFID and (OPTTYPE = 'FEA' and OPTCODE in (
                      'F906','F907','F908','F909','F910','F911','F912','F913','F916','F917','F918','F922','F923'
                      ))  where Month = @ReportMonth and Year = @ReportYear";

            var repoVehicles = SqlMapperUtil.SqlWithParams<UsedVehicle>(sqlGet, new { ReportMonth = monthId, ReportYear = yearId }, "ReynoldsData");

            var bRefresh = false;

            var soldVehicles = GetSoldVehicles(monthId, yearId);
            foreach (var vehicle in soldVehicles)
            {
                var usedVehicle = repoVehicles.Find(x => x.StockNumber.Trim() == vehicle.stk_no.Trim());

                if (usedVehicle != null)
                {
                    if (usedVehicle.IsSold == false)
                    {
                        UpdateSoldRepoVehicles(monthId, yearId, vehicle);
                        bRefresh = true;
                    }
                }
            }

            if (bRefresh)
            {
                repoVehicles = SqlMapperUtil.SqlWithParams<UsedVehicle>(sqlGet, new { ReportMonth = monthId, ReportYear = yearId }, "ReynoldsData");
            }

            return repoVehicles;
        }

        public static List<SoldVehicle> GetSoldVehicles(int monthId, int yearId)
        {
            var startDate = new DateTime(yearId, monthId, 1).AddDays(-15);
            var endDate = new DateTime(yearId, monthId, 1).AddMonths(1).AddDays(3);

            // WE NEED TO MAKE SURE WE GET BACK ONLY FINALIZED DEALS
            //var soldVehicles = SqlMapperUtil.SqlWithParams<SoldVehicle>("Select distinct sl_SellPrice as sell_price , sl_VehicleStockNumber as stk_no, sl_VehicleBuyerLast as b_last, sl_VehicleLoc as loc, sl_VehicleDealDate as deal_date, sl_VehicleCategory as category, sl_VehicleDaysInStock as daysinstk FROM [SALESCOMMISSION].[dbo].[saleslog] S WHERE sl_VehicleDealDate between @StartDate and @EndDate  AND sl_VehicleStockNumber IN (SELECT STOCKNumber FROM [REYDATA].[dbo].[AgedUnits] WHERE [MONTH] = @ReportMonth AND [YEAR] = @ReportYear) ORDER BY S.sl_VehicleLoc, sl_VehicleDaysInStock desc", new {StartDate = startDate, EndDate = endDate, ReportMonth = monthId, ReportYear = yearId }, "ReynoldsData");

            var soldVehicles = SqlMapperUtil.SqlWithParams<SoldVehicle>("Select distinct sell_price, stk_no, b_last, SUBSTRING(location,1,3) as loc, deal_date, category, daysinstk From FOXPROTABLES.dbo.RCI_Fimaster where deal_date between @StartDate and @EndDate  AND stk_no IN (SELECT STOCKNumber FROM [REYDATA].[dbo].[AgedUnits] WHERE [MONTH] = @ReportMonth AND [YEAR] = @ReportYear) and (status = 'F' or status = 'C') ORDER BY SUBSTRING(location,1,3), daysinstk desc", new { StartDate = startDate, EndDate = endDate, ReportMonth = monthId, ReportYear = yearId }, "ReynoldsData");
            
            return soldVehicles;
        }

        public static int UpdateSoldVehicles(int monthId, int yearId, SoldVehicle vehicle)
        {

            var customer = vehicle.b_last;
            if(vehicle.category == "T")
            {
                customer = "Transfer - " + vehicle.b_last;
            }

            var sqlUpdate = "UPDATE [REYDATA].[dbo].AgedUnits set IsSold = 1, SellPrice = @SellPrice, SoldLocation = @Location, SoldDate = @SoldDate, UpdateDate = GETDATE(), CustomerName = @CustomerName where [Month] = @ReportMonth and [Year] = @ReportYear and StockNumber = @StockNumber";

            var updated = SqlMapperUtil.InsertUpdateOrDeleteSql(sqlUpdate, new { SellPrice = vehicle.sell_price, Location = vehicle.loc, SoldDate = vehicle.deal_date, ReportMonth = monthId, ReportYear = yearId, StockNumber = vehicle.stk_no, CustomerName = customer }, "ReynoldsData");

            return updated;
        }

        public static int UpdateSoldRepoVehicles(int monthId, int yearId, SoldVehicle vehicle)
        {
            var sqlUpdate = "UPDATE [REYDATA].[dbo].RepoUnits set IsSold = 1, SellPrice = @SellPrice, SoldLocation = @Location, SoldDate = @SoldDate, UpdateDate = GETDATE(), CustomerName = @CustomerName where [Month] = @ReportMonth and [Year] = @ReportYear and StockNumber = @StockNumber";

            var updated = SqlMapperUtil.InsertUpdateOrDeleteSql(sqlUpdate, new { SellPrice = vehicle.sell_price, Location = vehicle.loc, SoldDate = vehicle.deal_date, ReportMonth = monthId, ReportYear = yearId, StockNumber = vehicle.stk_no, CustomerName = vehicle.b_last }, "ReynoldsData");

            return updated;
        }

    }

}