using Converter.Core.Services.MassTransit;
using ExchangeTypes;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Reflection.Metadata.Ecma335;

namespace Converter.Core.Services.Converter;

public class ConverterService : IConverterService
{
    private readonly IMassTransitService _massTransitService;
    private readonly ILogger<ConverterService> _logger;

    public ConverterService(
        IMassTransitService massTransitService,
        ILogger<ConverterService> logger
        )
    {
        _massTransitService = massTransitService;
        _logger = logger;
    }

    public async Task ConvertExchangeToExchange(ConvertExchangeRateDto dto)
    {
        var items = new List<ExchangedRatesDtoItem>();

        for (int i = 0; i < dto.Items.Length; i++)
        { 
            var mainItem = dto.Items[i];

            for (int j = 0; j < dto.Items.Length; j++)
            {
                if (mainItem.Id == dto.Items[j].Id)
                    continue;

                items.Add(new ExchangedRatesDtoItem
                {
                    BaseCurrencyId = mainItem.Id,
                    CurrencyId = dto.Items[j].Id,
                    Date = mainItem.Date,
                    Value = mainItem.Value / dto.Items[j].Value,
                });
            }
        }

        var arr = new ExchangedRatesDto() { Items = items.ToArray() };
        
        await _massTransitService.PublishData(arr);

        _logger.LogInformation("{ExchangedRatedDtosCount} exchange rates was successfully converted..", arr.Items.Length);
    }
}