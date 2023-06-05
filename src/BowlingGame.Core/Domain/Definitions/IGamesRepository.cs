using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Core.Domain.Definitions
{
    public interface IGamesRepository
    {
        Task<Game> CreateAsync(Game game);
    }
}
