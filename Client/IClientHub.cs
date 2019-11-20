namespace Chat
{
    internal interface IClientHub
    {
        void Send(string name, string message);
    }
}