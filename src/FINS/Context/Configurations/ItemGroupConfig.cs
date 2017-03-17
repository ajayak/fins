using FINS.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class ItemGroupConfig
    {
        public static void Configure(this EntityTypeBuilder<ItemGroup> entity)
        {
            entity.Property(p => p.Id).HasColumnName($"{nameof(ItemGroup)}Id");
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.DisplayName).IsRequired().HasMaxLength(200);
            entity.Property(p => p.OrganizationId).IsRequired();
        }
    }
}
