using Chat.Utilities;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Chat.Views
{
    public class MainWindowVm : ObservableObject
    {
        private bool _canConnect = true;
        private string _serverUri = "http://localhost:57900/";
        private bool _isConnected = false;
        private string _clientName = "Test";
        private string _message = string.Empty;

        private HubConnection _connection;
        private IHubProxy _proxy;
        private UserControl _currentView = new LoginView();

        public MainWindowVm()
        {
            ConnectCommand = new RelayCommand(Connect, (o) => CanConnect);
            DisconnectCommand = new RelayCommand(Disconnect, (o) => IsConnected);
            SendCommand = new RelayCommand(Send, (o) => IsConnected);
        }

        private void Send(object obj)
        {
            _proxy.Invoke("Send", ClientName, Message);
            Message = string.Empty;
        }

        private void Disconnect(object obj)
        {
            _connection.Stop();
            IsConnected = false;
            CanConnect = true;
            CurrentView = new LoginView();
        }

        private void Connect(object obj)
        {
            CanConnect = false;

            try
            {
                _connection = new HubConnection(ServerUri);

                _proxy = _connection.CreateHubProxy("ChatHub");
                _proxy.On<string, string>("AddNewMessage", AddNewMessage);

                Task.Run(() => _connection.Start());

                CurrentView = new Chat();
                IsConnected = true;
            }
            catch (Exception)
            {
                IsConnected = false;
                CanConnect = true;
            }
        }

        private void AddNewMessage(string name, string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add($"From {name}: {message}");
            });
        }
        
        public UserControl CurrentView
        {
            get => _currentView; set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public string ServerUri
        {
            get => _serverUri; set
            {
                _serverUri = value;
                OnPropertyChanged(nameof(ServerUri));
            }
        }
        public string ClientName
        {
            get => _clientName; set
            {
                _clientName = value;
                OnPropertyChanged(nameof(ClientName));
            }
        }
        public bool CanConnect
        {
            get => _canConnect; set
            {
                _canConnect = value;
                OnPropertyChanged(nameof(CanConnect));
            }
        }
        public bool IsConnected
        {
            get => _isConnected; set
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }
        public string Message
        {
            get => _message; set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();

        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }
        public ICommand SendCommand { get; }
    }
}