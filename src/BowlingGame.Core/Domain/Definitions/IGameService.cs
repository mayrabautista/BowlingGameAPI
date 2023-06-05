using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Core.Domain.Definitions
{
    public interface IGameService
    {
        Task<Game> CreateAsync(Game game);
    }
}
