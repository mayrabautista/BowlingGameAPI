using BowlingGame.Core.Aplication.Services;
using BowlingGame.Core.Aplication.Validators;
using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DependencyInjectionConfiguration
    {
        private static IServiceCollection AddBowlingGameServices(this IServiceCollection services)
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
