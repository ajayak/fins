using FINS.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class TaxConfig
    {
        public static void Configure(this EntityTypeBuilder<Tax> entity)
        {
            entity.Property(p => p.Id).HasColumnName($"{nameof(Tax)}Id");
            entity.Property(p => p.Category).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Percentage).HasColumnType("decimal").IsRequired();
        }
    }
}
