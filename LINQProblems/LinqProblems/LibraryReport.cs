using DateTime = System.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.LinqProblems
{
    public class LibraryReport
    {
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }

    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime MembershipDate { get; set; }
        public string FavoriteGenre { get; set; }
    }

    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned => ReturnDate.HasValue;
        public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;
    }
}
