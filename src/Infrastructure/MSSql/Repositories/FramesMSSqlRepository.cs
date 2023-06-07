using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Infrastructure.MSSql.Repositories
{
    public class FramesMSSqlRepository : IFramesRepository
    {
        public Task<Frame> CreateAsync(Frame frame)
        {
            return Task.FromResult(frame);
        }

        public Task<List<Frame>> GetAsync(FrameFilter? framefilter)
        {
            return Task.FromResult(new List<Frame>());
        }

        public Task<Frame?> GetById(Guid id)
        {
           return null;
        }

        public Task<Frame> UpdateAsync(Frame frame)
        {
            return Task.FromResult(frame);
        }
    }
}
