namespace UltraPlay.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string ExternalId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public bool IsLive { get; set; }
        public bool IsActive { get; set; } = true;
        public int SportId { get; set; }
        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}