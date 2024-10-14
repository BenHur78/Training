using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RibbonControl_Ex
{
    public class WebApiInitializer
    {
        private IHost _webApiHost;

        public WebApiInitializer()
        {
        }

        public void InitWebApi()
        {
            //var builder1 = WebApplication.CreateBuilder();
            //builder.Environment.EnvironmentName = "Development";
            //builder.Logging.ClearProviders(); //did not worked
            //builder.Logging.AddConsole();


            var builder = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.ListenLocalhost(18081, o => o.Protocols =
                        HttpProtocols.Http1AndHttp2);
                });

                webBuilder.UseStartup<Startup>();

            });

            var app = builder.Build();
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
