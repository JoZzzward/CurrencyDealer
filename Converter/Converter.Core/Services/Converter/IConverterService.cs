using ExchangeTypes;

namespace Converter.Core.Services.Converter;

public interface IConverterService
{
    Task ConvertExchangeToExchange(ConvertExchangeRateDto convertExchangeRateDto);
}