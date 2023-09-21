using MassTransit;

namespace Crawler.Main.Configuration;

public static class MassTransitConfiguration
{
    public static void AddAppMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration["RabbitMqSettings:Host"];
        var username = configuration["RabbitMqSettings:UserName"];
        var password = configuration["RabbitMqSettings:Password"];        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            config.UsingRabbitMq((context, config) =>
            {
                config.Host(new Uri(host), "/", host =>
                {
                    host.Username(username);
                    host.Password(password);
                });
                config.ConfigureEndpoints(context);
            });
        });
    }
}