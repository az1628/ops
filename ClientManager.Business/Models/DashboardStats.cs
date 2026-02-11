namespace ClientManager.Business.Models
{
    public class DashboardStats
    {
        public int TotalClients { get; set; }
        public int ActiveClients { get; set; }
        public int ProspectCount { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalAUM { get; set; }
        public int RecentTransactionCount { get; set; }
    }
}
