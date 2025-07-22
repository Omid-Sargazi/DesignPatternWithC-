using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.ChainOfResponsibility
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
    public delegate bool UserRule(User user);
    public class UserValidator
    {
        private readonly List<UserRule> _rules = new();

        public UserValidator AddRule(UserRule rule)
        {
            _rules.Add(rule);
            return this;
        }

        public bool Validate(User user)
        {
            foreach (var rule in _rules)
            {
                if (!rule(user))
                {
                    Console.WriteLine("validation failed.");
                    return false;
                }
            }
            Console.WriteLine("validation passed.");
            return true;
        }
    }

    public class UserValidation
    {
        public static void Run()
        {
            var user1 = new User { Username = "Ali", Email = "ali@example.com", Age = 25 };
            var user2 = new User { Username = "", Email = "no-at-symbol", Age = 17 };

            var validator = new UserValidator()
                .AddRule(user => !string.IsNullOrWhiteSpace(user.Username))
                .AddRule(user => user.Email.Contains("@"))
                .AddRule(user => user.Age >= 18);

            validator.Validate(user1);
            validator.Validate(user2);
        }
    }
}
