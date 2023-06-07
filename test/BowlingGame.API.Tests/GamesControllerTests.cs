using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Presentation.RestAPI.Controllers;
using BowlingGame.Presentation.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BowlingGame.API.Tests
{
    public class GamesControllerTests
    {
        [Test]
        public async Task CreateAsync_ValidGameDto_ReturnsCreatedResult()
        {
            Mock<IGamesService> serviceMock = new Mock<IGamesService>();
         
            GameDto gameDto = new GameDto
            {
                PlayerName = "Jhon",
            };

            serviceMock
                .Setup(r => r.CreateAsync(It.IsAny<Game>()))
                .ReturnsAsync((Game m) => m);

            GamesController gameController = new GamesController(serviceMock.Object);
            var result = await gameController.CreateAsync(gameDto);
            Assert.That( result, Is.InstanceOf(typeof(CreatedResult)));
        }

        [Test]
        public async Task CreateAsync_InvalidGame_BadRequestObjectResult()
        {
            Mock<IGamesService> serviceMock = new Mock<IGamesService>();

            GameDto gameDto = new GameDto
            {
                PlayerName = "Jhon",
            };

            var exception = new ArgumentException();
            serviceMock
                .Setup(r => r.CreateAsync(It.IsAny<Game>()))
                .ThrowsAsync(exception);

            GamesController gameController = new GamesController(serviceMock.Object);
            var result = await gameController.CreateAsync(gameDto);
            Assert.That(result, Is.InstanceOf(typeof(BadRequestObjectResult)));
        }
    }
}
