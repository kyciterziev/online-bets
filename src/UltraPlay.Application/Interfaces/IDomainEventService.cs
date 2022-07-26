using UltraPlay.Domain.Common;

namespace UltraPlay.Application.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent @event);
    }
}