using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Database.Repositories;
using Storage.Database.Repositories.Interfaces;

namespace Storage.Database;

public static class Bootstrapper
{
    private static void AddAppDbRepositories(this IServiceCollection services)
    {
        services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
    }

    public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAppDbRepositories();
        var conn = configuration["Database:ConnectionString"];
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                    conn,
                    o =>
                    {
                        o.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                        o.MigrationsHistoryTable("EFMigrationHistory", "public");
                    });
            options.EnableSensitiveDataLogging();
            options.UseLazyLoadingProxies();
            options.EnableDetailedErrors();
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}