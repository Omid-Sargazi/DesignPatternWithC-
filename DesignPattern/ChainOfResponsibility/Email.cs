using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.ChainOfResponsibility
{
    public delegate bool EmailRule(Email email);
    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class EmailHandler
    {
        private readonly List<EmailRule> _rules = new List<EmailRule>();

        public EmailHandler AddRule(EmailRule rule)
        {
            _rules.Add(rule);
            return this;
        }

        public bool Handle(Email email)
        {
            foreach (var rule in _rules)
            {
                if (!rule(email))
                {
                    Console.WriteLine("Email rejected.");
                    ; return false;
                }
            }
            Console.WriteLine("Email accepted.");
            return true;
        }
    }

    public class EmailValidate
    {
        public static void Run()
        {
            var email1 = new Email { Subject = "Buy now spam", Body = "Limited time offer" };
            var email2 = new Email { Subject = "Monthly invoice", Body = "Your invoice for services is ready." };

            var handler = new EmailHandler()
                .AddRule(e => !e.Subject.ToLower().Contains("spam"))
                .AddRule(e => e.Body.Length >= 20)
                .AddRule(e => e.Body.Contains("invoice") || e.Subject.Contains("invoice"));

            handler.Handle(email1);
            handler.Handle(email2);
        }
    }
}
