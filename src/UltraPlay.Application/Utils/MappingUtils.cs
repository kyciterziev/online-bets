using UltraPlay.Application.Models;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Application.Utils
{
    public static class MappingUtils
    {
        public static Sport MapToEntity(SportModel sportModel)
        {
            return new Sport
            {
                Name = sportModel.Name,
                ExternalId = sportModel.Id,
                Events = sportModel.Events.Select(x => MapToEntity(x)).ToList(),
            };
        }
        public static Event MapToEntity(EventModel eventModel, int sportId = 0)
        {
            return new Event
            {
                SportId = sportId,
                Name = eventModel.Name,
                CategoryId = eventModel.CategoryId,
                ExternalId = eventModel.Id,
                IsLive = eventModel.IsLive,
                Matches = eventModel.Matches.Select(x => MapToEntity(x)).ToList()
            };
        }

        public static Match MapToEntity(MatchModel matchModel, int eventId = 0)
        {
            return new Match
            {
                EventId = eventId,
                Name = matchModel.Name,
                ExternalId = matchModel.Id,
                StartDate = matchModel.StartDate,
                MatchType = matchModel.MatchType,
                Bets = matchModel.Bets.Select(x => MapToEntity(x)).ToList()
            };
        }
        public static Bet MapToEntity(BetModel betModel, int matchId = 0)
        {
            return new Bet
            {
                MatchId = matchId,
                Name = betModel.Name,
                ExternalId = betModel.Id,
                IsLive = betModel.IsLive,
                Odds = betModel.Odds.Select(x => MapToEntity(x)).ToList()
            };
        }
        public static Odd MapToEntity(OddModel oddModel, int betId = 0)
        {
            return new Odd
            {
                BetId = betId,
                Name = oddModel.Name,
                ExternalId = oddModel.Id,
                Value = oddModel.Value,
                SpecialBetValue = oddModel.SpecialBetValue
            };
        }
    }
}