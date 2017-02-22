﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINS.Models.Account;
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
            entity.Property(p => p.DisplayName).IsRequired().HasMaxLength(200);
            entity.Property(p => p.AccountGroupId).IsRequired();
            entity.Property(p => p.OrganizationId).IsRequired();
            entity.Property(p => p.OpeningBalance).HasDefaultValue("0").IsRequired();
        }
    }
}
