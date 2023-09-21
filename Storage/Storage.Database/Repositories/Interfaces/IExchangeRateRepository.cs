using Storage.Database.Models;

namespace Storage.Database.Repositories.Interfaces;

public interface IExchangeRateRepository
{
    Task<IEnumerable<ExchangeRate>> GetAllAsync();
    Task<IEnumerable<ExchangeRate>> GetAllByCodeAsync(string code);
    Task AddRangeAsync(IEnumerable<ExchangeRate> data);
    Task UpdateRangeAsync(IEnumerable<ExchangeRate> data);
}
