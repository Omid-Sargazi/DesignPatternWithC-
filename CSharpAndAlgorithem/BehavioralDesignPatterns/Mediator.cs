using System;
using System.Collections.Generic;
using System.Data;
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

    public interface IRequest<TResult>{}

    public interface IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        Task<TResult> Handle(TRequest request, CancellationToken  cancellationToken);
    }

    public interface ICommand : IRequest<bool>{}



    public interface IQuery<TResult>:IRequest<TResult>{}

    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query, CancellationToken  cancellationToken);
    }

    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<bool> Handle(TCommand command, CancellationToken  cancellationToken);
    }

    public class CreateUserCommand : ICommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class GetUserQuery : IQuery<bool>
    {
        public int UserId { get; set; }
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        public async Task<bool> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Creating user: {command.Name}, {command.Email}");
            await Task.Delay(100);
            return true;
        }
    }

    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, bool>
    {
        public async Task<bool> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Getting User with ID {query.UserId}");
            await Task.Delay(100);
            return true;
        }
    }
}
