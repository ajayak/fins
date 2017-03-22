using FINS.Models.Accounting;
using FINS.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class UnitConversionConfig
    {
        public static void Configure(this EntityTypeBuilder<UnitConversion> entity)
        {
            entity.Property(p => p.Id).HasColumnName($"{nameof(UnitConversion)}Id");
            entity.Property(p => p.MultiplicationFactor).HasColumnType("decimal").IsRequired();
        }
    }
}
