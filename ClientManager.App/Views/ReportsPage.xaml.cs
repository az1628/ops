using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClientManager.Business.Helpers;
using ClientManager.Business.Services;

namespace ClientManager.App.Views
{
    public partial class ReportsPage : Page
    {
        private readonly ReportService _reportService;

        public ReportsPage(ReportService reportService)
        {
            _reportService = reportService;
            InitializeComponent();
        }

        private void BtnRunReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var filter = (cmbStatusFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
                var data = _reportService.GetClientSummary(filter);
                dgReport.ItemsSource = data;

                // Legacy: compute totals manually in code-behind
                if (data.Any())
                {
                    var totalBalance = data.Sum(d => d.TotalBalance);
                    var totalDeposits = data.Sum(d => d.TotalDeposits);
                    var totalWithdrawals = data.Sum(d => d.TotalWithdrawals);
                    var totalTxn = data.Sum(d => d.TransactionCount);

                    txtTotalSummary.Text = string.Format(
                        "Totals  —  Balance: {0}  |  Deposits: {1}  |  Withdrawals: {2}  |  Transactions: {3}",
                        FormatHelper.FormatCurrency(totalBalance),
                        FormatHelper.FormatCurrency(totalDeposits),
                        FormatHelper.FormatCurrency(totalWithdrawals),
                        totalTxn);
                }
                else
                {
                    txtTotalSummary.Text = "No data found for the selected filter.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error running report: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
