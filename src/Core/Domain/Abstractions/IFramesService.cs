using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Core.Domain.Abstractions
{
    public interface IFramesService
    {
        Task<Frame> CalculateFrameScores(Frame frame, List<Frame> frames);
    }
}
