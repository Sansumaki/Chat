using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Chat.Utilities;
using ChatLib;

namespace Chat.Views
{
    public class ChatViewVm : ObservableObject
    {
        private readonly ChatService _chatService;
        private string _message = string.Empty;

        public ChatViewVm(ChatService chatService)
        {
            ClientName = chatService.Username;
            ServerUri = chatService.ServerUri;
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

        public string ClientName { get; }
        public string ServerUri { get; }
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
    }
}