
using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Presentation.RestAPI.Controllers;
using BowlingGame.Presentation.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BowlingGame.API.Tests
{
    public  class FramesControllerTests
    {
        [Test]
        public async Task CreateAsync_ValidFrameDto_ReturnsCreatedResult()
        {
            Mock<IFramesService> serviceMock = new Mock<IFramesService>();

            FrameDto frameDto = new FrameDto
            {
                GameId = Guid.NewGuid(),
                FirstRoll = 5,
                SecondRoll = 2,
                Index = 1,
                IsStrike = false,
                IsSpare = false,
            };

            serviceMock
                .Setup(r => r.CreateAsync(It.IsAny<Frame>()))
                .ReturnsAsync((Frame m) => m);

            serviceMock
                .Setup(r => r.UpdateScoresFromLastFrame(It.IsAny<Frame>()))
                .ReturnsAsync((Frame m) => m);

            FramesController frameController = new FramesController(serviceMock.Object);
            var result = await frameController.CreateAsync(frameDto);
            Assert.That(result, Is.InstanceOf(typeof(CreatedResult)));
        }

    }
}
