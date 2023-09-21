namespace ExchangeTypes;

public class CurrencyHandbookDto
{
    public CurrencyHandbookDtoItem[] Items { get; set; } = Array.Empty<CurrencyHandbookDtoItem>();
}

public class CurrencyHandbookDtoItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EngName { get; set; }
    public string ParentCode { get; set; }
    public string ISOCharCode { get; set; }
}
