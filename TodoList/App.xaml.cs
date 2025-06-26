using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Windows;
using TodoList.Configuration;
using TodoList.Data;
using TodoList.Interfaces.Services;

namespace TodoList
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        private IConfiguration _configuration;

        public App()
        {
            // Konstruktor niepotrzebny, usuń jeśli niepotrzebne
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigureService.ConfigureServices(services, _configuration);

            ServiceProvider = services.BuildServiceProvider();

            ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

            var loginWindow = new LoginWindow(
                ServiceProvider.GetRequiredService<AppDbContext>(),
                ServiceProvider.GetRequiredService<IUserService>());

            bool? result = loginWindow.ShowDialog();
            Debug.WriteLine($"ShowDialog result: {result}");

            if (result == true)
            {
                var user = loginWindow.LoggedInUser;

               
                var mainWindow = new MainWindow(
        ServiceProvider.GetRequiredService<AppDbContext>(),
        user 
    );
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
            }
            else
            {
                Shutdown();
            }
        }
    }
}
