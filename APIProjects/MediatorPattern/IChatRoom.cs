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

    public interface IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        Task<TResult> Handle(TRequest request,CancellationToken  cancellationToken);
    }

    public interface IMediator
    {
        Task<TResult> Send<TResult>(IRequest<TResult> request, CancellationToken cancellationToken=default);
    }

    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, Type> _handlerMappings;

        public Mediator(IServiceProvider serviceProvider, Dictionary<Type,Type> handlerMappings)
        {
            _handlerMappings = handlerMappings;
            _serviceProvider = serviceProvider;
        }
        public Task<TResult> Send<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
