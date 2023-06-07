
using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Presentation.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BowlingGame.Presentation.RestAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FramesController : ControllerBase
    {
        private readonly IFramesService _service;

        public FramesController(IFramesService service)
        {
            _service = service;
        }

        [HttpPost("games/{Id}/frames")]
        public async Task<IActionResult> CreateAsync(FrameDto frameDto)
        {
            try
            {
                var model = frameDto.ToModel();
                var frameCreated = await _service.CreateAsync(model);
                var frameCreatedWithUpdatedScore = await _service.CreateAsync(frameCreated);
                frameDto.FromModel(frameCreatedWithUpdatedScore);
                return Created(nameof(CreateAsync), frameDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("games/{id}/frames")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var filter = new FrameFilter()
            {
                GameId = Guid.Parse(id),
            };
            var frames = await _service.GetAsync(filter);
            return Ok(frames);
        }
    }
}
