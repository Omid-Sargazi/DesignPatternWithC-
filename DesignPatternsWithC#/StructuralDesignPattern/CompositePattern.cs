using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.StructuralDesignPattern
{
    public interface IContentComponent
    {
        int GetTotalWords();
        double GetReadingTime(int wordPerMInute = 200);
        void DisplayHierarchy(int indent=0);
        List<IContentComponent> Search(string keyword);

    }
    public class SimplePage:IContentComponent
    {
        
        public int Words { get; set; }

        public SimplePage(int words)
        {
            Words = words;
        }

        public int GetTotalWords()
        {
            return Words;
        }

        public double GetReadingTime(int wordPerMInute = 200)
        {
            return Words/wordPerMInute;
        }

        public void DisplayHierarchy(int indent = 0)
        {
            throw new NotImplementedException();
        }

        public List<IContentComponent> Search(string keyword)
        {
            throw new NotImplementedException();
        }
    }

    public class Season:IContentComponent
    {
        public List<IContentComponent> Children { get; set; } = new List<IContentComponent>();
        public string Title { get; set; }

        public int GetTotalWords()
        {
            int total = 0;
            foreach (var child in Children)
            {
                total += child.GetTotalWords();

            }
            return total;
        }

        public double GetReadingTime(int wordPerMInute = 200)
        {
            throw new NotImplementedException();
        }

        public void DisplayHierarchy(int indent = 0)
        {
            throw new NotImplementedException();
        }

        public List<IContentComponent> Search(string keyword)
        {
            throw new NotImplementedException();
        }
    }

    public class Book:IContentComponent
    {
        public List<IContentComponent> Children { get; set; } = new List<IContentComponent>();
        public DateTime PublishYear { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DateTime { get; set; }

        public int GetTotalWords()
        {
            int total = 0;
            foreach (var child in Children)
            {
                total += child.GetTotalWords();
                
            }
            return total;
        }

        public double GetReadingTime(int wordPerMInute = 200)
        {
            throw new NotImplementedException();
        }

        public void DisplayHierarchy(int indent = 0)
        {
            throw new NotImplementedException();
        }

        public List<IContentComponent> Search(string keyword)
        {
            throw new NotImplementedException();
        }
    }

    public class Librray:IContentComponent
    {
        public List<IContentComponent> Children { get; set; } = new List<IContentComponent>();

        public int GetTotalWords()
        {
            int total = 0;
            foreach (var child in Children)
            {
                total += child.GetTotalWords();
            }
            return total;
        }

        public double GetReadingTime(int wordPerMInute = 200)
        {
            throw new NotImplementedException();
        }

        public void DisplayHierarchy(int indent = 0)
        {
            throw new NotImplementedException();
        }

        public List<IContentComponent> Search(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
