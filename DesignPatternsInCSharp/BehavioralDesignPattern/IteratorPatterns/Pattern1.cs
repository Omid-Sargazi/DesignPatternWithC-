using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DesignPatternsInCSharp.BehavioralDesignPattern.IteratorPatterns
{
    public interface IIterator
    {
        bool HasNext();
        object Next();
    }

    public class BlcakBox
    {
        public string[] toys = new string[] {"Ball","Car","Toy1","Toy2"};

        public string GetItem(int index)
        {
            return toys[index];
        }

        public int ToyCount () => toys.Length;
    }
    public class CountBlcakBox: IIterator
    {
        private readonly BlcakBox _blcakBox;
        private  int _counter;
        public CountBlcakBox(BlcakBox box)
        {
            _blcakBox = box;
        }

        public bool HasNext()
        {
            return _counter < _blcakBox.ToyCount();
        }

        public object Next()
        {
            var item = _blcakBox.GetItem(_counter);
            _counter++;
            return item;

        }
    }
}
