using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FINS.Core.Configuration
{
    internal static class Options
    {
        internal static void LoadConfigurationOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection("Data:DefaultConnection"));
            services.Configure<EmailSettings>(configuration.GetSection("Email"));
            services.Configure<SampleDataSettings>(configuration.GetSection("SampleData"));
            services.Configure<GeneralSettings>(configuration.GetSection("General"));
            services.Configure<TwitterAuthenticationSettings>(configuration.GetSection("Authentication:Twitter"));
            services.Configure<TwilioSettings>(configuration.GetSection("Authentication:Twilio"));
            services.Configure<FinsPathsSettings>(configuration.GetSection("FinsPaths"));
        }
    }
}
