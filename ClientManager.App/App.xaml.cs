using ClientManager.App.Views;
using ClientManager.Business.Services;
using ClientManager.Data.Context;
using ClientManager.Data.Repositories;
using ClientManager.Data.StoredProcedures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
namespace ClientManager.App
{
    public partial class App : Application
    {
        private IHost _host;

        public static IServiceProvider Services { get; private set; }
        public App()
        {
            DispatcherUnhandledException += (s, e) =>
            {
                MessageBox.Show(e.Exception.ToString(), "Unhandled Error");
                e.Handled = true;
            };

            _host = new HostBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // DbContext
                    services.AddDbContext<ClientManagerDbContext>(options =>
                        options.UseSqlServer(
                            context.Configuration.GetConnectionString("ClientManagerDb")));

                    // Repositories
                    services.AddScoped<IClientRepository, ClientRepository>();
                    services.AddScoped<IAccountRepository, AccountRepository>();
                    services.AddScoped<IReportQueries, ReportQueries>();

                    // Services
                    services.AddScoped<ClientService>();
                    services.AddScoped<ReportService>();

                    // Pages
                    services.AddTransient<LoginPage>();
                    services.AddTransient<DashboardPage>();
                    services.AddTransient<ClientListPage>();
                    services.AddTransient<ClientDetailPage>();
                    services.AddTransient<ReportsPage>();

                    // Main Window
                    services.AddSingleton<MainWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                await _host.StartAsync();
                Services = _host.Services;

                using (var scope = _host.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ClientManagerDbContext>();
                    await db.Database.EnsureCreatedAsync();
                }

                var mainWindow = _host.Services.GetRequiredService<MainWindow>();
                mainWindow.Show();

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Startup Error");
                Shutdown();
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
