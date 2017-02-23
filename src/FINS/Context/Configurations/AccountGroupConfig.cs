using FINS.Models.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class AccountGroupConfig
    {
        public static void Configure(this EntityTypeBuilder<AccountGroup> entity)
        {
            entity.Property(p => p.Id).HasColumnName($"{nameof(AccountGroup)}Id");
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.DisplayName).IsRequired().HasMaxLength(200);
            entity.Property(p => p.OrganizationId).IsRequired();
        }
    }
}
