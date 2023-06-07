using BowlingGame.Core.Aplication.Services;
using BowlingGame.Core.Aplication.Validators;
using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Infrastructure.Mongo.Interfaces;
using BowlingGame.Infrastructure.Mongo.Models;
using BowlingGame.Infrastructure.Mongo.Repositories;
using BowlingGame.Infrastructure.MSSql;
using BowlingGame.Infrastructure.MSSql.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInyectionConfiguration
    {
       public static IServiceCollection AddBowlingGame(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddBowlingGameServices();
            string? dbStrategy = configuration["DatabaseStrategy"];
            if(dbStrategy == "Sql")
            {
                services.AddBowlingGameSQLRepositories();
                string? dbConection = configuration["SqlDbSettings:ConnectionString"];
                services.AddDbContext<BowlingGameContext>(options => options.UseSqlServer(dbConection));
            }
            else
            {
                services.AddBowlingGameNoSQLRepositories();
                services.Configure<MongoDBSettings>(configuration.GetSection(nameof(MongoDBSettings)));
                services.AddSingleton<IMongoDBSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
                services.AddSingleton<IMongoClient>(s => new MongoClient(configuration["MongoDBSettings:ConnectionString"]));
            }

            services.AddBowlingGameValidators();
            return services;
        }


        private static IServiceCollection  AddBowlingGameServices(this IServiceCollection services)
        {
            services.AddScoped<IGamesService, GamesService>();
            services.AddScoped<IFramesService, FramesService>();
            return services;
        }
        
        private static IServiceCollection AddBowlingGameSQLRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGamesRepository, GamesMSSqlRepository>();
            services.AddScoped<IFramesRepository, FramesMSSqlRepository>();

            return services;
        }

        private static IServiceCollection AddBowlingGameNoSQLRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGamesRepository, GamesMongoRepository>();
            services.AddScoped<IFramesRepository, FramesMongoRepository>();
            return services;
        }

        private static IServiceCollection AddBowlingGameValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Game>, GameValidator>();
            services.AddScoped<IValidator<Frame>, FrameValidator>();
            return services;
        }
    }
}