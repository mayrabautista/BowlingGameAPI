using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Core.Domain.Abstractions
{
    public interface IGamesRepository
    {
        Task<Game> CreateAsync(Game game);
    }
}
