using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace MyMessanger
{
    public partial class ChatPage : Page
    {
        private string currentUser;
        private ObservableCollection<string> messages;
        private string connectionString = "Your_Connection_String"; // Замените на ваше подключение
        private ObservableCollection<User> users = new ObservableCollection<User>();

        public ChatPage(string username)
        {
            InitializeComponent();
            currentUser = username;
            MessageListView.ItemsSource = messages;

            // Пример добавления пользователей в коллекцию
            users.Add(new User { Username = "Максим" });
            users.Add(new User { Username = "Анастасия" });

            UserListView.ItemsSource = users;
        }

        private void LoadMessages()
        {
            messages.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Sender, MessageText FROM Messages ORDER BY MessageTime";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string sender = reader["Sender"].ToString();
                        string messageText = reader["MessageText"].ToString();
                        string formattedMessage = $"{sender}: {messageText}";
                        messages.Add(formattedMessage);
                    }
                }
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string messageText = MessageTextBox.Text;

            if (!string.IsNullOrWhiteSpace(messageText))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Messages (Sender, MessageText, MessageTime) VALUES (@Sender, @MessageText, @MessageTime)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Sender", currentUser);
                        command.Parameters.AddWithValue("@MessageText", messageText);
                        command.Parameters.AddWithValue("@MessageTime", DateTime.Now);
                        command.ExecuteNonQuery();
                    }
                }

                LoadMessages(); // Обновить список сообщений после отправки
                MessageTextBox.Clear();
            }
        }
    }
}
