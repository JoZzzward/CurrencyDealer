using Crawler.Database.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Database;

public static class Bootstrapper
{
    public static void AddHangFireDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration["HangFireSettings:DatabaseConnection"];
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                    conn,
                    o =>
                    {
                        o.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                        o.MigrationsHistoryTable("EFMigrationHistory", "public");
                    });
        });

        DbInitializer.Execute(services.BuildServiceProvider());
    }
}
