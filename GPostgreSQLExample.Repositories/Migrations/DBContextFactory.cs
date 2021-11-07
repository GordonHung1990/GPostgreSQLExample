using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GPostgreSQLExample.Repositories.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;


namespace GPostgreSQLExample.Repositories.Migrations
{
    public class DBContextFactory : IDesignTimeDbContextFactory<MainContext>
    {
        MainContext IDesignTimeDbContextFactory<MainContext>.CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder
                .AddJsonFile("appsettings.json");
            var config = configBuilder.Build();

            var builder = new DbContextOptionsBuilder<MainContext>();
            var connectionString = config.GetConnectionString("Default");

            builder.UseNpgsql(connectionString);

            return new MainContext(builder.Options);
        }
    }
}