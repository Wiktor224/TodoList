using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Interfaces.Repositories;
using TodoList.Interfaces.Services;
using TodoList.Repositories;
using TodoList.Services;

namespace TodoList.Configuration
{
    public static class ConfigureService
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<MainWindow>();
            services.AddTransient<LoginWindow>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
