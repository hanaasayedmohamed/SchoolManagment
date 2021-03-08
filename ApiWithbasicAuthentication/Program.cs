using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithbasicAuthentication
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
                }).UseSerilog((context, configureLogger) =>
                {
                    configureLogger
                        .ReadFrom.Configuration(context.Configuration)
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                        .MinimumLevel.Override("System", LogEventLevel.Error)
                        .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")));
                });
    }
}
