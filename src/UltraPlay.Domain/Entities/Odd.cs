using UltraPlay.Domain.Common;
using UltraPlay.Domain.Events;

namespace UltraPlay.Domain.Entities
{
    public class Odd : AuditableEntity, IHasDomainEvent
    {
        private decimal _value;
        private bool isUpdated = false;

        public int Id { get; set; }
        public string ExternalId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Value
        {
            get => _value;
            set
            {
                if (_value != value && !isUpdated)
                {
                    isUpdated = true;
                    this.DomainEvents.Add(new OddUpdatedEvent(this));
                }
                _value = value;
            }
        }
        public int BetId { get; set; }
        public string? SpecialBetValue { get; set; }
        public bool IsActive { get; set; } = true;
        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}