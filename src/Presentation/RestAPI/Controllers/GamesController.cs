using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Presentation.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BowlingGame.Presentation.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _service;

        public GamesController(IGamesService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GameDto gameDto)
        {
            var model = gameDto.ToModel();
            var gameCreated = await _service.CreateAsync(model);
            gameDto.FromModel(gameCreated);
            return Created(nameof(CreateAsync), gameDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var games = await _service.GetAsync();
            return Ok(games.Select(x => GameDto.ReturnFromModel(x)));
        }
    }
}
