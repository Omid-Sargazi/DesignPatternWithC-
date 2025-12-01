using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.StructuralDesignPattern
{
    public interface IContentComponent
    {
        string Title { get; set; }
        int GetTotalWords();
        double GetReadingTime(int wordPerMInute = 200);
        void DisplayHierarchy(int indent=0);
        List<IContentComponent> Search(string keyword);
        void Add(IContentComponent  component);
        void Remove(IContentComponent component);

    }
    public class SimplePage:IContentComponent
    {
        
        public int Words { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }


        public SimplePage(string title, string content, int words, string author)
        {
            Title = title;
            Content = content;
            Words = words;
            Author = author;
            CreatedDate = DateTime.Now;
        }

        public int GetTotalWords()
        {
            return Words;
        }

        public double GetReadingTime(int wordPerMInute = 200)
        {
            return (double)Words / wordPerMInute;
        }

        public void DisplayHierarchy(int indent = 0)
        {
            string spaces = new string(' ', indent);
            Console.WriteLine($"{spaces}📄 {Title} ({Words} words)");
        }

        public List<IContentComponent> Search(string keyword)
        {
            var result = new List<IContentComponent>();
            if (Title.Contains(keyword) || Content.Contains(keyword))
            {
                result.Add(this);
            }

            return result;
        }

        public void Add(IContentComponent component)
        {
            throw new NotImplementedException();
        }

        public void Remove(IContentComponent component)
        {
            throw new NotImplementedException();
        }
    }

    public class Season:IContentComponent
    {
        public List<IContentComponent> Children { get; set; } = new List<IContentComponent>();
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }

        public Season(string title)
        {
            Title = title;
            CreatedDate = DateTime.Now;
        }

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
            return Children.Sum(child => child.GetReadingTime(wordPerMInute));
        }

        public void DisplayHierarchy(int indent = 0)
        {
            string spaces = new string(' ', indent);
            Console.WriteLine($"{spaces}📁 {Title} ({Children.Count} items, {GetTotalWords()} words)");

            foreach (var child in Children)
            {
                child.DisplayHierarchy(indent + 4);
            }
        }

        public List<IContentComponent> Search(string keyword)
        {
            var results = new List<IContentComponent>();

            // جستجو در خود المان مرکب
            if (Title.Contains(keyword))
                results.Add(this);

            // جستجو در فرزندان
            foreach (var child in Children)
                results.AddRange(child.Search(keyword));

            return results;
        }

        public void Add(IContentComponent component)
        {
            Children.Add(component);
        }

        public void Remove(IContentComponent component)
        {
            Children.Add(component);
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
            var results = new List<IContentComponent>();

            // جستجو در خود المان مرکب
            if (Title.Contains(keyword))
                results.Add(this);

            // جستجو در فرزندان
            foreach (var child in Children)
                results.AddRange(child.Search(keyword));

            return results;
        }

        public void Add(IContentComponent component)
        {
            Children.Add(component);
        }
            
        public void Remove(IContentComponent component)
        {
            throw new NotImplementedException();
        }
    }

    public class Library : IContentComponent
    {
        public string Title { get; set; }
        public string Classification { get; set; }
        public List<IContentComponent> Children { get; set; } = new List<IContentComponent>();
        public DateTime CreatedDate { get; set; }

        public Library(string title, string classification = "General")
        {
            Title = title;
            Classification = classification;
            CreatedDate = DateTime.Now;
        }

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
            string spaces = new string(' ', indent);
            Console.WriteLine($"{spaces}🏛️ {Title} [{Classification}]");
            Console.WriteLine($"{spaces}   Total Contents: {Children.Count}, Total Words: {GetTotalWords()}");
            Console.WriteLine($"{spaces}   Estimated Reading: {GetReadingTime():F1} minutes");
            Console.WriteLine($"{spaces}   {new string('═', 50)}");

            foreach (var child in Children)
            {
                child.DisplayHierarchy(indent + 4);
            }
        }

        public List<IContentComponent> Search(string keyword)
        {
            var results = new List<IContentComponent>();

            // جستجو در خود المان مرکب
            if (Title.Contains(keyword))
                results.Add(this);

            // جستجو در فرزندان
            foreach (var child in Children)
                results.AddRange(child.Search(keyword));

            return results;
        }

        public void Add(IContentComponent component)
        {
            Children.Add(component);
        }

        public void Remove(IContentComponent component)
        {
            Children.Add(component);
        }
    }
}
