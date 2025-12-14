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

            var driverPerformance = trips
                .Where(t => t.Status == "Completed")
                .GroupBy(t => t.DriverId)
                .Select(g => new
                {
                    DriverId = g.Key,
                    TotalTrips = g.Count(),
                    TotalDistance = g.Sum(t => t.Distance),
                    TotalRevenue = g.Sum(t => t.Fare),
                    TotalFuelCost = g.Sum(t => t.FuelCost),
                    AverageTripDistance = Math.Round(g.Average(t => t.Distance), 1)
                })
                .Join(drivers,
                    stats => stats.DriverId,
                    driver => driver.Id,
                    (stats, driver) => new
                    {
                        driver.Name,
                        driver.LicenseType,
                        Experience = (DateTime.Now - driver.HireDate).Days / 365,
                        stats.TotalTrips,
                        stats.TotalDistance,
                        stats.TotalRevenue,
                        stats.TotalFuelCost,
                        stats.AverageTripDistance,
                        Profit = stats.TotalRevenue - stats.TotalFuelCost,
                        Efficiency = Math.Round((double)stats.TotalDistance / stats.TotalTrips, 1)
                    })
                .OrderByDescending(d => d.Profit)
                .ToList();

            Console.WriteLine("\n=== Driver Performance ===");
            foreach (var driver in driverPerformance)
            {
                Console.WriteLine($"{driver.Name} (License: {driver.LicenseType}, Exp: {driver.Experience} years):");
                Console.WriteLine($"  Trips: {driver.TotalTrips}, Distance: {driver.TotalDistance}km");
                Console.WriteLine($"  Revenue: {driver.TotalRevenue:C0}, Fuel Cost: {driver.TotalFuelCost:C0}");
                Console.WriteLine($"  Profit: {driver.Profit:C0}, Avg Distance: {driver.AverageTripDistance}km");
                Console.WriteLine($"  Efficiency: {driver.Efficiency} km/trip");
            }

            var vehiclesNeedingService = vehicles
                .Select(v => new
                {
                    v.LicensePlate,
                    v.Type,
                    LastService = v.LastServiceDate,
                    DaysSinceService = (DateTime.Now - v.LastServiceDate).Days,
                    v.Odometer,
                    NextServiceKm = maintenances
                        .Where(m => m.VehicleId == v.Id)
                        .OrderByDescending(m => m.ServiceDate)
                        .FirstOrDefault()?.NextServiceKm ?? 5000,
                    KmSinceLastService = v.Odometer - (maintenances
                        .Where(m => m.VehicleId == v.Id && m.ServiceDate <= v.LastServiceDate)
                        .OrderByDescending(m => m.ServiceDate)
                        .FirstOrDefault()?.NextServiceKm ?? (v.Odometer - 5000))
                })
                .Where(v => v.DaysSinceService > 30 || v.KmSinceLastService > 5000)
                .OrderByDescending(v => v.DaysSinceService)
                .ToList();

            Console.WriteLine("\n=== Vehicles Needing Service ===");
            foreach (var vehicle in vehiclesNeedingService)
            {
                string serviceNeeded = "";
                if (vehicle.DaysSinceService > 30) serviceNeeded += $"Days: {vehicle.DaysSinceService} > 30";
                if (vehicle.KmSinceLastService > 5000)
                    serviceNeeded += $"{(serviceNeeded.Length > 0 ? ", " : "")}KM: {vehicle.KmSinceLastService} > 5000";

                Console.WriteLine($"{vehicle.LicensePlate} ({vehicle.Type}):");
                Console.WriteLine($"  Last Service: {vehicle.LastService:yyyy-MM-dd} ({vehicle.DaysSinceService} days ago)");
                Console.WriteLine($"  Odometer: {vehicle.Odometer}km, Next service at: {vehicle.NextServiceKm}km");
                Console.WriteLine($"  Service Needed: {serviceNeeded}");
            }

            var dailyRevenue = trips
                .Where(t => t.Status == "Completed" && t.EndTime.HasValue)
                .GroupBy(t => t.EndTime.Value.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalRevenue = g.Sum(t => t.Fare),
                    TotalTrips = g.Count(),
                    TotalDistance = g.Sum(t => t.Distance),
                    TotalFuelCost = g.Sum(t => t.FuelCost),
                    NetProfit = g.Sum(t => t.Fare - t.FuelCost),
                    AvgTripRevenue = Math.Round(g.Average(t => t.Fare), 0)
                })
                .OrderByDescending(d => d.Date)
                .Take(7)
                .ToList();

            Console.WriteLine("\n=== Daily Revenue (Last 7 Days) ===");
            foreach (var day in dailyRevenue)
            {
                Console.WriteLine($"{day.Date:yyyy-MM-dd}:");
                Console.WriteLine($"  Revenue: {day.TotalRevenue:C0}, Trips: {day.TotalTrips}");
                Console.WriteLine($"  Distance: {day.TotalDistance}km, Fuel Cost: {day.TotalFuelCost:C0}");
                Console.WriteLine($"  Net Profit: {day.NetProfit:C0}, Avg/Trip: {day.AvgTripRevenue:C0}");
            }

            var profitableRoutes = trips
                .Where(t => t.Status == "Completed")
                .GroupBy(t => new { t.StartLocation, t.EndLocation })
                .Select(g => new
                {
                    Route = $"{g.Key.StartLocation} → {g.Key.EndLocation}",
                    TotalTrips = g.Count(),
                    TotalDistance = g.Sum(t => t.Distance),
                    TotalRevenue = g.Sum(t => t.Fare),
                    TotalFuelCost = g.Sum(t => t.FuelCost),
                    AvgDistance = Math.Round(g.Average(t => t.Distance), 1),
                    AvgRevenue = Math.Round(g.Average(t => t.Fare), 0),
                    ProfitPerKm = Math.Round(g.Sum(t => t.Fare - t.FuelCost) / g.Sum(t => t.Distance), 0)
                })
                .OrderByDescending(r => r.ProfitPerKm)
                .Take(5)
                .ToList();

            Console.WriteLine("\n=== Most Profitable Routes ===");
            foreach (var route in profitableRoutes)
            {
                Console.WriteLine($"{route.Route}:");
                Console.WriteLine($"  Trips: {route.TotalTrips}, Distance: {route.TotalDistance}km");
                Console.WriteLine($"  Revenue: {route.TotalRevenue:C0}, Fuel Cost: {route.TotalFuelCost:C0}");
                Console.WriteLine($"  Avg Distance: {route.AvgDistance}km, Avg Revenue: {route.AvgRevenue:C0}");
                Console.WriteLine($"  Profit per KM: {route.ProfitPerKm:C0}");
            }

        }
    }

}
