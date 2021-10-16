using System;
using GPostgreSQLExample.Repositories;
using GPostgreSQLExample.Repositories.Entities;
using GPostgreSQLExample.Repositories.ModelsAutoMapp;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoryConfigurationExtensions
    {
        /// <summary>
        /// Adds the repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="efConfigure">The ef configure.</param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
             Action<IServiceProvider, DbContextOptionsBuilder> efConfigure)
        {
            services.AddDbContext<MainContext>(efConfigure);
            services.AddAutoMapper(config =>
            {
                config.AddProfile<PlayersAutoMapp>();
            });
            return services.AddTransient<IPlayersRepository, PlayersRepository>();
        }
    }
}