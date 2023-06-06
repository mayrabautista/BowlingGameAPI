using BowlingGame.Core.Domain.Models;
using FluentValidation;

namespace BowlingGame.Core.Aplication.Validators
{
    public class GameValidator : AbstractValidator<Game>
    {
        public GameValidator()
        {
            RuleFor(x => x.PlayerName)
                .NotNull()
                .NotEmpty();
        }
    }
}
