using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace FINS.Core.Helpers
{
    public static class HttpHeaderExtensions
    {
        public static string GetValueFromHeader(this IHeaderDictionary headers, string key)
        {
            StringValues values;
            headers.TryGetValue(key, out values);
            return values.Any() ? values.FirstOrDefault() : string.Empty;
        }

        public static int GetOrgIdFromHeader(this IHeaderDictionary headers)
        {
            var orgId = headers.GetValueFromHeader("orgId");
            return string.IsNullOrEmpty(orgId) ? 0 : Convert.ToInt32(orgId);
        }
    }
}
