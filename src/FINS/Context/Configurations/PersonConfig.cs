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
            entity.Property(p => p.EmailId).HasMaxLength(250);
            entity.Property(p => p.Mobile).IsRequired();
            entity.Property(p => p.Address).HasMaxLength(1000);
            entity.Property(p => p.CstNumber).HasMaxLength(15);
            entity.Property(p => p.ItPanNumber).HasMaxLength(15);
            entity.Property(p => p.LstNumber).HasMaxLength(15);
            entity.Property(p => p.ServiceTaxNumber).HasMaxLength(15);
            entity.Property(p => p.TinNumber).HasMaxLength(15);
            entity.Property(p => p.Ward).HasMaxLength(50);
        }
    }
}
