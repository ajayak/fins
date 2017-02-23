using FINS.Models.App;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class ApplicationUserConfig
    {
        public static void Configure(this EntityTypeBuilder<ApplicationUser> entity)
        {
            entity.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(p => p.LastName).IsRequired().HasMaxLength(50);
        }
    }
}
