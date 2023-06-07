namespace BowlingGame.Core.Domain.Models
{
    public class FrameFilter
    {
        public Guid? GameId { get; set; }
        public int IndexFrom { get; set; }
        public int IndexTo { get; set; }
    }
}
