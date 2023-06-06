
using BowlingGame.Core.Domain.Definitions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Core.Services.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace BowlingGame.Core.Services.Tests
{
    public class GamesServiceTests
    {
        [Test]
        public async Task CreateAsync_ValidGame_ReturnsTheSameGameCreated()
        {
            Mock<IGamesRepository> repositoryMock = new Mock<IGamesRepository>();
            Mock<IValidator<Game>> validatorMock = new Mock<IValidator<Game>>(MockBehavior.Strict);

            Game game = new Game
            {
                PlayerName = "Jhon",
            };

            validatorMock
                .Setup(validator => validator.ValidateAsync(game, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            repositoryMock
                .Setup(r => r.CreateAsync(game))
                .ReturnsAsync(game);

            GamesService service = new GamesService(repositoryMock.Object, validatorMock.Object);
         
            Game result = await service.CreateAsync(game);
            Assert.That(result.Id, Is.EqualTo(game.Id));
        }

        [Test]
        public void CreateAsync_InvalidGame_ThrowsModelValidationException()
        {
            Mock<IGamesRepository> repositoryMock = new Mock<IGamesRepository>();
            Mock<IValidator<Game>> validatorMock = new Mock<IValidator<Game>>(MockBehavior.Strict);

            Game game = new Game();

            validatorMock
                .Setup(validator => validator.ValidateAsync(game, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult() { Errors = new List<ValidationFailure> { new ValidationFailure() } });
            repositoryMock
                .Setup(r => r.CreateAsync(game))
                .ReturnsAsync(game);

            GamesService service = new(repositoryMock.Object, validatorMock.Object);
            Assert.ThrowsAsync<ModelValidationException>(async () => await service.CreateAsync(game));
        }
    }
}
