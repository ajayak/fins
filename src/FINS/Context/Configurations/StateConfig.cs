using FINS.Models.Accounting;
using FINS.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class StateConfig
    {
        public static void Configure(this EntityTypeBuilder<State> entity)
        {
            entity.Property(p => p.Id).HasColumnName($"{nameof(Person)}Id");
            entity.Property(p => p.Name).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Code).IsRequired().HasMaxLength(3);
        }
    }
}
