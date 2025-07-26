namespace DesignPattern.MediatorPattern
{
    public interface IChatRoom1
    {
        void RegisterUser(IUser user);
        void SendGroupMessage(string message, IUser sender);
        void SendMessage(string message, IUser sender, IUser recipient);
    }
}