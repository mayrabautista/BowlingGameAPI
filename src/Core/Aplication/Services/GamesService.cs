﻿using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using FluentValidation;

namespace BowlingGame.Core.Aplication.Services
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
            await _validator.ValidateAndThrowAsync(game);
            var gameCreated = await _repository.CreateAsync(game);
            return gameCreated;
        }

        public async Task<IEnumerable<Game>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<Game> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }
    }
}
