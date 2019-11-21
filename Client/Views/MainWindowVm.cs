using Chat.Utilities;

namespace Chat.Views
{
    public class MainWindowVm : ObservableObject
    {
        private readonly ChatService _chatService;
        private ObservableObject _currentView;

        public MainWindowVm()
        {
            _chatService = new ChatService();
            _chatService.ConnectedChanged += OnConnectedChanged;

            CurrentView = new LoginViewVm(_chatService);
        }

        public ObservableObject CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private void OnConnectedChanged(object sender, EventArgs<bool> e)
        {
            if (e.Value)
                CurrentView = new ChatViewVm(_chatService);
            else
                CurrentView = new LoginViewVm(_chatService);
        }
    }
}