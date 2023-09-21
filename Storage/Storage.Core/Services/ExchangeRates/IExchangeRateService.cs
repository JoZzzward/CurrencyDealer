using ExchangeTypes;
using Storage.Core.Services.ExchangeRates.Models;

namespace Storage.Core.Services.ExchangeRates;

public interface IExchangeRateService
{
    Task<IEnumerable<ExchangeRateResponse>> GetExchangeRateValuesBetweenDates(DateTime startDate, DateTime endDate);
    Task<IEnumerable<ExchangeRateResponse>> GetExchangeRateValuesByCode(string code);
    Task SendConvertedExchangeRatesToDatabase(ExchangedRatesDto message);
}