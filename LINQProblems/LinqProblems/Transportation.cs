using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.LinqProblems
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Type { get; set; } // "Car", "Van", "Truck", "Bus"
        public int Capacity { get; set; }
        public decimal FuelEfficiency { get; set; } // km per liter
        public DateTime LastServiceDate { get; set; }
        public int Odometer { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseType { get; set; } // "A", "B", "C"
        public DateTime HireDate { get; set; }
        public decimal HourlyRate { get; set; }
    }

    public class Trip
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public int Distance { get; set; } // in km
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Status { get; set; } // "Scheduled", "InProgress", "Completed", "Cancelled"
        public decimal Fare { get; set; }
        public decimal FuelCost { get; set; }
    }

    public class Maintenance
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServiceType { get; set; } // "Regular", "Emergency"
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public int NextServiceKm { get; set; }
    }

}
