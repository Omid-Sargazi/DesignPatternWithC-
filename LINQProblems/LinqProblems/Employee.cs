using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.LinqProblems
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public int YearsOfExperience { get; set; }

        public static void Execute()
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Ali Rezaei", Department = "IT", Salary = 5000, YearsOfExperience = 3 },
                new Employee { Id = 2, Name = "Maryam Mohammadi", Department = "IT", Salary = 7500, YearsOfExperience = 7 },
                new Employee { Id = 3, Name = "Reza Ahmadi", Department = "IT", Salary = 6000, YearsOfExperience = 5 },
                new Employee { Id = 4, Name = "Sara Hosseini", Department = "HR", Salary = 4500, YearsOfExperience = 2 },
                new Employee { Id = 5, Name = "Mohammad Karimi", Department = "HR", Salary = 5500, YearsOfExperience = 8 },
                new Employee { Id = 6, Name = "Zahra Alavi", Department = "Finance", Salary = 6500, YearsOfExperience = 4 },
                new Employee { Id = 7, Name = "Hassan Nabavi", Department = "Finance", Salary = 8000, YearsOfExperience = 10 },
                new Employee { Id = 8, Name = "Fatemeh Rahim", Department = "Finance", Salary = 7000, YearsOfExperience = 6 },
                new Employee { Id = 9, Name = "Amir Jafari", Department = "Marketing", Salary = 4800, YearsOfExperience = 3 },
                new Employee { Id = 10, Name = "Narges Moradi", Department = "Marketing", Salary = 5200, YearsOfExperience = 7 }
            };

            var departmentStats = employees
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Dep = g.Key,
                    AveSalary = g.Average(e => e.Salary),
                    MaxSalary = g.Max(e => e.Salary),
                    MinSalary = g.Min(e => e.Salary),
                    EmployeeCount = g.Count()
                });

            var aboveAvgEmployees = employees
                .Join(departmentStats, e => e.Department, d => d.Dep, (e, d) => new { Employee = e, Stats = d })
                .Where(x => x.Employee.Salary > x.Stats.AveSalary)
                .Select(x => new
                {
                    x.Employee.Name,
                    x.Employee.Department,
                    x.Employee.Salary,
                    DepAve = x.Stats.AveSalary
                });

            var departmentStatistics = employees
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    AverageSalary = Math.Round(g.Average(e => e.Salary), 2),
                    MaxSalary = g.Max(e => e.Salary),
                    MinSalary = g.Min(g => g.Salary),
                    EmployeeCount = g.Count(),
                    TotalSalary = g.Sum(e => e.Salary)
                })
                .OrderByDescending(d => d.AverageSalary).ToList();

            var aboveAverageEmployees = employees
                .Join(departmentStatistics,emp=>emp.Department,
                    dept=>dept.Department,
                    (emp,dept)=>new {Employee=emp, DepartmentStats =dept})
                .Where(x=>x.Employee.Salary>x.DepartmentStats.AverageSalary)
                .Select(x=>new
                {
                    x.Employee.Name,
                    x.Employee.Department,
                    Salary = x.Employee.Salary,
                    DepartmentAverage = x.DepartmentStats.AverageSalary,
                    Difference = Math.Round(x.Employee.Salary - x.DepartmentStats.AverageSalary, 2),
                    x.Employee.YearsOfExperience
                }).OrderByDescending(x=>x.Difference)
                .ToList();
        }
    }


    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // "Temperature", "Humidity", "Pressure"
        public string Location { get; set; }
        public decimal MinSafeValue { get; set; }
        public decimal MaxSafeValue { get; set; }
    }

    public class SensorReading
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public decimal Value { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsAlert { get; set; }
    }

    public class ReadSensorExecute
    {
        public static void Execute()
        {
            var sensors = new List<Sensor>
            {
                new Sensor { Id = 1, Name = "Temp-1", Type = "Temperature", Location = "Room-A", MinSafeValue = 15, MaxSafeValue = 30 },
                new Sensor { Id = 2, Name = "Temp-2", Type = "Temperature", Location = "Room-B", MinSafeValue = 15, MaxSafeValue = 30 },
                new Sensor { Id = 3, Name = "Humid-1", Type = "Humidity", Location = "Room-A", MinSafeValue = 30, MaxSafeValue = 70 },
                new Sensor { Id = 4, Name = "Humid-2", Type = "Humidity", Location = "Room-B", MinSafeValue = 30, MaxSafeValue = 70 },
                new Sensor { Id = 5, Name = "Press-1", Type = "Pressure", Location = "Lab-1", MinSafeValue = 900, MaxSafeValue = 1100 },
                new Sensor { Id = 6, Name = "Vib-1", Type = "Vibration", Location = "Machine-1", MinSafeValue = 0, MaxSafeValue = 10 }
            };

            var random = new Random();
            var readings = new List<SensorReading>();
            var baseTime = DateTime.Now.AddDays(-30);

            foreach (var sensor in sensors)
            {
                for (int i = 0; i < 100; i++)
                {
                    decimal baseValue = sensor.Type switch
                    {
                        "Temperature" => 22,
                        "Humidity" => 50,
                        "Pressure" => 1000,
                        "Vibration" => 2,
                        _ => 0
                    };

                    // اضافه کردن نوسان تصادفی
                    decimal value = baseValue + (decimal)(random.NextDouble() * 10 - 5);

                    // اضافه کردن برخی مقادیر غیرعادی
                    if (random.Next(100) < 5) // 5% شانس برای outlier
                    {
                        value = baseValue + (decimal)(random.NextDouble() * 50 - 25);
                    }

                    readings.Add(new SensorReading
                    {
                        Id = readings.Count + 1,
                        SensorId = sensor.Id,
                        Value = Math.Round(value, 2),
                        Timestamp = baseTime.AddHours(i * 2),
                        IsAlert = value < sensor.MinSafeValue || value > sensor.MaxSafeValue
                    });
                }
            }

            var sensorStatistics = readings
                .GroupBy(r => r.SensorId)
                .Select(g => new
                {
                    SensorId = g.Key,
                    ReadingCount = g.Count(),
                    AverageValue = Math.Round(g.Average(r => r.Value), 2),
                    MinValue = g.Min(r => r.Value),
                    MaxValue = g.Max(r => r.Value),
                    Range = Math.Round(g.Max(r => r.Value) - g.Min(r => r.Value), 2)
                })
                .Join(sensors,
                    stats => stats.SensorId,
                    sensor => sensor.Id,
                    (stats, sensor) => new
                    {
                        SensorName = sensor.Name,
                        SensorType = sensor.Type,
                        Location = sensor.Location,
                        stats.ReadingCount,
                        stats.AverageValue,
                        stats.MinValue,
                        stats.MaxValue,
                        stats.Range,
                        sensor.MinSafeValue,
                        sensor.MaxSafeValue
                    })
                .OrderBy(x => x.SensorType)
                .ThenBy(x => x.Location)
                .ToList();


            var sensorWithStdDev = readings
                .GroupBy(r => r.SensorId)
                .Select(g =>
                {
                    var average = g.Average(r => r.Value);
                    var variance = g.Average(r => (double)((r.Value - average) * (r.Value - average)));
                    return new
                    {
                        SensorId = g.Key,
                        StandardDeviation = Math.Round((decimal)Math.Sqrt(variance), 2)
                    };
                })
                .Join(sensors,
                    stats => stats.SensorId,
                    sensor => sensor.Id,
                    (stats, sensor) => new
                    {
                        SensorName = sensor.Name,
                        SensorType = sensor.Type,
                        Location = sensor.Location,
                        stats.StandardDeviation
                    })
                .OrderByDescending(x => x.StandardDeviation)
                .ToList();

            var alertAnalysis = readings
            .Where(r => r.IsAlert)
            .GroupBy(r => r.SensorId)
            .Select(g => new
            {
                SensorId = g.Key,
                AlertCount = g.Count(),
                FirstAlert = g.Min(r => r.Timestamp),
                LastAlert = g.Max(r => r.Timestamp)
            })
            .Join(sensors,
                  alert => alert.SensorId,
                  sensor => sensor.Id,
                  (alert, sensor) => new
                  {
                      SensorName = sensor.Name,
                      SensorType = sensor.Type,
                      Location = sensor.Location,
                      alert.AlertCount,
                      alert.FirstAlert,
                      alert.LastAlert,
                      AlertRate = Math.Round((double)alert.AlertCount / readings.Count(r => r.SensorId == sensor.Id) * 100, 1)
                  })
            .OrderByDescending(x => x.AlertCount)
            .ToList();

            Console.WriteLine("\n=== Alert Analysis ===");
            foreach (var alert in alertAnalysis)
            {
                Console.WriteLine($"{alert.SensorName}: {alert.AlertCount} alerts ({alert.AlertRate}%)");
                Console.WriteLine($"  First: {alert.FirstAlert:yyyy-MM-dd}, Last: {alert.LastAlert:yyyy-MM-dd}");
                Console.WriteLine($"  Type: {alert.SensorType}, Location: {alert.Location}");
            }

            // 5. تحلیل روند روزانه
            var dailyTrends = readings
                .GroupBy(r => new { r.SensorId, r.Timestamp.Date })
                .Select(g => new
                {
                    SensorId = g.Key.SensorId,
                    Date = g.Key.Date,
                    DailyAvg = Math.Round(g.Average(r => r.Value), 2),
                    ReadingCount = g.Count()
                })
                .Join(sensors,
                      trend => trend.SensorId,
                      sensor => sensor.Id,
                      (trend, sensor) => new
                      {
                          SensorName = sensor.Name,
                          trend.Date,
                          trend.DailyAvg,
                          trend.ReadingCount
                      })
                .OrderBy(x => x.SensorName)
                .ThenBy(x => x.Date)
                .ToList();



        }
    }
}
