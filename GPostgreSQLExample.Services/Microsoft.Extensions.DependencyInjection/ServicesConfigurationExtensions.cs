using GPostgreSQLExample.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesConfigurationExtensions
    {
        /// <summary>
        /// Adds the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddTransient<IPlayersService, PlayersService>();
        }
    }
}