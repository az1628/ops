using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClientManager.App.Helpers;
using ClientManager.Business.Services;
using ClientManager.Data.Entities;
using Microsoft.Extensions.DependencyInjection;


namespace ClientManager.App.Views
{
    public partial class ClientListPage : Page
    {
        private readonly ClientService _clientService;

        public ClientListPage(ClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService;
        }


        private void ClientListPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadClients();
        }

        private void LoadClients()
        {
            try
            {
                var clients = _clientService.GetAllClients();
                dgClients.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading clients: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var term = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(term))
                {
                    LoadClients();
                    return;
                }
                var results = _clientService.SearchClients(term);
                dgClients.ItemsSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            LoadClients();
        }

        private void DgClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgClients.SelectedItem is Client selected)
            {
                var page = App.Services.GetRequiredService<ClientDetailPage>();
                page.Load(selected.ClientId);
                NavigationHelper.NavigateTo(page);
            }
        }

        private void BtnNewClient_Click(object sender, RoutedEventArgs e)
        {
            var page = App.Services.GetRequiredService<ClientDetailPage>();
            page.Load(0);
            NavigationHelper.NavigateTo(page);
        }
    }
}
