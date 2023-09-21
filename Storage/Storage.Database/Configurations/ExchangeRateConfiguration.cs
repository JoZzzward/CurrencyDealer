using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Database.Models;

namespace Storage.Database.Configurations;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(er => er.Id);

        builder.HasOne(x => x.BaseCurrency)
            .WithMany(x => x.BaseExchangeRates)
            .HasForeignKey(x => x.BaseCurrencyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Currency)
            .WithMany(x => x.ExchangeRates)
            .HasForeignKey(x => x.CurrencyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}