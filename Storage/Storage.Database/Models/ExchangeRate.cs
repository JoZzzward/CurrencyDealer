namespace Storage.Database.Models;

public class ExchangeRate
{
    public Guid Id { get; set; }

    public string BaseCurrencyId { get; set; }
    public virtual CurrencyHandbook BaseCurrency { get; set; }

    public string CurrencyId { get; set; }
    public virtual CurrencyHandbook Currency { get; set; }

    public DateTime Date { get; set; }
    public decimal Value { get; set; }
}