using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using DBGame = BowlingGame.Infrastructure.MSSql.Models.DBGame;

namespace BowlingGame.Infrastructure.MSSql.Repositories
{
    public class GamesMSSqlRepository : IGamesRepository
    {
        private BowlingGameContext _context;

        public GamesMSSqlRepository(BowlingGameContext context)
        {
            _context = context;
        }

        public Task<Game> CreateAsync(Game game)
        {
            DBGame dbGame = new DBGame
            {
                Id = game.Id,
                TotalScore = game.TotalScore,
                PlayerName = game.PlayerName,
                Status = game.Status,
            };

            var result = _context.Games.Add(dbGame);
            _context.SaveChanges();

            if (result is not null)
            {
                return Task.FromResult(new Game
                {
                    Id = dbGame.Id,
                    TotalScore = dbGame.TotalScore,
                    PlayerName = dbGame.PlayerName,
                    Status = dbGame.Status,
                });
            }
            throw new ArgumentException(nameof(DBGame));
        }

        public async Task<IEnumerable<Game>> GetAsync()
        {
            return await _context.Games.Select(dbGame => new Game
            {
                Id = dbGame.Id,
                TotalScore = dbGame.TotalScore,
                PlayerName = dbGame.PlayerName,
                Status = dbGame.Status,
            }).ToListAsync();
        }

        public async Task<Game> GetAsync(Guid id)
        {
            var dbGame = await _context.Games.FindAsync(id) ?? throw new ArgumentNullException("Entity not found");
            return new Game
            {
                Id = dbGame.Id,
                TotalScore = dbGame.TotalScore,
                PlayerName = dbGame.PlayerName,
                Status = dbGame.Status,
            };
        }

        public async Task<Game> UpdateAsync(Game game)
        {
            var dbGame = await _context.Games.FindAsync(game.Id) ?? throw new ArgumentNullException(nameof(game));
            dbGame.TotalScore = game.TotalScore;
            dbGame.PlayerName = game.PlayerName;
            dbGame.Status = game.Status;
            _context.SaveChanges();

            return game;
        }
    }
}
