using Microsoft.EntityFrameworkCore;
using Storage.Database.Models;
using Storage.Database.Repositories.Interfaces;

namespace Storage.Database.Repositories;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ExchangeRateRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ExchangeRate>> GetAllAsync()
    {
        var data = await _dbContext
            .ExchangeRates
            .ToListAsync();

        return data;
    }

    public async Task<IEnumerable<ExchangeRate>> GetAllByCodeAsync(string code)
    {
        var exchangeRates = await _dbContext.ExchangeRates
            .Include(c => c.BaseCurrency)
            .Where(x => x.BaseCurrency.ISOCharCode.ToUpper() == code.ToUpper())
            .ToListAsync();

        return exchangeRates;
    }

    public async Task AddRangeAsync(IEnumerable<ExchangeRate> data)
    {
        _dbContext.ExchangeRates.AddRange(data);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<ExchangeRate> data)
    {
        _dbContext.ExchangeRates.UpdateRange(data);
        await _dbContext.SaveChangesAsync();
    }
}