using Langueedu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Langueedu.Infrastructure;

public static class StartupSetup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("SqliteConnection");
        services.AddDbContext(connectionString);
    }

    internal static void AddDbContext(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString)); // will be created in web project root
}

