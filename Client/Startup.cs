using System;
using System.Windows;
using Chat.Views;

namespace Chat
{
    public static class Startup
    {
        [STAThread]
        public static void Main()
        {
            var viewmodel = new MainWindowVm();
            var view = new MainWindow { DataContext = viewmodel };
            var app = new Application();
            app.Run(view);
        }
    }
}