using BowlingGame.Core.Domain.Models;
using FluentValidation;

namespace BowlingGame.Core.Aplication.Validators
{
    public class FrameValidator : AbstractValidator<Frame>
    {
        public FrameValidator()
        {
            RuleFor(x => x.GameId)
                .NotNull()
                .NotEmpty();
        }
    }
}
