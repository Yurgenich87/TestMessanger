using System;
using System.Windows;
using System.Windows.Controls;

namespace MyMessanger
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private void SwitchToLoginPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = null; // Очистите содержимое Frame
            MainFrame.Content = new LoginPage(); // Загрузите новую страницу (LoginPage)

            // Скрываем кнопки "Вход" и "Регистрация"
            EntryButton.Visibility = Visibility.Collapsed;
            RegistrationButton.Visibility = Visibility.Collapsed;
            Border.Visibility = Visibility.Collapsed;

        }

        public void SwitchToRegistrationPage()
        {
            MainFrame.Content = null; // Очистите содержимое Frame
            MainFrame.Content = new RegistrationPage(); // Загрузите новую страницу (RegistrationPage)

            // Скройте кнопки "Вход" и "Регистрация"
            EntryButton.Visibility = Visibility.Collapsed;
            RegistrationButton.Visibility = Visibility.Collapsed;
            Border.Visibility = Visibility.Collapsed;
        }

        private void SwitchToRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            // Вызовите метод SwitchToRegistrationPage для перехода на страницу регистрации
            SwitchToRegistrationPage();
        }

        public void SwitchToHome()
        {
            MainFrame.Content = null; // Очищаем содержимое
            EntryButton.Visibility = Visibility.Visible; // Показываем кнопку "Вход"
            RegistrationButton.Visibility = Visibility.Visible; // Показываем кнопку "Регистрация"
            // Здесь можно добавить другие элементы главной страницы, если необходимо
        }




    }

}


