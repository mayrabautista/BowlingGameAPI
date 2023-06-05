using BowlingGame.Core.Domain.Enums;

namespace BowlingGame.Core.Domain.Models
{
    internal class Game
    {
        public List<Frame> Frames { get; set; } = new List<Frame>();

        public int TotalScore { get; set; }

        public string PlayerName { get; set; } = string.Empty;

        public GameStatus Status { get; set; }
    }
}
