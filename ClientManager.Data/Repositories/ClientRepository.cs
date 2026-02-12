using System.Collections.Generic;
using System.Linq;
using ClientManager.Data.Context;
using ClientManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientManager.Data.Repositories
{
    public interface IClientRepository
    {
        List<Client> GetAllClients();
        Client GetClientById(int clientId);
        void SaveClient(Client client);
        void DeleteClient(int clientId);
        List<Client> SearchClients(string searchTerm);
    }

    public class ClientRepository: IClientRepository
    {
         private readonly ClientManagerDbContext db;
        public ClientRepository(ClientManagerDbContext context)
        {
            db = context;
        }
        public List<Client> GetAllClients()
        {

                return db.Clients
                    .Include(c => c.Accounts)
                    .OrderBy(c => c.LastName)
                    .ToList();
        
        }

        public Client GetClientById(int clientId)
        {

                return db.Clients
                    .Include(c => c.Accounts)
                    .Include(c => c.ClientNotes)
                    .FirstOrDefault(c => c.ClientId == clientId);
        }

        public void SaveClient(Client client)
        {

                if (client.ClientId == 0)
                {
                    db.Clients.Add(client);
                }
                else
                {
                    db.Entry(client).State = EntityState.Modified;
                }
                db.SaveChanges();
        }

        public void DeleteClient(int clientId)
        {
  
                var client = db.Clients.Find(clientId);
                if (client != null)
                {
                    db.Clients.Remove(client);
                    db.SaveChanges();
                }
        }

        public List<Client> SearchClients(string searchTerm)
        {
                return db.Clients
                    .Include(c => c.Accounts)
                    .Where(c => c.FirstName.Contains(searchTerm)
                             || c.LastName.Contains(searchTerm)
                             || c.Email.Contains(searchTerm))
                    .OrderBy(c => c.LastName)
                    .ToList();
        }
    }
}
