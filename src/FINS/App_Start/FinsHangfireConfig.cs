using FINS.Hangfire;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FINS
{
    public static class FinsHangfireConfig
    {
        public static IServiceCollection ConfigureHangfire(this IServiceCollection services, string connectionString, bool isTest)
        {
            if (isTest)
            {
                services.AddHangfire(configuration =>
                {
                    configuration.UseMemoryStorage();
                });
            }
            else
            {
                services.AddHangfire(configuration =>
                {
                    configuration.UseSqlServerStorage(connectionString);
                });
            }
            return services;
        }

        public static IApplicationBuilder UseFinsHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions { Authorization = new[] { new HangireDashboardAuthorizationFilter() } });
            app.UseHangfireServer();
            return app;
        }
    }
}
