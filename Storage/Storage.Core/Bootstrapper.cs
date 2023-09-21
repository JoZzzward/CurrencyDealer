using Microsoft.Extensions.DependencyInjection;
using Storage.Core.Services.AppCurrencyHandbook;
using Storage.Core.Services.ExchangeRates;

namespace Storage.Core;

public static class Bootstrapper
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IExchangeRateService, ExchangeRateService>();
        services.AddScoped<ICurrencyHandbookService, CurrencyHandbookService>();
    }
}