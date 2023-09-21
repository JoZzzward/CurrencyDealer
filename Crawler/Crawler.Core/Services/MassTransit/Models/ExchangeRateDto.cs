namespace Crawler.Core.Services.MassTransit.Models;

public class ExchangeRateDto
{
    public string BaseCurrencyId { get; set; }
    public string CurrencyId { get; set; }
    public DateTime Date { get; set; }
    public float Value { get; set; }
}
