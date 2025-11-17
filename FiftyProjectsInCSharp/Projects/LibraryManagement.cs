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

    public class BookRepository:IBookRepository
    {
        public Book GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Book entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAvailableBooks()
        {
            throw new NotImplementedException();
        }
    }
}
