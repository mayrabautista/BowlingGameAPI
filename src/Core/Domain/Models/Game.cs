﻿using BowlingGame.Core.Domain.Enums;

namespace BowlingGame.Core.Domain.Models
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int TotalScore { get; set; }

        public string PlayerName { get; set; } = string.Empty;

        public GameStatus Status { get; set; }
    }
}
