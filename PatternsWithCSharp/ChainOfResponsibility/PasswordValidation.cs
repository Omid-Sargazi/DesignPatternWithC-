using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsWithCSharp.ChainOfResponsibility
{
    public class PasswordValidator
    {
        private readonly List<Func<string, bool>> _rules = new();

        public PasswordValidator AddRule(Func<string,bool> rule)
        {
            _rules.Add(rule);
            return this;
        }

        public bool Validate(string password)
        {
            foreach(var rule in _rules)
            {
                if(!rule(password))
                {
                    Console.WriteLine("❌ Password is not strong enough.");
                    return false;
                }
            }
            Console.WriteLine("✅ Password is strong.");
            return true;
        }
    }

    public class ClientPassword
    {
        public static void Run()
        {
            var validator = new PasswordValidator()
                .AddRule(p => p.Length >= 8)
                .AddRule(p => p.Any(char.IsDigit))
               .AddRule(p => p.Any(char.IsUpper))
               .AddRule(p => p.Any(c => "!@#$%^&*()".Contains(c)));

            validator.Validate("Password");
            validator.Validate("Weak");
        }
    }
}
