using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApi
{
    public class WebApiInitializer
    {
        private IHost _webApiHost;

        public WebApiInitializer()
        {
        }

        public void InitWebApi()
        {
            var builder = WebApplication.CreateBuilder();
            //builder.Environment.EnvironmentName = "Development";
            //builder.Logging.ClearProviders(); //did not worked
            //builder.Logging.AddConsole();


            //var builder = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
            //{
            //    webBuilder.ConfigureKestrel(serverOptions =>
            //    {
            //        serverOptions.ListenLocalhost(18081, o => o.Protocols =
            //            HttpProtocols.Http1AndHttp2);
            //    });

            //    webBuilder.UseStartup<Startup>();

            //});

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenLocalhost(18081, o => o.Protocols =
                    HttpProtocols.Http1AndHttp2);
            });

            builder.Services.AddLogging((bd) =>
            {
                bd.SetMinimumLevel(LogLevel.Trace);
                //bd.ClearProviders();
                bd.AddDebug();
                bd.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Trace);
                bd.AddFilter("Default", LogLevel.Trace);
            });

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddApplicationPart(typeof(HomeController).Assembly);

            var app = builder.Build();

            app.UseRouting();

            //app.MapControllers();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            _webApiHost = app;

            try
            {
                _webApiHost.Start();

                var loggerFactory = (ILoggerFactory)_webApiHost.Services.GetService(typeof(ILoggerFactory));
                var logger = loggerFactory.CreateLogger<WebApiInitializer>();

                //Now both are working
                logger.LogCritical("Critical World");
                logger.LogDebug("Debug World");
                logger.LogInformation("Hello World");
                logger.LogTrace("Trace World");
                logger.LogWarning("Warning World");
                logger.LogError("Error World");

                //if (_webApiHost.Environment.IsDevelopment())
                //{
                //    logger.LogInformation("Environment is development.");
                //}
            }
            catch (Exception e)
            {
                _webApiHost = null;
            }

        }
    }
}
