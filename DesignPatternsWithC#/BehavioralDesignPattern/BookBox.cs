using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.BehavioralDesignPattern
{
    public class BookBox:IBookCollection
    {
        private string[] _books = { "C# Basics", "Design Patterns", "Clean Code", "Algorithms" };

        public string GetBook(int index) => _books[index];
        public int BookCount => _books.Length;
        public IBookIterator CreateIterator()
        {
            return new SimpleBookIterator(this);
        }
    }

    public interface IBookIterator
    {
        bool HasMoreBooks();
        string GetNextBook();
        void Reset();
    }

    public class SimpleBookIterator : IBookIterator
    {
        private BookBox _bookBox;
        private int _currentPosition = 0;

        public SimpleBookIterator(BookBox bookBox)
        {
            _bookBox = bookBox;
        }
        public bool HasMoreBooks()
        {
            return _currentPosition < _bookBox.BookCount;
        }

        public string GetNextBook()
        {
            if (!HasMoreBooks())
            {
                return "No more books!";
            }

            string book = _bookBox.GetBook(_currentPosition);
            _currentPosition++;
            return book;
        }

        public void Reset()
        {
            _currentPosition = 0;
        }
    }

    public class BookIterator
    {
        public static void Execute()
        {
            BookBox bookBox = new BookBox();
            IBookIterator iterator = new SimpleBookIterator(bookBox);

            while (iterator.HasMoreBooks())
            {
                string book = iterator.GetNextBook();
                Console.WriteLine($"Reading: {book}");
            }

            // اگر دوباره بخواهیم از اول شروع کنیم
            iterator.Reset();
            Console.WriteLine("\nReading again:");
            Console.WriteLine(iterator.GetNextBook()); // C# Basics
        }
    }

    public interface IBookCollection
    {
        IBookIterator CreateIterator();
    }


    public interface Beshmor
    {
        bool HasValue();
        object Next();
    }

    public class ToyBag
    {
        public object[] _toys = { "Car", "Toy", "ball", "Puzzle" };

        public Beshmor CreateBeshmor()
        {
            return new ToyBagBeshmor(this);
        }
    }

    public class ToyBagBeshmor : Beshmor
    {
        private ToyBag _bag;
        private int _current = 0;

        public ToyBagBeshmor(ToyBag bag)
        {
            _bag = bag;
        }
        public bool HasValue()
        {
            return _current < _bag._toys.Length;
        }

        public object Next()
        {
            if (!HasValue()) return null;

            object toy = _bag._toys[_current];
            _current++;
            return toy;
        }
    }
}
