using Serilog;

namespace Crawler.Main.Configuration;
public static class SerilogConfiguration
{
    public static void AddLogger(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .Enrich.WithCorrelationIdHeader()
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog(logger, true);
    }
}
