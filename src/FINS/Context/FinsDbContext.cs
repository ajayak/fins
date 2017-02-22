using FINS.Context.Configurations;
using FINS.Models.Account;
using FINS.Models.App;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FINS.Context
{
    public class FinsDbContext : IdentityDbContext<ApplicationUser>
    {
        public FinsDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Organization> Organizations { get; set; }
        public DbSet<AccountGroup> AccountGroups { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Keep old database table naming convention
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityRole>().ToTable("Roles");

            builder.Entity<ApplicationUser>().Configure();
            builder.Entity<Organization>().Configure();
            builder.Entity<AccountGroup>().Configure();
            builder.Entity<Account>().Configure();
        }
    }
}
