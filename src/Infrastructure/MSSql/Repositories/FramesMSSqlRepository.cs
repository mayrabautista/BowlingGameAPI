using BowlingGame.Core.Domain.Abstractions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Infrastructure.MSSql.Models;
using Microsoft.EntityFrameworkCore;

namespace BowlingGame.Infrastructure.MSSql.Repositories
{
    public class FramesMSSqlRepository : IFramesRepository
    {
        private BowlingGameContext _context;

        public FramesMSSqlRepository(BowlingGameContext context)
        {
            _context = context;
        }

        public Task<Frame> CreateAsync(Frame frame)
        {
            DBFrame dbFrame = new DBFrame
            {
                Id = frame.Id,
                FirstRoll = frame.FirstRoll,
                SecondRoll = frame.SecondRoll,
                ThirdRoll = frame.ThirdRoll,
                Index = frame.Index,
                TotalScore = frame.TotalScore,
                IsStrike = frame.IsStrike,
                IsSpare = frame.IsSpare,
                GameId = frame.GameId,
            };

            var result = _context.Frames.Add(dbFrame);
            _context.SaveChanges();

            if (result is not null)
            {
                return Task.FromResult(new Frame
                {
                    Id = dbFrame.Id,
                    FirstRoll = dbFrame.FirstRoll,
                    SecondRoll = dbFrame.SecondRoll,
                    ThirdRoll = dbFrame.ThirdRoll,
                    Index = dbFrame.Index,
                    TotalScore = dbFrame.TotalScore,
                    IsStrike = dbFrame.IsStrike,
                    IsSpare = dbFrame.IsSpare,
                    GameId = dbFrame.GameId,
                });
            }
            throw new ArgumentException(nameof(Frame));
        }

        public async Task<List<Frame>> GetAsync(FrameFilter? framefilter)
        {
            var query = _context.Frames.AsQueryable();

            if (framefilter is not null)
            {
                query = query
                   .Where(x => framefilter.GameId == null || x.GameId == framefilter.GameId)
                   .Where(x => framefilter.IndexFrom == 0 || x.Index >= framefilter.IndexFrom)
                   .Where(x => framefilter.IndexTo == 0 || x.Index <= framefilter.IndexTo);
            }

            return await query
                .Select(dbFrame => new Frame
                {
                    Id = dbFrame.Id,
                    GameId = dbFrame.GameId,
                    FirstRoll = dbFrame.FirstRoll,
                    SecondRoll = dbFrame.SecondRoll,
                    ThirdRoll = dbFrame.ThirdRoll,
                    Index = dbFrame.Index,
                    TotalScore = dbFrame.TotalScore,
                    IsStrike = dbFrame.IsStrike,
                    IsSpare = dbFrame.IsSpare,
                })
                .OrderBy(x => x.Index)
                .ToListAsync();
        }

        public Task<Frame> UpdateAsync(Frame frame)
        {
            var dbFrame = _context.Frames.FirstOrDefault(x => x.Id == frame.Id) ?? throw new ArgumentException(nameof(Frame));
            dbFrame.FirstRoll = frame.FirstRoll;
            dbFrame.SecondRoll = frame.SecondRoll;
            dbFrame.ThirdRoll = frame.ThirdRoll;
            dbFrame.Index = frame.Index;
            dbFrame.TotalScore = frame.TotalScore;
            dbFrame.IsStrike = frame.IsStrike;
            dbFrame.IsSpare = frame.IsSpare;
            dbFrame.GameId = frame.GameId;

            _context.SaveChanges();

            return Task.FromResult(new Frame
            {
                Id = dbFrame.Id,
                FirstRoll = dbFrame.FirstRoll,
                SecondRoll = dbFrame.SecondRoll,
                ThirdRoll = dbFrame.ThirdRoll,
                Index = dbFrame.Index,
                TotalScore = dbFrame.TotalScore,
                IsStrike = dbFrame.IsStrike,
                IsSpare = dbFrame.IsSpare,
                GameId = dbFrame.GameId,
            });
        }
    }
}
