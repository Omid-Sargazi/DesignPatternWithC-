using System.ComponentModel.DataAnnotations;

namespace FiftyProjectsInCSharp.Projects
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }

    public class BorrowRecord
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; } // تاریخ بازگشت
        public DateTime? ReturnDate { get; set; } // تاریخ واقعی بازگشت
        public Book Book { get; set; }
        public User User { get; set; }
    }          
   

    public interface IRepository<T> where T:class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }


    public interface IBookRepository:IRepository<Book>
    {
        IEnumerable<Book> SearchBooks(string searchTerm);
        IEnumerable<Book> GetAvailableBooks();
    }

    public interface IBorrowRecordRepository:IRepository<BorrowRecord>
    {}

    public class BookRepository:IBookRepository
    {
        private readonly List<Book> _books;

        public BookRepository()
        {
            _books = new List<Book>
            {
                new Book
                {
                    Id = 1, Title = "Clean Code", Author = "Robert C. Martin", ISBN = "123", IsAvailable = true
                },
                new Book { Id = 2, Title = "Design Patterns", Author = "GoF", ISBN = "456", IsAvailable = false }
            };
        }
        public Book GetById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _books;
        }

        public void Add(Book entity)
        {
            _books.Add(entity);
        }

        public void Update(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Book entity)
        {
            _books.Remove(entity);
        }

        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            return _books.Where(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                                     ||b.Author.Contains(searchTerm,StringComparison.OrdinalIgnoreCase) || 
                                     b.ISBN.Contains(searchTerm,StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Book> GetAvailableBooks()
        {
            return _books.Where(b => b.IsAvailable);
        }
    }

    public interface ICommand
    {

    }

    public class BorrowBookCommand : ICommand
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class ReturnBookCommand : ICommand
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }

    public class BorrowBookCommandHandler
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowRecordRepository _borrowRecordRepository;

        public BorrowBookCommandHandler(IBookRepository bookRepository, IBorrowRecordRepository borrowRecordRepository)
        {
            _bookRepository = bookRepository;
            _borrowRecordRepository = borrowRecordRepository;
        }


        public bool Handle(BorrowBookCommand command)
        {
            ///////Complete 
        }
    }
}
