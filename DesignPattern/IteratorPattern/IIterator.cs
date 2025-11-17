using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.IteratorPattern
{ 
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
        void Reset();
    }
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }

    public interface IBookCollection
    {
        IIterator<Book> GetIterator();
    }

    public class ArrayBookShelf:IBookCollection
    {
        private Book[] _books;
        private int _count = 0;

        public ArrayBookShelf(int capacity)
        {
            _books = new Book[capacity];
        }
        public void AddBook(string title, string author)
        {
            if (_count < _books.Length)
            {
                _books[_count] = new Book { Title = title, Author = author };
                _count++;
            }
        }

        public IIterator<Book> GetIterator()
        {
            throw new NotImplementedException();
        }

        private class ArrayIterator:IIterator<Book>
        {
            private Book[] _books;
            private int _count;
            private int _currentPosition = 0;

            public ArrayIterator(Book[] books,int count)
            {
                _books = books;
                _count = count;
            }

            public bool HasNext()
            {
                return _currentPosition < _count;
            }

            public Book Next()
            {
                if (!HasNext())
                    throw new InvalidOperationException("No more books!");

                // کتاب فعلی را برگردان و موقعیت را افزایش بده
                Book book = _books[_currentPosition];
                _currentPosition++;
                return book;
            }

            public void Reset()
            {
                _currentPosition = 0;
            }
        }
    }


    public interface IIteratorr<T>
    {
        bool HasNext();
        string Next();
    }

    public interface IMyCollection<T> 
    {
        void Add(T t);
        IIteratorr<T> CreateIterator();
    }

    public class ListIterator<T>:IIteratorr<T>
    {
        private readonly List<T> _items;
        private int _position=0;

        public ListIterator(List<T> collection)
        {
            _items = collection;
        }
        public bool HasNext()
        {
            throw new NotImplementedException();
        }

        public string Next()
        {
            throw new NotImplementedException();
        }
    }
}
