using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Presentation.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BowlingGame.Presentation.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _service;
        private IMemoryCache _memoryCache;
        public GamesController(IGamesService service, IMemoryCache memoryCache)
        {
            _service = service;
            _memoryCache = memoryCache;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var gameDto = new GameDto();
            
            if(_memoryCache.TryGetValue($"Game_{id}", out gameDto))
            {
                return Ok(gameDto);
            }
            
            var game = await _service.GetAsync(Guid.Parse(id));
            gameDto = GameDto.ReturnFromModel(game);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(5));
            _memoryCache.Set($"Game_{id}", gameDto, cacheEntryOptions);
                
            return Ok(gameDto);
        }
    }
}
