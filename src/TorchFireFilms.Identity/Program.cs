using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;

namespace TorchFireFilms.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((context, logger) =>
                {
                    var isDevelopment = context.HostingEnvironment.IsDevelopment();
                    logger.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
                    logger.Enrich.FromLogContext();
                    if (isDevelopment)
                        logger.WriteTo.Console();
                    logger.WriteTo.File(new RenderedCompactJsonFormatter(),
                        isDevelopment ? @".\log\log-.txt" : @"./log/log-.txt",
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: null,
                        rollOnFileSizeLimit: true);
                });
    }
}
