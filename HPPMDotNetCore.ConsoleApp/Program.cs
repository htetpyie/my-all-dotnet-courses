using HPPMDotNetCore.ConsoleApp.AdoDotNetCodeExample;
using HPPMDotNetCore.ConsoleApp.RepoDBCodeExample;
//using HPPMDotNetCore.ConsoleApp.DapperCodeExample;
using HPPMDotNetCore.ConsoleApp.RepositoryCodeExample;
using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using HPPMDotNetCore.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using RepoDb;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using HPPMDotNetCore.ConsoleApp.HttpClientCodeExample;
using HPPMDotNetCore.ConsoleApp.FileCodeExample;
using Refit;
using HPPMDotNetCore.ConsoleApp.RestClientCodeExample;
using Serilog;
using HPPMDotNetCore.ConsoleApp.RetfitClientCodeExample;

namespace HPPMDotNetCore.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/HPPMDotNetCore.log");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            Log.Information("Hello, world!");

            int a = 10, b = 0;
            try
            {
                Log.Debug("Dividing {A} by {B}", a, b);
                Console.WriteLine(a / b);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            Console.WriteLine("Hello World!");
            Console.Write("Press any key to continue...");
            Console.ReadKey();

            //AdoDotNetExample.Run();

            //await DapperExample.RunAsync();

            //await new RepositoryExample().RunAsync();

            // new RepoDBExample().RunAsync();

            // Directory.CreateDirectory("FileExamples");
            // string path = @"FileExamples\test.txt";
            // string content = "Example content as a string message";
            // File.WriteAllText(path, content);
            // Directory.Delete("FileExamples", true);

            //await new HttpClientExample().RunAsync();

            //await new RestClientExample().RunAsync();

            await new RefitClientExample().RunAsync();

            //FileExample.Run();

            Console.ReadKey();
        }
    }
}
