using FINS.Models.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class PersonConfig
    {
        public static void Configure(this EntityTypeBuilder<Person> entity)
        {
            entity.Property(p => p.Id).HasColumnName($"{nameof(Person)}Id");
            entity.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            entity.Property(p => p.EmailId).HasMaxLength(250);
            entity.Property(p => p.Decription).HasMaxLength(1000);
            entity.Property(p => p.Mobile).IsRequired();
        }
    }
}
