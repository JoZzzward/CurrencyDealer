using ExchangeTypes;
using Microsoft.Extensions.Logging;
using Storage.Core.Services.ExchangeRates.Models;
using Storage.Database.Models;
using Storage.Database.Repositories.Interfaces;

namespace Storage.Core.Services.ExchangeRates;

public class ExchangeRateService : IExchangeRateService
{
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly ILogger<ExchangeRateService> _logger;

    public ExchangeRateService(
        IExchangeRateRepository exchangeRateRepository,
        ILogger<ExchangeRateService> logger
        )
    {
        _exchangeRateRepository = exchangeRateRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<ExchangeRateResponse>> GetExchangeRateValuesBetweenDates(
        DateTime startDate,
        DateTime endDate)
    {
        _logger.LogInformation("Trying to return exchange rate values between dates({StartDate}-{EndDate})", 
            startDate.ToShortDateString(), 
            endDate.ToShortDateString());

        var data = await _exchangeRateRepository.GetAllAsync();

        if (!data.Any())
        {
            _logger.LogInformation("Exchange rate values storage is empty!");
            return default!;
        }

        var mappedData = data
            .Where(exchangeRate => exchangeRate.Date >= startDate && exchangeRate.Date <= endDate)
            .Select(x => new ExchangeRateResponse()
            {
                Id = x.Id,
                BaseCurrencyId = x.BaseCurrencyId,
                CurrencyId = x.CurrencyId,
                Date = x.Date,
                Value = x.Value
            });

        _logger.LogInformation("Exchange rates was successfully returned. Amount: {ExchangeRateCount}", mappedData.Count());

        return mappedData;
    }

    public async Task<IEnumerable<ExchangeRateResponse>> GetExchangeRateValuesByCode(string code)
    {
        _logger.LogInformation("Trying to return exchange rate values by code({Code})", code);
        var data = await _exchangeRateRepository.GetAllByCodeAsync(code);

        if (!data.Any())
        {
            _logger.LogInformation("Exchange rate values storage is empty!");
            return default!;
        }

        var mappedData = data
            .Select(x => new ExchangeRateResponse()
            {
                Id = x.Id,
                BaseCurrencyId = x.BaseCurrencyId,
                CurrencyId = x.CurrencyId,
                Date = x.Date,
                Value = x.Value
            });

        _logger.LogInformation("Exchange rates by code({Code}) was successfully returned. Amount: {ExchangeRateCount}", 
            code, 
            mappedData.Count());

        return mappedData;
    }

    public async Task SendConvertedExchangeRatesToDatabase(ExchangedRatesDto message)
    {
        var data = message.Items
            .Select(x => new ExchangeRate()
            {
                BaseCurrencyId = (x.BaseCurrencyId == "R01090") ? "R01090B" : x.BaseCurrencyId,
                CurrencyId = (x.CurrencyId == "R01090") ? "R01090B" : x.CurrencyId,
                Date = x.Date,
                Value = x.Value
            });

        await _exchangeRateRepository.AddRangeAsync(data);
    }
}