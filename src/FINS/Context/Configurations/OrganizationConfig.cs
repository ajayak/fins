using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FINS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FINS.Context.Configurations
{
    public static class OrganizationConfig
    {
        public static void Configure(this EntityTypeBuilder<Organization> entity)
        {
            entity.Property(p => p.Id).HasColumnName($"{nameof(Organization)}Id");
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Summary).HasMaxLength(250);
            entity.Property(p => p.LogoUrl).HasMaxLength(250);
            entity.Property(p => p.WebUrl).HasMaxLength(250);
            entity.Property(p => p.DescriptionHtml).HasMaxLength(2000);
            entity.Property(p => p.PrivacyPolicy).HasMaxLength(2000);
        }
    }
}
