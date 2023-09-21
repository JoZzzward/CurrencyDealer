namespace ExchangeTypes;

public class ExchangedRatesDto
{
    public ExchangedRatesDtoItem[] Items { get; set; } = Array.Empty<ExchangedRatesDtoItem>();
}

public class ExchangedRatesDtoItem
{
    public Guid Id { get; set; }
    public string BaseCurrencyId { get; set; }
    public string CurrencyId { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
}
