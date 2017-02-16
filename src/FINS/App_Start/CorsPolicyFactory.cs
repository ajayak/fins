using Microsoft.AspNetCore.Cors.Infrastructure;

namespace FINS.App_Start
{
    public static class CorsPolicyFactory
    {
        public static CorsPolicy BuildFinsOpenCorsPolicy()
        {
            var corsBuilder = new CorsPolicyBuilder();

            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            // we need to revisit this and build a whitelise of IP addresses
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            return corsBuilder.Build();
        }
    }
}
