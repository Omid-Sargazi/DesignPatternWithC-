using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BehavioralDesignPatternSecond
{
    public interface IIterator<T>
    {
        T GetNext();
        bool HasMore();
    }

    public interface IAggregate<T>
    {
        IIterator<T> CreateIterator();
    }

    public class SimpleCollection:IAggregate<string>
    {
        private string[] _items;

        public SimpleCollection(string[] items)
        {
            _items = items;
        }
        public IIterator<string> CreateIterator()
        {
            return new SimpleIterator(this);
        }

        public int Count
        {
            get { return _items.Length; }
        }

        public string this[int index]
        {
            get { return _items[index]; }
        }
    }

    public class SimpleIterator:IIterator<string>
    {
        private SimpleCollection _collection;
        private int _currentIndex = 0;

        public SimpleIterator(SimpleCollection collection)
        {
            _collection = collection;
        }
        public string GetNext()
        {
            if (!HasMore())
                throw new InvalidOperationException("No more items");

            return _collection[_currentIndex++];
        }

        public bool HasMore()
        {
            return _currentIndex < _collection.Count;
        }

       
    }
}
