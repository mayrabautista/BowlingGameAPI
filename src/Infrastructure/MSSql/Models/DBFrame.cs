using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Infrastructure.MSSql.Models
{
    public class DBFrame
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int FirstRoll { get; set; }

        public int SecondRoll { get; set; }

        public int? ThirdRoll { get; set; }

        public int Index { get; set; }

        public int TotalScore { get; set; }

        public bool IsStrike { get; set; }

        public bool IsSpare { get; set; }

        public Guid GameId { get; set; }

        public DBGame? Game { get; set; }
    }
}
