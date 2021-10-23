using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrapper.Services.Scrapp;
using System;
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<IScrapperService, ScrapperService>();
                services.AddScoped<Program>();
            });

        public async Task RunAsync()
        {
            await scrapperService.RunAsync();
        }
    }
}
