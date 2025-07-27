using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.ObserverPattern
{
    public interface IChatRoom
    {
        void Register();
        void SendMessage(string message);
    }

    public class ChatRoom : IChatRoom
    {
        public void Register()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string message)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BaseUser
    {
        protected IChatRoom _chatRoom;
        protected BaseUser(IChatRoom chatRoom)
        {
            _chatRoom = chatRoom;
        }
    }
}
