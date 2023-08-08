using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HPPMDotNetCore.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Serilog Example
            //var configuration = new ConfigurationBuilder()
            //           .SetBasePath(Directory.GetCurrentDirectory())
            //           .AddJsonFile("appsettings.json")
            //           .Build();
            //string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/HPPMDotNetCore.log");
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.Console()
            //    .WriteTo.File(logPath, rollingInterval: RollingInterval.Hour)
            //    .WriteTo.MSSqlServer(
            //        connectionString: configuration.GetSection("Serilog:ConnectionStrings:LogDatabase").Value,
            //        tableName: configuration.GetSection("Serilog:TableName").Value,
            //        appConfiguration: configuration,
            //        autoCreateSqlTable: true,
            //        columnOptionsSection: configuration.GetSection("Serilog:ColumnOptions"),
            //        schemaName: configuration.GetSection("Serilog:SchemaName").Value)
            //    .CreateLogger();
            //Log.Information("Starting web application");
            #endregion

            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/HppmLogs.log");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Console()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Hour)
                .CreateLogger();
               
            Log.Information("Welcome to web logging");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
