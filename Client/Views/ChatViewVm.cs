using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Chat.Utilities;
using ChatLib;

namespace Chat.Views
{
    public class ChatViewVm : ObservableObject, IActivatableView
    {
        private readonly ChatService _chatService;
        private string _message = string.Empty;
        private string _clientName;
        private string _serverUri;

        public ChatViewVm(ChatService chatService)
        {
            _chatService = chatService;
            _chatService.NewMessageReceived += OnNewMessageReceived;

            DisconnectCommand = new RelayCommand(Disconnect, o => _chatService.IsConnected);
            SendCommand = new RelayCommand(Send, o => _chatService.IsConnected);
        }


        public ObservableCollection<MessageObject> Messages { get; } = new ObservableCollection<MessageObject>();

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public string ClientName
        {
            get => _clientName;
            private set
            {
                _clientName = value;
                OnPropertyChanged(nameof(ClientName));
            }
        }
        public string ServerUri
        {
            get => _serverUri;
            private set
            {
                _serverUri = value;
                OnPropertyChanged(nameof(ServerUri));
            }
        }

        public ICommand DisconnectCommand { get; }
        public ICommand SendCommand { get; }

        private void Disconnect(object obj)
        {
            _chatService.Disconnect();
        }

        private void OnNewMessageReceived(object sender, EventArgs<MessageObject> e)
        {
            Application.Current.Dispatcher?.Invoke(() =>
            {
                Messages.Add(e.Value);
                OnMessagesChanged.Invoke(this, new EventArgs());
            });
        }

        private void Send(object obj)
        {
            _chatService.Send(Message);
            Message = string.Empty;
        }

        public event EventHandler OnMessagesChanged = delegate { };

        public void Activate()
        {
            ClientName = _chatService.Username;
            ServerUri = _chatService.ServerUri;
        }

        public void Deactivate()
        {
            Messages.Clear();
        }
    }
}