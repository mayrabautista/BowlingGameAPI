using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Core.Domain.Abstractions
{
    public interface IGamesService
    {
        Task<Game> CreateAsync(Game game);
        Task<IEnumerable<Game>> GetAsync();
    }
}
