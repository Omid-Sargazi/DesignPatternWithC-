using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsInCSharp.BridgePattern
{
    public interface INotificationChannel
    {
        void Send(string message);
    }

    public class EmailChannel : INotificationChannel
    {
        public void Send(string message)
        {
            throw new NotImplementedException();
        }
    }

    public class SMSChannel : INotificationChannel
    {
        public void Send(string message)
        {
            throw new NotImplementedException();
        }
    }
}
