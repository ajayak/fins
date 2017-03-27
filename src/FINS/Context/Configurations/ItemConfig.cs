using FINS.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class ItemConfig
    {
        public static void Configure(this EntityTypeBuilder<Item> entity)
        {
            entity.Property(p => p.Id).HasColumnName($"{nameof(Item)}Id");
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Code).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Description).HasMaxLength(1000);
            entity.Property(p => p.Quantity).IsRequired();
            entity.Property(p => p.UnitId).IsRequired();
            entity.Property(p => p.Color).HasMaxLength(20);
            entity.Property(p => p.SafetyStockLevel).HasColumnType("smallint");
            entity.Property(p => p.DaysToManufacture).HasColumnType("smallint");
            entity.Property(p => p.StandardCost).HasColumnType("money");
            entity.Property(p => p.ListPrice).HasColumnType("money");
            entity.Property(p => p.ReorderPoint).HasMaxLength(50);
            entity.Property(p => p.ImageName).HasMaxLength(250);
            entity.Property(p => p.StandardCost).HasMaxLength(250);
        }
    }
}
