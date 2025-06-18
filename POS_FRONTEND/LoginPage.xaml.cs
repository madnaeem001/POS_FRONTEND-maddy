using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace POS_FRONTEND
{
    public partial class LoginPage : Window
    {
        private string connectionString = "Data Source=MY-THINKPAD;Initial Catalog=AL_Sheikh_Database;Integrated Security=True;Trust Server Certificate=True";

        public LoginPage()
        {
            InitializeComponent();
        }

        // Event handler for the Login button click
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Validate input fields
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrorMessage.Text = "Please enter both username and password.";
                ErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            // Validate user credentials from the database
            if (ValidateUserFromDatabase(username, password))
            {
                ErrorMessage.Visibility = Visibility.Collapsed;

                // Navigate to dashboard
                MyDashboard dashboard = new MyDashboard();

                if (this.WindowState == WindowState.Maximized)
                {
                    dashboard.WindowState = WindowState.Maximized;
                }
                else
                {
                    dashboard.Width = this.Width;
                    dashboard.Height = this.Height;
                    dashboard.Left = this.Left;
                    dashboard.Top = this.Top;
                }

                dashboard.Show();
                this.Hide();
            }
            else
            {
                ErrorMessage.Text = "Invalid username or password.";
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }

        // Validate user credentials against the database
        private bool ValidateUserFromDatabase(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Use parameterized queries to prevent SQL Injection
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        // Check if user exists
                        int userCount = (int)command.ExecuteScalar();
                        return userCount > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}");
                    return false;
                }
            }
        }

        // Optional: Handle the Enter key for login
        private void LoginPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }
    }
}
