using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BehavioralDesignPatternSecond
{
    public class SimpleCollection2
    {
        private string[] _items = { "Apple", "Banana", "Cherry" };

        public string GetItem(int index)
        {
            return _items[index];
        }

        public int Count => _items.Length;
    }

    public interface IIterator
    {
        bool HasNext();
        object Next();
    }

    public class SimpleIterator2:IIterator
    {
        private SimpleCollection2 _collection;
        private int _currentIndex = 0;

        public SimpleIterator2(SimpleCollection2 collection)
        {
            _collection = collection;
        }
        public bool HasNext()
        {
            return _currentIndex < _collection.Count;
        }

        public object Next()
        {
            if (HasNext())
            {
                throw new InvalidOperationException("No more items");
            }

            var item = _collection.GetItem(_currentIndex);
            _currentIndex++;
            return item;
        }
    }
}
