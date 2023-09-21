using Crawler.Core.Services.AppHttpClient.Models;

namespace Crawler.Core.Services.AppHttpClient;

public interface IHttpClientService
{
    Task<IEnumerable<ExchangeValueResponse>> GetExchangeRatesAsync(DateTime limit);
    Task<CurrencyHandbookArray> GetCurrencyHandbookAsync(bool needToBeSended = true);
    Task<IEnumerable<ExchangeValueResponse>> GetExchangeValueResponsesByDateAsync(DateTime dateTime);
}
