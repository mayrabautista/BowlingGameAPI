using BowlingGame.API.Models;
using BowlingGame.Core.Domain.Definitions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Core.Services.Exceptions;
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
            Result result;
            try
            {
                var model = gameDto.ToModel();
                var gameCreated = await _service.CreateAsync(model);
                gameDto.FromModel(gameCreated);
                result = new Result(gameDto);
                return Created(nameof(CreateAsync), result);
            }
            catch (ModelValidationException ex)
            {
                result = new (ex.ValidationErrors);
                return BadRequest(result);
            }
        }
    }
}
