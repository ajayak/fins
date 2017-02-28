using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using FINS.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FINS.Security
{
    public static class SecurityPolicies
    {
        public static IServiceCollection AddSecurityPolicies(this IServiceCollection service)
        {
            service.AddAuthorization(options =>
            {
                options.AddPolicy("OrgAdmin", b =>
                {
                    b.RequireClaim(ClaimTypes.UserType, "OrgAdmin");
                });
                options.AddPolicy("SiteAdmin", b =>
                {
                    b.RequireClaim(ClaimTypes.UserType, "SiteAdmin", "OrgAdmin");
                });

                options.AddPolicy(Accounting.AccountGroupManager.ToString(), b =>
                {
                    b.RequireClaim(ClaimTypes.Accounting, Accounting.AccountGroupManager.ToString());
                });
            });
            return service;
        }
    }
}
