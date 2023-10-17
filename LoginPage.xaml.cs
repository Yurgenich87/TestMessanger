using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;




namespace MyMessanger
{


    public partial class LoginPage : Page
    {
        // Определите события для переключения на другие окна
        public event EventHandler SwitchToChat;
        public event EventHandler SwitchToRegistration;
        private ChatPage chatPage;

        public LoginPage()
        {
            InitializeComponent();
            chatPage = null; // Изначально страница чата не установлена
        }
        public bool Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage.Text = "Введите имя пользователя и пароль.";
                ErrorMessage.Visibility = Visibility.Visible;
                return false; // Возвращаем false, так как вход не выполнен
            }

            // Создаем подключение к базе данных
            string connectionString = ConfigurationManager.ConnectionStrings["Users"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL-запрос для проверки наличия пользователя в базе
                string query = "SELECT PasswordHash FROM Users WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    string storedPasswordHash = (string)command.ExecuteScalar();

                    if (string.IsNullOrEmpty(storedPasswordHash))
                    {
                        // Пользователь не существует
                        return false; // Возвращаем false, так как вход не выполнен
                    }
                    else if (storedPasswordHash == password)
                    {
                        // Пользователь успешно вошел, перейдем на страницу чата
                        chatPage = new ChatPage(username); // Передача имени пользователя
                        return true; // Возвращаем true, чтобы показать успешный вход
                    }
                    else
                    {
                        // Неверный пароль
                        ErrorMessage.Text = "Неверное имя пользователя или пароль.";
                        ErrorMessage.Visibility = Visibility.Visible;
                        return false; // Возвращаем false, так как вход не выполнен
                    }
                }
            }
        }



        private void SwitchToHomePage(object sender, RoutedEventArgs e)
        {
            // Переключение на главную страницу (Home)
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.SwitchToHome();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            bool loggedIn = Login(username, password);

            if (loggedIn)
            {
                // Вход выполнен успешно, переход на страницу чата
                NavigateToChatPage();
            }
            else
            {
                // Вход не выполнен, показываем сообщение об ошибке
                ErrorMessage.Text = "Неверное имя пользователя или пароль.";
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }

        // Метод для перехода на страницу чата
        public void NavigateToChatPage()
        {
            if (chatPage != null)
            {
                NavigationService.Navigate(chatPage);
            }
        }
    }
}