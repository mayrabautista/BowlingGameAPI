using BowlingGame.Core.Aplication.Services;
using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using FluentValidation;

namespace BowlingGame.Core.Services.Tests
{
    public class FramesServiceTests
    {
        private FramesService _framesService;


        [SetUp]
        public void Init()
        {
            Mock<IFramesRepository> repositoryMock = new Mock<IFramesRepository>();
            Mock<IGamesRepository> gamesRepositoryMock = new Mock<IGamesRepository>();

            Mock<IValidator<Frame>> validatorMock = new Mock<IValidator<Frame>>(MockBehavior.Strict);

            repositoryMock
                .Setup(r => r.GetAsync(It.IsAny<FrameFilter>()))
                .ReturnsAsync(new List<Frame>());

            _framesService = new FramesService(repositoryMock.Object, gamesRepositoryMock.Object, validatorMock.Object);
        }

        [Test]
        public void CalculateScore_FirstRegularFrame_UpdatesTheTotalScore()
        {
            Frame frame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 5,
                SecondRoll = 2,
                Index = 1,
                IsStrike = false,
                IsSpare = false,
            };

            Frame? previousFrame = null;
            Frame? twoFramesAgo = null;


            _framesService.CalculateScore(
                ref frame,
                ref previousFrame,
                ref twoFramesAgo);

            Assert.That(frame.TotalScore, Is.EqualTo(7));
            Assert.Multiple(() =>
            {
                Assert.That(previousFrame, Is.EqualTo(null));
                Assert.That(twoFramesAgo, Is.EqualTo(null));
            });
        }

        [Test]
        public void CalculateScore_RegularFrame_RegularPreviousFrames_UpdatesTheTotalRegularFrameNoChangesPreviousFrame()
        {
            Frame frame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 5,
                SecondRoll = 1,
                Index = 2,
                IsStrike = false,
                IsSpare = false,
            };

            Frame? previousFrame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 5,
                SecondRoll = 2,
                Index = 1,
                TotalScore = 7,
                IsStrike = false,
                IsSpare = false,
            };

            Frame? twoFramesAgo = null;


            _framesService.CalculateScore(
                ref frame,
                ref previousFrame,
                ref twoFramesAgo);

            Assert.That(frame.TotalScore, Is.EqualTo(13));
            Assert.That(previousFrame?.TotalScore, Is.EqualTo(7));
            Assert.That(twoFramesAgo, Is.EqualTo(null));
        }

        [Test]
        public void CalculateScore_RegularFrame_SparePreviousFrame_UpdatesBothTotalScores()
        {
            Frame frame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 5,
                SecondRoll = 1,
                Index = 2,
                IsStrike = false,
                IsSpare = false,
            };

            Frame? previousFrame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 5,
                SecondRoll = 5,
                Index = 1,
                TotalScore = 10,
                IsStrike = false,
                IsSpare = true,
            };

            Frame? twoFramesAgo = null;


            _framesService.CalculateScore(
                ref frame,
                ref previousFrame,
                ref twoFramesAgo);

            Assert.That(frame.TotalScore, Is.EqualTo(21));
            Assert.That(previousFrame?.TotalScore, Is.EqualTo(15));
            Assert.That(twoFramesAgo, Is.EqualTo(null));
        }

        [Test]
        public void CalculateScore_RegularFrame_StrikePreviousFrame_UpdatesBothTotalScores()
        {
            Frame frame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 5,
                SecondRoll = 1,
                Index = 2,
                IsStrike = false,
                IsSpare = false,
            };

            Frame? previousFrame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 10,
                SecondRoll = 0,
                Index = 1,
                TotalScore = 10,
                IsStrike = true,
            };

            Frame? twoFramesAgo = null;


            _framesService.CalculateScore(
                ref frame,
                ref previousFrame,
                ref twoFramesAgo);

            Assert.That(frame.TotalScore, Is.EqualTo(22));
            Assert.That(previousFrame?.TotalScore, Is.EqualTo(16));
            Assert.That(twoFramesAgo, Is.EqualTo(null));
        }

        [Test]
        public void CalculateScore_RegularFrame_SparePreviousFrame_SpareTwoFramesAgo_UpdatesPreviousAndCurrentTotalScores()
        {
            Frame frame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 5,
                SecondRoll = 1,
                Index = 3,
                IsStrike = false,
                IsSpare = false,
            };

            Frame? previousFrame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 6,
                SecondRoll = 4,
                Index = 2,
                TotalScore = 26,
                IsSpare = true,
            };

            Frame? twoFramesAgo = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 5,
                SecondRoll = 5,
                Index = 1,
                TotalScore = 10,
                IsSpare = true,
            }; 


            _framesService.CalculateScore(
                ref frame,
                ref previousFrame,
                ref twoFramesAgo);

            Assert.That(frame.TotalScore, Is.EqualTo(37));
            Assert.That(previousFrame?.TotalScore, Is.EqualTo(31));
            Assert.That(twoFramesAgo?.TotalScore, Is.EqualTo(10));
        }

        [Test]
        public void CalculateScore_StrikeFrame_StrikePreviousFrame_StrikeTwoFramesAgo_UpdatesTotalScores()
        {
            Frame frame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 10,
                SecondRoll = 0,
                Index = 3,
                IsStrike = true,
            };

            Frame? previousFrame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 10,
                SecondRoll = 0,
                Index = 2,
                TotalScore = 30,
                IsStrike = true,
            };

            Frame? twoFramesAgo = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 10,
                SecondRoll = 0,
                Index = 1,
                TotalScore = 20,
                IsStrike = true,
            };


            _framesService.CalculateScore(
                ref frame,
                ref previousFrame,
                ref twoFramesAgo);

            Assert.That(frame.TotalScore, Is.EqualTo(60));
            Assert.That(previousFrame?.TotalScore, Is.EqualTo(50));
            Assert.That(twoFramesAgo?.TotalScore, Is.EqualTo(30));
        }

        [Test]
        public void CalculateScore_LastStrikeFrame_StrikePreviousFrame_StrikeTwoFramesAgo_UpdatesTotalScoresToPerfectGame()
        {
            Frame frame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 10,
                SecondRoll = 10,
                ThirdRoll = 10,
                Index = 10,
                IsStrike = true,
            };

            Frame? previousFrame = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 10,
                SecondRoll = 0,
                Index = 9,
                TotalScore = 240,
                IsStrike = true,
            };

            Frame? twoFramesAgo = new Frame
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 10,
                SecondRoll = 0,
                Index = 8,
                TotalScore = 230,
                IsStrike = true,
            };


            _framesService.CalculateScore(
                ref frame,
                ref previousFrame,
                ref twoFramesAgo);

            Assert.That(frame.TotalScore, Is.EqualTo(300));
            Assert.That(previousFrame?.TotalScore, Is.EqualTo(270));
            Assert.That(twoFramesAgo?.TotalScore, Is.EqualTo(240));
        }
    }
}
