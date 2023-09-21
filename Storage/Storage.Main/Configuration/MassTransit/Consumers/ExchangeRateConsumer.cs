using ExchangeTypes;
using MassTransit;
using Storage.Core.Services.ExchangeRates;

namespace Storage.Main.Configuration.MassTransit.Consumers;

public class ExchangeRateConsumer : IConsumer<ExchangedRatesDto>
{
    private readonly IExchangeRateService _exchangeRateService;

    public ExchangeRateConsumer(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    public async Task Consume(ConsumeContext<ExchangedRatesDto> context)
    {
        await _exchangeRateService.SendConvertedExchangeRatesToDatabase(context.Message);
    }
}
