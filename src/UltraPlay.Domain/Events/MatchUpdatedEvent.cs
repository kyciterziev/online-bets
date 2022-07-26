using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraPlay.Domain.Common;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Domain.Events
{
    public class MatchUpdatedEvent : DomainEvent
    {
        public MatchUpdatedEvent(Match match)
        {
            Match = match;
        }

        public Match Match { get; }
    }
}