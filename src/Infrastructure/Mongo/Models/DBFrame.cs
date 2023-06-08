using BowlingGame.Core.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BowlingGame.Infrastructure.Mongo.Models
{
    public class DBFrame
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } =string.Empty;

        [BsonElement("frameId")]
        public string FrameId { get; set; } = string.Empty;

        [BsonElement("firstRoll")]
        public int FirstRoll { get; set; }

        [BsonElement("SecondRoll")]
        public int SecondRoll { get; set; }

        [BsonElement("thirdRoll")]
        public int? ThirdRoll { get; set; }

        [BsonElement("index")]
        public int Index { get; set; }

        [BsonElement("totalScore")]
        public int TotalScore { get; set; }

        [BsonElement("isStrike")]
        public bool IsStrike { get; set; }

        [BsonElement("isSpare")]
        public bool IsSpare { get; set; }
    }
}
