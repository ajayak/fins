using System;
using Autofac;
using FINS.Context;
using FINS.Core;
using FINS.Core.AutoMap;
using FINS.Core.Configuration;
using FINS.Core.DataAccess;
using FINS.Core.Middlewares;
using FINS.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace FINS
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            else if (env.IsStaging() || env.IsProduction())
            {
                builder.AddApplicationInsightsSettings(developerMode: false);
            }

            // configure serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.LiterateConsole()
                .WriteTo.RollingFile("logs/log-{Date}.txt")
                .CreateLogger();

            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add CORS support.
            // Must be first to avoid OPTIONS issues when calling from Angular/Browser
            services.AddCors(options =>
            {
                options.AddPolicy("FINS", CorsPolicyFactory.BuildFinsOpenCorsPolicy());
            });

            // Add Application Insights data collection services to the services container.
            services.AddApplicationInsightsTelemetry(Configuration);

            // Add Entity Framework services to the services container.
            services.SetupFinsDbContext(Configuration["Data:DefaultConnection:ConnectionString"], HostingEnvironment.IsEnvironment("Test"));

            Options.LoadConfigurationOptions(services, Configuration);

            // Register the Identity services.
            services.ConfigureFinsIdentity();

            // Add Authorization rules for the app
            services.AddSecurityPolicies();

            services.AddMemoryCache();

            services.AddResponseCompression();

            // Add MVC services to the services container.
            services.AddMvc()
                .AddJsonOptions(options =>
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.ConfigureFinsOpenIdConnect();

            ////Hangfire
            //services.ConfigureHangfire(Configuration["Data:HangfireConnection:ConnectionString"], HostingEnvironment.IsEnvironment("Test"));

            // configure IoC support
            var container = Core.Configuration.Services.CreateIoCContainer(services, Configuration);
            return container.Resolve<IServiceProvider>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, FinsDbContext context, SampleDataGenerator sampleData)
        {
            // Put first to avoid issues with OPTIONS when calling from Angular/Browser.  
            app.UseCors("FINS");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Initialize Automapper
            AutomapperConfig.InitializeAutoMapper();

            // Add Serilog to the logging pipeline
            loggerFactory.AddSerilog();

            // Add the following to the request pipeline only in development environment.
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else if (env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // sends the request to the following path or controller action.
                //TODO: create this page!
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMiddleware(typeof(FinsErrorHandler));

            app.UseIdentity();

            app.UseOpenIddict();

            // Add a middleware used to validate access
            // tokens and protect the API endpoints.
            app.UseOAuthValidation();

            //call Migrate here to force the creation of the FINS database so Hangfire can create its schema under it
            if (env.IsDevelopment())
            {
                context.Database.Migrate();
            }

            // GZIP Compression
            app.UseResponseCompression();

            ////Hangfire
            //app.UseFinsHangfire();

            // Fins static resource
            app.UseFinsStaticResources((HostingEnvironment)env);

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areaRoute", template: "{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Add sample data and test admin accounts if specified in Config.Json.
            // for production applications, this should either be set to false or deleted.
            if (Configuration["SampleData:InsertSampleData"] == "true" &&
                !env.IsEnvironment("Test"))
            {
                await sampleData.InsertDemoData();
            }
        }
    }
}
