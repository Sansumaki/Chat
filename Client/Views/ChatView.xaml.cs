using Chat.Utilities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Chat.Views
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {
        private MainWindowVm _viewmodel;
        
        public ChatView()
        {
            InitializeComponent();

            // Subscribe for new Datacontext to subscribe for messages changed
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _viewmodel = e.NewValue as MainWindowVm;
            if (_viewmodel != null)
            {
                _viewmodel.OnMessagesChanged += OnMessagesChanged;
            }
        }

        private void OnMessagesChanged(object sender, EventArgs e)
        {
            // Scroll to bottom of the Messages
            //TODO: Disable with some logic or checkbox
            messagesView.ScrollIntoView();
        }

        private void MessageKeyDown(object sender, KeyEventArgs e)
        {
            // Enter to send Message
            if (e.Key == Key.Enter)
            {
                var vm = DataContext as MainWindowVm;
                if (vm != null)
                {
                    vm.SendCommand.Execute(sender);
                }
            }
        }

    }
}
