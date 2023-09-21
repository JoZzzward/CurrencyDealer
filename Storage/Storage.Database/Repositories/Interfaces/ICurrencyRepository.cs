using Storage.Database.Models;

namespace Storage.Database.Repositories.Interfaces;

public interface ICurrencyRepository
{
    Task<IEnumerable<CurrencyHandbook>> GetAllAsync();
    Task AddRangeAsync(IEnumerable<CurrencyHandbook> data);
    Task UpdateRangeAsync(IEnumerable<CurrencyHandbook> data);
}
