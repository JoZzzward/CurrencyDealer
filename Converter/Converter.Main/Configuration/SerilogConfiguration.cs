using Serilog;

namespace Converter.Main.Configuration;
public static class SerilogConfiguration
{
    public static IHostBuilder AddLogger(this IHostBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.WithCorrelationIdHeader()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} ({CorrelationId})] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        builder.UseSerilog(logger);

        return builder;
    }
}
