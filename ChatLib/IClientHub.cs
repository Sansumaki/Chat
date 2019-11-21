namespace ChatLib
{
    public interface IClientHub
    {
        void Send(MessageObject message);
        bool ConnectUser(string username);
    }
}