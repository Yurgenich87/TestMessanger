using Microsoft.Data.SqlClient;
using System.Text;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;

namespace MyMessanger
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(RegistrationPage));

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = NewUsernameTextBox.Text;
            string password = NewPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите имя пользователя и пароль.");
                return;
            }

            // Здесь вызовите метод для регистрации пользователя в базе данных
            AddUserToDatabase(username, password);

            // После успешной регистрации переключитесь на окно входа (LoginPage)
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.SwitchToHome();
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
        public void AddUserToDatabase(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Users"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL-запрос для вставки нового пользователя
                string query = "INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Хешируйте пароль, прежде чем сохранять его в базе данных
                    string hashedPassword = HashPassword(password);

                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    // Выполните запрос
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Пользователь успешно добавлен
                        MessageBox.Show("Пользователь успешно зарегистрирован.");
                    }
                    else
                    {
                        // Возникла ошибка при добавлении пользователя
                        MessageBox.Show("Ошибка регистрации пользователя.");
                    }
                }
            }
        }

        private string HashPassword(string password)
        {
            
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

    }
}