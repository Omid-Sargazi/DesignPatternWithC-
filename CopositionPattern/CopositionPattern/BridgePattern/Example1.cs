using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopositionPattern.BridgePattern
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

    public abstract class Message
    {
        protected INotificationChannel _channel;
        protected Message(INotificationChannel channel)
        {
            _channel = channel;
        }

        public abstract void SendMessage(string message);
    }

    public class AlertMessage : Message
    {
        public AlertMessage(INotificationChannel channel) : base(channel)
        {
        }

        public override void SendMessage(string message)
        {
            _channel.Send(message);
        }
    }

    public class PromotionMessage : Message
    {
        public PromotionMessage(INotificationChannel channel) : base(channel)
        {
        }

        public override void SendMessage(string message)
        {
            _channel.Send(message); 
        }
    }
}
