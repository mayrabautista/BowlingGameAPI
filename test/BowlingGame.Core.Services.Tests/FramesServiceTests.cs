using BowlingGame.Core.Aplication.Services;
using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace BowlingGame.Core.Services.Tests
{
    public class FramesServiceTests
    {
        private FramesService _framesService;


        [SetUp]
        public void Init()
        {
            Mock<IFramesRepository> repositoryMock = new Mock<IFramesRepository>();
            Mock<IValidator<Frame>> validatorMock = new Mock<IValidator<Frame>>(MockBehavior.Strict);

            validatorMock
                .Setup(validator => validator.ValidateAsync(It.IsAny<Frame>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            repositoryMock
                .Setup(r => r.GetFramesAsync(It.IsAny<FrameFilter>()))
                .ReturnsAsync(new List<Frame>());

            _framesService = new FramesService(repositoryMock.Object, validatorMock.Object);
        }

        [Test]
        public async Task CalculateFrameScores_RegularFrames_UpdatesTheTotalScore()
        {
            Frame frame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 5,
                SecondRoll = 2,
                IsStrike = false,
                IsSpare = false,
            };

            Frame result = await _framesService.CalculateFrameScores(frame, new List<Frame>());
            Assert.That(result.TotalScore, Is.EqualTo(7));
        }

        [Test]
        public async Task CalculateFrameScores_StrikeFrames_UpdatesTheCurrentScore()
        {
            Frame frame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 10,
                SecondRoll = 0,
                IsStrike = true,
                IsSpare = false,
            };

            Frame result = await _framesService.CalculateFrameScores(frame, new List<Frame>());
            Assert.That(result.CurrentScore, Is.EqualTo(10));
        }

        [Test]
        public async Task CalculateFrameScores_StrikeFrames_SetsTheTotalScoreAsNull()
        {
            Frame frame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 10,
                SecondRoll = 0,
                IsStrike = true,
                IsSpare = false,
            };

            Frame result = await _framesService.CalculateFrameScores(frame, new List<Frame>());
            Assert.That(result.TotalScore, Is.EqualTo(null));
        }
    }
}
