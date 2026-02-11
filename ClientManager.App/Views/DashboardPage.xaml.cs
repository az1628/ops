using System;
using System.Windows;
using System.Windows.Controls;
using ClientManager.App.Helpers;
using ClientManager.Business.Helpers;
using ClientManager.Business.Services;

namespace ClientManager.App.Views
{
    public partial class DashboardPage : Page
    {
        // Direct instantiation — no DI
        private readonly ReportService _reportService = new ReportService();

        public DashboardPage()
        {
            InitializeComponent();
        }

        private void DashboardPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var stats = _reportService.GetDashboardStats();

                txtTotalClients.Text = stats.TotalClients.ToString();
                txtActiveClients.Text = stats.ActiveClients.ToString();
                txtProspects.Text = stats.ProspectCount.ToString();
                txtTotalAUM.Text = FormatHelper.FormatCurrency(stats.TotalAUM);
                txtTotalAccounts.Text = stats.TotalAccounts.ToString();
                txtRecentTxn.Text = stats.RecentTransactionCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnViewClients_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo(new ClientListPage());
        }

        private void BtnRunReports_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo(new ReportsPage());
        }

        private void BtnNewClient_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo(new ClientDetailPage(0));
        }
    }
}
