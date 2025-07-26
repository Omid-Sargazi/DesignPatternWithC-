using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.MediatorPattern
{
    public interface IUser
    {
        string Name { get; set; }
        void SendGroupMessage(string message);
        void ReceiveMessage(string message, IUser user);
        void SendMessage(string message, IUser recipient);
    }

    public interface IChatRoom
    {
        void RegisterUser(IUser user);
        void SendMessage(string message, IUser sender, IUser recipient);
        void SendGroupMessage(string message, IUser sender);
    }

    public class ChatRoom : IChatRoom
    {
        private readonly List<IUser> _users = new List<IUser>();
        public void RegisterUser(IUser user)
        {
            _users.Add(user);
        }

        public void SendGroupMessage(string message, IUser sender)
        {
           foreach (var user in _users)
            {
                sender.ReceiveMessage(message, user);
            }
        }

        public void SendMessage(string message, IUser sender, IUser recipient)
        {
            sender.ReceiveMessage(message, recipient);
        }
    }


    public class User : IUser
    {
        private readonly IChatRoom _chatRoom;
        public User(IChatRoom chatRoom, string name)
        {
            _chatRoom = chatRoom;
            Name = name;
        }

        public string Name { get; set; }

        public void ReceiveMessage(string message, IUser user)
        {
            Console.WriteLine($"{user.Name} to {Name}: {message}");
        }

        public void SendGroupMessage(string message)
        {
            _chatRoom.SendGroupMessage(message,this);
        }

        public void SendMessage(string message, IUser recipient)
        {
            _chatRoom.SendMessage(message, this, recipient);
        }
    }

    public class ClientMediator
    {
        public static void Run()
        {
            IChatRoom chatRoom = new ChatRoom();
            IUser user1 = new User(chatRoom, "Omid");
            IUser user2 = new User(chatRoom, "saeed");
            IUser user3 = new User(chatRoom, "vahid");
            IUser user4 = new User(chatRoom, "saleh");

            chatRoom.RegisterUser(user1);
            chatRoom.RegisterUser(user2);
            chatRoom.RegisterUser(user3);
            chatRoom.RegisterUser(user4);
            user1.SendGroupMessage("Hello evryone");
            user2.SendMessage("Hi Omid", user1);
        }
    }
}
