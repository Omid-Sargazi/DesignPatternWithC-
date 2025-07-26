using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.MediatorPattern
{
    public interface IChatRoomOfficee
    {
        void RegisterEmployee(IEmployee employee);
        void SendPrivateMessage(string message,IEmployee sender,IEmployee recipient);
        void SendGroupMessage(string message,IEmployee sender, List<IEmployee> recipients);
    }

    public interface IEmployee
    {
        string Name { get; }
        string Department { get; }
        void ReceiveMessage(string message, IEmployee sender);
        void SendProvateMessage(string message, IEmployee recipient);
        void SendGroupMessage(string message, List<IEmployee> recipients);
    }

    public class OfficeChatRoom : IChatRoomOfficee
    {
        private readonly List<IEmployee> _employees = new List<IEmployee>();
        public void RegisterEmployee(IEmployee employee)
        {
            _employees.Add(employee);   
        }

        public void SendGroupMessage(string message, IEmployee sender, List<IEmployee> recipients)
        {
            throw new NotImplementedException();
        }

        public void SendPrivateMessage(string message, IEmployee sender, IEmployee recipient)
        {
           if(_employees.Contains(recipient))
            {
                recipient.ReceiveMessage(message, sender);
            }
           else
                Console.WriteLine($"[خطا] کارمند {recipient.Name} در سیستم وجود ندارد!");
        }
    }
}
