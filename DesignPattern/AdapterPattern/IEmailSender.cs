using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.AdapterPattern
{
    public interface IEmailSender
    {
        void SendEmail(string to, string subject,string body);
    }

    public class ExternalMailService
    {
        public void Send(string recipient, string title,string message)
        {
            Console.WriteLine("✅ Email sent via ExternalMailService.");
        }
    }

    public class EmailAdapter : IEmailSender
    {
        private readonly ExternalMailService _service;
        public EmailAdapter(ExternalMailService service)
        {
            _service = service;
        }
        public void SendEmail(string to, string subject, string body)
        {
           _service.Send(to, subject, body);
        }
    }
}
