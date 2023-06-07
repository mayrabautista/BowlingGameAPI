namespace BowlingGame.Core.Domain.Models
{
    public class Frame
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? GameId { get; set; }

        public int FirstRoll { get; set; }

        public int SecondRoll { get; set; }

        public int? ThirdRoll { get; set; }

        public int Index { get; set; }

        public int? TotalScore { get; set; }

        public bool IsStrike { get; set; }

        public bool IsSpare { get; set; }
    }
}
