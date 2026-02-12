using System.Windows;
using ClientManager.App.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace ClientManager.App.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            NavigationHelper.MainFrame = MainFrame;
            var page = App.Services.GetRequiredService<LoginPage>();
            NavigationHelper.NavigateTo(page);
        }

        public void ShowNavigation(string username)
        {
            navPanel.Visibility = Visibility.Visible;
            btnLogout.Visibility = Visibility.Visible;
            txtCurrentUser.Text = "Logged in as: " + username;
            txtStatus.Text = "Connected to database";
        }

        public void HideNavigation()
        {
            navPanel.Visibility = Visibility.Collapsed;
            btnLogout.Visibility = Visibility.Collapsed;
            txtCurrentUser.Text = "";
            txtStatus.Text = "Ready";
        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            var page = App.Services.GetRequiredService<DashboardPage>();
            NavigationHelper.NavigateTo(page);
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            var page = App.Services.GetRequiredService<ClientListPage>();
            NavigationHelper.NavigateTo(page);
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            var page = App.Services.GetRequiredService<ReportsPage>();
            NavigationHelper.NavigateTo(page);
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            SessionHelper.Logout();
            HideNavigation();
            var page = App.Services.GetRequiredService<LoginPage>();
            NavigationHelper.NavigateTo(page);
        }
    }
}