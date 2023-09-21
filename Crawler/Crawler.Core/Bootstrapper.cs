using Crawler.Core.Services.AppHttpClient;
using Crawler.Core.Services.Hangfire;
using Crawler.Core.Services.MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Core;

public static class Bootstrapper
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpClientService, HttpClientService>();
        services.AddSingleton<IHangfireService, HangfireService>();
        services.AddSingleton<IMassTransitService, MassTransitService>();
    }
}