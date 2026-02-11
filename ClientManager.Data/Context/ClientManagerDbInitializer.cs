using System;
using System.Collections.Generic;
using System.Data.Entity;
using ClientManager.Data.Entities;

namespace ClientManager.Data.Context
{
    public class ClientManagerDbInitializer : CreateDatabaseIfNotExists<ClientManagerDbContext>
    {
        protected override void Seed(ClientManagerDbContext context)
        {
            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "James", LastName = "Worthington",
                    Email = "j.worthington@email.com", Phone = "020 7946 0123",
                    Address = "14 Kensington High St, London W8 4PT",
                    Status = "Active", DateOnboarded = new DateTime(2015, 3, 12),
                    RiskProfile = "Medium",
                    Accounts = new List<Account>
                    {
                        new Account
                        {
                            AccountNumber = "ISA-001-2015", AccountType = "ISA",
                            Balance = 45230.50m, OpeningBalance = 20000m,
                            DateOpened = new DateTime(2015, 4, 6), Status = "Open",
                            Transactions = new List<Transaction>
                            {
                                new Transaction { TransactionType = "Deposit", Amount = 20000m, TransactionDate = new DateTime(2015, 4, 6), Description = "Initial deposit", ReferenceNumber = "TXN-0001" },
                                new Transaction { TransactionType = "Deposit", Amount = 15000m, TransactionDate = new DateTime(2016, 4, 6), Description = "Annual ISA allowance", ReferenceNumber = "TXN-0002" },
                                new Transaction { TransactionType = "Dividend", Amount = 1230.50m, TransactionDate = new DateTime(2017, 6, 15), Description = "Dividend payment Q2", ReferenceNumber = "TXN-0003" },
                                new Transaction { TransactionType = "Deposit", Amount = 10000m, TransactionDate = new DateTime(2018, 4, 6), Description = "Annual contribution", ReferenceNumber = "TXN-0004" },
                                new Transaction { TransactionType = "Withdrawal", Amount = 1000m, TransactionDate = new DateTime(2019, 12, 20), Description = "Partial withdrawal", ReferenceNumber = "TXN-0005" },
                            }
                        },
                        new Account
                        {
                            AccountNumber = "GIA-001-2016", AccountType = "GIA",
                            Balance = 120750.00m, OpeningBalance = 100000m,
                            DateOpened = new DateTime(2016, 9, 1), Status = "Open",
                            Transactions = new List<Transaction>
                            {
                                new Transaction { TransactionType = "Deposit", Amount = 100000m, TransactionDate = new DateTime(2016, 9, 1), Description = "Initial investment", ReferenceNumber = "TXN-0006" },
                                new Transaction { TransactionType = "Dividend", Amount = 5750m, TransactionDate = new DateTime(2017, 12, 15), Description = "Annual dividend", ReferenceNumber = "TXN-0007" },
                                new Transaction { TransactionType = "Fee", Amount = 500m, TransactionDate = new DateTime(2018, 1, 5), Description = "Management fee", ReferenceNumber = "TXN-0008" },
                                new Transaction { TransactionType = "Deposit", Amount = 15500m, TransactionDate = new DateTime(2019, 3, 10), Description = "Additional investment", ReferenceNumber = "TXN-0009" },
                            }
                        }
                    },
                    ClientNotes = new List<Note>
                    {
                        new Note { Content = "Client prefers email communication. Review meeting scheduled quarterly.", CreatedDate = new DateTime(2015, 3, 12), CreatedBy = "admin", Category = "General" },
                        new Note { Content = "KYC documents refreshed. Passport and utility bill on file.", CreatedDate = new DateTime(2020, 1, 15), CreatedBy = "admin", Category = "Compliance" },
                    }
                },
                new Client
                {
                    FirstName = "Sarah", LastName = "Mitchell",
                    Email = "s.mitchell@corporate.co.uk", Phone = "020 7946 0456",
                    Address = "88 Canary Wharf, London E14 5AB",
                    Status = "Active", DateOnboarded = new DateTime(2018, 7, 20),
                    RiskProfile = "High",
                    Accounts = new List<Account>
                    {
                        new Account
                        {
                            AccountNumber = "SIPP-002-2018", AccountType = "SIPP",
                            Balance = 310000m, OpeningBalance = 250000m,
                            DateOpened = new DateTime(2018, 8, 1), Status = "Open",
                            Transactions = new List<Transaction>
                            {
                                new Transaction { TransactionType = "Deposit", Amount = 250000m, TransactionDate = new DateTime(2018, 8, 1), Description = "Pension transfer in", ReferenceNumber = "TXN-0010" },
                                new Transaction { TransactionType = "Deposit", Amount = 40000m, TransactionDate = new DateTime(2019, 4, 5), Description = "Annual contribution", ReferenceNumber = "TXN-0011" },
                                new Transaction { TransactionType = "Dividend", Amount = 22000m, TransactionDate = new DateTime(2020, 3, 15), Description = "Fund distribution", ReferenceNumber = "TXN-0012" },
                                new Transaction { TransactionType = "Fee", Amount = 2000m, TransactionDate = new DateTime(2020, 4, 1), Description = "Annual management fee", ReferenceNumber = "TXN-0013" },
                            }
                        }
                    },
                    ClientNotes = new List<Note>
                    {
                        new Note { Content = "High net worth client. Interested in alternative investments.", CreatedDate = new DateTime(2018, 7, 20), CreatedBy = "admin", Category = "General" },
                    }
                },
                new Client
                {
                    FirstName = "Robert", LastName = "Chen",
                    Email = "r.chen@gmail.com", Phone = "07700 900123",
                    Address = "22 Richmond Road, Twickenham TW1 3AB",
                    Status = "Active", DateOnboarded = new DateTime(2020, 1, 5),
                    RiskProfile = "Low",
                    Accounts = new List<Account>
                    {
                        new Account
                        {
                            AccountNumber = "ISA-003-2020", AccountType = "ISA",
                            Balance = 18500m, OpeningBalance = 15000m,
                            DateOpened = new DateTime(2020, 2, 1), Status = "Open",
                            Transactions = new List<Transaction>
                            {
                                new Transaction { TransactionType = "Deposit", Amount = 15000m, TransactionDate = new DateTime(2020, 2, 1), Description = "Opening deposit", ReferenceNumber = "TXN-0014" },
                                new Transaction { TransactionType = "Dividend", Amount = 3500m, TransactionDate = new DateTime(2021, 6, 30), Description = "Interest and dividends", ReferenceNumber = "TXN-0015" },
                            }
                        }
                    }
                },
                new Client
                {
                    FirstName = "Patricia", LastName = "O'Brien",
                    Email = "patricia.obrien@outlook.com", Phone = "07700 900456",
                    Address = "5 Georgian Terrace, Bath BA1 2EN",
                    Status = "Inactive", DateOnboarded = new DateTime(2012, 11, 3),
                    RiskProfile = "Medium",
                    Accounts = new List<Account>
                    {
                        new Account
                        {
                            AccountNumber = "GIA-004-2012", AccountType = "GIA",
                            Balance = 0m, OpeningBalance = 50000m,
                            DateOpened = new DateTime(2012, 12, 1), Status = "Closed",
                            Transactions = new List<Transaction>
                            {
                                new Transaction { TransactionType = "Deposit", Amount = 50000m, TransactionDate = new DateTime(2012, 12, 1), Description = "Initial deposit", ReferenceNumber = "TXN-0016" },
                                new Transaction { TransactionType = "Withdrawal", Amount = 50000m, TransactionDate = new DateTime(2022, 6, 15), Description = "Full withdrawal - account closure", ReferenceNumber = "TXN-0017" },
                            }
                        }
                    },
                    ClientNotes = new List<Note>
                    {
                        new Note { Content = "Client relocated abroad. Account closed per request.", CreatedDate = new DateTime(2022, 6, 15), CreatedBy = "admin", Category = "General" },
                    }
                },
                new Client
                {
                    FirstName = "David", LastName = "Patel",
                    Email = "d.patel@techfirm.io", Phone = "020 7946 0789",
                    Address = "Unit 4, Shoreditch Works, London EC2A 3AY",
                    Status = "Prospect", DateOnboarded = new DateTime(2023, 9, 1),
                    RiskProfile = "High",
                    Accounts = new List<Account>()
                }
            };

            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();

