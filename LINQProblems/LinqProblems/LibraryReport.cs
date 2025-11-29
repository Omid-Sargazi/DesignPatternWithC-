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
        public static void Execute()
        {
            var authors = new List<Author>
            {
                new Author { Id = 1, Name = "George Orwell", Country = "UK", BirthDate = new DateTime(1903, 6, 25) },
                new Author { Id = 2, Name = "J.K. Rowling", Country = "UK", BirthDate = new DateTime(1965, 7, 31) },
                new Author { Id = 3, Name = "Stephen King", Country = "USA", BirthDate = new DateTime(1947, 9, 21) },
                new Author { Id = 4, Name = "Agatha Christie", Country = "UK", BirthDate = new DateTime(1890, 9, 15) },
                new Author { Id = 5, Name = "Paulo Coelho", Country = "Brazil", BirthDate = new DateTime(1947, 8, 24) }
            };

            var books = new List<Book>
            {
                new Book { Id = 1, Title = "1984", AuthorId = 1, Genre = "Dystopian", PublicationYear = 1949, TotalCopies = 5, AvailableCopies = 2, ISBN = "978-0451524935" },
                new Book { Id = 2, Title = "Animal Farm", AuthorId = 1, Genre = "Political Satire", PublicationYear = 1945, TotalCopies = 4, AvailableCopies = 1, ISBN = "978-0451526342" },
                new Book { Id = 3, Title = "Harry Potter and the Philosopher's Stone", AuthorId = 2, Genre = "Fantasy", PublicationYear = 1997, TotalCopies = 8, AvailableCopies = 3, ISBN = "978-0439708180" },
                new Book { Id = 4, Title = "The Shining", AuthorId = 3, Genre = "Horror", PublicationYear = 1977, TotalCopies = 6, AvailableCopies = 4, ISBN = "978-0307743657" },
                new Book { Id = 5, Title = "Murder on the Orient Express", AuthorId = 4, Genre = "Mystery", PublicationYear = 1934, TotalCopies = 5, AvailableCopies = 2, ISBN = "978-0062693662" },
                new Book { Id = 6, Title = "The Alchemist", AuthorId = 5, Genre = "Fiction", PublicationYear = 1988, TotalCopies = 7, AvailableCopies = 5, ISBN = "978-0062315007" },
                new Book { Id = 7, Title = "Harry Potter and the Chamber of Secrets", AuthorId = 2, Genre = "Fantasy", PublicationYear = 1998, TotalCopies = 7, AvailableCopies = 2, ISBN = "978-0439064873" },
                new Book { Id = 8, Title = "It", AuthorId = 3, Genre = "Horror", PublicationYear = 1986, TotalCopies = 5, AvailableCopies = 3, ISBN = "978-1501142970" }
            };

            var members = new List<Member>
            {
                new Member { Id = 1, Name = "Ali Rezaei", Email = "ali@email.com", MembershipDate = new DateTime(2023, 1, 15), FavoriteGenre = "Fantasy", MembershipLevel = "Premium" },
                new Member { Id = 2, Name = "Maryam Mohammadi", Email = "maryam@email.com", MembershipDate = new DateTime(2023, 2, 20), FavoriteGenre = "Mystery", MembershipLevel = "Basic" },
                new Member { Id = 3, Name = "Reza Ahmadi", Email = "reza@email.com", MembershipDate = new DateTime(2023, 3, 10), FavoriteGenre = "Horror", MembershipLevel = "VIP" },
                new Member { Id = 4, Name = "Sara Hosseini", Email = "sara@email.com", MembershipDate = new DateTime(2023, 1, 5), FavoriteGenre = "Dystopian", MembershipLevel = "Premium" },
                new Member { Id = 5, Name = "Mohammad Karimi", Email = "mohammad@email.com", MembershipDate = new DateTime(2023, 4, 25), FavoriteGenre = "Fiction", MembershipLevel = "Basic" }
            };

            var loans = new List<Loan>
            {
                new Loan { Id = 1, BookId = 3, MemberId = 1, LoanDate = new DateTime(2024, 1, 10), DueDate = new DateTime(2024, 1, 24), ReturnDate = new DateTime(2024, 1, 20) },
                new Loan { Id = 2, BookId = 3, MemberId = 2, LoanDate = new DateTime(2024, 1, 15), DueDate = new DateTime(2024, 1, 29), ReturnDate = new DateTime(2024, 1, 28) },
                new Loan { Id = 3, BookId = 1, MemberId = 3, LoanDate = new DateTime(2024, 2, 1), DueDate = new DateTime(2024, 2, 15), ReturnDate = new DateTime(2024, 2, 20) }, // دیر
                new Loan { Id = 4, BookId = 3, MemberId = 4, LoanDate = new DateTime(2024, 2, 5), DueDate = new DateTime(2024, 2, 19), ReturnDate = new DateTime(2024, 2, 18) },
                new Loan { Id = 5, BookId = 6, MemberId = 1, LoanDate = new DateTime(2024, 2, 10), DueDate = new DateTime(2024, 2, 24), ReturnDate = null }, // هنوز بازگردانده نشده
                new Loan { Id = 6, BookId = 4, MemberId = 2, LoanDate = new DateTime(2024, 2, 12), DueDate = new DateTime(2024, 2, 26), ReturnDate = new DateTime(2024, 2, 25) },
                new Loan { Id = 7, BookId = 3, MemberId = 5, LoanDate = new DateTime(2024, 2, 15), DueDate = new DateTime(2024, 2, 29), ReturnDate = new DateTime(2024, 2, 28) },
                new Loan { Id = 8, BookId = 7, MemberId = 1, LoanDate = new DateTime(2024, 2, 18), DueDate = new DateTime(2024, 3, 3), ReturnDate = null }, // هنوز بازگردانده نشده
                new Loan { Id = 9, BookId = 2, MemberId = 3, LoanDate = new DateTime(2024, 2, 20), DueDate = new DateTime(2024, 3, 5), ReturnDate = new DateTime(2024, 3, 4) },
                new Loan { Id = 10, BookId = 3, MemberId = 4, LoanDate = new DateTime(2024, 2, 22), DueDate = new DateTime(2024, 3, 7), ReturnDate = new DateTime(2024, 3, 6) }
            };

            var popularBooks = loans.GroupBy(l=>l.BookId)
                .Select(g=>new
                {
                    BookId=g.Key,
                    LoanCount=g.Count(),
                    UniqueBorrowers = g.Select(x=>x.MemberId).Distinct().Count()
                }).Join(books,
                    stats=>stats.BookId,
                    book=>book.Id,
                    (stats,book)=>new
                    {
                        BookTitle=book.Title,
                        BookGenre = book.Genre,
                        stats.LoanCount,
                        stats.UniqueBorrowers,
                        AvailabilityRate = Math.Round((double)book.AvailableCopies / book.TotalCopies * 100, 1)
                    })
                .OrderByDescending(x => x.LoanCount)
                .Take(5)
                .ToList();

            Console.WriteLine("=== Most Popular Books ===");
            foreach (var book in popularBooks)
            {
                Console.WriteLine($"Title: {book.BookTitle}");
                Console.WriteLine($"  Genre: {book.BookGenre}");
                Console.WriteLine($"  Total Loans: {book.LoanCount}");
                Console.WriteLine($"  Unique Borrowers: {book.UniqueBorrowers}");
                Console.WriteLine($"  Availability: {book.AvailabilityRate}%");
            }

        }
    }



    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime? BirthDate { get; set; }
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

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string Genre { get; set; }
        public int PublicationYear { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public string ISBN { get; set; }

    }
