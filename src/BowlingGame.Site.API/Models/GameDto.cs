using BowlingGame.API.Interfaces;
using BowlingGame.Core.Domain.Enums;
using BowlingGame.Core.Domain.Models;

namespace BowlingGame.API.Models
{
    public class GameDto : IDto<Game>
    {
        public Guid? Id { get; set; }

        public int TotalScore { get; set; }

        public string? PlayerName { get; set; }

        public string? Status { get; set; }

        public void FromModel(Game model)
        {
            Id = model.Id;
            TotalScore = model.TotalScore;
            PlayerName = model.PlayerName;
            Status = model.Status.ToString();
        }

        public Game ToModel()
        {
            Game model = new Game
            {
                Id = Id ?? Guid.NewGuid(),
                TotalScore = TotalScore,
                PlayerName = PlayerName ?? string.Empty,
            };

            if (Enum.TryParse("Status", out GameStatus status))
            {
                model.Status = status;
            }

            return model;
        }
    }
}
