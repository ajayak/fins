using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINS.Models.Account;
using FINS.Models.App;
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
        }
    }
}
