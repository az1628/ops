using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ClientManager.Data.Context;

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

    public class ReportQueries
    {
        public List<ClientSummaryReport> GetClientSummaryReport(string statusFilter = null)
        {
            using (var db = new ClientManagerDbContext())
            {
                var param = new SqlParameter("@StatusFilter",
                    (object)statusFilter ?? System.DBNull.Value);

                return db.Database.SqlQuery<ClientSummaryReport>(
                    "EXEC sp_GetClientSummaryReport @StatusFilter", param)
                    .ToList();
            }
        }
    }
}
