using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsedVehicles.Models
{
    public class UsedVehicleModel
    {
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public string ReportFilter { get; set; }
        public List<UsedVehicle> UsedVehicles { get; set; }
        public List<UsedVehicle> TransferredVehicles { get; set; }
        public List<UsedVehicle> RepoVehicles { get; set; }
        public List<UsedVehicle> AllUsedVehicles { get; set; }
        public List<UsedVehicle> AuctionVehicles { get; set; }
        public List<UsedVehicle> OtherStatusVehicles { get; set; }
        public List<InventoryHistoryStatus> InventoryStatusHistory { get; set; }
        public List<VehicleNotes> AllVehicleNotes { get; set; }
        public List<OpenRecalls> AllOpenRecalls { get; set; }
        public List<UsedVehicle> NewStatus5Vehicles { get; set; }
        public List<UsedVehicle> NewStatus20Vehicles { get; set; }

    }

    public class UsedInventoryModel
    {        
        public string Location { get; set; }
        public int MinDays { get; set; }
        public int MaxDays { get; set; }
        public string ReportFilter { get; set; }
        public string TitleStatus { get; set; }
        public List<UsedVehicle> AllUsedVehicles { get; set; }
        public List<VehiclePriceChange> VehiclePriceChanges { get; set; }
    }

    public class UsedInventoryStatusModel
    {
        public string Location { get; set; }
        public string ReportFilter { get; set; }
        public string[] TitleStatus { get; set; }
        public string[] InventoryStatus { get; set; }

        public List<UsedVehicle> AllUsedVehicles { get; set; }
        public List<VehiclePriceChange> VehiclePriceChanges { get; set; }
    }

    public class VehiclePriceChange
    {
        public DateTime PriceDate { get; set; }
        public string StockNumber { get; set; }
        public string Location { get; set; }
        public int DaysInventory { get; set; }
        public decimal ListAmount { get; set; }
    }

    public class InventoryHistoryStatus
    {
        public int UCHISTID { get; set; }
        public string days { get; set; }
        public string loc { get; set; }
        public string stk { get; set; }
        public string vin { get; set; }
        public string color { get; set; }
        public int status { get; set; }
        public decimal list_amt { get; set; }
        public DateTime updated { get; set; }

    }

    public class UsedVehicle
    {
        public int Id { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public DateTime InventoryDate { get; set; }
        public string Location { get; set; }
        public string CurrentLocation { get; set; }
        public string StockNumber { get; set; }
        public string XrefId { get; set; }
        public string ModelYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public int Status { get; set; }
        public string Color { get; set; }
        public int Miles { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int DaysInventory { get; set; }
        public int Days4008 { get; set; }
        public decimal ListAmount { get; set; }
        public decimal CostAmount { get; set; }
        public decimal NewListAmount { get; set; }
        public bool IsSold { get; set; }
        public decimal SellPrice { get; set; }
        public string SoldLocation { get; set; }
        public DateTime SoldDate { get; set; }
        public string Certified { get; set; }
        public string TitleStatus { get; set; }
        public DateTime TitleUpdatedDate { get; set; }
        public string TitleUpdatedUser {get; set;}
        public string Memo2 { get; set; }
        public string Memo1 { get; set; }
        public DateTime RODate { get; set; }
        public string RONumber { get; set; }
        public string ROMiles { get; set; }

        //Fields for Status 5, join the Sales Log Table on Status 5 only
        public string CustomerName { get; set; }
        public string DealNumber { get; set; }
        public decimal CurrentInvAmount { get; set; }
        public decimal TotalPVR { get; set; }
    }

    public class OpenRecalls
    {
        public string Loc { get; set; }
        public string StockNo  { get; set; }
        public string VIN  { get; set; }
        public string Campaign_Code { get; set; }
        public string Description { get; set; }
    }

    public class SoldVehicle
    {
        public decimal sell_price { get; set; }
        public string stk_no { get; set; }
        public string b_last { get; set; }
        public string loc { get; set; }
        public DateTime deal_date { get; set; }
        public string category { get; set; }
        public string daysinstk { get; set; }
        public decimal FinanceIncome { get; set; } 
        public decimal ServiceContract { get; set; }
        public decimal GAP { get; set; }
        public decimal MaintenanceContract { get; set; }
        public decimal DealGross { get; set; }
        public string DealKey { get; set; }
    }


    public class TitleStatus
    {
        public int Id { get; set; }
        public int DealId { get; set; }
        public string DealKey { get; set; }
        public DateTime DealDate { get; set; }
        public string Location { get; set; }
        public string LocationName { get; set; }
        public string BuyerName { get; set; }
        public string BuyerLastName { get; set; }
        public string SalesAssociate1 { get; set; }
        public string SalesAssociate2 { get; set; }
        public string FinanceManager { get; set; }
        public string SalesManager { get; set; }
        public string SalesAssociate1Name { get; set; }
        public string SalesAssociate2Name { get; set; }
        public string FinanceManagerName { get; set; }
        public string SalesManagerName { get; set; }
        public string VIN { get; set; }
        public bool ClearTitle { get; set; }
        public bool TitleDueBank { get; set; }
        public bool TitleDueCustomer { get; set; }
        public bool LienDueCustomer { get; set; }
        public bool TitleDueInterco { get; set; }
        public bool TitleDueAuction { get; set; }
        public bool LienDueBank { get; set; }
        public bool OdomDueCustomer { get; set; }
        public bool POADueCust { get; set; }
        public bool PayoffDueCust { get; set; }
        public bool WaitingOutSTTitle { get; set; }
        public bool DuplicateTitleAppliedFor { get; set; }
        public bool Other { get; set; }
        public string Notes { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string StockNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Comments { get; set; }
        public int InventoryStatus { get; set; }
        public string BankName { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerEmail { get; set; }
        public bool NoTitleDispose { get; set; }
        public bool ElectronicTitle { get; set; }
        public string FinanceManagerId { get; set; }
        public string SalesManagerId { get; set; }

    }

    public class VehicleNoteModel
    {
        public UsedVehicle VehicleInformation { get; set; }
        public VehicleNotes VehicleNotes { get; set; }
    }

    public class VehicleNotes
    {
        public int Id { get; set; }
        public string Location { get; set;}
        public string VIN { get; set; }
        public string StockNumber { get; set; }
        public int InventoryStatus { get; set; }
        public string Notes { get; set; }
        public string LotLocation { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateUser { get; set;}
        public bool VehicleSold { get; set; }
        public DateTime VehicleSoldDate { get; set; }
        public string VehicleSoldNotes { get; set; }
    }
}