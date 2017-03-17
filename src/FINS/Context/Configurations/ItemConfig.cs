using FINS.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class ItemConfig
    {
        public static void Configure(this EntityTypeBuilder<Item> entity)
        {
            entity.Property(p => p.Id).HasColumnName($"{nameof(ItemGroup)}Id");
        }
    }
}
