using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrapper.Configuration.Model;
using Scrapper.Services.Scrapp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ScrapperConsole
{
    public class Program
    {
        private readonly IScrapperService scrapperService;

        public Program(IScrapperService scrapperService)
        {
            this.scrapperService = scrapperService;
        }

        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Program mainProgram = host.Services.GetRequiredService<Program>();

            await mainProgram.RunAsync();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                        .ConfigureServices(ConfigureServices);
        }

        public static void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");

            services.AddScoped<IScrapperService, ScrapperService>();
            services.AddScoped<Program>();


            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{environmentName}.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

            services.Configure<Position>(configuration.GetSection("Position"));
        }

        public async Task RunAsync()
        {
            await scrapperService.RunAsync();
        }
    }
}
