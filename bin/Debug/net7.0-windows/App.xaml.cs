using System;
using System.Windows;

namespace MyMessanger
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Создайте экземпляр MainWindow и установите его как главное окно
            MainWindow mainWindow = new MainWindow();
            MainWindow = mainWindow;

            // Покажите MainWindow
            mainWindow.Show();
        }
    }
}