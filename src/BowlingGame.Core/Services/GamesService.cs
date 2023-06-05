using BowlingGame.Core.Domain.Definitions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Core.Services.Exceptions;
using FluentValidation;

namespace BowlingGame.Core.Services
{
    public class GamesService : IGamesService
    {
        private readonly IGamesRepository _repository;
        private readonly IValidator<Game> _validator;

        public GamesService(IGamesRepository repository, IValidator<Game> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Game> CreateAsync(Game game)
        {
            var result = await _validator.ValidateAsync(game);
            if (!result.IsValid)
            {
                throw new ModelValidationException(result.Errors);
            }

            var gameCreated = await _repository.CreateAsync(game);
            return gameCreated;
        }
    }
}
