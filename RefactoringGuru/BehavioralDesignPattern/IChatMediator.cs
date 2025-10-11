using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.BehavioralDesignPattern
{
    public class User
    {
        private IChatMediator _chatMediator;
        public string Name { get; }

        public User(string name, IChatMediator chatMediator)
        {
            Name = name;
            _chatMediator = chatMediator;
            _chatMediator.Register(this);
        }

        public void SendMessage(string message)
        {
            Console.WriteLine($"{Name} sends {message}");
            _chatMediator.SendMessage(message,this);
        }

        public void ReceiveMessage(string message,User sender)
        {
            Console.WriteLine($"{Name} received from {sender.Name}:{message}");
        }
    }
    public interface IChatMediator
    {
        void SendMessage(string message, User sender);
        void Register(User user);
    }

    public class ChatMediator : IChatMediator
    {
        private List<User> _users = new List<User>();
        public void SendMessage(string message, User sender)
        {
            foreach (var user in _users)
            {
                if (user != sender)
                {
                    user.ReceiveMessage(message,sender);
                }
            }
        }

        public void Register(User user)
        {
            if (!_users.Contains(user))
            {
                _users.Add(user);
            }
        }
    }
}
