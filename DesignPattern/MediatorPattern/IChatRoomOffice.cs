using System;
using System.Collections.Generic;
using System.ComponentModel;
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
           foreach(var recipient in recipients)
            {
                if (_employees.Contains(recipient))
                {
                    recipient.ReceiveMessage(message, sender);
                }
                else
                {
                    Console.WriteLine($"[خطا] کارمند {recipient.Name} در سیستم وجود ندارد!");
                }
            }
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

    public class Employee : IEmployee
    {
        public string Name { get; }

        public string Department {  get; }
        private IChatRoomOfficee _chatroom;

        public Employee(IChatRoomOfficee chatRoom, string name, string department)
        {
            _chatroom = chatRoom;
            Name = name;
            Department = department;
        }

        public void ReceiveMessage(string message, IEmployee sender)
        {
            Console.WriteLine($"{Name} (از {Department}): پیامی از {sender.Name} (در {sender.Department}) دریافت کرد:\n{message}\n");
        }

        public void SendGroupMessage(string message, List<IEmployee> recipients)
        {
            _chatroom.SendGroupMessage(message, this, recipients);
        }

        public void SendProvateMessage(string message, IEmployee recipient)
        {
            _chatroom.SendPrivateMessage(message, this, recipient);
        }
    }
}
