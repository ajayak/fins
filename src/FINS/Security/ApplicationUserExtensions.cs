using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FINS.Models;
using Microsoft.AspNetCore.Identity;

namespace FINS.Security
{
    public static class ApplicationUserExtensions
    {
        public static bool IsUserType(this ApplicationUser user, UserType userType)
        {
            return user?.Claims != null && user.Claims.Any(c => c.ClaimType == ClaimTypes.UserType && c.ClaimValue == Enum.GetName(typeof(UserType), userType));
        }

        public static int? GetOrganizationId(this ApplicationUser user)
        {
            int? result = null;
            var organizationIdClaim = user.Claims.FirstOrDefault(c => c.ClaimType == ClaimTypes.Organization);
            if (organizationIdClaim != null)
            {
                int organizationId;
                if (int.TryParse(organizationIdClaim.ClaimValue, out organizationId))
                    result = organizationId;
            }

            return result;
        }
    }
}
