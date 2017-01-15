using FINS.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class ApplicationUserConfig
    {
        public static void Configure(this EntityTypeBuilder<ApplicationUser> entity)
        {
            entity.Property(p => p.FirstName).HasMaxLength(50);
            entity.Property(p => p.LastName).HasMaxLength(50);
        }
    }
}
