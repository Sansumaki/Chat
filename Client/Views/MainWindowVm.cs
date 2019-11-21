using Chat.Utilities;

namespace Chat.Views
{
    public class MainWindowVm : ObservableObject
    {
        private readonly ChatService _chatService;
        private IActivatableView _currentView;
        private ChatViewVm _chatView;
        private LoginViewVm _loginView;

        public MainWindowVm()
        {
            _chatService = new ChatService();
            _chatService.ConnectedChanged += OnConnectedChanged;


            _chatView = new ChatViewVm(_chatService);
            _loginView = new LoginViewVm(_chatService);

            CurrentView = _loginView;
        }

        public IActivatableView CurrentView
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
            CurrentView.Deactivate();
            if (e.Value)
                CurrentView = _chatView;
            else
                CurrentView = _loginView;
            CurrentView.Activate();
        }
    }
}