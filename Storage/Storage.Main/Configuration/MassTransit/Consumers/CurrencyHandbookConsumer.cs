using MassTransit;
using Storage.Core.Services.AppCurrencyHandbook;
using ExchangeTypes;

namespace Storage.Main.Configuration.MassTransit.Consumers;

public class CurrencyHandbookConsumer : IConsumer<CurrencyHandbookDto>
{
    private readonly ICurrencyHandbookService _currencyHandbookService;

    public CurrencyHandbookConsumer(ICurrencyHandbookService currencyHandbookService)
    {
        _currencyHandbookService = currencyHandbookService;
    }

    public async Task Consume(ConsumeContext<CurrencyHandbookDto> context)
    {
        await _currencyHandbookService.UpdateOrAddInformation(context.Message);
    }
}