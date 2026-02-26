using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Composition
{
    public interface INotifier
    {
        void Notify(string message);
    }

    public class EmailNotifier : INotifier
    {
        public void Notify(string message) => Console.WriteLine($"Email: {message}");
    }

    public class SmsNotifier : INotifier
    {
        public void Notify(string message) => Console.WriteLine($"SMS: {message}");
    }

    public class MultiNotifier : INotifier
    {
        private readonly List<INotifier> _notifiers;

        public MultiNotifier(params INotifier[] notifiers)
        {
            _notifiers = notifiers.ToList();
        }

        public void Notify(string message)
        {
            foreach (var notifier in _notifiers)
                notifier.Notify(message);
        }
    }

}
