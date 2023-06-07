using BowlingGame.Core.Domain.Enums;

namespace BowlingGame.Infrastructure.MSSql.Models
{
    public class DBGame
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int TotalScore { get; set; }

        public string PlayerName { get; set; } = string.Empty;

        public GameStatus Status { get; set; }

        public List<DBFrame> Frames { get; set; } = new List<DBFrame>();

    }
}
