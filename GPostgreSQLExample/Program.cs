using System;
using System.Linq;
using System.Threading.Tasks;
using GPostgreSQLExample.Repositories;
using GPostgreSQLExample.Repositories.Entities;
using GPostgreSQLExample.Repositories.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GPostgreSQLExample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await ExecuteMigrationsAndSeedingsAsync(host.Services);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task ExecuteMigrationsAndSeedingsAsync(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
                var builder = new DbContextOptionsBuilder<MainContext>();
                builder.UseNpgsql(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("Default"));
                var mainContext = new MainContext(builder.Options);
                IMainContextInitialization mainContextInitialization = new MainContextInitialization(mainContext);
                await mainContextInitialization.InitializationAsync(env);
            }
        }
    }
}