namespace Storage.Database.Models;

public class CurrencyHandbook
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EngName { get; set; }
    public string ParentCode { get; set; }
    public string ISOCharCode { get; set; }

    public virtual ICollection<ExchangeRate> BaseExchangeRates { get; set; }
    public virtual ICollection<ExchangeRate> ExchangeRates { get; set; }
}