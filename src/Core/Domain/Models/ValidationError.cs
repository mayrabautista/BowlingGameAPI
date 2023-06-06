namespace BowlingGame.Core.Domain.Models
{
    public class ValidationError
    {
        public string? PropertyName { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
