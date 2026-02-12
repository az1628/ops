using ClientManager.App.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace ClientManager.App.Views
{
    public partial class LoginPage : Page
    {
        private readonly MainWindow _mainWindow;
        private int _loginAttempts = 0;

        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        // Legacy: all business logic crammed into click handler
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Password;

            // Hardcoded credentials — legacy realism
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                txtError.Text = "Please enter both username and password.";
                return;
            }

            // Read max attempts from config — legacy ConfigurationManager usage
            var maxAttempts = int.Parse(ConfigurationManager.AppSettings["MaxLoginAttempts"] ?? "3");

            if (_loginAttempts >= maxAttempts)
            {
                txtError.Text = "Too many failed attempts. Please restart the application.";
                return;
            }

            if (username == "admin" && password == "password123")
            {
                SessionHelper.Login(username);
                _mainWindow.ShowNavigation(username);
                var page = App.Services.GetRequiredService<DashboardPage>();
                NavigationHelper.NavigateTo(page);
            }
            else
            {
                _loginAttempts++;
                txtError.Text = $"Invalid credentials. Attempt {_loginAttempts} of {maxAttempts}.";
                txtPassword.Clear();
            }
        }
    }
}
