using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.MediatorPattern
{
    public interface IUser
    {
        string Name { get; set; }
        void SendGroupMessage(string message, IUser user);
        void ReceiveMessage(string message, IUser user);
    }

    public interface IChatRoom
    {
        void RegisterUser(IUser user);
        void SendMessage(string message, IUser sender, IUser recipient);
        void SendGroupMessage(string message, IUser sender);
    }

    public class ChatRoom : IChatRoom
    {
        
        public void RegisterUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public void SendGroupMessage(string message, IUser sender)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string message, IUser sender, IUser recipient)
        {
            throw new NotImplementedException();
        }
    }
}
