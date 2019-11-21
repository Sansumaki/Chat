using System.Threading.Tasks;
using System.Windows.Input;
using Chat.Utilities;

namespace Chat.Views
{
    public class LoginViewVm : ObservableObject
    {
        private readonly ChatService _chatService;
        private string _clientName = "Test";
        private string _serverUri = "http://localhost:57900/";

        public LoginViewVm(ChatService chatService)
        {
            _chatService = chatService;

            ConnectCommand = new RelayCommand(Connect, o => !_chatService.IsConnected);
        }

        public string ServerUri
        {
            get => _serverUri;
            set
            {
                _serverUri = value;
                OnPropertyChanged(nameof(ServerUri));
            }
        }

        public string ClientName
        {
            get => _clientName;
            set
            {
                _clientName = value;
                OnPropertyChanged(nameof(ClientName));
            }
        }

        public ICommand ConnectCommand { get; }

        private void Connect(object obj)
        {
            Task.Run(() => _chatService.Connect(ServerUri, ClientName));
        }
    }
}