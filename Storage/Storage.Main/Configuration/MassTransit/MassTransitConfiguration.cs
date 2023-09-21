using MassTransit;

namespace Storage.Main.Configuration.MassTransit;

public static class MassTransitConfiguration
{
    public static void AddAppMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration["RabbitMqSettings:Host"];
        var username = configuration["RabbitMqSettings:UserName"];
        var password = configuration["RabbitMqSettings:Password"];

        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            var assembly = typeof(Program).Assembly;

            config.AddConsumers(assembly);
            config.AddSagaStateMachines(assembly);
            config.AddSagas(assembly);
            config.AddActivities(assembly);

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
