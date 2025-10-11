using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BehavioralDesignPattern
{
    public class User
    {
        public string Name { get; }
        private IChatMediator _chatMediator;

        public User(string name, IChatMediator chatMediator)
        {
            _chatMediator = chatMediator;
            Name = name;
            _chatMediator.Register(this);
        }

        public void SendMessage(string message)
        {
            Console.WriteLine($"{Name} sends:{message}");
        }

        public void ReceiveMessage(string message, User sender)
        {
            Console.WriteLine($"{Name} received from {sender.Name}:{message}");
        }
    }
    public interface IChatMediator
    {
        void SendMessage(string message, User user);
        void Register(User user);
    }
    public class ChatMediator : IChatMediator
    {
        private List<User> _users = new();
        public void SendMessage(string message, User sender)
        {
            foreach (var user  in _users)
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
                Console.WriteLine($"User {user.Name} registered in chat");
            }
        }
    }

    public class ClientMediator
    {
        public static void Run()
        {
            IChatMediator mediator = new ChatMediator();

            var user1 = new User("Alice", mediator);
            var user2 = new User("Bob", mediator);
            var user3 = new User("Charlie", mediator);

            user1.SendMessage("Hello there");
            user2.SendMessage("Hi Alice");
        }
    }
}
