using System;
using System.Collections.Generic;
using System.Linq;
using ClientManager.Business.Models;
using ClientManager.Data.Context;
using ClientManager.Data.Repositories;
using ClientManager.Data.StoredProcedures;

namespace ClientManager.Business.Services
{
    public class ReportService
    {
        private readonly ReportQueries _reportQueries = new ReportQueries();

        // Legacy smell: service directly creating DbContext instead of using repository
        public DashboardStats GetDashboardStats()
        {
            using (var db = new ClientManagerDbContext())
            {
                var stats = new DashboardStats();
                stats.TotalClients = db.Clients.Count();
                stats.ActiveClients = db.Clients.Count(c => c.Status == "Active");
                stats.ProspectCount = db.Clients.Count(c => c.Status == "Prospect");
                stats.TotalAccounts = db.Accounts.Count(a => a.Status == "Open");
                stats.TotalAUM = db.Accounts.Where(a => a.Status == "Open").Sum(a => (decimal?)a.Balance) ?? 0;

                var thirtyDaysAgo = DateTime.Now.AddDays(-30);
                stats.RecentTransactionCount = db.Transactions
                    .Count(t => t.TransactionDate >= thirtyDaysAgo);

                return stats;
            }
        }

        public List<ClientSummaryReport> GetClientSummary(string statusFilter = null)
        {
            if (statusFilter == "All") statusFilter = null;
            return _reportQueries.GetClientSummaryReport(statusFilter);
        }
    }
}
