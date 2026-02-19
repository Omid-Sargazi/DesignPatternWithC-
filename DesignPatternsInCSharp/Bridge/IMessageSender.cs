using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Bridge
{
    public interface IMessageSender
    {
        void SendMessage(string title, string body);
    }

    public class EmailSender : IMessageSender
    {
        public void SendMessage(string title, string body)
            => Console.WriteLine($"[EMAIL] Subject: {title}\nBody: {body}");
    }

    public class SmsSender : IMessageSender
    {
        public void SendMessage(string title, string body)
            => Console.WriteLine($"[SMS] {title}: {body}");
    }

    public abstract class Message
    {
        protected IMessageSender _sender;
        public Message(IMessageSender sender) => _sender = sender;
        public abstract void Send(string content);
    }

    public class UrgentMessage : Message
    {
        public UrgentMessage(IMessageSender sender) : base(sender) { }
        public override void Send(string content)
            => _sender.SendMessage("🚨 URGENT", content);
    }

    public class NormalMessage : Message
    {
        public NormalMessage(IMessageSender sender) : base(sender) { }
        public override void Send(string content)
            => _sender.SendMessage("Info", content);
    }

}
