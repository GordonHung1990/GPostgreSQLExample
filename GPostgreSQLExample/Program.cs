using System;
using System.Linq;
using System.Threading.Tasks;
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
            var configBuilder = new ConfigurationBuilder();
            configBuilder
                .AddJsonFile("appsettings.json");
            var config = configBuilder.Build();

            var builder = new DbContextOptionsBuilder<MainContext>();
            var connectionString = config.GetConnectionString("Default");

            builder.UseNpgsql(connectionString);

            using (var context = new MainContext(builder.Options))
            {
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                {
                    Console.WriteLine($"You have {pendingMigrations.Count()} pending migrations to apply.");
                    Console.WriteLine("Applying pending migrations now");
                    await context.Database.MigrateAsync();
                }

                var lastAppliedMigration = (await context.Database.GetAppliedMigrationsAsync()).Last();

                Console.WriteLine($"You're on schema version: {lastAppliedMigration}");
            }
        }
    }
}