using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.ObserverPattern
{
    public interface IMainObserver
    {
        void Update(string productId, int newStock);
    }

    public class InventorySubject
    {
        private readonly List<IMainObserver> _observers = new List<IMainObserver>();

        public void AddObserver(IMainObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IMainObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(string productId, int newStock)
        {
            foreach(var  observer in _observers)
            {
                observer.Update(productId, newStock);
            }
        }
    }

    public class EmailNotificationObserver : IMainObserver
    {
        public void Update(string productId, int newStock)
        {
            Console.WriteLine($"Sending email: Product {productId} is out of stock.", productId);
        }
    }

    public class WebDisplayObserver : IMainObserver
    {
        public void Update(string productId, int newStock)
        {
            Console.WriteLine($"Sending web: Product {productId} is out of stock.", productId);
        }
    }
}
