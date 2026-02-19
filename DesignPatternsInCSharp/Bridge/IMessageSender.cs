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
}
