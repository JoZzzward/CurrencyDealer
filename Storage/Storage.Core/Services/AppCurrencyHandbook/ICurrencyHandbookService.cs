using ExchangeTypes;

namespace Storage.Core.Services.AppCurrencyHandbook;

public interface ICurrencyHandbookService
{
    Task UpdateOrAddInformation(CurrencyHandbookDto model);
}