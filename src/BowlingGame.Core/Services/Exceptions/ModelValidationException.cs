using BowlingGame.Core.Services.Models;
using FluentValidation.Results;

namespace BowlingGame.Core.Services.Exceptions
{
    public class ModelValidationException : Exception
    {
        public ModelValidationException(IEnumerable<ValidationFailure> validationErrors)
        {
            ValidationErrors = validationErrors.Select(x => new ValidationError
            {
                PropertyName = x.PropertyName,
                ErrorMessage = x.ErrorMessage,
            });
        }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}
