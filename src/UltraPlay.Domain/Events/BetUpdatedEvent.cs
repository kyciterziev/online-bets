using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraPlay.Domain.Common;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Domain.Events
{
    public class BetUpdatedEvent : DomainEvent
    {
        public BetUpdatedEvent(Bet bet)
        {
            Bet = bet;
        }

        public Bet Bet { get; }
    }
}