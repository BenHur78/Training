using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddLogging((bd) =>
            {
                bd.ClearProviders();
                bd.SetMinimumLevel(LogLevel.Trace);
                bd.AddDebug();
                //bd.AddProvider(new FileLoggerProvider(@"C:\Dev\"));
                bd.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Trace);
                bd.AddFilter("Default", LogLevel.Trace);
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
