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
    public static partial class DependencyInjectionConfiguration
    {
       public static IServiceCollection AddBowlingGame(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddBowlingGameServices();
            string? dbStrategy = configuration["DatabaseStrategy"];
            if(dbStrategy == "Sql")
            {
                services.AddBowlingGameMSSql(configuration);
            }
            else
            {
                services.AddBowlingGameMongo(configuration);
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
    
        private static IServiceCollection AddBowlingGameValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Game>, GameValidator>();
            services.AddScoped<IValidator<Frame>, FrameValidator>();
            return services;
        }
    }
}