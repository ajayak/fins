using FINS.Models.Accounting;
using FINS.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class UnitConfig
    {
        public static void Configure(this EntityTypeBuilder<Unit> entity)
        {
            entity.Property(p => p.Id).HasColumnName($"{nameof(Unit)}Id");
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Code).IsRequired().HasMaxLength(5);
        }
    }
}
