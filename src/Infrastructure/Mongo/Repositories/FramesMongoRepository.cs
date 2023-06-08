using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Infrastructure.Mongo.Interfaces;
using BowlingGame.Infrastructure.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BowlingGame.Infrastructure.Mongo.Repositories
{
    public class FramesMongoRepository : IFramesRepository
    {
        private IMongoCollection<DBGame> _games;

        public FramesMongoRepository(IMongoDBSettings dBSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(dBSettings.DatabaseName);
            _games = database.GetCollection<DBGame>("Game");
        }

        public Task<Frame> CreateAsync(Frame frame)
        {
            DBFrame newFrame = new DBFrame()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FrameId = frame.Id.ToString(),
                FirstRoll = frame.FirstRoll,
                SecondRoll = frame.SecondRoll,
                ThirdRoll = frame.ThirdRoll,
                Index = frame.Index,
                TotalScore = frame.TotalScore,
                IsStrike = frame.IsStrike,
                IsSpare = frame.IsSpare,
            };

            var filter = Builders<DBGame>.Filter.Where(rec => rec.GameId == frame.GameId.ToString());
            var update = Builders<DBGame>.Update.Push("frames", newFrame);
            var result = _games.FindOneAndUpdate<DBGame>(filter, update);
            return Task.FromResult(frame);
        }

        public async Task<IEnumerable<Frame>> GetAsync(FrameFilter? framefilter)
        {
            var gameId = framefilter?.GameId.ToString() ?? string.Empty;
            var result = await _games.Find(x => x.GameId == gameId).FirstOrDefaultAsync();

            return result?.Frames.Select(dbFrame => new Frame()
            {
                Id = Guid.Parse(dbFrame.FrameId),
                GameId = Guid.Parse(gameId),
                FirstRoll = dbFrame.FirstRoll,
                SecondRoll = dbFrame.SecondRoll,
                ThirdRoll = dbFrame.ThirdRoll,
                Index = dbFrame.Index,
                TotalScore = dbFrame.TotalScore,
                IsStrike = dbFrame.IsStrike,
                IsSpare = dbFrame.IsSpare,
            }) ?? new List<Frame>();
        }

        public Task<Frame> UpdateAsync(Frame frame)
        {
            DBFrame newFrame = new DBFrame()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FrameId = frame.Id.ToString(),
                FirstRoll = frame.FirstRoll,
                SecondRoll = frame.SecondRoll,
                ThirdRoll = frame.ThirdRoll,
                Index = frame.Index,
                TotalScore = frame.TotalScore,
                IsStrike = frame.IsStrike,
                IsSpare = frame.IsSpare,
            };

            var filter = Builders<DBGame>.Filter.Eq("gameId", frame.GameId.ToString())
                        & Builders<DBGame>.Filter.Eq("frames.frameId", frame.Id.ToString());

            var update = Builders<DBGame>.Update.Set("frames.$", newFrame);
            _games.FindOneAndUpdate<DBGame>(filter, update);

            return Task.FromResult(frame);
        }
    }
}
