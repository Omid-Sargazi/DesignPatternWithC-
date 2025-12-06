using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExamples.LinqProblems1
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double GPA { get; set; }
        public string Department { get; set; }
    }


    public class ExecuteLinq
    {
        public static void Run()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, FirstName = "Ali", LastName = "Rezaei", Age = 22, GPA = 17.5, Department = "Computer Science" },
                new Student { Id = 2, FirstName = "Sara", LastName = "Mohammadi", Age = 19, GPA = 16.8, Department = "Mathematics" },
                new Student { Id = 3, FirstName = "Reza", LastName = "Ahmadi", Age = 21, GPA = 14.3, Department = "Physics" },
                new Student { Id = 4, FirstName = "Maryam", LastName = "Hosseini", Age = 20, GPA = 18.2, Department = "Computer Science" },
                new Student { Id = 5, FirstName = "Hassan", LastName = "Karimi", Age = 23, GPA = 15.7, Department = "Engineering" },
                new Student { Id = 6, FirstName = "Fatemeh", LastName = "Alavi", Age = 18, GPA = 19.1, Department = "Mathematics" },
                new Student { Id = 7, FirstName = "Amir", LastName = "Naderi", Age = 24, GPA = 13.9, Department = "Physics" },
                new Student { Id = 8, FirstName = "Narges", LastName = "Rahimi", Age = 21, GPA = 16.5, Department = "Computer Science" }
            };

            var studentsAbove20 = students
                .Where(s => s.Age > 20)
                .Select(s => new
                {
                    s.FirstName,
                    s.LastName,
                    s.Age,
                    s.Department
                })
                .ToList();

            Console.WriteLine("=== Students Above 20 Years Old ===");
            foreach (var student in studentsAbove20)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}, Age: {student.Age}, Department: {student.Department}");
            }

            var studentsHighGPA = students
                .Where(s => s.GPA > 15)
                .OrderByDescending(s => s.GPA)
                .Select(s => new
                {
                    FullName = $"{s.FirstName} {s.LastName}",
                    s.GPA,
                    s.Department
                })
                .ToList();

            Console.WriteLine("\n=== Students with GPA > 15 (Sorted by GPA) ===");
            foreach (var student in studentsHighGPA)
            {
                Console.WriteLine($"{student.FullName}: GPA = {student.GPA:F1}, Department: {student.Department}");
            }

            var studentsSortedByLastName = students
                .OrderBy(s => s.LastName)
                .Select(s => new
                {
                    s.FirstName,
                    s.LastName,
                    s.Age
                })
                .ToList();

            Console.WriteLine("\n=== Students Sorted by Last Name ===");
            foreach (var student in studentsSortedByLastName)
            {
                Console.WriteLine($"{student.LastName}, {student.FirstName} - Age: {student.Age}");
            }

            var filteredStudents = students
                .Where(s => s.Age > 20 && s.GPA > 15)
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .Select(s => new
                {
                    Name = $"{s.FirstName} {s.LastName}",
                    s.Age,
                    GPA = $"{s.GPA:F1}",
                    s.Department
                })
                .ToList();

            if (filteredStudents.Any())
            {
                foreach (var student in filteredStudents)
                {
                    Console.WriteLine($"{student.Name}, Age: {student.Age}, GPA: {student.GPA}, Department: {student.Department}");
                }
            }
            else
            {
                Console.WriteLine("No students match the criteria.");
            }

            var departmentCounts = students
                .GroupBy(s => s.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    StudentCount = g.Count(),
                    AverageGPA = Math.Round(g.Average(s => s.GPA), 2),
                    AverageAge = Math.Round(g.Average(s => s.Age), 1)
                })
                .OrderByDescending(d => d.StudentCount)
                .ToList();

            Console.WriteLine("\n=== Students by Department ===");
            foreach (var dept in departmentCounts)
            {
                Console.WriteLine($"{dept.Department}: {dept.StudentCount} students");
                Console.WriteLine($"  Average GPA: {dept.AverageGPA}, Average Age: {dept.AverageAge}");
            }

            // 6. ساده‌ترین کوئری: همه دانشجویان با فرمت زیبا
            Console.WriteLine("\n=== All Students ===");
            foreach (var student in students.OrderBy(s => s.Department).ThenBy(s => s.LastName))
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.FirstName} {student.LastName}");
                Console.WriteLine($"  Age: {student.Age}, GPA: {student.GPA:F1}, Department: {student.Department}");
                Console.WriteLine();
            }


        }
    }
}
