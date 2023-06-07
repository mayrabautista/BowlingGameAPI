using BowlingGame.Infrastructure.Mongo.Interfaces;

namespace BowlingGame.Infrastructure.Mongo.Models
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;

    }
}
