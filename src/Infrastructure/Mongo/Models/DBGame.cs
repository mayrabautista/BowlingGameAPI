
using BowlingGame.Core.Domain.Enums;
using BowlingGame.Core.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BowlingGame.Infrastructure.Mongo.Models
{
    public class DBGame
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("gameId")]
        public string GameId { get; set; } = string.Empty;

        [BsonElement("totalScore")]
        public int TotalScore { get; set; }
        
        [BsonElement("playerName")]
        public string PlayerName { get; set; } = string.Empty;
        
        [BsonElement("status")]
        public GameStatus Status { get; set; }

        [BsonElement("frames")]
        public List<DBFrame> Frames { get; set; } = new List<DBFrame> { };
    }
}
