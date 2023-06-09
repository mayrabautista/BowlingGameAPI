﻿using BowlingGame.Core.Domain.Models;

namespace BowlingGame.Core.Domain.Abstractions
{
    public interface IFramesRepository
    {
        Task<Frame> CreateAsync(Frame frame);

        Task<IEnumerable<Frame>> GetAsync(FrameFilter? framefilter);

        Task<Frame> UpdateAsync(Frame frame);
    }
}
