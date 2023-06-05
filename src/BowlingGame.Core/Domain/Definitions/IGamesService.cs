using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Core.Domain.Definitions
{
    public interface IGamesService
    {
        Task<Game> CreateAsync(Game game);
    }
}
