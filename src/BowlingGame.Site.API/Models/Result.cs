using BowlingGame.Core.Services.Models;

namespace BowlingGame.API.Models
{
    public class Result
    {
        public Result(object data)
        {
            Data = data;
            Success = true;
        }

        public Result(IEnumerable<ValidationError> errors)
        {
            Message = "Validation failed: See messages for details";
            Errors = errors;
            Success = false;
        }

        public Result(string message)
        {
            Message = message;
            Success = false;
        }

        public object? Data { get; set; }

        public IEnumerable<ValidationError>? Errors { get; set; }

        public string? Message { get; set; }

        public bool Success { get; set; }

        public bool IsCompleted { get; set; } = true;
    }
}
