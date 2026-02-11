using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ClientManager.Data.Context;
using ClientManager.Data.Entities;

namespace ClientManager.Data.Repositories
{
    public class AccountRepository
    {
        public List<Account> GetAccountsByClient(int clientId)
        {
            using (var db = new ClientManagerDbContext())
            {
                return db.Accounts
                    .Include(a => a.Transactions)
                    .Where(a => a.ClientId == clientId)
                    .ToList();
            }
        }

        public Account GetAccountById(int accountId)
        {
            using (var db = new ClientManagerDbContext())
            {
                return db.Accounts
                    .Include(a => a.Transactions)
                    .Include(a => a.Client)
                    .FirstOrDefault(a => a.AccountId == accountId);
            }
        }

        public void SaveAccount(Account account)
        {
            using (var db = new ClientManagerDbContext())
            {
                if (account.AccountId == 0)
                    db.Accounts.Add(account);
                else
                    db.Entry(account).State = EntityState.Modified;

                db.SaveChanges();
            }
        }
    }
}
