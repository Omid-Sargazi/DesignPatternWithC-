using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.BehavioralDesignPattern
{
    public class SimpleCollection
    {
        private string[] _items = { "Apple", "Banana", "Cherry" };

        public string GetItem(int index)
        {
            return _items[index];
        }

        public int Count=> _items.Length;
    }

    public interface IIterator
    {
        bool HasNext();
        object Next();
    }

    public class SimpleIterator:IIterator
    {
        private SimpleCollection _collection;
        private int _currentIndex = 0;

        public SimpleIterator(SimpleCollection collection)
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

            var item = _collection.GetItem(_currentIndex);
            _currentIndex++;
            return item;
        }
    }
}
