namespace APIProjects.MediatorPattern
{
    public class User
    {
        public string Name { get; set; }
        private IChatRoom _chatRoom;

        public User(string name, IChatRoom chatRoom)
        {
            _chatRoom = chatRoom;
            Name = name;
            _chatRoom.Register(this);
        }

        public void SendMessage(string message)
        {
            _chatRoom.SendMessage(message,this);
        }

        public void ReceiveMessage(string message, User sender)
        {
            Console.WriteLine($"Message:{message} receive by {Name} and sender is {sender} ");
        }
    }
    public interface IChatRoom
    {
        void Register(User user);
        void SendMessage(string msg, User sender);
    }

    public class ChatRoom:IChatRoom
    {
        private readonly List<User> _users= new List<User>();
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
                if (user != sender)
                {
                    user.ReceiveMessage(msg,sender);
                }
            }
        }
    }

    public interface IRequest<TResult>{}

    public interface IRequestHandler<TResult, TRequest>
    {
        TResult Handle(TRequest request);
    }
}
