using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.FileProviders;

namespace FINS.Core.Middlewares
{
    public static class FinsStaticResources
    {
        public static IApplicationBuilder UseFinsStaticResources(this IApplicationBuilder app, HostingEnvironment env)
        {
            // this allows for serving up contents in a folder named 'content'
            var path = Path.Combine(env.ContentRootPath, "content");
            var provider = new PhysicalFileProvider(path);

            var options = new StaticFileOptions
            {
                RequestPath = "/content",
                FileProvider = provider
            };
            app.UseStaticFiles(options);
            return app;
        }
    }
}
