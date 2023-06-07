using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Infrastructure.MSSql.Repositories
{
    public class GamesMSSqlRepository : IGamesRepository
    {
        public Task<Game> CreateAsync(Game game)
        {
            return Task.FromResult(game);
        }
    }
}
