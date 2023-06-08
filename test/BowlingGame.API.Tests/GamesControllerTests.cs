using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Presentation.RestAPI.Controllers;
using BowlingGame.Presentation.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BowlingGame.API.Tests
{
    public class GamesControllerTests
    {
        [Test]
        public async Task CreateAsync_ValidGameDto_ReturnsCreatedResult()
        {
            Mock<IGamesService> serviceMock = new Mock<IGamesService>();
            Mock<IMemoryCache> memoryCache = new Mock<IMemoryCache>();

            GameDto gameDto = new GameDto
            {
                PlayerName = "Jhon",
            };

            serviceMock
                .Setup(r => r.CreateAsync(It.IsAny<Game>()))
                .ReturnsAsync((Game m) => m);

            GamesController gameController = new GamesController(serviceMock.Object, memoryCache.Object);
            var result = await gameController.CreateAsync(gameDto);
            Assert.That( result, Is.InstanceOf(typeof(CreatedResult)));
        }
    }
}
