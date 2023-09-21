using Crawler.Core.Services.AppHttpClient;
using Hangfire;
using Hangfire.PostgreSql;

namespace Crawler.Main.Configuration;

public static class HangfireConfiguration
{
    public static void AddAppHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration["HangFireSettings:DatabaseConnection"];

        services.AddHangfire(x =>
        {
            x.UsePostgreSqlStorage(conn);
            x.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
            x.UseSimpleAssemblyNameTypeSerializer();
            x.UseRecommendedSerializerSettings();
        });

        services.AddHangfireServer();
    }

    public static void UseAppHangfire(this WebApplication app)
    {
        app.UseHangfireDashboard("/dashboard");

        ExecuteJobs();
    }

    private static void ExecuteJobs()
    {
        BackgroundJob.Enqueue<IHttpClientService>((service) => service.GetCurrencyHandbookAsync(true));

        RecurringJob.AddOrUpdate<IHttpClientService>("DailyJob", (service) => service.GetExchangeRatesAsync(DateTime.UtcNow), Cron.Daily);
    }
}
