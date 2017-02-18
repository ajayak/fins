﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FINS.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FINS.App_Start
{
    public static class SetupFinsDb
    {
        public static IServiceCollection SetupFinsDbContext(this IServiceCollection services, string connectionString, bool isTest)
        {
            if (isTest)
            {
                services.AddDbContext<FinsDbContext>(options =>
                {
                    options.UseInMemoryDatabase();
                    options.UseOpenIddict();
                });
            }
            else
            {
                services.AddDbContext<FinsDbContext>(options =>
                {
                    options.UseSqlServer(
                        connectionString,
                        option => option.EnableRetryOnFailure(2)
                    );
                    options.UseOpenIddict();
                });
            }
            return services;
        }
    }
}