using FINS.Models.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class AccountConfig
    {
        public static void Configure(this EntityTypeBuilder<Account> entity)
        {
            entity.Property(p => p.Id).HasColumnName($"{nameof(Account)}Id");
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Code).IsRequired().HasMaxLength(50);
            entity.Property(p => p.DisplayName).IsRequired().HasMaxLength(200);
            entity.Property(p => p.AccountGroupId).IsRequired();
            entity.Property(p => p.OpeningBalance).HasDefaultValue("0").IsRequired();
            entity.Property(p => p.Address).HasMaxLength(1000);
            entity.Property(p => p.CstNumber).HasMaxLength(15);
            entity.Property(p => p.ItPanNumber).HasMaxLength(15);
            entity.Property(p => p.LstNumber).HasMaxLength(15);
            entity.Property(p => p.ServiceTaxNumber).HasMaxLength(15);
            entity.Property(p => p.TinNumber).HasMaxLength(15);
            entity.Property(p => p.Ward).HasMaxLength(50);

            entity.HasIndex(e => e.Code).IsUnique();
        }
    }
}
