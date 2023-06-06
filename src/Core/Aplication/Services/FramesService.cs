using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using FluentValidation;

namespace BowlingGame.Core.Aplication.Services
{
    public class FramesService : IFramesService
    {
        private readonly IFramesRepository _repository;
        private readonly IValidator<Frame> _validator;

        public FramesService(IFramesRepository repository, IValidator<Frame> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Frame> CalculateFrameScores(Frame frame, List<Frame> frames)
        {
            FrameFilter frameFilter = new FrameFilter()
            {
                GameId = frame.GameId,
            };

            if (frame.IsSpare)
            {
                frame.CurrentScore = 10;
                frame.TotalScore = 0;
            }

            if (frame.IsStrike)
            {
                frame.CurrentScore = 10;
                frame.TotalScore = 0;
            }

            frame.TotalScore = frame.FirstRoll + frame.SecondRoll;
            return frame;
        }
    }
}
