using ExchangeTypes;
using Microsoft.Extensions.Logging;
using Storage.Database.Models;
using Storage.Database.Repositories.Interfaces;

namespace Storage.Core.Services.AppCurrencyHandbook;

public class CurrencyHandbookService : ICurrencyHandbookService
{
    private readonly ILogger<CurrencyHandbookService> _logger;
    private readonly ICurrencyRepository _context;

    public CurrencyHandbookService(
        ICurrencyRepository context,
        ILogger<CurrencyHandbookService> logger
        )
    {
        _logger = logger;
        _context = context;
    }

    public async Task UpdateOrAddInformation(CurrencyHandbookDto model)
    {
        var data = await _context.GetAllAsync();

        var newData = model.Items
            .AsParallel()
            .Select(x => new CurrencyHandbook()
            {
                Id = x.Id,
                Name = x.Name,
                EngName = x.EngName,
                ParentCode = x.ParentCode,
                ISOCharCode = x.ISOCharCode,
            });

        if (data.Any())
        {
            _logger.LogInformation("Any data exists in the database.. trying to update it..");
            await _context.UpdateRangeAsync(newData);
            _logger.LogInformation("The data was successfully updated in the database.");
        }    
        else
        {
            _logger.LogInformation("The data doesnt exists in the database.. trying to add it.");
            await _context.AddRangeAsync(newData);
            _logger.LogInformation("The data was successfully added to the database.");
        }
    }
}