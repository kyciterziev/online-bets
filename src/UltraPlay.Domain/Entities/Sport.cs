using UltraPlay.Domain.Common;

namespace UltraPlay.Domain.Entities
{
    public class Sport : AuditableEntity
    {
        public int Id { get; set; }
        public string ExternalId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}