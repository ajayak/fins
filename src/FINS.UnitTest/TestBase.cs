using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using FINS.Context;
using Microsoft.Extensions.PlatformAbstractions;

namespace FINS.UnitTest
{
    /// <summary>
    /// Inherit from this type to implement tests that
    /// have access to a service provider, empty in-memory
    /// database, and basic configuration.
    /// </summary>
    public abstract class TestBase
    {
        protected TestBase()
        {
            if (ServiceProvider == null)
            {
                var path = PlatformServices.Default.Application.ApplicationBasePath;
                var setDir = Path.GetFullPath(Path.Combine(path, "../../../../FINS"));

                var builder = new WebHostBuilder()
                    .UseContentRoot(setDir)
                    .UseEnvironment("Test")
                    .UseKestrel()
                    .UseStartup<Startup>();
                var host = builder.Build();
                ServiceProvider = host.Services;
            }
        }

        protected IServiceProvider ServiceProvider { get; }

        // https://docs.efproject.net/en/latest/miscellaneous/testing.html
        protected DbContextOptions<FinsDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<FinsDbContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}