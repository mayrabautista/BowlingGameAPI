﻿using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Core.Domain.Abstractions
{
    public interface IFramesService
    {
        Task<Frame> CreateAsync(Frame frame);
        Task<Frame> UpdateScoresFromLastFrame(Frame frame);
    }
}
