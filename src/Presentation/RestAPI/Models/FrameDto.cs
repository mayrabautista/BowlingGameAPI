
using BowlingGame.Core.Domain.Models;
using BowlingGame.Presentation.RestAPI.Interfaces;

namespace BowlingGame.Presentation.RestAPI.Models
{
    public class FrameDto : IDto<Frame>
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid GameId { get; set; }

        public int FirstRoll { get; set; }

        public int SecondRoll { get; set; }

        public int? ThirdRoll { get; set; }

        public int Index { get; set; }

        public int TotalScore { get; set; }

        public bool IsStrike { get; set; }

        public bool IsSpare { get; set; }


        public void FromModel(Frame model)
        {
            Id = model.Id;
            GameId = model.GameId;
            FirstRoll = model.FirstRoll;
            SecondRoll = model.SecondRoll;
            ThirdRoll = model.ThirdRoll;
            Index = model.Index;
            TotalScore = model.TotalScore;
            IsStrike = model.IsStrike;
            IsSpare = model.IsSpare;
        }

        public Frame ToModel()
        {
            return new Frame
            {
                Id = Id,
                GameId = GameId,
                FirstRoll = FirstRoll,
                SecondRoll = SecondRoll,
                ThirdRoll = ThirdRoll,
                Index = Index,
                TotalScore = TotalScore,
                IsStrike = IsStrike,
                IsSpare = IsSpare,
            };
        }
    }
}
