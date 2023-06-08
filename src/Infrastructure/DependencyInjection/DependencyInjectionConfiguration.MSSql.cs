using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Infrastructure.MSSql;
using BowlingGame.Infrastructure.MSSql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DependencyInjectionConfiguration
    {

        private static IServiceCollection AddBowlingGameMSSql(this IServiceCollection services, IConfiguration configuration)
        {
            string? dbConection = configuration["SqlDbSettings:ConnectionString"];
            services.AddDbContext<BowlingGameContext>(options => options.UseSqlServer(dbConection));
            services.AddBowlingGameMSSqlRepositories();
            return services;
        }

        private static IServiceCollection AddBowlingGameMSSqlRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGamesRepository, GamesMSSqlRepository>();
            services.AddScoped<IFramesRepository, FramesMSSqlRepository>();
            return services;
        }
    }
}
