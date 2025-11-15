using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.Problems02
{

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }
    public class Problems02
    {

        public static void Execute()
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "علی", Department = "فروش", Salary = 50000 },
                new Employee { Id = 2, Name = "رضا", Department = "توسعه", Salary = 70000 },
                new Employee { Id = 3, Name = "سارا", Department = "فروش", Salary = 55000 },
                new Employee { Id = 4, Name = "نازنین", Department = "توسعه", Salary = 80000 },
                new Employee { Id = 5, Name = "محمد", Department = "پشتیبانی", Salary = 45000 }
            };
            var average = employees.Average(e => e.Salary);

            var result1 = employees.Where(e => e.Salary > average)
                .Select(e => new { Name = e.Name, Dep = e.Department, Salary = e.Salary });


            var departmentStats = employees.GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    EmployeeCount = g.Count(),
                    AverageSalary = g.Average(e => e.Salary),
                    MaxSalary = g.Max(e => e.Salary)  // استفاده از Max برای بهینه‌تر شدن
                });

            var topEarnersByDept = employees
                .GroupBy(emp => emp.Department)
                .Select(group => group.OrderByDescending(emp => emp.Salary).First());

            var employeesWithDuplicates = new List<Employee>
            {
                new Employee { Id = 1, Name = "علی", Department = "فروش", Salary = 50000 },
                new Employee { Id = 2, Name = "رضا", Department = "توسعه", Salary = 70000 },
                new Employee { Id = 3, Name = "علی", Department = "مارکتینگ", Salary = 60000 },
                new Employee { Id = 4, Name = "سارا", Department = "توسعه", Salary = 75000 },
                new Employee { Id = 5, Name = "رضا", Department = "پشتیبانی", Salary = 48000 }
            };

            var reult2 = employeesWithDuplicates.GroupBy(e => e.Name).Where(g => g.Count() > 1)
                .SelectMany(g => g.Select(e => new { GroupName = g.Key, Employee = e }));

            var duplicateEmployees = from emp in employeesWithDuplicates
                group emp by emp.Name into nameGroup
                where nameGroup.Count() > 1
                from employee in nameGroup
                select new
                {
                    DuplicateName = nameGroup.Key,
                    Employee = employee
                };

            var res3 = employees.GroupBy(e => e.Department).OrderByDescending(g => g.Key)
                .Select(g => g.OrderByDescending(e => e.Salary)).Take(1);




        }

        public class PagedResult<T>
        {
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }
            public IEnumerable<T> Data { get; set; }
        }
    }
}
