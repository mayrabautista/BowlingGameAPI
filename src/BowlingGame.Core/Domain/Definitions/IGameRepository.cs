using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Core.Domain.Definitions
{
    public interface IGameRepository
    {
        Task<Game> CreateAsync(Game game);
    }
}
