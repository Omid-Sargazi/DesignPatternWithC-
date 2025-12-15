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
    internal class ELearningProblem
    {
    }
}
