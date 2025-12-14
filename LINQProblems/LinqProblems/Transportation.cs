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

    public class Transportation
    {
        public static void Execute()
        {
            var vehicles = new List<Vehicle>
        {
            new Vehicle { Id = 1, LicensePlate = "12ب12345", Type = "Car", Capacity = 4,
                         FuelEfficiency = 12, LastServiceDate = DateTime.Now.AddDays(-30),
                         Odometer = 50000, IsAvailable = true },
            new Vehicle { Id = 2, LicensePlate = "22ب12346", Type = "Van", Capacity = 8,
                         FuelEfficiency = 8, LastServiceDate = DateTime.Now.AddDays(-60),
                         Odometer = 75000, IsAvailable = true },
            new Vehicle { Id = 3, LicensePlate = "32ب12347", Type = "Truck", Capacity = 2,
                         FuelEfficiency = 5, LastServiceDate = DateTime.Now.AddDays(-90),
                         Odometer = 120000, IsAvailable = false },
            new Vehicle { Id = 4, LicensePlate = "42ب12348", Type = "Bus", Capacity = 40,
                         FuelEfficiency = 4, LastServiceDate = DateTime.Now.AddDays(-15),
                         Odometer = 200000, IsAvailable = true }
        };

            var drivers = new List<Driver>
        {
            new Driver { Id = 1, Name = "Ali Rezaei", LicenseNumber = "L123456",
                        LicenseType = "B", HireDate = DateTime.Now.AddYears(-3), HourlyRate = 25 },
            new Driver { Id = 2, Name = "Mohammad Karimi", LicenseNumber = "L234567",
                        LicenseType = "C", HireDate = DateTime.Now.AddYears(-1), HourlyRate = 30 },
            new Driver { Id = 3, Name = "Hassan Ahmadi", LicenseNumber = "L345678",
                        LicenseType = "D", HireDate = DateTime.Now.AddYears(-5), HourlyRate = 35 }
        };

            var trips = new List<Trip>
        {
            new Trip { Id = 1, VehicleId = 1, DriverId = 1, StartLocation = "Tehran",
                      EndLocation = "Karaj", Distance = 40, StartTime = DateTime.Now.AddHours(-5),
                      EndTime = DateTime.Now.AddHours(-4), Status = "Completed", Fare = 250000, FuelCost = 50000 },
            new Trip { Id = 2, VehicleId = 2, DriverId = 2, StartLocation = "Tehran",
                      EndLocation = "Qom", Distance = 140, StartTime = DateTime.Now.AddHours(-3),
                      EndTime = null, Status = "InProgress", Fare = 800000, FuelCost = 140000 },
            new Trip { Id = 3, VehicleId = 4, DriverId = 3, StartLocation = "Tehran",
                      EndLocation = "Isfahan", Distance = 450, StartTime = DateTime.Now.AddDays(-1),
                      EndTime = DateTime.Now.AddDays(-1).AddHours(6), Status = "Completed", Fare = 2500000, FuelCost = 450000 },
            new Trip { Id = 4, VehicleId = 1, DriverId = 1, StartLocation = "Tehran",
                      EndLocation = "Shiraz", Distance = 950, StartTime = DateTime.Now.AddDays(1),
                      EndTime = null, Status = "Scheduled", Fare = 5000000, FuelCost = 950000 },
            new Trip { Id = 5, VehicleId = 3, DriverId = 2, StartLocation = "Tehran",
                      EndLocation = "Mashhad", Distance = 900, StartTime = DateTime.Now.AddDays(-2),
                      EndTime = DateTime.Now.AddDays(-1), Status = "Completed", Fare = 4500000, FuelCost = 900000 }
        };

            var maintenances = new List<Maintenance>
        {
            new Maintenance { Id = 1, VehicleId = 1, ServiceDate = DateTime.Now.AddDays(-30),
                            ServiceType = "Regular", Cost = 500000, Description = "Oil change and filter", NextServiceKm = 1000 },
            new Maintenance { Id = 2, VehicleId = 2, ServiceDate = DateTime.Now.AddDays(-60),
                            ServiceType = "Regular", Cost = 800000, Description = "Brake pads replacement", NextServiceKm = 5000 },
            new Maintenance { Id = 3, VehicleId = 3, ServiceDate = DateTime.Now.AddDays(-90),
                            ServiceType = "Emergency", Cost = 2000000, Description = "Engine repair", NextServiceKm = 3000 },
            new Maintenance { Id = 4, VehicleId = 4, ServiceDate = DateTime.Now.AddDays(-15),
                            ServiceType = "Regular", Cost = 1500000, Description = "Tire replacement", NextServiceKm = 10000 }
        };

            var activeTrips = trips
                .Where(t => t.Status == "InProgress")
                .Select(t => new
                {
                    t.Id,
                    Vehicle = vehicles.First(v => v.Id == t.VehicleId).LicensePlate,
                    Driver = drivers.First(d => d.Id == t.DriverId).Name,
                    Route = $"{t.StartLocation} → {t.EndLocation}",
                    t.Distance,
                    Duration = t.EndTime.HasValue ?
                        (t.EndTime.Value - t.StartTime).TotalHours :
                        (DateTime.Now - t.StartTime).TotalHours,
                    t.Fare
                })
                .ToList();

            Console.WriteLine("=== Active Trips ===");
            foreach (var trip in activeTrips)
            {
                Console.WriteLine($"Trip #{trip.Id}: {trip.Route}");
                Console.WriteLine($"  Vehicle: {trip.Vehicle}, Driver: {trip.Driver}");
                Console.WriteLine($"  Distance: {trip.Distance}km, Duration: {trip.Duration:F1}h");
                Console.WriteLine($"  Fare: {trip.Fare:C0}");
            }
        }
    }

}
