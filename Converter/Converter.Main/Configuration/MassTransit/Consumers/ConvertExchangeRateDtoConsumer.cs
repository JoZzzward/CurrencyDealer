using Converter.Core.Services.Converter;
using ExchangeTypes;
using MassTransit;

namespace Converter.Main.Configuration.MassTransit.Consumers;

public class ConvertExchangeRateDtoConsumer : IConsumer<ConvertExchangeRateDto>
{
    private readonly IConverterService _converterService;

    public ConvertExchangeRateDtoConsumer(
        IConverterService converterService
        )
    {
        _converterService = converterService;
    }

    public async Task Consume(ConsumeContext<ConvertExchangeRateDto> context)
    {
        await _converterService.ConvertExchangeToExchange(context.Message);
    }
}
