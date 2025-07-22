using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.ChainOfResponsibility
{
    public class PasswordValidation
    {
        private readonly List<Func<string, bool>> _rules = new();
        public PasswordValidation AddRules(Func<string,bool> rule)
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
                    Console.WriteLine($"Password is not strong enough.");
                    return false;
                }
            }

            Console.WriteLine("Password is strong");
            return true;
        }

        public class RunValidatePassword
        {
            public static void Run()
            {
                var validator = new PasswordValidation()
                    .AddRules(p => p.Length >= 8)
                    .AddRules(p => p.Any(char.IsDigit))
                    .AddRules(p => p.Any(char.IsUpper))
                    .AddRules(p => p.Any(c => "!@#$%^&*()".Contains(c)));

                validator.Validate("Omid1$@");
                validator.Validate("password");
            }
        }
    }
}
