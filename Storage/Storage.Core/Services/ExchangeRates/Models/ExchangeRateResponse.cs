using Storage.Database.Models;

namespace Storage.Core.Services.ExchangeRates.Models;

public class ExchangeRateResponse
{
    public Guid Id { get; set; }
    public string BaseCurrencyId { get; set; }
    public string CurrencyId { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
}
