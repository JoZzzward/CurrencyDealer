namespace ExchangeTypes;

public class ConvertExchangeRateDto
{
    public ConvertExchangeRateItemDto[] Items { get; set; } = Array.Empty<ConvertExchangeRateItemDto>();
}

public class ConvertExchangeRateItemDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EngName { get; set; }
    public string ISO_Char_Code { get; set; }
    public int Nominal { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
}
