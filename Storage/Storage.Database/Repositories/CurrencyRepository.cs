using Microsoft.EntityFrameworkCore;
using Storage.Database.Models;
using Storage.Database.Repositories.Interfaces;

namespace Storage.Database.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CurrencyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CurrencyHandbook>> GetAllAsync()
    {
        var data = await _dbContext
            .Currencies
            .ToListAsync();

        return data;
    }

    public async Task AddRangeAsync(IEnumerable<CurrencyHandbook> data)
    {
        _dbContext.Currencies.AddRange(data);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<CurrencyHandbook> data)
    {
        _dbContext.Currencies.UpdateRange(data);
        await _dbContext.SaveChangesAsync();
    }
}