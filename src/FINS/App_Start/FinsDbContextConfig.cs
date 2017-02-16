using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FINS.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FINS.App_Start
{
    public static class FinsDbContextConfig
    {
        public static void SetupDbContextConfig(this IServiceCollection services, string connectionString)
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
    }
}
