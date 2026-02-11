using System.Data.Entity;
using ClientManager.Data.Entities;

namespace ClientManager.Data.Context
{
    public class ClientManagerDbContext : DbContext
    {
        public ClientManagerDbContext() : base("name=ClientManagerDb")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Accounts)
                .WithRequired(a => a.Client)
                .HasForeignKey(a => a.ClientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.ClientNotes)
                .WithRequired(n => n.Client)
                .HasForeignKey(n => n.ClientId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithRequired(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
