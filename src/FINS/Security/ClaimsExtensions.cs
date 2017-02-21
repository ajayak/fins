using System;
using System.Linq;
using System.Security.Claims;

namespace FINS.Security
{
    public static class ClaimsExtensions
    {
        public static bool IsUserType(this ClaimsPrincipal user, UserType type)
        {
            var userTypeString = Enum.GetName(typeof(UserType), type);
            return user.HasClaim(ClaimTypes.UserType, userTypeString);
        }

        public static bool HasAccess(this ClaimsPrincipal user, AccessLevel access)
        {
            var accessString = Enum.GetName(typeof(AccessLevel), access);
            return user.HasClaim(ClaimTypes.AccessLevel, accessString);
        }

        public static bool IsOrganizationAdmin(this ClaimsPrincipal user)
        {
            var userOrganizationId = user.GetOrganizationId();
            return userOrganizationId.HasValue && user.IsOrganizationAdmin(userOrganizationId.Value);
        }

        public static bool IsOrganizationAdmin(this ClaimsPrincipal user, int organizationId)
        {
            var userOrganizationId = user.GetOrganizationId();
            return user.IsUserType(UserType.SiteAdmin) ||
                  (user.IsUserType(UserType.OrgAdmin) && userOrganizationId.HasValue && userOrganizationId.Value == organizationId);
        }

        public static int? GetOrganizationId(this ClaimsPrincipal user)
        {
            int? result = null;
            var organizationIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Organization);
            if (organizationIdClaim != null)
            {
                int organizationId;
                if (int.TryParse(organizationIdClaim.Value, out organizationId))
                    result = organizationId;
            }

            return result;
        }
    }
}
