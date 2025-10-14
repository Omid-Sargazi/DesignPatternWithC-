using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndAlgorithem.BehavioralDesignPatterns
{
    public class User
    {
        private readonly IMediator _mediator;
        public string Name { get; set; }
        public User(IMediator mediator, string name)
        {
            _mediator = mediator;
            Name = name;
            _mediator.Register(this);
        }

        public void SendMessage(string msg)
        {
            _mediator.SendMessage(msg,this);
        }

        public void ReceiveMessage(string msg, User sender)
        {
            Console.WriteLine($"This Message:{msg} Received from {sender} by {Name} ");
        }
    }
    public interface IMediator
    {
        void Register(User user);
        void SendMessage(string meg, User sender);
    }
    public class Mediator : IMediator
    {
        private readonly List<User> _users;

        public Mediator()
        {
            _users = new List<User>();
           
        }
        public string Name { get; set; }
        public void Register(User user)
        {
            if (!_users.Contains(user))
            {
                _users.Add(user);
            }
        }

        public void SendMessage(string msg, User sender)
        {
            foreach (var user in _users)
            {
                if (sender != user)
                {
                    user.ReceiveMessage(msg,sender);
                }
            }
        }
    }
}
