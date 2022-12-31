using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UsedVehicles.Business;
using UsedVehicles.Models;
using System.Web.Mvc;

namespace UsedVehicles.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var usedVehicleModel = new UsedVehicleModel();

            usedVehicleModel.MonthId = DateTime.Now.Month;
            usedVehicleModel.YearId = DateTime.Now.Year;

            usedVehicleModel.UsedVehicles = SqlQueries.GetUsedVehicles(usedVehicleModel.MonthId, usedVehicleModel.YearId);
            usedVehicleModel.RepoVehicles = SqlQueries.GetRepoVehicles(usedVehicleModel.MonthId, usedVehicleModel.YearId);

            usedVehicleModel.AuctionVehicles = SqlQueries.GetAllUsedAuctionVehicles();
            usedVehicleModel.OtherStatusVehicles = SqlQueries.GetAllOtherStatusVehicles();
            usedVehicleModel.AllUsedVehicles = SqlQueries.GetAllUsedVehicles();
            usedVehicleModel.AllVehicleNotes = SqlQueries.GetAllVehicleNotes();
            usedVehicleModel.AllOpenRecalls = SqlQueries.GetAllOpenRecalls();

            usedVehicleModel.NewStatus5Vehicles = SqlQueries.GetNewStatus5Vehicles();
            usedVehicleModel.NewStatus20Vehicles = SqlQueries.GetNewStatus20Vehicles();

            try
            {
                //usedVehicleModel.InventoryStatusHistory = new List<InventoryHistoryStatus>();
                usedVehicleModel.InventoryStatusHistory = SqlQueries.GetInventoryHistory();
            }
            catch (Exception ex)
            {
                usedVehicleModel.InventoryStatusHistory = new List<InventoryHistoryStatus>();
            }

            foreach (var vehicle in usedVehicleModel.UsedVehicles)
            {
                var liveVehicle = usedVehicleModel.AllUsedVehicles.Find(x => x.StockNumber.Trim() == vehicle.StockNumber.Trim());

                if (liveVehicle != null && liveVehicle.ListAmount > 0)
                {
                    vehicle.NewListAmount = liveVehicle.ListAmount;
                    vehicle.CurrentInvAmount = liveVehicle.CostAmount;
                    vehicle.CurrentLocation = liveVehicle.Location;

                    if(vehicle.CurrentLocation != vehicle.Location)
                    {
                        vehicle.Days4008 = liveVehicle.Days4008;
                    }

                }
                else
                {
                    vehicle.CurrentInvAmount = vehicle.CostAmount;
                }
            }

            usedVehicleModel.TransferredVehicles = usedVehicleModel.UsedVehicles.FindAll(x => x.CustomerName != null && x.CustomerName.StartsWith("Transfer"));
            //see if Transferred Vehicles have been sold?


            return View(usedVehicleModel);
        }

        [HttpPost]
        public ActionResult Index(UsedVehicleModel usedVehicleModel)
        {
            usedVehicleModel.UsedVehicles = SqlQueries.GetUsedVehicles(usedVehicleModel.MonthId, usedVehicleModel.YearId);
            usedVehicleModel.RepoVehicles = SqlQueries.GetRepoVehicles(usedVehicleModel.MonthId, usedVehicleModel.YearId);
            usedVehicleModel.AuctionVehicles = SqlQueries.GetAllUsedAuctionVehicles();
            usedVehicleModel.OtherStatusVehicles = SqlQueries.GetAllOtherStatusVehicles();
            usedVehicleModel.AllUsedVehicles = SqlQueries.GetAllUsedVehicles();
            usedVehicleModel.AllVehicleNotes = SqlQueries.GetAllVehicleNotes();
            usedVehicleModel.AllOpenRecalls = SqlQueries.GetAllOpenRecalls();

            usedVehicleModel.NewStatus5Vehicles = SqlQueries.GetNewStatus5Vehicles();
            usedVehicleModel.NewStatus20Vehicles = SqlQueries.GetNewStatus20Vehicles();

            try
            {
                //usedVehicleModel.InventoryStatusHistory = new List<InventoryHistoryStatus>();
                usedVehicleModel.InventoryStatusHistory = SqlQueries.GetInventoryHistory();
            }
            catch (Exception ex)
            {
                usedVehicleModel.InventoryStatusHistory = new List<InventoryHistoryStatus>();
            }

            if (usedVehicleModel.ReportFilter != null && usedVehicleModel.ReportFilter != "")
            {

                switch(usedVehicleModel.ReportFilter)
                {
                    case "HANDYMAN":
                        usedVehicleModel.UsedVehicles = usedVehicleModel.UsedVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F916" || x.Certified.Trim() == "F917" || x.Certified.Trim() == "F918"));
                        usedVehicleModel.RepoVehicles = usedVehicleModel.RepoVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F916" || x.Certified.Trim() == "F917" || x.Certified.Trim() == "F918"));
                        usedVehicleModel.AuctionVehicles = usedVehicleModel.AuctionVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F916" || x.Certified.Trim() == "F917" || x.Certified.Trim() == "F918"));
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F916" || x.Certified.Trim() == "F917" || x.Certified.Trim() == "F918"));
                        usedVehicleModel.OtherStatusVehicles = usedVehicleModel.OtherStatusVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F916" || x.Certified.Trim() == "F917" || x.Certified.Trim() == "F918"));

                        usedVehicleModel.NewStatus5Vehicles = usedVehicleModel.NewStatus5Vehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F916" || x.Certified.Trim() == "F917" || x.Certified.Trim() == "F918"));
                        usedVehicleModel.NewStatus20Vehicles = usedVehicleModel.NewStatus20Vehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F916" || x.Certified.Trim() == "F917" || x.Certified.Trim() == "F918"));
                        
                        break;
                    case "CPO":
                        usedVehicleModel.UsedVehicles = usedVehicleModel.UsedVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F906" || x.Certified.Trim() == "F907" || x.Certified.Trim() == "F908" || x.Certified.Trim() == "F909" || x.Certified.Trim() == "F911" || x.Certified.Trim() == "F912" || x.Certified.Trim() == "F913" || x.Certified.Trim() == "F922" || x.Certified.Trim() == "F923"));
                        usedVehicleModel.RepoVehicles = usedVehicleModel.RepoVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F906" || x.Certified.Trim() == "F907" || x.Certified.Trim() == "F908" || x.Certified.Trim() == "F909" || x.Certified.Trim() == "F911" || x.Certified.Trim() == "F912" || x.Certified.Trim() == "F913" || x.Certified.Trim() == "F922" || x.Certified.Trim() == "F923"));
                        usedVehicleModel.AuctionVehicles = usedVehicleModel.AuctionVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F906" || x.Certified.Trim() == "F907" || x.Certified.Trim() == "F908" || x.Certified.Trim() == "F909" || x.Certified.Trim() == "F911" || x.Certified.Trim() == "F912" || x.Certified.Trim() == "F913" || x.Certified.Trim() == "F922" || x.Certified.Trim() == "F923"));
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F906" || x.Certified.Trim() == "F907" || x.Certified.Trim() == "F908" || x.Certified.Trim() == "F909" || x.Certified.Trim() == "F911" || x.Certified.Trim() == "F912" || x.Certified.Trim() == "F913" || x.Certified.Trim() == "F922" || x.Certified.Trim() == "F923"));
                        usedVehicleModel.OtherStatusVehicles = usedVehicleModel.OtherStatusVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F906" || x.Certified.Trim() == "F907" || x.Certified.Trim() == "F908" || x.Certified.Trim() == "F909" || x.Certified.Trim() == "F911" || x.Certified.Trim() == "F912" || x.Certified.Trim() == "F913" || x.Certified.Trim() == "F922" || x.Certified.Trim() == "F923"));

                        usedVehicleModel.NewStatus5Vehicles = usedVehicleModel.NewStatus5Vehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F906" || x.Certified.Trim() == "F907" || x.Certified.Trim() == "F908" || x.Certified.Trim() == "F909" || x.Certified.Trim() == "F911" || x.Certified.Trim() == "F912" || x.Certified.Trim() == "F913" || x.Certified.Trim() == "F922" || x.Certified.Trim() == "F923"));
                        usedVehicleModel.NewStatus20Vehicles = usedVehicleModel.NewStatus20Vehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F906" || x.Certified.Trim() == "F907" || x.Certified.Trim() == "F908" || x.Certified.Trim() == "F909" || x.Certified.Trim() == "F911" || x.Certified.Trim() == "F912" || x.Certified.Trim() == "F913" || x.Certified.Trim() == "F922" || x.Certified.Trim() == "F923"));

                        break;
                    case "NEXTCAR":

                        //StockNumber.Last();

                        //var isTrade = false;
                        //isTrade = Char.IsLetter(lastChar);

                        usedVehicleModel.UsedVehicles = usedVehicleModel.UsedVehicles.FindAll(x => (x.StockNumber.Substring(1, 1) == "N" || x.StockNumber.Substring(1, 1) == "F") && !Char.IsLetter(x.StockNumber.Trim().Last()) );
                        usedVehicleModel.RepoVehicles = usedVehicleModel.RepoVehicles.FindAll(x => (x.StockNumber.Substring(1, 1) == "N" || x.StockNumber.Substring(1, 1) == "F") && !Char.IsLetter(x.StockNumber.Trim().Last()));
                        usedVehicleModel.AuctionVehicles = usedVehicleModel.AuctionVehicles.FindAll(x => (x.StockNumber.Substring(1, 1) == "N" || x.StockNumber.Substring(1, 1) == "F") && !Char.IsLetter(x.StockNumber.Trim().Last()));
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => (x.StockNumber.Substring(1, 1) == "N" || x.StockNumber.Substring(1, 1) == "F") && !Char.IsLetter(x.StockNumber.Trim().Last()));
                        usedVehicleModel.OtherStatusVehicles = usedVehicleModel.OtherStatusVehicles.FindAll(x => (x.StockNumber.Substring(1, 1) == "N" || x.StockNumber.Substring(1, 1) == "F") && !Char.IsLetter(x.StockNumber.Trim().Last()));

                        usedVehicleModel.NewStatus5Vehicles = usedVehicleModel.NewStatus5Vehicles.FindAll(x => (x.StockNumber.Substring(1, 1) == "N" || x.StockNumber.Substring(1, 1) == "F") && !Char.IsLetter(x.StockNumber.Trim().Last()));
                        usedVehicleModel.NewStatus20Vehicles = usedVehicleModel.NewStatus20Vehicles.FindAll(x => (x.StockNumber.Substring(1, 1) == "N" || x.StockNumber.Substring(1, 1) == "F") && !Char.IsLetter(x.StockNumber.Trim().Last()));

                        break;
                    case "ALL":
                        break;

                }

            }


            foreach (var vehicle in usedVehicleModel.UsedVehicles)
            {
                var liveVehicle = usedVehicleModel.AllUsedVehicles.Find(x => x.StockNumber.Trim() == vehicle.StockNumber.Trim());

                if (liveVehicle != null && liveVehicle.ListAmount > 0)
                {
                    vehicle.NewListAmount = liveVehicle.ListAmount;
                    vehicle.CurrentInvAmount = liveVehicle.CostAmount;
                    vehicle.CurrentLocation = liveVehicle.Location;

                    if (vehicle.CurrentLocation != vehicle.Location)
                    {
                        vehicle.Days4008 = liveVehicle.Days4008;
                    }

                }
                else
                {
                    vehicle.CurrentInvAmount = vehicle.CostAmount;
                }
            }

            usedVehicleModel.TransferredVehicles = usedVehicleModel.UsedVehicles.FindAll(x => x.CustomerName != null && x.CustomerName.StartsWith("Transfer"));
            //see if Transferred Vehicles have been sold?


            return View(usedVehicleModel);
        }

        public ActionResult Inventory(string location, string minDays, string maxDays, string filter = "")
        {

            var usedVehicleModel = new UsedInventoryModel();            

            if (location != null && location != "")
            {
                var allUsedVehicles = SqlQueries.GetAllUsedVehicles();

                if (location != "ALL")
                {
                    var locationUsedVehicles = allUsedVehicles.FindAll(x => x.Location == location);
                    usedVehicleModel.AllUsedVehicles = locationUsedVehicles;
                }
                else
                {
                    usedVehicleModel.AllUsedVehicles = allUsedVehicles;
                }

            }

            if ((maxDays != null && maxDays != "") && (minDays != null && minDays != ""))
            {
                var daysUsedVehicles = new List<UsedVehicle>();
                if (Int32.Parse(maxDays) == 0 && Int32.Parse(minDays) == 0)
                {
                    daysUsedVehicles = usedVehicleModel.AllUsedVehicles;
                }
                else if (Int32.Parse(maxDays) > Int32.Parse(minDays))
                {
                    daysUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.DaysInventory > Int32.Parse(minDays) && x.DaysInventory <= Int32.Parse(maxDays));
                }
                else if (Int32.Parse(maxDays) == Int32.Parse(minDays))
                {
                    daysUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.DaysInventory > Int32.Parse(minDays) && x.DaysInventory <= Int32.Parse(maxDays) + 30);
                }
                else if (Int32.Parse(minDays) > Int32.Parse(maxDays))
                {
                    daysUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.DaysInventory > Int32.Parse(minDays));
                }

                usedVehicleModel.AllUsedVehicles = daysUsedVehicles;

                var allPriceChanges = SqlQueries.GetAllVehiclePriceChanges();

                var vehiclePriceChanges = new List<VehiclePriceChange>();

                foreach (var vehicle in usedVehicleModel.AllUsedVehicles)
                {
                    var priceChanges = allPriceChanges.FindAll(x => x.StockNumber == vehicle.StockNumber).OrderByDescending(x => x.PriceDate).ToList();

                    try
                    {
                        var lastChange = priceChanges.First(x => x.ListAmount != vehicle.ListAmount);

                        if (lastChange != null && lastChange.ListAmount != 0)
                        {
                            vehiclePriceChanges.Add(lastChange);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Item not found, so skip it!
                    }
                }

                usedVehicleModel.VehiclePriceChanges = vehiclePriceChanges;


            }
            else
            {
                minDays = "0";
                maxDays = "0";
            }


            if (filter != null && filter != "")
            {

                switch (filter)
                {
                    case "HANDYMAN":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F916" || x.Certified.Trim() == "F917" || x.Certified.Trim() == "F918"));
                        break;
                    case "CPO":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F906" || x.Certified.Trim() == "F907" || x.Certified.Trim() == "F908" || x.Certified.Trim() == "F909" || x.Certified.Trim() == "F911" || x.Certified.Trim() == "F912" || x.Certified.Trim() == "F913" || x.Certified.Trim() == "F922" || x.Certified.Trim() == "F923"));
                        break;
                    case "NEXTCAR":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => (x.StockNumber.Substring(1, 1) == "N" || x.StockNumber.Substring(1, 1) == "F") && !Char.IsLetter(x.StockNumber.Trim().Last()));
                        break;
                    case "ALL":
                        break;

                }

                usedVehicleModel.ReportFilter = filter;

            }

            var titleStatus = UsedVehicles.Business.SqlQueries.GetAllTitleStatus();

            if (usedVehicleModel.AllUsedVehicles != null)
            {
                foreach (var vehicle in usedVehicleModel.AllUsedVehicles)
                {
                    var titleDue = titleStatus.Find(x => x.VIN == vehicle.VIN);

                    var ManagerNoStatus = true;
                    var status = "";

                    if (titleDue != null)
                    {
                        if (titleDue.ClearTitle)
                        {
                            status += "Clear Title, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.TitleDueBank)
                        {
                            status += "Title Bank, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.TitleDueCustomer)
                        {
                            status += "Title Customer, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.TitleDueInterco)
                        {
                            status += "Title Interco, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.TitleDueAuction)
                        {
                            status += "Title Auction, ";
                            ManagerNoStatus = false;
                        }

                        if (titleDue.LienDueBank)
                        {
                            status += "Lien Bank, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.LienDueCustomer)
                        {
                            status += "Lien Customer, ";
                            ManagerNoStatus = false;
                        }

                        if (titleDue.OdomDueCustomer)
                        {
                            status += "Odom Customer, ";
                            ManagerNoStatus = false;
                        }

                        if (titleDue.POADueCust)
                        {
                            status += "POA Due, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.PayoffDueCust)
                        {
                            status += "Payoff Due, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.WaitingOutSTTitle)
                        {
                            status += "Waiting Out, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.DuplicateTitleAppliedFor)
                        {
                            status += "Dup Title, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.Other)
                        {
                            status += "Other, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.NoTitleDispose)
                        {
                            status += "No Title Dispose, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.ElectronicTitle)
                        {
                            status += "E-Title, ";
                            ManagerNoStatus = false;
                        }
                    }

                    if (ManagerNoStatus)
                    {
                        status = "No Status";
                    }
                    status = status.TrimEnd(' ').TrimEnd(',');

                    vehicle.TitleStatus = status;
                }
            }

            usedVehicleModel.Location = location;
            usedVehicleModel.MinDays = Int32.Parse(minDays);
            usedVehicleModel.MaxDays = Int32.Parse(maxDays);

            usedVehicleModel.TitleStatus = "";

            return View(usedVehicleModel);
        }

        [HttpPost]
        public ActionResult Inventory(UsedInventoryModel usedVehicleModel)
        {
            var allUsedVehicles = SqlQueries.GetAllUsedVehicles();

            if (usedVehicleModel.Location != null && usedVehicleModel.Location != "" && usedVehicleModel.Location != "ALL")
            {                                
                var locationUsedVehicles = allUsedVehicles.FindAll(x => x.Location == usedVehicleModel.Location);
                usedVehicleModel.AllUsedVehicles = locationUsedVehicles;
            }
            else
            {
                usedVehicleModel.AllUsedVehicles = allUsedVehicles;
            }


            
                var minDays = 0;
                var maxDays = 0;

                maxDays = usedVehicleModel.MaxDays;
                minDays = usedVehicleModel.MinDays;

                var daysUsedVehicles = new List<UsedVehicle>();
                if (usedVehicleModel.MaxDays == 0 && usedVehicleModel.MinDays == 0)
                {
                    daysUsedVehicles = usedVehicleModel.AllUsedVehicles;
                }
                else if (usedVehicleModel.MaxDays > usedVehicleModel.MinDays)
                {
                    daysUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.DaysInventory > minDays && x.DaysInventory <= maxDays);
                }
                else if (usedVehicleModel.MaxDays == usedVehicleModel.MinDays)
                {
                    daysUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.DaysInventory > minDays && x.DaysInventory <= maxDays + 15);
                }
                else if (usedVehicleModel.MinDays > usedVehicleModel.MaxDays)
                {
                    daysUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.DaysInventory > minDays);
                }

                usedVehicleModel.AllUsedVehicles = daysUsedVehicles;

            if (usedVehicleModel.ReportFilter != null && usedVehicleModel.ReportFilter != "")
            {

                switch (usedVehicleModel.ReportFilter)
                {
                    case "HANDYMAN":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F916" || x.Certified.Trim() == "F917" || x.Certified.Trim() == "F918"));
                        break;
                    case "CPO":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F906" || x.Certified.Trim() == "F907" || x.Certified.Trim() == "F908" || x.Certified.Trim() == "F909" || x.Certified.Trim() == "F911" || x.Certified.Trim() == "F912" || x.Certified.Trim() == "F913" || x.Certified.Trim() == "F922" || x.Certified.Trim() == "F923"));
                        break;
                    case "NEXTCAR":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => (x.StockNumber.Substring(1, 1) == "N" || x.StockNumber.Substring(1, 1) == "F") && !Char.IsLetter(x.StockNumber.Trim().Last()));
                        break;
                    case "ALL":
                        break;

                }

            }


            var allPriceChanges = SqlQueries.GetAllVehiclePriceChanges();
            var titleStatus = UsedVehicles.Business.SqlQueries.GetAllTitleStatus();

            var vehiclePriceChanges = new List<VehiclePriceChange>();

            foreach (var vehicle in usedVehicleModel.AllUsedVehicles)
            {
                var priceChanges = allPriceChanges.FindAll(x => x.StockNumber == vehicle.StockNumber).OrderByDescending(x => x.PriceDate).ToList();

                try
                {
                    var lastChange = priceChanges.First(x => x.ListAmount != vehicle.ListAmount);

                    if (lastChange != null && lastChange.ListAmount != 0)
                    {
                        vehiclePriceChanges.Add(lastChange);
                    }
                }
                catch(Exception ex)
                {
                    //Item not found, so skip it!
                }

                var titleDue = titleStatus.Find(x => x.VIN == vehicle.VIN);

                var ManagerNoStatus = true;
                var status = "";

                if (titleDue != null)
                {
                    if (titleDue.ClearTitle)
                    {
                        status += "Clear Title, ";
                        ManagerNoStatus = false;
                    }
                    if (titleDue.TitleDueBank)
                    {
                        status += "Title Bank, ";
                        ManagerNoStatus = false;
                    }
                    if (titleDue.TitleDueCustomer)
                    {
                        status += "Title Customer, ";
                        ManagerNoStatus = false;
                    }
                    if (titleDue.TitleDueInterco)
                    {
                        status += "Title Interco, ";
                        ManagerNoStatus = false;
                    }
                    if (titleDue.TitleDueAuction)
                    {
                        status += "Title Auction, ";
                        ManagerNoStatus = false;
                    }

                    if (titleDue.LienDueBank)
                    {
                        status += "Lien Bank, ";
                        ManagerNoStatus = false;
                    }
                    if (titleDue.LienDueCustomer)
                    {
                        status += "Lien Customer, ";
                        ManagerNoStatus = false;
                    }

                    if (titleDue.OdomDueCustomer)
                    {
                        status += "Odom Customer, ";
                        ManagerNoStatus = false;
                    }

                    if (titleDue.POADueCust)
                    {
                        status += "POA Due, ";
                        ManagerNoStatus = false;
                    }
                    if (titleDue.PayoffDueCust)
                    {
                        status += "Payoff Due, ";
                        ManagerNoStatus = false;
                    }
                    if (titleDue.WaitingOutSTTitle)
                    {
                        status += "Waiting Out, ";
                        ManagerNoStatus = false;
                    }
                    if (titleDue.DuplicateTitleAppliedFor)
                    {
                        status += "Dup Title, ";
                        ManagerNoStatus = false;
                    }
                    if (titleDue.Other)
                    {
                        status += "Other, ";
                        ManagerNoStatus = false;
                    }

                    if (titleDue.NoTitleDispose)
                    {
                        status += "No Title Dispose, ";
                        ManagerNoStatus = false;
                    }
                    if (titleDue.ElectronicTitle)
                    {
                        status += "E-Title, ";
                        ManagerNoStatus = false;
                    }
                }

                if (ManagerNoStatus)
                {
                    status = "No Status";
                }
                status = status.TrimEnd(' ').TrimEnd(',');

                vehicle.TitleStatus = status;

            }


            if(usedVehicleModel.TitleStatus != "ALL")
            {
                switch (usedVehicleModel.TitleStatus)
                {
                    case "0":
                        //no status
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("No Status"));
                        break;
                    case "1":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Clear Title"));
                        break;
                    case "2":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Bank"));
                        break;
                    case "3":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Customer"));
                        break;
                    case "4":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Lien Customer"));
                        break;
                    case "5":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Interco"));
                        break;
                    case "6":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Auction"));
                        break;
                    case "7":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Lien Bank"));
                        break;
                    case "8":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Odom Customer"));
                        break;
                    case "9":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("POA Due"));
                        break;
                    case "10":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Payoff Due"));
                        break;
                    case "11":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Waiting Out"));
                        break;
                    case "12":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Other"));
                        break;
                    case "13":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Dup Title"));
                        break;
                    case "14":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("No Title Dispose"));
                        break;
                    case "15":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("E-Title"));
                        break;
                        
                }
            }

            usedVehicleModel.VehiclePriceChanges = vehiclePriceChanges;

                // Set the price changes
                //var vehiclePriceChanges = new List<VehiclePriceChange>();
                //foreach(var vehicle in usedVehicleModel.AllUsedVehicles)
                //{
                //    var priceChange = SqlQueries.GetVehiclePriceChange(vehicle.Location, vehicle.StockNumber, vehicle.ListAmount);

                //    if(priceChange != null && priceChange.ListAmount > 0)
                //    {
                //        vehiclePriceChanges.Add(priceChange);
                //    }
                //}



                return View(usedVehicleModel);
        }


        public ActionResult TitleStatus()
        {
            var usedVehicleModel = new UsedInventoryStatusModel();

            //var allUsedVehicles = SqlQueries.GetAllUsedVehicles();
            
           
            //var auctionVehicles = SqlQueries.GetAllUsedAuctionVehicles();
            //if(auctionVehicles != null)
            //{
            //    allUsedVehicles.AddRange(auctionVehicles);
            //}

            //var otherStatusVehicles = SqlQueries.GetAllOtherStatusVehicles();
            //if(otherStatusVehicles != null)
            //{
            //    allUsedVehicles.AddRange(otherStatusVehicles);
            //}

            //usedVehicleModel.AllUsedVehicles = allUsedVehicles;

            //var titleStatus = UsedVehicles.Business.SqlQueries.GetAllTitleStatus();

            //var allPriceChanges = SqlQueries.GetAllVehiclePriceChanges();

            //var vehiclePriceChanges = new List<VehiclePriceChange>();


            //if (usedVehicleModel.AllUsedVehicles != null)
            //{
            //    foreach (var vehicle in usedVehicleModel.AllUsedVehicles)
            //    {

            //        var priceChanges = allPriceChanges.FindAll(x => x.StockNumber == vehicle.StockNumber).OrderByDescending(x => x.PriceDate).ToList();

            //        try
            //        {
            //            var lastChange = priceChanges.First(x => x.ListAmount != vehicle.ListAmount);

            //            if (lastChange != null && lastChange.ListAmount != 0)
            //            {
            //                vehiclePriceChanges.Add(lastChange);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            //Item not found, so skip it!
            //        }

            //        var titleDue = titleStatus.Find(x => x.VIN == vehicle.VIN);

            //        var ManagerNoStatus = true;
            //        var status = "";

            //        if (titleDue != null)
            //        {
            //            if (titleDue.ClearTitle)
            //            {
            //                status += "Clear Title, ";
            //                ManagerNoStatus = false;
            //            }
            //            if (titleDue.TitleDueBank)
            //            {
            //                status += "Title Bank, ";
            //                ManagerNoStatus = false;
            //            }
            //            if (titleDue.TitleDueCustomer)
            //            {
            //                status += "Title Customer, ";
            //                ManagerNoStatus = false;
            //            }
            //            if (titleDue.TitleDueInterco)
            //            {
            //                status += "Title Interco, ";
            //                ManagerNoStatus = false;
            //            }
            //            if (titleDue.TitleDueAuction)
            //            {
            //                status += "Title Auction, ";
            //                ManagerNoStatus = false;
            //            }

            //            if (titleDue.LienDueBank)
            //            {
            //                status += "Lien Bank, ";
            //                ManagerNoStatus = false;
            //            }
            //            if (titleDue.LienDueCustomer)
            //            {
            //                status += "Lien Customer, ";
            //                ManagerNoStatus = false;
            //            }

            //            if (titleDue.OdomDueCustomer)
            //            {
            //                status += "Odom Customer, ";
            //                ManagerNoStatus = false;
            //            }

            //            if (titleDue.POADueCust)
            //            {
            //                status += "POA Due, ";
            //                ManagerNoStatus = false;
            //            }
            //            if (titleDue.PayoffDueCust)
            //            {
            //                status += "Payoff Due, ";
            //                ManagerNoStatus = false;
            //            }
            //            if (titleDue.WaitingOutSTTitle)
            //            {
            //                status += "Waiting Out, ";
            //                ManagerNoStatus = false;
            //            }
            //            if (titleDue.DuplicateTitleAppliedFor)
            //            {
            //                status += "Dup Title, ";
            //                ManagerNoStatus = false;
            //            }
            //            if (titleDue.Other)
            //            {
            //                status += "Other, ";
            //                ManagerNoStatus = false;
            //            }
            //        }

            //        if (ManagerNoStatus)
            //        {
            //            status = "No Status";
            //        }
            //        status = status.TrimEnd(' ').TrimEnd(',');

            //        vehicle.TitleStatus = status;
            //    }
            //}

            //usedVehicleModel.VehiclePriceChanges = vehiclePriceChanges;

            usedVehicleModel.Location = "ALL";
            
            usedVehicleModel.TitleStatus = new string[1];
            usedVehicleModel.InventoryStatus = new string[1];

            return View(usedVehicleModel);
        }

        [HttpPost]
        public ActionResult TitleStatus(UsedInventoryStatusModel usedVehicleModel)
        {
            var allUsedVehicles = SqlQueries.GetAllUsedTitleVehicles();

            //var auctionVehicles = SqlQueries.GetAllUsedAuctionVehicles();
            //if (auctionVehicles != null)
            //{
            //    allUsedVehicles.AddRange(auctionVehicles);
            //}

            //var otherStatusVehicles = SqlQueries.GetAllOtherStatusVehicles();
            //if (otherStatusVehicles != null)
            //{
            //    allUsedVehicles.AddRange(otherStatusVehicles);
            //}

            if (usedVehicleModel.Location != null && usedVehicleModel.Location != "" && usedVehicleModel.Location != "ALL")
            {
                var location = usedVehicleModel.Location;

                if(location == "FTO")
                {
                    location = "FTN";
                }

                var locationUsedVehicles = allUsedVehicles.FindAll(x => x.Location == location);
                usedVehicleModel.AllUsedVehicles = locationUsedVehicles;
            }
            else
            {
                usedVehicleModel.AllUsedVehicles = allUsedVehicles;
            }
            

            var titleStatus = UsedVehicles.Business.SqlQueries.GetAllTitleStatus();

            if (usedVehicleModel.ReportFilter != null && usedVehicleModel.ReportFilter != "")
            {

                switch (usedVehicleModel.ReportFilter)
                {
                    case "HANDYMAN":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F916" || x.Certified.Trim() == "F917" || x.Certified.Trim() == "F918"));
                        break;
                    case "CPO":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.Certified != null && (x.Certified.Trim() == "F906" || x.Certified.Trim() == "F907" || x.Certified.Trim() == "F908" || x.Certified.Trim() == "F909" || x.Certified.Trim() == "F911" || x.Certified.Trim() == "F912" || x.Certified.Trim() == "F913" || x.Certified.Trim() == "F922" || x.Certified.Trim() == "F923"));
                        break;
                    case "NEXTCAR":
                        usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => (x.StockNumber.Substring(1, 1) == "N" || x.StockNumber.Substring(1, 1) == "F") && !Char.IsLetter(x.StockNumber.Trim().Last()));
                        break;
                    case "ALL":
                        break;

                }

            }


            if (usedVehicleModel.InventoryStatus != null && usedVehicleModel.InventoryStatus.Length > 0)
            {
                var filteredTitleDue = new List<UsedVehicle>();

                foreach (var stat in usedVehicleModel.InventoryStatus)
                {
                    if (stat != "" && stat != "ALL")
                    {
                        var statTitleDue = new List<UsedVehicle>();

                        statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.Status == Int32.Parse(stat));
                        filteredTitleDue.AddRange(statTitleDue);
                    }
                    else
                    {
                        filteredTitleDue = usedVehicleModel.AllUsedVehicles;
                        break;
                    }
                }

                usedVehicleModel.AllUsedVehicles = filteredTitleDue;
                //usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.Status.ToString() == usedVehicleModel.InventoryStatus);                

            }

            
            if (usedVehicleModel.AllUsedVehicles != null)
            {
                foreach (var vehicle in usedVehicleModel.AllUsedVehicles)
                {


                    var titleDue = titleStatus.Find(x => x.VIN == vehicle.VIN);

                    var ManagerNoStatus = true;
                    var status = "";

                    if (titleDue != null)
                    {
                        if (titleDue.ClearTitle)
                        {
                            status += "Clear Title, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.TitleDueBank)
                        {
                            status += "Title Bank, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.TitleDueCustomer)
                        {
                            status += "Title Customer, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.TitleDueInterco)
                        {
                            status += "Title Interco, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.TitleDueAuction)
                        {
                            status += "Title Auction, ";
                            ManagerNoStatus = false;
                        }

                        if (titleDue.LienDueBank)
                        {
                            status += "Lien Bank, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.LienDueCustomer)
                        {
                            status += "Lien Customer, ";
                            ManagerNoStatus = false;
                        }

                        if (titleDue.OdomDueCustomer)
                        {
                            status += "Odom Customer, ";
                            ManagerNoStatus = false;
                        }

                        if (titleDue.POADueCust)
                        {
                            status += "POA Due, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.PayoffDueCust)
                        {
                            status += "Payoff Due, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.WaitingOutSTTitle)
                        {
                            status += "Waiting Out, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.DuplicateTitleAppliedFor)
                        {
                            status += "Dup Title, ";
                            ManagerNoStatus = false;
                        }
                        if (titleDue.Other)
                        {
                            status += "Other, ";
                            ManagerNoStatus = false;
                        }
                    }

                    if (ManagerNoStatus)
                    {
                        status = "No Status";
                    }
                    status = status.TrimEnd(' ').TrimEnd(',');

                    vehicle.TitleStatus = status;
                    if (titleDue != null)
                    {
                        vehicle.TitleUpdatedDate = titleDue.UpdateDate;
                        vehicle.TitleUpdatedUser = titleDue.UpdateUser;
                    }

                }
            }

            if (usedVehicleModel.TitleStatus != null && usedVehicleModel.TitleStatus.Length > 0)
            {
                var filteredTitleDue = new List<UsedVehicle>();

                foreach (var stat in usedVehicleModel.TitleStatus)
                {
                    if (stat != "" && stat != "ALL")
                    {
                        var statTitleDue = new List<UsedVehicle>();

                        switch (stat)
                        {
                            case "0":
                                //no status
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("No Status"));
                                break;
                            case "1":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Clear Title"));
                                break;
                            case "2":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Bank"));
                                break;
                            case "3":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Customer"));
                                break;
                            case "4":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Lien Customer"));
                                break;
                            case "5":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Interco"));
                                break;
                            case "6":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Auction"));
                                break;
                            case "7":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Lien Bank"));
                                break;
                            case "8":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Odom Customer"));
                                break;
                            case "9":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("POA Due"));
                                break;
                            case "10":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Payoff Due"));
                                break;
                            case "11":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Waiting Out"));
                                break;
                            case "12":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Other"));
                                break;
                            case "13":
                                statTitleDue = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Dup Title"));
                                break;

                        }
                        filteredTitleDue.AddRange(statTitleDue);
                    }
                    else
                    {
                        filteredTitleDue = usedVehicleModel.AllUsedVehicles;
                        break;
                    }
                }

                usedVehicleModel.AllUsedVehicles = filteredTitleDue;

            }


            //if (usedVehicleModel.TitleStatus != "ALL")
            //{
            //    switch (usedVehicleModel.TitleStatus)
            //    {
            //        case "0":
            //            //no status
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("No Status"));
            //            break;
            //        case "1":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Clear Title"));
            //            break;
            //        case "2":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Bank"));
            //            break;
            //        case "3":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Customer"));
            //            break;
            //        case "4":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Lien Customer"));
            //            break;
            //        case "5":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Interco"));
            //            break;
            //        case "6":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Title Auction"));
            //            break;
            //        case "7":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Lien Bank"));
            //            break;
            //        case "8":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Odom Customer"));
            //            break;
            //        case "9":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("POA Due"));
            //            break;
            //        case "10":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Payoff Due"));
            //            break;
            //        case "11":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Waiting Out"));
            //            break;
            //        case "12":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Other"));
            //            break;
            //        case "13":
            //            usedVehicleModel.AllUsedVehicles = usedVehicleModel.AllUsedVehicles.FindAll(x => x.TitleStatus.Contains("Dup Title"));
            //            break;

            //    }
            //}



            //usedVehicleModel.VehiclePriceChanges = vehiclePriceChanges;

            usedVehicleModel.Location = "ALL";

            if (usedVehicleModel.TitleStatus == null)
            {
                usedVehicleModel.TitleStatus = new string[1];
            }

            if (usedVehicleModel.InventoryStatus == null)
            {
                usedVehicleModel.InventoryStatus = new string[1];
            }

            return View(usedVehicleModel);
        }

        public ActionResult VehicleNotes(string status, string vin)
        {
            var vehicleNoteModel = new VehicleNoteModel();

            if(vin != null && vin != "")
            {
                vehicleNoteModel.VehicleInformation = SqlQueries.GetUsedVehicle(vin);
                vehicleNoteModel.VehicleNotes = SqlQueries.GetVehicleNotes(status, vin);    
            }

            if(vehicleNoteModel.VehicleInformation != null)
            {
                if(vehicleNoteModel.VehicleNotes.Location == null || vehicleNoteModel.VehicleNotes.Location == "")
                {
                    vehicleNoteModel.VehicleNotes.Location = vehicleNoteModel.VehicleInformation.Location;
                }

                if (vehicleNoteModel.VehicleNotes.StockNumber == null || vehicleNoteModel.VehicleNotes.StockNumber == "")
                {
                    vehicleNoteModel.VehicleNotes.StockNumber = vehicleNoteModel.VehicleInformation.StockNumber;
                }
            }

            return View(vehicleNoteModel);
        }

        [HttpPost]
        public ActionResult VehicleNotes(VehicleNoteModel vehicleNoteModel)
        {

            if(vehicleNoteModel.VehicleNotes != null)
            {
                vehicleNoteModel.VehicleNotes.UpdateDate = DateTime.Now;
                vehicleNoteModel.VehicleNotes.UpdateUser = Session["UserName"].ToString();

                if(vehicleNoteModel.VehicleNotes.VehicleSoldDate.ToShortDateString() == "1/1/0001")
                {
                    vehicleNoteModel.VehicleNotes.VehicleSoldDate = new DateTime(1900, 1, 1);
                }

                var success = SqlQueries.UpdateVehicleNotesById(vehicleNoteModel.VehicleNotes);
            }

            vehicleNoteModel.VehicleInformation = new UsedVehicle();
            return View(vehicleNoteModel);
        }
        
    }
}