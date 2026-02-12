using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClientManager.App.Helpers;
using ClientManager.Business.Services;
using ClientManager.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ClientManager.App.Views
{
    public partial class ClientDetailPage : Page
    {
        private readonly ClientService _clientService;
        private int _clientId;
        private Client _currentClient;

        public ClientDetailPage(ClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService;
        }

        public void Load(int clientId)
        {
            _clientId = clientId;
        }

        private void ClientDetailPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_clientId > 0)
            {
                LoadClient();
            }
            else
            {
                txtPageTitle.Text = "New Client";
                btnDelete.Visibility = Visibility.Collapsed;
                accountsPanel.Visibility = Visibility.Collapsed;
                cmbStatus.SelectedIndex = 2;
                cmbRisk.SelectedIndex = 1;
            }
        }

        private void LoadClient()
        {
            try
            {
                _currentClient = _clientService.GetClientDetails(_clientId);
                if (_currentClient == null)
                {
                    MessageBox.Show("Client not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    NavigationHelper.GoBack();
                    return;
                }

                txtPageTitle.Text = "Edit: " + _currentClient.FullName;
                txtFirstName.Text = _currentClient.FirstName;
                txtLastName.Text = _currentClient.LastName;
                txtEmail.Text = _currentClient.Email;
                txtPhone.Text = _currentClient.Phone;
                txtAddress.Text = _currentClient.Address;
                txtNotes.Text = _currentClient.Notes;

                SelectComboItem(cmbStatus, _currentClient.Status);
                SelectComboItem(cmbRisk, _currentClient.RiskProfile);

                var accounts = _clientService.GetClientAccounts(_clientId);
                dgAccounts.ItemsSource = accounts;
                accountsPanel.Visibility = accounts.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading client: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectComboItem(ComboBox combo, string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            foreach (ComboBoxItem item in combo.Items)
            {
                if (item.Content.ToString() == value)
                {
                    combo.SelectedItem = item;
                    return;
                }
            }
        }

        private string GetComboValue(ComboBox combo)
        {
            return (combo.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";

            var client = _currentClient ?? new Client();
            client.FirstName = txtFirstName.Text.Trim();
            client.LastName = txtLastName.Text.Trim();
            client.Email = txtEmail.Text.Trim();
            client.Phone = txtPhone.Text.Trim();
            client.Address = txtAddress.Text.Trim();
            client.Status = GetComboValue(cmbStatus);
            client.RiskProfile = GetComboValue(cmbRisk);
            client.Notes = txtNotes.Text.Trim();

            string errorMessage;
            if (_clientService.SaveClient(client, out errorMessage))
            {
                MessageBox.Show("Client saved successfully.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                var page = App.Services.GetRequiredService<ClientListPage>();
                NavigationHelper.NavigateTo(page);
            }
            else
            {
                txtError.Text = errorMessage;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_clientId == 0) return;

            var result = MessageBox.Show(
                "Are you sure you want to delete this client? This cannot be undone.",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _clientService.DeleteClient(_clientId);
                    MessageBox.Show("Client deleted.", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                    var page = App.Services.GetRequiredService<ClientListPage>();
                    NavigationHelper.NavigateTo(page);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting client: " + ex.Message, "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var page = App.Services.GetRequiredService<ClientListPage>();
            NavigationHelper.NavigateTo(page);
        }
    }
}