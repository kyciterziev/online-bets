using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraPlay.Domain.Common;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Domain.Events
{
    public class OddUpdatedEvent : DomainEvent
    {
        public OddUpdatedEvent(Odd odd)
        {
            Odd = odd;
        }

        public Odd Odd { get; }
    }
}