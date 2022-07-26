using UltraPlay.Domain.Common;
using UltraPlay.Domain.Events;

namespace UltraPlay.Domain.Entities
{
    public class Bet
    {
        private bool isLive;
        private bool isUpdated = false;

        public int Id { get; set; }
        public string ExternalId { get; set; } = null!;
        public int MatchId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsLive
        {
            get => isLive;
            set
            {
                if (isLive != value && !isUpdated)
                {
                    isUpdated = true;
                    this.DomainEvents.Add(new BetUpdatedEvent(this));
                }
                isLive = value;
            }
        }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<Odd> Odds { get; set; } = new List<Odd>();
        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}