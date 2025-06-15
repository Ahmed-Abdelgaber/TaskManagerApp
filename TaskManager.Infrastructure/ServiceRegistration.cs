using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Security;
using TaskManager.Infrastructure.Persistence;
using TaskManager.Infrastructure.Security;

namespace TaskManager.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            // Connect to PostgresSQL database
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("TaskManager.Infrastructure")));

            return services;
        }
    }
}


// dotnet ef migrations add InitialPostgresMigration --project TaskManager.Infrastructure --startup-project TaskManager.API
// dotnet ef database update --project TaskManager.Infrastructure --startup-project TaskManager.API
