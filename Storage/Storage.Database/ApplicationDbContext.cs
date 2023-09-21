using Microsoft.EntityFrameworkCore;
using Storage.Database.Configurations;
using Storage.Database.Models;

namespace Storage.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<CurrencyHandbook> Currencies { get; set; } 
    public DbSet<ExchangeRate> ExchangeRates { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new CurrencyHandbookConfiguration());
        builder.ApplyConfiguration(new ExchangeRateConfiguration());
    }
}