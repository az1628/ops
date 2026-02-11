using System.Windows;
using ClientManager.App.Helpers;

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
            NavigationHelper.NavigateTo(new LoginPage(this));
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
            NavigationHelper.NavigateTo(new DashboardPage());
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo(new ClientListPage());
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo(new ReportsPage());
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            SessionHelper.Logout();
            HideNavigation();
            NavigationHelper.NavigateTo(new LoginPage(this));
        }
    }
}
