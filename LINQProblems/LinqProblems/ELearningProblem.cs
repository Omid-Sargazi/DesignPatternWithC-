using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.LinqProblems
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int InstructorId { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; } // in hours
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxStudents { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public string Level { get; set; } // "Beginner", "Intermediate", "Advanced"
    }

    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Expertise { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime HireDate { get; set; }
        public double Rating { get; set; }
    }

    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; } // "Active", "Completed", "Dropped"
        public decimal? FinalGrade { get; set; }
    }

    public class Assignment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public int MaxScore { get; set; }
    }

    public class Submission
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public int? Score { get; set; }
        public bool IsLate { get; set; }
    }
    public class ELearningProblem
    {
        public static void Execute()
        {
            var courses = new List<Course>
        {
            new Course { Id = 1, Title = "C# Programming Fundamentals", Category = "Programming",
                        InstructorId = 1, Price = 500000, Duration = 40, StartDate = DateTime.Now.AddDays(-30),
                        EndDate = DateTime.Now.AddDays(10), MaxStudents = 50 },
            new Course { Id = 2, Title = "Web Development with ASP.NET", Category = "Web Development",
                        InstructorId = 2, Price = 750000, Duration = 60, StartDate = DateTime.Now.AddDays(-15),
                        EndDate = DateTime.Now.AddDays(45), MaxStudents = 30 },
            new Course { Id = 3, Title = "Database Design", Category = "Database",
                        InstructorId = 1, Price = 400000, Duration = 30, StartDate = DateTime.Now.AddDays(-60),
                        EndDate = DateTime.Now.AddDays(-30), MaxStudents = 40 },
            new Course { Id = 4, Title = "JavaScript Advanced", Category = "Programming",
                        InstructorId = 3, Price = 600000, Duration = 50, StartDate = DateTime.Now.AddDays(5),
                        EndDate = DateTime.Now.AddDays(55), MaxStudents = 35 }
        };

            var students = new List<Student>
        {
            new Student { Id = 1, Name = "Ali Rezaei", Email = "ali@email.com",
                         JoinDate = DateTime.Now.AddMonths(-6), Level = "Intermediate" },
            new Student { Id = 2, Name = "Sara Mohammadi", Email = "sara@email.com",
                         JoinDate = DateTime.Now.AddMonths(-3), Level = "Beginner" },
            new Student { Id = 3, Name = "Reza Ahmadi", Email = "reza@email.com",
                         JoinDate = DateTime.Now.AddYears(-1), Level = "Advanced" },
            new Student { Id = 4, Name = "Maryam Hosseini", Email = "maryam@email.com",
                         JoinDate = DateTime.Now.AddMonths(-9), Level = "Intermediate" },
            new Student { Id = 5, Name = "Hassan Karimi", Email = "hassan@email.com",
                         JoinDate = DateTime.Now.AddMonths(-2), Level = "Beginner" }
        };

            var instructors = new List<Instructor>
        {
            new Instructor { Id = 1, Name = "Dr. Ahmad Zare", Expertise = "Programming, Database",
                           HourlyRate = 50000, HireDate = DateTime.Now.AddYears(-3), Rating = 4.8 },
            new Instructor { Id = 2, Name = "Eng. Fatemeh Alavi", Expertise = "Web Development",
                           HourlyRate = 45000, HireDate = DateTime.Now.AddYears(-2), Rating = 4.6 },
            new Instructor { Id = 3, Name = "Prof. Mohammad Nabavi", Expertise = "JavaScript, Frontend",
                           HourlyRate = 55000, HireDate = DateTime.Now.AddYears(-5), Rating = 4.9 }
        };

            var enrollments = new List<Enrollment>
        {
            new Enrollment { Id = 1, StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.Now.AddDays(-35),
                           Status = "Active", FinalGrade = null },
            new Enrollment { Id = 2, StudentId = 1, CourseId = 2, EnrollmentDate = DateTime.Now.AddDays(-20),
                           Status = "Active", FinalGrade = null },
            new Enrollment { Id = 3, StudentId = 2, CourseId = 1, EnrollmentDate = DateTime.Now.AddDays(-32),
                           Status = "Active", FinalGrade = null },
            new Enrollment { Id = 4, StudentId = 3, CourseId = 3, EnrollmentDate = DateTime.Now.AddDays(-65),
                           Status = "Completed", FinalGrade = 18.5m },
            new Enrollment { Id = 5, StudentId = 3, CourseId = 1, EnrollmentDate = DateTime.Now.AddDays(-28),
                           Status = "Active", FinalGrade = null },
            new Enrollment { Id = 6, StudentId = 4, CourseId = 2, EnrollmentDate = DateTime.Now.AddDays(-18),
                           Status = "Active", FinalGrade = null },
            new Enrollment { Id = 7, StudentId = 5, CourseId = 1, EnrollmentDate = DateTime.Now.AddDays(-25),
                           Status = "Dropped", FinalGrade = 12.0m }
        };

            var assignments = new List<Assignment>
        {
            new Assignment { Id = 1, CourseId = 1, Title = "Variables and Data Types",
                           DueDate = DateTime.Now.AddDays(-20), MaxScore = 20 },
            new Assignment { Id = 2, CourseId = 1, Title = "Control Structures",
                           DueDate = DateTime.Now.AddDays(-10), MaxScore = 25 },
            new Assignment { Id = 3, CourseId = 1, Title = "Object-Oriented Programming",
                           DueDate = DateTime.Now.AddDays(5), MaxScore = 30 },
            new Assignment { Id = 4, CourseId = 2, Title = "HTML/CSS Basics",
                           DueDate = DateTime.Now.AddDays(-5), MaxScore = 20 },
            new Assignment { Id = 5, CourseId = 2, Title = "ASP.NET MVC",
                           DueDate = DateTime.Now.AddDays(15), MaxScore = 35 },
            new Assignment { Id = 6, CourseId = 3, Title = "ER Diagrams",
                           DueDate = DateTime.Now.AddDays(-40), MaxScore = 25 }
        };

            var submissions = new List<Submission>
        {
            // Course 1 assignments
            new Submission { Id = 1, AssignmentId = 1, StudentId = 1, SubmissionDate = DateTime.Now.AddDays(-21),
                           Score = 18, IsLate = false },
            new Submission { Id = 2, AssignmentId = 1, StudentId = 2, SubmissionDate = DateTime.Now.AddDays(-19),
                           Score = 16, IsLate = false },
            new Submission { Id = 3, AssignmentId = 1, StudentId = 3, SubmissionDate = DateTime.Now.AddDays(-22),
                           Score = 20, IsLate = false },
            new Submission { Id = 4, AssignmentId = 1, StudentId = 5, SubmissionDate = DateTime.Now.AddDays(-18),
                           Score = 12, IsLate = true },

            new Submission { Id = 5, AssignmentId = 2, StudentId = 1, SubmissionDate = DateTime.Now.AddDays(-11),
                           Score = 22, IsLate = true },
            new Submission { Id = 6, AssignmentId = 2, StudentId = 2, SubmissionDate = DateTime.Now.AddDays(-9),
                           Score = 20, IsLate = false },
            new Submission { Id = 7, AssignmentId = 2, StudentId = 3, SubmissionDate = DateTime.Now.AddDays(-12),
                           Score = 25, IsLate = false },
            
            // Course 2 assignments
            new Submission { Id = 8, AssignmentId = 4, StudentId = 1, SubmissionDate = DateTime.Now.AddDays(-6),
                           Score = 18, IsLate = true },
            new Submission { Id = 9, AssignmentId = 4, StudentId = 4, SubmissionDate = DateTime.Now.AddDays(-4),
                           Score = 16, IsLate = false },
            
            // Course 3 assignments
            new Submission { Id = 10, AssignmentId = 6, StudentId = 3, SubmissionDate = DateTime.Now.AddDays(-41),
                           Score = 24, IsLate = false }
        };

            var popularCourses = enrollments
                .GroupBy(e => e.CourseId)
                .Select(g => new
                {
                    CourseId = g.Key,
                    TotalEnrollments = g.Count(),
                    ActiveEnrollments = g.Count(e => e.Status == "Active"),
                    CompletionRate = Math.Round((double)g.Count(e => e.Status == "Completed") / g.Count() * 100, 1),
                    DropoutRate = Math.Round((double)g.Count(e => e.Status == "Dropped") / g.Count() * 100, 1)
                })
                .Join(courses,
                    stats => stats.CourseId,
                    course => course.Id,
                    (stats, course) => new
                    {
                        course.Title,
                        course.Category,
                        course.Price,
                        course.Duration,
                        stats.TotalEnrollments,
                        stats.ActiveEnrollments,
                        stats.CompletionRate,
                        stats.DropoutRate,
                        EnrollmentRate = Math.Round((double)stats.ActiveEnrollments / course.MaxStudents * 100, 1)
                    })
                .OrderByDescending(c => c.TotalEnrollments)
                .ToList();

            Console.WriteLine("=== Popular Courses ===");
            foreach (var course in popularCourses)
            {
                Console.WriteLine($"{course.Title} ({course.Category}):");
                Console.WriteLine($"  Enrollments: {course.TotalEnrollments} total, {course.ActiveEnrollments} active");
                Console.WriteLine($"  Rate: {course.EnrollmentRate}% of capacity");
                Console.WriteLine($"  Completion: {course.CompletionRate}%, Dropout: {course.DropoutRate}%");
                Console.WriteLine($"  Price: {course.Price:C0}, Duration: {course.Duration}h");
            }

            var studentProgress = students
           .Select(student => new
           {
               student.Name,
               student.Level,
               student.JoinDate,
               Enrollments = enrollments.Where(e => e.StudentId == student.Id).ToList(),
               Submissions = submissions.Where(s => s.StudentId == student.Id).ToList()
           })
           .Select(data => new
           {
               data.Name,
               data.Level,
               MonthsAsStudent = (DateTime.Now - data.JoinDate).Days / 30,
               TotalCourses = data.Enrollments.Count,
               CompletedCourses = data.Enrollments.Count(e => e.Status == "Completed"),
               ActiveCourses = data.Enrollments.Count(e => e.Status == "Active"),
               AverageGrade = data.Enrollments
                   .Where(e => e.FinalGrade.HasValue)
                   .Average(e => e.FinalGrade.Value),
               AssignmentsSubmitted = data.Submissions.Count,
               AverageScore = data.Submissions
                   .Where(s => s.Score.HasValue)
                   .Average(s => s.Score.Value),
               LateSubmissions = data.Submissions.Count(s => s.IsLate)
           })
           .OrderByDescending(s => s.AverageGrade)
           .ToList();

            Console.WriteLine("\n=== Student Progress ===");
            foreach (var student in studentProgress)
            {
                Console.WriteLine($"{student.Name} ({student.Level}):");
                Console.WriteLine($"  Courses: {student.TotalCourses} total, {student.CompletedCourses} completed, {student.ActiveCourses} active");
                Console.WriteLine($"  Avg Grade: {(student.AverageGrade > 0 ? student.AverageGrade.ToString("F1") : "N/A")}");
                Console.WriteLine($"  Assignments: {student.AssignmentsSubmitted} submitted, Avg Score: {student.AverageScore:F1}");
                Console.WriteLine($"  Late Submissions: {student.LateSubmissions}");
            }

            var instructorPerformance = instructors
            .Select(instructor => new
            {
                instructor.Name,
                instructor.Expertise,
                instructor.Rating,
                instructor.HireDate,
                Courses = courses.Where(c => c.InstructorId == instructor.Id).ToList()
            })
            .Select(data => new
            {
                data.Name,
                data.Expertise,
                data.Rating,
                Experience = (DateTime.Now - data.HireDate).Days / 365,
                TotalCourses = data.Courses.Count,
                ActiveCourses = data.Courses.Count(c => c.EndDate > DateTime.Now),
                TotalStudents = enrollments
                    .Where(e => data.Courses.Any(c => c.Id == e.CourseId))
                    .Select(e => e.StudentId)
                    .Distinct()
                    .Count(),
                TotalRevenue = data.Courses
                    .Sum(c => enrollments.Count(e => e.CourseId == c.Id) * c.Price),
                StudentSatisfaction = Math.Round(enrollments
                    .Where(e => data.Courses.Any(c => c.Id == e.CourseId) && e.FinalGrade.HasValue)
                    .Average(e => e.FinalGrade.Value) / 20 * 100, 1) // Convert to percentage
            })
            .OrderByDescending(i => i.TotalRevenue)
            .ToList();

            Console.WriteLine("\n=== Instructor Performance ===");
            foreach (var instructor in instructorPerformance)
            {
                Console.WriteLine($"{instructor.Name} ({instructor.Expertise}):");
                Console.WriteLine($"  Rating: {instructor.Rating}/5, Experience: {instructor.Experience} years");
                Console.WriteLine($"  Courses: {instructor.TotalCourses} total, {instructor.ActiveCourses} active");
                Console.WriteLine($"  Students: {instructor.TotalStudents}, Revenue: {instructor.TotalRevenue:C0}");
                Console.WriteLine($"  Student Satisfaction: {instructor.StudentSatisfaction}%");
            }

            var overdueAssignments = assignments
                .Where(a => a.DueDate < DateTime.Now)
                .Select(a => new
                {
                    a.Title,
                    Course = courses.First(c => c.Id == a.CourseId).Title,
                    a.DueDate,
                    DaysOverdue = (DateTime.Now - a.DueDate).Days,
                    TotalStudents = enrollments.Count(e => e.CourseId == a.CourseId && e.Status == "Active"),
                    SubmissionsCount = submissions.Count(s => s.AssignmentId == a.Id),
                    SubmissionRate = Math.Round((double)submissions.Count(s => s.AssignmentId == a.Id) /
                        enrollments.Count(e => e.CourseId == a.CourseId && e.Status == "Active") * 100, 1)
                })
                .OrderByDescending(a => a.DaysOverdue)
                .ToList();

            Console.WriteLine("\n=== Overdue Assignments ===");
            foreach (var assignment in overdueAssignments)
            {
                Console.WriteLine($"{assignment.Title} ({assignment.Course}):");
                Console.WriteLine($"  Due: {assignment.DueDate:yyyy-MM-dd} ({assignment.DaysOverdue} days overdue)");
                Console.WriteLine($"  Students: {assignment.TotalStudents}, Submitted: {assignment.SubmissionsCount}");
                Console.WriteLine($"  Submission Rate: {assignment.SubmissionRate}%");
            }

            var pendingAssignments = assignments
                .Where(a => a.DueDate > DateTime.Now)
                .Select(a => new
                {
                    a.Title,
                    Course = courses.First(c => c.Id == a.CourseId).Title,
                    a.DueDate,
                    DaysLeft = (a.DueDate - DateTime.Now).Days,
                    TotalStudents = enrollments.Count(e => e.CourseId == a.CourseId && e.Status == "Active"),
                    Submitted = submissions.Count(s => s.AssignmentId == a.Id),
                    NotSubmitted = enrollments.Count(e => e.CourseId == a.CourseId && e.Status == "Active") -
                                   submissions.Count(s => s.AssignmentId == a.Id)
                })
                .OrderBy(a => a.DaysLeft)
                .ToList();

            Console.WriteLine("\n=== Pending Assignments ===");
            foreach (var assignment in pendingAssignments)
            {
                Console.WriteLine($"{assignment.Title} ({assignment.Course}):");
                Console.WriteLine($"  Due: {assignment.DueDate:yyyy-MM-dd} (in {assignment.DaysLeft} days)");
                Console.WriteLine($"  Status: {assignment.Submitted}/{assignment.TotalStudents} submitted");
                Console.WriteLine($"  Pending: {assignment.NotSubmitted} students");
            }

            var systemStats = new
            {
                TotalStudents = students.Count,
                TotalInstructors = instructors.Count,
                TotalCourses = courses.Count,
                ActiveCourses = courses.Count(c => c.EndDate > DateTime.Now),
                TotalEnrollments = enrollments.Count,
                ActiveEnrollments = enrollments.Count(e => e.Status == "Active"),
                CompletionRate = Math.Round((double)enrollments.Count(e => e.Status == "Completed") /
                    enrollments.Count * 100, 1),
                TotalRevenue = enrollments.Sum(e => courses.First(c => c.Id == e.CourseId).Price),
                AverageCoursePrice = Math.Round(courses.Average(c => c.Price), 0)
            };

            Console.WriteLine("\n=== E-Learning System Statistics ===");
            Console.WriteLine($"Students: {systemStats.TotalStudents}");
            Console.WriteLine($"Instructors: {systemStats.TotalInstructors}");
            Console.WriteLine($"Courses: {systemStats.TotalCourses} ({systemStats.ActiveCourses} active)");
            Console.WriteLine($"Enrollments: {systemStats.TotalEnrollments} ({systemStats.ActiveEnrollments} active)");
            Console.WriteLine($"Completion Rate: {systemStats.CompletionRate}%");
            Console.WriteLine($"Total Revenue: {systemStats.TotalRevenue:C0}");
            Console.WriteLine($"Average Course Price: {systemStats.AverageCoursePrice:C0}");


        }

    }
}
