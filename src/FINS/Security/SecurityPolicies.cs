using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
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
                options.AddPolicy(Accounting.AccountManager.ToString(), b =>
                {
                    b.RequireClaim(ClaimTypes.Accounting, Accounting.AccountManager.ToString());
                });

                options.AddPolicy(Inventory.ItemGroupManager.ToString(), b =>
                {
                    b.RequireClaim(ClaimTypes.Inventory, Inventory.ItemGroupManager.ToString());
                });
                options.AddPolicy(Accounting.AccountManager.ToString(), b =>
                {
                    b.RequireClaim(ClaimTypes.Inventory, Inventory.ItemManager.ToString());
                });
            });
            return service;
        }
    }
}
