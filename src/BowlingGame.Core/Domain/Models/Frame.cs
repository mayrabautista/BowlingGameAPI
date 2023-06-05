namespace BowlingGame.Core.Domain.Models
{
    public class Frame
    {
        public string FirstRoll { get; set; } = string.Empty;

        public string SecondRoll { get; set; } = string.Empty;

        public string? ThirdRoll { get; set; }

        public int Position { get; set; }

        public int TotalScore { get; set; }

        public bool IsStrike { get; set; }

        public bool IsSpare { get; set; }
    }
}
