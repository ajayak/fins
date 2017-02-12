using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.Variance;
using FINS.DataAccess;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FINS.Configuration
{
    internal static class Services
    {
        internal static IContainer CreateIoCContainer(IServiceCollection services, IConfiguration configuration)
        {
            // todo: move these to a proper autofac module
            // Register application services.
            services.AddSingleton(x => configuration);
            services.AddTransient<SampleDataGenerator>();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterSource(new ContravariantRegistrationSource());
            containerBuilder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            containerBuilder.RegisterAssemblyTypes(typeof(Startup).GetTypeInfo().Assembly).AsImplementedInterfaces();
            containerBuilder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return c.TryResolve(t, out o) ? o : null;
                };
            });

            containerBuilder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            //Hangfire
            containerBuilder.Register(icomponentcontext => new BackgroundJobClient(new SqlServerStorage(configuration["Data:HangfireConnection:ConnectionString"])))
                .As<IBackgroundJobClient>();

            //auto-register Hangfire jobs by convention
            //http://docs.autofac.org/en/latest/register/scanning.html
            var assembly = Assembly.GetEntryAssembly();
            containerBuilder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.Namespace == "FINS.Hangfire.Jobs" && t.GetTypeInfo().IsInterface)
                .AsImplementedInterfaces();

            //Populate the container with services that were previously registered
            containerBuilder.Populate(services);

            var container = containerBuilder.Build();
            return container;
        }

    }
}
