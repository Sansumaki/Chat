using System;
using System.Threading.Tasks;
using Chat.Utilities;
using ChatLib;
using Microsoft.AspNet.SignalR.Client;

namespace Chat
{
    public class ChatService
    {
        private HubConnection _connection;
        private bool _isConnecting;
        private IHubProxy _proxy;

        public bool IsConnected { get; private set; }
        public string Username { get; private set; }
        public string ServerUri { get; private set; }

        public async Task Connect(string serverUri, string username)
        {
            if (_isConnecting || IsConnected) return;
            try
            {
                Username = username;
                ServerUri = serverUri;
                _isConnecting = true;
                _connection = new HubConnection(serverUri);

                _proxy = _connection.CreateHubProxy("ChatHub");
                _proxy.On<MessageObject>(nameof(IServerHub.AddNewMessage), AddNewMessage);

                await _connection.Start();

                IsConnected = true;
                _isConnecting = false;
            }
            catch (Exception)
            {
                IsConnected = false;
                _isConnecting = false;
            }

            ConnectedChanged.Invoke(this, new EventArgs<bool>(IsConnected));
        }

        public event EventHandler<EventArgs<bool>> ConnectedChanged = delegate { };
        public event EventHandler<EventArgs<MessageObject>> NewMessageReceived = delegate { };

        public void Disconnect()
        {
            _connection.Stop();
            IsConnected = false;
            ConnectedChanged.Invoke(this, new EventArgs<bool>(IsConnected));
        }

        private void AddNewMessage(MessageObject messageObject)
        {
            NewMessageReceived.Invoke(this, new EventArgs<MessageObject>(messageObject));
        }

        public void Send(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            _proxy.Invoke(nameof(IClientHub.Send),
                new MessageObject {Username = Username, Message = message.Replace("\r\n", "\\\r\n")});
        }
    }
}