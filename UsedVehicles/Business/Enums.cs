using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsedVehicles.Business
{
    public class Enums
    {
        public static IEnumerable<Month> Months = new List<Month> {
            new Month {
                MonthId = 1,
                Name = "January"
            },
            new Month {
                MonthId = 2,
                Name = "February"
            },
            new Month {
                MonthId = 3,
                Name = "March"
            },
            new Month {
                MonthId = 4,
                Name = "April"
            },
            new Month {
                MonthId = 5,
                Name = "May"
            },
            new Month {
                MonthId = 6,
                Name = "June"
            },
            new Month {
                MonthId = 7,
                Name = "July"
            },
            new Month {
                MonthId = 8,
                Name = "August"
            },
            new Month {
                MonthId = 9,
                Name = "September"
            },
            new Month {
                MonthId = 10,
                Name = "October"
            },
            new Month {
                MonthId = 11,
                Name = "November"
            },
            new Month {
                MonthId = 12,
                Name = "December"
            }
        };

        public static IEnumerable<Year> Years = new List<Year> {
            new Year {
                YearId = DateTime.Now.AddYears(-10).Year,
                Name = DateTime.Now.AddYears(-10).Year.ToString()
            },
             new Year {
                YearId = DateTime.Now.AddYears(-9).Year,
                Name = DateTime.Now.AddYears(-9).Year.ToString()
            },
            new Year {
                YearId = DateTime.Now.AddYears(-8).Year,
                Name = DateTime.Now.AddYears(-8).Year.ToString()
            },
            new Year {
                YearId = DateTime.Now.AddYears(-7).Year,
                Name = DateTime.Now.AddYears(-7).Year.ToString()
            },
            new Year {
                YearId = DateTime.Now.AddYears(-6).Year,
                Name = DateTime.Now.AddYears(-6).Year.ToString()
            },
            new Year {
                YearId = DateTime.Now.AddYears(-5).Year,
                Name = DateTime.Now.AddYears(-5).Year.ToString()
            },
            new Year {
                YearId = DateTime.Now.AddYears(-4).Year,
                Name = DateTime.Now.AddYears(-4).Year.ToString()
            },
            new Year {
                YearId = DateTime.Now.AddYears(-3).Year,
                Name = DateTime.Now.AddYears(-3).Year.ToString()
            },
            new Year {
                YearId = DateTime.Now.AddYears(-2).Year,
                Name = DateTime.Now.AddYears(-2).Year.ToString()
            },
            new Year {
                YearId = DateTime.Now.AddYears(-1).Year,
                Name = DateTime.Now.AddYears(-1).Year.ToString()
            },
            new Year {
                YearId = DateTime.Now.Year,
                Name = DateTime.Now.Year.ToString()
            },
            new Year {
                YearId = DateTime.Now.AddYears(1).Year,
                Name = DateTime.Now.AddYears(1).Year.ToString()
            },
        };

        public static IEnumerable<Day> Days = new List<Day> {
            new Day {
                DayId = 0,
                Name = "0"
            },
            new Day {
                DayId = 15,
                Name = "15"
            },
            new Day {
                DayId = 30,
                Name = "30"
            },
            new Day {
                DayId = 45,
                Name = "45"
            },
            new Day {
                DayId = 60,
                Name = "60"
            },
            new Day {
                DayId = 75,
                Name = "75"
            },
            new Day {
                DayId = 90,
                Name = "90"
            },
            new Day {
                DayId = 105,
                Name = "105"
            },
            new Day {
                DayId = 120,
                Name = "120+"
            },

        };

        public static IEnumerable<Location> Locations = new List<Location> {
            new Location {
                LocationId = "ALL",
                Name = "All Stores"
            },
            new Location {
                LocationId = "FOC",
                Name = "Annapolis Cadillac/Volkswagen"
            },
            new Location {
                LocationId = "FMM",
                Name = "Annapolis Mazda/Mitsubishi"
            },
            new Location {
                LocationId = "FTO",
                Name = "Chambersburg"
            },
            new Location {
                LocationId = "CHY",
                Name = "Clearwater Hyundai"
            },
            new Location {
                LocationId = "CJE",
                Name = "Clearwater Jeep"
            },
            new Location {
                LocationId = "CSS",
                Name = "Clearwater Subaru"
            },
            new Location {
                LocationId = "COC",
                Name = "Clearwater Outlet Center"
            },
            new Location {
                LocationId = "FCG",
                Name = "Frederick Baughmans Lane"
            },
            new Location {
                LocationId = "FSS",
                Name = "Frederick Route 85"
            },
            new Location {
                LocationId = "LFO",
                Name = "Gaithersburg Hyundai/Subaru"
            },
            new Location {
                LocationId = "LFT",
                Name = "Gaithersburg Toyota"
            },
            new Location {
                LocationId = "LFM",
                Name = "Germantown"
            },
            new Location {
                LocationId = "FHT",
                Name = "Hagerstown Chrysler"
            },
            new Location {
                LocationId = "FHG",
                Name = "Hagerstown GM"
            },
            new Location {
                LocationId = "FLP",
                Name = "Lexington Park"
            },
            new Location {
                LocationId = "CDO",
                Name = "Rockville Hyundai"
            },
            new Location {
                LocationId = "FBN",
                Name = "Rockville Nicholson"
            },
            new Location {
                LocationId = "FBC",
                Name = "Rockville Subaru"
            },
            new Location {
                LocationId = "WDC",
                Name = "Wheaton"
            }
        };

    }

    public class Month
    {
        public int MonthId { get; set; }
        public string Name { get; set; }
    }

    public class Year
    {
        public int YearId { get; set; }
        public string Name { get; set; }
    }

    public class Day
    {
        public int DayId { get; set; }
        public string Name { get; set; }
    }

    public class Location
    {
        public string LocationId { get; set; }
        public string Name { get; set; }
    }

}