﻿using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Enums;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Infrastructure.Mongo.Interfaces;
using BowlingGame.Infrastructure.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BowlingGame.Infrastructure.Mongo.Repositories
{
    public class GamesMongoRepository : IGamesRepository
    {
        private IMongoCollection<DBGame> _games;

        public GamesMongoRepository(IMongoDBSettings dBSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(dBSettings.DatabaseName);
            _games = database.GetCollection<DBGame>("Game");
        }
        public Task<Game> CreateAsync(Game game)
        {
            DBGame dbGame = new DBGame()
            {
                GameId = game.Id.ToString(),
                TotalScore = game.TotalScore,
                PlayerName = game.PlayerName,
                Status = game.Status,
            };

            _games.InsertOne(dbGame);
            return Task.FromResult(game);
        }

        public async Task<IEnumerable<Game>> GetAsync()
        {
            var result = await _games.Find(x => true).ToListAsync();

            return result.Select(dbGame => new Game()
            {
                Id = Guid.Parse(dbGame.GameId),
                TotalScore = dbGame.TotalScore,
                PlayerName = dbGame.PlayerName,
                Status = dbGame.Status,
            });
        }

        public async Task<Game> GetAsync(Guid id)
        {
            var dbGame = await _games.Find(x => x.GameId == id.ToString()).FirstOrDefaultAsync() ?? throw new ArgumentNullException("Entity not found");

            return new Game()
            {
                Id = Guid.Parse(dbGame.GameId),
                TotalScore = dbGame.TotalScore,
                PlayerName = dbGame.PlayerName,
                Status = dbGame.Status,
            };
        }

        public Task<Game> UpdateAsync(Game game)
        {
            var filter = Builders<DBGame>.Filter.Eq("gameId", game.Id.ToString());
            var update = Builders<DBGame>.Update
                .Set("totalScore", game.TotalScore)
                .Set("playerName", game.PlayerName)
                .Set("status", game.Status);

            _games.FindOneAndUpdate<DBGame>(filter, update);

            return Task.FromResult(game);
        }
    }
}
