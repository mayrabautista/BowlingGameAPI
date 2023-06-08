using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Infrastructure.Mongo.Interfaces;
using BowlingGame.Infrastructure.Mongo.Models;
using BowlingGame.Infrastructure.Mongo.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DependencyInjectionConfiguration
    {

        private static IServiceCollection AddBowlingGameMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(configuration.GetSection(nameof(MongoDBSettings)));
            services.AddSingleton<IMongoDBSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
            services.AddSingleton<IMongoClient>(s => new MongoClient(configuration["MongoDBSettings:ConnectionString"]));

            services.AddBowlingGameMongoRepositories();
            return services;
        }

        private static IServiceCollection AddBowlingGameMongoRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGamesRepository, GamesMongoRepository>();
            services.AddScoped<IFramesRepository, FramesMongoRepository>();

            return services;
        }
    }
}
