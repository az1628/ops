using System;
using System.Windows;
using System.Windows.Controls;
using ClientManager.App.Helpers;
using ClientManager.Business.Helpers;
using ClientManager.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClientManager.App.Views
{
    public partial class DashboardPage : Page
    {
        private readonly ReportService _reportService;

        public DashboardPage(ReportService reportService)
        {
            _reportService = reportService;
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
            var page = App.Services.GetRequiredService<ClientListPage>();
            NavigationHelper.NavigateTo(page);
        }

        private void BtnRunReports_Click(object sender, RoutedEventArgs e)
        {
            var page = App.Services.GetRequiredService<ReportsPage>();
            NavigationHelper.NavigateTo(page);
        }

        private void BtnNewClient_Click(object sender, RoutedEventArgs e)
        {
            var page = App.Services.GetRequiredService<ClientDetailPage>();
            page.Load(0);
            NavigationHelper.NavigateTo(page);
        }
    }
}