using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DependencyInjectionConfiguration
    {
       public static IServiceCollection AddBowlingGame(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddBowlingGameServices();
            services.AddBowlingGameValidators();

            string? dbStrategy = configuration["DatabaseStrategy"];
            if(dbStrategy == "Sql")
            {
                services.AddBowlingGameMSSql(configuration);
            }
            else
            {
                services.AddBowlingGameMongo(configuration);
            }

            return services;
        }
    }
}