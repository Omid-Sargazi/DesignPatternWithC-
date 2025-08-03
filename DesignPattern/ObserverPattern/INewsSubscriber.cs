using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.ObserverPattern
{
   public enum NewsCategory
    {
        Sports,
        Tech,
        Politics,
        Economy
    }
    public interface INewsSubscriber
    {
        string Name { get; }
        List<NewsCategory> Interests { get; }
        void Update(string news, NewsCategory category);
    }

    public class Mike : INewsSubscriber
    {
        public string Name => "Mike";

        public List<NewsCategory> Interests => new() { NewsCategory.Sports};

        public void Update(string news, NewsCategory category)
        {
            Console.WriteLine($"Mike Recived {category} news"+news);
        }
    }

    public class Frank : INewsSubscriber
    {
        public string Name => "Frank";

        public List<NewsCategory> Interests => new() { NewsCategory.Tech};

        public void Update(string news, NewsCategory category)
        {
            Console.WriteLine($"Frank received {category} news"+news);
        }
    }
    public class Sara : INewsSubscriber
    {
        public string Name =>"Sara";

        public List<NewsCategory> Interests => new() { NewsCategory.Economy} ;

        public void Update(string news, NewsCategory category)
        {
            Console.WriteLine($"Sara received {category} news" + news);

        }
    }

    public class NewsAgency
    {
        public List<INewsSubscriber> _subscribers = new List<INewsSubscriber>();
        public void Subscribe(INewsSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(INewsSubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void PublishNews(string news, NewsCategory newCategory)
        {
            Console.WriteLine("Breaking News:" + news);
            foreach(var subscribe in _subscribers)
            {
                if(subscribe.Interests.Contains(newCategory))
                {
                    subscribe.Update(news, newCategory);    
                }
            }
        }
    }
}
