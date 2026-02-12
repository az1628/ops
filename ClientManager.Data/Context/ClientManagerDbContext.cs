using ClientManager.Data.Entities;
using ClientManager.Data.StoredProcedures;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClientManager.Data.Context
{
    public class ClientManagerDbContext : DbContext
    {
        public ClientManagerDbContext(DbContextOptions<ClientManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ClientSummaryReport> ClientSummaryReports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Accounts)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.ClientNotes)
                .WithOne(n => n.Client)
                .HasForeignKey(n => n.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ClientSummaryReport>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null); // tells EF Core not to create a table for this
            });

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // EF Core HasData requires explicit IDs for everything
            // This is different from EF6 where the DB generated them

            modelBuilder.Entity<Client>().HasData(
                new { ClientId = 1, FirstName = "James", LastName = "Worthington", Email = "j.worthington@email.com", Phone = "020 7946 0123", Address = "14 Kensington High St, London W8 4PT", Status = "Active", DateOnboarded = new DateTime(2015, 3, 12), RiskProfile = "Medium" },
                new { ClientId = 2, FirstName = "Sarah", LastName = "Mitchell", Email = "s.mitchell@corporate.co.uk", Phone = "020 7946 0456", Address = "88 Canary Wharf, London E14 5AB", Status = "Active", DateOnboarded = new DateTime(2018, 7, 20), RiskProfile = "High" },
                new { ClientId = 3, FirstName = "Robert", LastName = "Chen", Email = "r.chen@gmail.com", Phone = "07700 900123", Address = "22 Richmond Road, Twickenham TW1 3AB", Status = "Active", DateOnboarded = new DateTime(2020, 1, 5), RiskProfile = "Low" },
                new { ClientId = 4, FirstName = "Patricia", LastName = "O'Brien", Email = "patricia.obrien@outlook.com", Phone = "07700 900456", Address = "5 Georgian Terrace, Bath BA1 2EN", Status = "Inactive", DateOnboarded = new DateTime(2012, 11, 3), RiskProfile = "Medium" },
                new { ClientId = 5, FirstName = "David", LastName = "Patel", Email = "d.patel@techfirm.io", Phone = "020 7946 0789", Address = "Unit 4, Shoreditch Works, London EC2A 3AY", Status = "Prospect", DateOnboarded = new DateTime(2023, 9, 1), RiskProfile = "High" }
            );

            modelBuilder.Entity<Account>().HasData(
                new { AccountId = 1, ClientId = 1, AccountNumber = "ISA-001-2015", AccountType = "ISA", Balance = 45230.50m, OpeningBalance = 20000m, DateOpened = new DateTime(2015, 4, 6), Status = "Open" },
                new { AccountId = 2, ClientId = 1, AccountNumber = "GIA-001-2016", AccountType = "GIA", Balance = 120750.00m, OpeningBalance = 100000m, DateOpened = new DateTime(2016, 9, 1), Status = "Open" },
                new { AccountId = 3, ClientId = 2, AccountNumber = "SIPP-002-2018", AccountType = "SIPP", Balance = 310000m, OpeningBalance = 250000m, DateOpened = new DateTime(2018, 8, 1), Status = "Open" },
                new { AccountId = 4, ClientId = 3, AccountNumber = "ISA-003-2020", AccountType = "ISA", Balance = 18500m, OpeningBalance = 15000m, DateOpened = new DateTime(2020, 2, 1), Status = "Open" },
                new { AccountId = 5, ClientId = 4, AccountNumber = "GIA-004-2012", AccountType = "GIA", Balance = 0m, OpeningBalance = 50000m, DateOpened = new DateTime(2012, 12, 1), Status = "Closed" }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new { TransactionId = 1, AccountId = 1, TransactionType = "Deposit", Amount = 20000m, TransactionDate = new DateTime(2015, 4, 6), Description = "Initial deposit", ReferenceNumber = "TXN-0001" },
                new { TransactionId = 2, AccountId = 1, TransactionType = "Deposit", Amount = 15000m, TransactionDate = new DateTime(2016, 4, 6), Description = "Annual ISA allowance", ReferenceNumber = "TXN-0002" },
                new { TransactionId = 3, AccountId = 1, TransactionType = "Dividend", Amount = 1230.50m, TransactionDate = new DateTime(2017, 6, 15), Description = "Dividend payment Q2", ReferenceNumber = "TXN-0003" },
                new { TransactionId = 4, AccountId = 1, TransactionType = "Deposit", Amount = 10000m, TransactionDate = new DateTime(2018, 4, 6), Description = "Annual contribution", ReferenceNumber = "TXN-0004" },
                new { TransactionId = 5, AccountId = 1, TransactionType = "Withdrawal", Amount = 1000m, TransactionDate = new DateTime(2019, 12, 20), Description = "Partial withdrawal", ReferenceNumber = "TXN-0005" },
                new { TransactionId = 6, AccountId = 2, TransactionType = "Deposit", Amount = 100000m, TransactionDate = new DateTime(2016, 9, 1), Description = "Initial investment", ReferenceNumber = "TXN-0006" },
                new { TransactionId = 7, AccountId = 2, TransactionType = "Dividend", Amount = 5750m, TransactionDate = new DateTime(2017, 12, 15), Description = "Annual dividend", ReferenceNumber = "TXN-0007" },
                new { TransactionId = 8, AccountId = 2, TransactionType = "Fee", Amount = 500m, TransactionDate = new DateTime(2018, 1, 5), Description = "Management fee", ReferenceNumber = "TXN-0008" },
                new { TransactionId = 9, AccountId = 2, TransactionType = "Deposit", Amount = 15500m, TransactionDate = new DateTime(2019, 3, 10), Description = "Additional investment", ReferenceNumber = "TXN-0009" },
                new { TransactionId = 10, AccountId = 3, TransactionType = "Deposit", Amount = 250000m, TransactionDate = new DateTime(2018, 8, 1), Description = "Pension transfer in", ReferenceNumber = "TXN-0010" },
                new { TransactionId = 11, AccountId = 3, TransactionType = "Deposit", Amount = 40000m, TransactionDate = new DateTime(2019, 4, 5), Description = "Annual contribution", ReferenceNumber = "TXN-0011" },
                new { TransactionId = 12, AccountId = 3, TransactionType = "Dividend", Amount = 22000m, TransactionDate = new DateTime(2020, 3, 15), Description = "Fund distribution", ReferenceNumber = "TXN-0012" },
                new { TransactionId = 13, AccountId = 3, TransactionType = "Fee", Amount = 2000m, TransactionDate = new DateTime(2020, 4, 1), Description = "Annual management fee", ReferenceNumber = "TXN-0013" },
                new { TransactionId = 14, AccountId = 4, TransactionType = "Deposit", Amount = 15000m, TransactionDate = new DateTime(2020, 2, 1), Description = "Opening deposit", ReferenceNumber = "TXN-0014" },
                new { TransactionId = 15, AccountId = 4, TransactionType = "Dividend", Amount = 3500m, TransactionDate = new DateTime(2021, 6, 30), Description = "Interest and dividends", ReferenceNumber = "TXN-0015" },
                new { TransactionId = 16, AccountId = 5, TransactionType = "Deposit", Amount = 50000m, TransactionDate = new DateTime(2012, 12, 1), Description = "Initial deposit", ReferenceNumber = "TXN-0016" },
                new { TransactionId = 17, AccountId = 5, TransactionType = "Withdrawal", Amount = 50000m, TransactionDate = new DateTime(2022, 6, 15), Description = "Full withdrawal - account closure", ReferenceNumber = "TXN-0017" }
            );

            modelBuilder.Entity<Note>().HasData(
                new { NoteId = 1, ClientId = 1, Content = "Client prefers email communication. Review meeting scheduled quarterly.", CreatedDate = new DateTime(2015, 3, 12), CreatedBy = "admin", Category = "General" },
                new { NoteId = 2, ClientId = 1, Content = "KYC documents refreshed. Passport and utility bill on file.", CreatedDate = new DateTime(2020, 1, 15), CreatedBy = "admin", Category = "Compliance" },
                new { NoteId = 3, ClientId = 2, Content = "High net worth client. Interested in alternative investments.", CreatedDate = new DateTime(2018, 7, 20), CreatedBy = "admin", Category = "General" },
                new { NoteId = 4, ClientId = 4, Content = "Client relocated abroad. Account closed per request.", CreatedDate = new DateTime(2022, 6, 15), CreatedBy = "admin", Category = "General" }
            );
        }
    }
}