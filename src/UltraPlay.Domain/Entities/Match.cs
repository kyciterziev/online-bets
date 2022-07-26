using UltraPlay.Domain.Common;
using UltraPlay.Domain.Events;

namespace UltraPlay.Domain.Entities
{
    public class Match : AuditableEntity, IHasDomainEvent
    {
        private string matchType = null!;
        private bool isUpdated = false;
        private DateTime startDate;
        public int Id { get; set; }
        public string ExternalId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                if (startDate != value && !isUpdated)
                {
                    isUpdated = true;
                    this.DomainEvents.Add(new MatchUpdatedEvent(this));
                }
                startDate = value;
            }
        }
        public string MatchType
        {
            get => matchType;
            set
            {
                if (matchType != value && !isUpdated)
                {
                    isUpdated = true;
                    this.DomainEvents.Add(new MatchUpdatedEvent(this));
                }
                matchType = value;
            }
        }
        public int EventId { get; set; }
        public virtual ICollection<Bet> Bets { get; set; } = new List<Bet>();
        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}