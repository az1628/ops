using System;
using System.Collections.Generic;
using System.Linq;
using ClientManager.Data.Context;
using ClientManager.Data.Entities;

namespace ClientManager.Data.Repositories
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactionsByAccount(int accountId);
        List<Transaction> GetTransactionsByDateRange(DateTime start, DateTime end);
        void AddTransaction(Transaction transaction);
    }
    public class TransactionRepository
    {
        private readonly ClientManagerDbContext db;

        public TransactionRepository(ClientManagerDbContext context)
        {
            db = context;
        }

        public List<Transaction> GetTransactionsByAccount(int accountId)
        {

                return db.Transactions
                    .Where(t => t.AccountId == accountId)
                    .OrderByDescending(t => t.TransactionDate)
                    .ToList();
           
        }

        public List<Transaction> GetTransactionsByDateRange(DateTime start, DateTime end)
        {
                return db.Transactions
                    .Where(t => t.TransactionDate >= start && t.TransactionDate <= end)
                    .OrderByDescending(t => t.TransactionDate)
                    .ToList();
        }

        public void AddTransaction(Transaction transaction)
        {
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }
    }
}
