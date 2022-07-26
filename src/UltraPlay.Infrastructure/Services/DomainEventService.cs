using UltraPlay.Application.Interfaces;
using UltraPlay.Domain.Common;

namespace UltraPlay.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        public Task Publish(DomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}