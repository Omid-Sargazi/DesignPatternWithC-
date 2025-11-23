using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.BehavioralDesignPattern
{
    public class SimpleCollection2
    {
        private string[] _items = { "Apple", "Banana", "Cherry" };

        public string GetItems(int index)
        {
            return _items[index];
        }

        public int Count => _items.Length;
    }

    public interface IIterator2
    {
        bool HasNext();
        object Next();
    }

    public class SimpleIteratr2:IIterator2
    {
        private readonly SimpleCollection2 _collection;
        private int _currentIndex = 0;

        public SimpleIteratr2(SimpleCollection2 collection)
        {
            _collection = collection;
        }
        public bool HasNext()
        {
            return _currentIndex < _collection.Count;
        }

        public object Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("No more items");
            }

            var item = _collection.GetItems(_currentIndex);
            _currentIndex++;
            return item;
        }
    }
}
