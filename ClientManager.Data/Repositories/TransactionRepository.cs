using System;
using System.Collections.Generic;
using System.Linq;
using ClientManager.Data.Context;
using ClientManager.Data.Entities;

namespace ClientManager.Data.Repositories
{
    public class TransactionRepository
    {
        public List<Transaction> GetTransactionsByAccount(int accountId)
        {
            using (var db = new ClientManagerDbContext())
            {
                return db.Transactions
                    .Where(t => t.AccountId == accountId)
                    .OrderByDescending(t => t.TransactionDate)
                    .ToList();
            }
        }

        public List<Transaction> GetTransactionsByDateRange(DateTime start, DateTime end)
        {
            using (var db = new ClientManagerDbContext())
            {
                return db.Transactions
                    .Where(t => t.TransactionDate >= start && t.TransactionDate <= end)
                    .OrderByDescending(t => t.TransactionDate)
                    .ToList();
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            using (var db = new ClientManagerDbContext())
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
            }
        }
    }
}
