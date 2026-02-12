using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using ClientManager.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClientManager.Data.StoredProcedures
{
    public class ClientSummaryReport
    {
        public string ClientName { get; set; }
        public int AccountCount { get; set; }
        public decimal TotalBalance { get; set; }
        public int TransactionCount { get; set; }
        public decimal TotalDeposits { get; set; }
        public decimal TotalWithdrawals { get; set; }
    }

    public interface IReportQueries
    {
        List<ClientSummaryReport> GetClientSummaryReport(string statusFilter = null);
    }

    public class ReportQueries: IReportQueries
    {
        private readonly ClientManagerDbContext db;

        public ReportQueries(ClientManagerDbContext context)
        {
            db = context;
        }
        public List<ClientSummaryReport> GetClientSummaryReport(string statusFilter = null)
        {
            if (statusFilter == null)
            {
                return db.ClientSummaryReports
                    .FromSqlRaw("EXEC sp_GetClientSummaryReport @StatusFilter = NULL")
                    .ToList();
            }

            return db.ClientSummaryReports
                .FromSqlRaw("EXEC sp_GetClientSummaryReport {0}", statusFilter)
                .ToList();
        }
    }
  
}