            // Create the stored procedure via raw SQL
            context.Database.ExecuteSqlCommand(@"
                CREATE PROCEDURE sp_GetClientSummaryReport
                    @StatusFilter NVARCHAR(50) = NULL
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT
                        c.FirstName + ' ' + c.LastName AS ClientName,
                        COUNT(DISTINCT a.AccountId) AS AccountCount,
                        ISNULL(SUM(a.Balance), 0) AS TotalBalance,
                        ISNULL(COUNT(t.TransactionId), 0) AS TransactionCount,
                        ISNULL(SUM(CASE WHEN t.TransactionType = 'Deposit' THEN t.Amount ELSE 0 END), 0) AS TotalDeposits,
                        ISNULL(SUM(CASE WHEN t.TransactionType = 'Withdrawal' THEN t.Amount ELSE 0 END), 0) AS TotalWithdrawals
                    FROM Clients c
                    LEFT JOIN Accounts a ON a.ClientId = c.ClientId
                    LEFT JOIN Transactions t ON t.AccountId = a.AccountId
                    WHERE (@StatusFilter IS NULL OR c.Status = @StatusFilter)
                    GROUP BY c.FirstName, c.LastName
                    ORDER BY TotalBalance DESC;
                END
            ");

            base.Seed(context);
        }
    }
}
