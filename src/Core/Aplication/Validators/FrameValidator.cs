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

            RuleFor(x => x.FirstRoll)
                .Must((frame, firstRoll) => firstRoll + frame.SecondRoll <= 10)
                .When(x => !x.IsStrike && !x.IsSpare && x.Index < 10)
                .WithMessage("A regular frame must add up to 10.");

            RuleFor(x => x.IsStrike)
               .Must((frame, model) => frame.FirstRoll == 10 && frame.SecondRoll == 0)
               .When(x => x.IsStrike && x.Index < 10)
               .WithMessage("A strike must get 10 in the first roll and 0 in the second roll.");

            RuleFor(x => x.IsSpare)
                .Must((frame, model) => frame.FirstRoll + frame.SecondRoll == 10)
                .When(x => x.IsSpare && x.Index < 10)
                .WithMessage("The sum of the rolls in a spare must always add up to 10.");

            RuleFor(x => x.ThirdRoll)
                .Must(model => model == null)
                .When(x => x.Index == 10 && x.FirstRoll + x.SecondRoll < 10)
                .WithMessage("The player does not have an additional roll as it was neither a strike nor a spare.");

            RuleFor(x => x.Index)
           .Must(index => index <= 10)
           .WithMessage("A game cannot contain more than 10 frames.");
        }
    }
}
