using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Infrastructure.MSSql.Repositories
{
    public class GamesMSSqlRepository : IGamesRepository
    {
        private List<Game> Games = new List<Game>();
        public Task<Game> CreateAsync(Game game)
        {
            Console.WriteLine("Hello from MSSql");
            Games.Add(game);
            return Task.FromResult(game);
        }
    }
}
