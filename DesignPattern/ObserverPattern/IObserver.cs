using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.ObserverPattern
{
    public interface IObserver
    {
        void Update(float temp);
    }

    public interface ISubject
    {
        void Attach(IObserver observable);
        void Detach(IObserver observable);
        void Notify();
    }

    public class Minitor : IObserver
    {
        public void Update(float temp)
        {
            Console.WriteLine($"Updated {temp}");
        }
    }

    public class SmartWatch : IObserver
    {
        public void Update(float temp)
        {
            Console.WriteLine($"Updated temp by Samrtwatch {temp}");

        }
    }

    public class WeatherCenter : ISubject
    {
        private float _temp;
        public WeatherCenter(float temp)
        {
            _temp = temp;
        }
        private readonly List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observable)
        {
            _observers.Add(observable);
        }

        public void Detach(IObserver observable)
        {
            _observers.Remove(observable);
        }

        public void Notify()
        {
           foreach(var observer in _observers)
            {
                if (_observers.Count > 0)
                {
                    observer.Update(_temp);
                }
            }
        }
    }
}
