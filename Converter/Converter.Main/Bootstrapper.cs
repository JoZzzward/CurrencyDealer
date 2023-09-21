using Converter.Core.Services.Converter;
using Converter.Core.Services.MassTransit;
using Converter.Main.Configuration;

namespace Converter.Main;

public static class Bootstrapper
{
    public static Action<HostBuilderContext, IServiceCollection> AddAppServices()
    => (context, services) =>
    {
        var configuration = context.Configuration;

        services.AddSingleton<IMassTransitService, MassTransitService>();
        services.AddSingleton<IConverterService, ConverterService>();

        services.AddAppMassTransit(configuration);
    };
}
