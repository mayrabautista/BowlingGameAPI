using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Core.Domain.Abstractions
{
    public interface IGamesRepository
    {
        Task<IEnumerable<Game>> GetAsync();
        Task<Game> GetAsync(Guid id);
        Task<Game> CreateAsync(Game game);
        Task<Game> UpdateAsync(Game game);
    }
}
