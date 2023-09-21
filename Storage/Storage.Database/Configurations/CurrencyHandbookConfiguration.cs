using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Database.Models;

namespace Storage.Database.Configurations;

internal class CurrencyHandbookConfiguration : IEntityTypeConfiguration<CurrencyHandbook>
{
    public void Configure(EntityTypeBuilder<CurrencyHandbook> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ParentCode).HasColumnName("RId");

        builder.Property(x => x.ISOCharCode).HasColumnName("IsoCode");
    }
}