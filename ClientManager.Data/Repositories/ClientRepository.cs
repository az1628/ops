using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ClientManager.Data.Context;
using ClientManager.Data.Entities;

namespace ClientManager.Data.Repositories
{
    public class ClientRepository
    {
        public List<Client> GetAllClients()
        {
            using (var db = new ClientManagerDbContext())
            {
                return db.Clients
                    .Include(c => c.Accounts)
                    .OrderBy(c => c.LastName)
                    .ToList();
            }
        }

        public Client GetClientById(int clientId)
        {
            using (var db = new ClientManagerDbContext())
            {
                return db.Clients
                    .Include(c => c.Accounts)
                    .Include(c => c.ClientNotes)
                    .FirstOrDefault(c => c.ClientId == clientId);
            }
        }

        public void SaveClient(Client client)
        {
            using (var db = new ClientManagerDbContext())
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
        }

        public void DeleteClient(int clientId)
        {
            using (var db = new ClientManagerDbContext())
            {
                var client = db.Clients.Find(clientId);
                if (client != null)
                {
                    db.Clients.Remove(client);
                    db.SaveChanges();
                }
            }
        }

        public List<Client> SearchClients(string searchTerm)
        {
            using (var db = new ClientManagerDbContext())
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
}
