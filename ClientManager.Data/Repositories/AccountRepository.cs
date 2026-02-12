using System.Collections.Generic;
using System.Linq;
using ClientManager.Data.Context;
using ClientManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientManager.Data.Repositories
{
    public interface IAccountRepository
    {
        List<Account> GetAccountsByClient(int clientId);
        Account GetAccountById(int accountId);
        void SaveAccount(Account account);
    }
    public class AccountRepository: IAccountRepository
    {
        private readonly ClientManagerDbContext _db;

        public AccountRepository(ClientManagerDbContext db)
        {
            _db = db;
        }


        public List<Account> GetAccountsByClient(int clientId)
        {

            return _db.Accounts
                    .Include(a => a.Transactions)
                    .Where(a => a.ClientId == clientId)
                    .ToList();
           
        }

        public Account GetAccountById(int accountId)
        {
 
                return _db.Accounts
                    .Include(a => a.Transactions)
                    .Include(a => a.Client)
                   .FirstOrDefault(a => a.AccountId == accountId);
          
        }

        public void SaveAccount(Account account)
        {

                if (account.AccountId == 0)
                    _db.Accounts.Add(account);
                else
                _db.Entry(account).State = EntityState.Modified;

            _db.SaveChanges();
            
        }
    }
}
