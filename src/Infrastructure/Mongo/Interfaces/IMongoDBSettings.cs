
namespace BowlingGame.Infrastructure.Mongo.Interfaces
{
    public interface IMongoDBSettings
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}
