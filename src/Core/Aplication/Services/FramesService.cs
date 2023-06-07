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

        public Task<Frame> CreateAsync(Frame frame)
        {
            var validationResult = _validator.Validate(frame);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(nameof(frame));
            }

            var frameCreated = _repository.CreateAsync(frame);
            return frameCreated;
        }

        public async Task<Frame> UpdateScoresFromLastFrame(Frame frame)
        {
            var filter = new FrameFilter()
            {
                GameId = frame.GameId,
                IndexFrom = frame.Index - 2,
                IndexTo = frame.Index,
            };

            List<Frame> previousFrames = await _repository.GetAsync(filter);
            Frame? previousFrame = previousFrames.FirstOrDefault(x => x.Index == frame.Index - 1);
            Frame? twoFramesAgo = previousFrames.FirstOrDefault(x => x.Index == frame.Index - 2);
            CalculateScore(ref frame, ref previousFrame, ref twoFramesAgo);
            await _repository.UpdateAsync(frame);

            if (previousFrame is not null)
            {
                await _repository.UpdateAsync(previousFrame);
            }

            if (twoFramesAgo is not null)
            {
                await _repository.UpdateAsync(twoFramesAgo);
            }

            return frame;
        }

        public void CalculateScore(
        ref Frame frame,
        ref Frame? previousFrame,
        ref Frame? twoFramesAgo)
        {
            if (frame.Index == 1)
            {
                frame.TotalScore = frame.FirstRoll + frame.SecondRoll;
                return;
            }

            if (previousFrame is not null)
            {
                if (previousFrame.IsStrike)
                {
                    previousFrame.TotalScore += frame.FirstRoll + frame.SecondRoll;

                    if (twoFramesAgo is not null)
                    {
                        if (twoFramesAgo.IsStrike)
                        {
                            twoFramesAgo.TotalScore += frame.FirstRoll;
                        }
                    }
                }
                else if (previousFrame.IsSpare)
                {
                    previousFrame.TotalScore += frame.FirstRoll;

                    if (twoFramesAgo is not null)
                    {
                        if (twoFramesAgo.IsStrike)
                        {
                            twoFramesAgo.TotalScore += frame.FirstRoll;
                        }
                    }
                }

                frame.TotalScore = frame.FirstRoll + frame.SecondRoll + previousFrame.TotalScore;
                if (frame.Index == 10 && frame.IsStrike)
                {
                    frame.TotalScore += frame.ThirdRoll;
                }
            }
        }
    }
}