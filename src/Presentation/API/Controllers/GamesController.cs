using BowlingGame.API.Models;
using BowlingGame.Core.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BowlingGame.API.Controllers
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
            try
            {
                var model = gameDto.ToModel();
                var gameCreated = await _service.CreateAsync(model);
                gameDto.FromModel(gameCreated);
                return Created(nameof(CreateAsync), gameDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
