using System;
using System.Collections.Generic;
using ClientManager.Business.Helpers;
using ClientManager.Data.Entities;
using ClientManager.Data.Repositories;

namespace ClientManager.Business.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepo;
        private readonly IAccountRepository _accountRepo;
        public ClientService(IClientRepository clientRepository, IAccountRepository accountRepository)
        {
             _clientRepo = clientRepository;
            _accountRepo = accountRepository;

        }       

        public List<Client> GetAllClients()
        {
            return _clientRepo.GetAllClients();
        }

        public Client GetClientDetails(int clientId)
        {
            return _clientRepo.GetClientById(clientId);
        }

        public List<Client> SearchClients(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return GetAllClients();

            return _clientRepo.SearchClients(ValidationHelper.SanitizeInput(term));
        }

        public bool SaveClient(Client client, out string errorMessage)
        {
            errorMessage = null;

            if (string.IsNullOrWhiteSpace(client.FirstName))
            {
                errorMessage = "First name is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(client.LastName))
            {
                errorMessage = "Last name is required.";
                return false;
            }
            if (!string.IsNullOrEmpty(client.Email) && !ValidationHelper.IsValidEmail(client.Email))
            {
                errorMessage = "Invalid email address.";
                return false;
            }

            try
            {
                if (client.ClientId == 0)
                {
                    client.DateOnboarded = DateTime.Now;
                    if (string.IsNullOrEmpty(client.Status))
                        client.Status = "Prospect";
                }

                _clientRepo.SaveClient(client);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Error saving client: " + ex.Message;
                return false;
            }
        }

        public void DeleteClient(int clientId)
        {
            _clientRepo.DeleteClient(clientId);
        }

        public List<Account> GetClientAccounts(int clientId)
        {
            return _accountRepo.GetAccountsByClient(clientId);
        }
    }
}
