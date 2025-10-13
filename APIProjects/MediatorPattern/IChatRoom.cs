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
        public async Task<TResult> Send<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();

            if (!_handlerMappings.TryGetValue(requestType, out var handlerType))
            {
                throw new Exception($"No handler registered for request type {requestType}");
            }

            var handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new Exception($"Handler for type {handlerType} not found in DI container");
            }
            var handleMethod = handlerType.GetMethod("Handle");
            if (handleMethod != null)
            {
                throw new Exception($"Handle method not found on {handlerType}");
            }

            var result = handleMethod.Invoke(handler, new object?[] { request, cancellationToken });
            return await (Task<TResult>)result;

        }
    }

    public record GetUserQuery(int UserId) : IRequest<UserDto>;

    public record UserDto(string Name, string Email);

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return new UserDto("John", "O@o.com");
        }
    }
}
