using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPostgreSQLExample.Repositories.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GPostgreSQLExample.Repositories
{
    public class MainContextInitialization : IMainContextInitialization
    {
        private readonly MainContext _mainContext;

        public MainContextInitialization(MainContext mainContext)
        {
            _mainContext = mainContext ?? throw new ArgumentNullException(nameof(mainContext));
        }
        public async ValueTask InitializationAsync(IWebHostEnvironment env)
        {

            var isMigrate = (await _mainContext.Database.GetPendingMigrationsAsync()).Any();

            if (isMigrate)
            {
                Console.WriteLine($"Applying pending migrations now.");
                await _mainContext.Database.MigrateAsync();
            }

            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {

            }


            var lastAppliedMigration = (await _mainContext.Database.GetAppliedMigrationsAsync()).Last();

            Console.WriteLine($"You're on schema version: {lastAppliedMigration}");
        }
    }
}
