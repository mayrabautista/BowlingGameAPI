using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Infrastructure.Mongo.Repositories
{
    public class GamesMongoRepository : IGamesRepository
    {
        public Task<Game> CreateAsync(Game game)
        {
            Console.WriteLine("Hello from mongo repository");
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
