using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FINS.Models;
using FINS.Security;
using Hangfire.Dashboard;

namespace FINS.Hangfire
{
    public class HangireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return httpContext.User.IsUserType(UserType.SiteAdmin);
        }
    }
}
