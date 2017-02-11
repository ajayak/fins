using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FINS.Models;

namespace FINS.Security
{
    public static class ClaimsExtensions
    {
        public static bool IsUserType(this ClaimsPrincipal user, UserType type)
        {
            var userTypeString = Enum.GetName(typeof(UserType), type);
            return user.HasClaim(ClaimTypes.UserType, userTypeString);
        }
    }
}
