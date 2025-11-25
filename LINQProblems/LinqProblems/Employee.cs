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
        }
    }
}
