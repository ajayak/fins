using System.Linq;
using FINS.Context.Configurations;
using FINS.Models.Accounting;
using FINS.Models.App;
using FINS.Models.Common;
using FINS.Models.Inventory;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FINS.Context
{
    public class FinsDbContext : IdentityDbContext<ApplicationUser>
    {
        public FinsDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Organization> Organizations { get; set; }
        public DbSet<AccountGroup> AccountGroups { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Restric cascade delete
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

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
            builder.Entity<ItemGroup>().Configure();
            builder.Entity<Item>().Configure();
            builder.Entity<Person>().Configure();
            builder.Entity<State>().Configure();
        }
    }
}
