using BowlingGame.Core.Domain.Definitions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame.Infraestructure.Mocks
{
    public class GamesRepositoryMock : IGamesRepository
    {
        private List<Game> Games = new List<Game>();
        public Task<Game> CreateAsync(Game game)
        {
            Games.Add(game);
            return Task.FromResult(game);
        }
    }
}
