using Chat.Views;
using System;
using System.Windows;

namespace Chat
{
    class Startup
    {
        [STAThread]
        static void Main()
        {
            var viewmodel = new MainWindowVm();
            var view = new MainWindow();
            view.DataContext = viewmodel;
            var app = new Application();
            app.Run(view);
        }

    }
}
