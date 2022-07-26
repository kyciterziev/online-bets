using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UltraPlay.Application.Clients;
using UltraPlay.Application.Interfaces;
using UltraPlay.Application.Models;
using UltraPlay.Application.Utils;
using UltraPlay.Domain.Entities;

namespace UltraPlay.BackgroundJobs
{
    public class UltraPlayApiWorker : IScopedProcessingService
    {
        private int _executionCount;
        private readonly ILogger<UltraPlayApiWorker> _logger;
        private readonly IUltraPlayClient _client;
        private readonly IApplicationDbContext _context;
        private readonly IOptions<UltraPlayClientConfig> _options;

        public UltraPlayApiWorker(
            ILogger<UltraPlayApiWorker> logger, IUltraPlayClient client, IApplicationDbContext context, IOptions<UltraPlayClientConfig> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ++_executionCount;

                _logger.LogInformation(
                    "{ServiceName} working, execution count: {Count}",
                    nameof(UltraPlayApiWorker),
                    _executionCount);
                var response = (await _client.GetSports(_options.Value.ClientKey, sportId: 2357, days: 7));

                var existingSport = _context.Sports
                    .Include(x => x.Events)
                        .ThenInclude(x => x.Matches.Where(x => x.StartDate >= DateTime.UtcNow.AddMinutes(-5)))
                        .ThenInclude(x => x.Bets)
                        .ThenInclude(x => x.Odds)
                    .FirstOrDefault(x => x.ExternalId == response.Sport.Id);

                if (existingSport == null)
                {
                    await this.HandleNew(response.Sport, stoppingToken);
                }
                else
                {
                    await this.HandleExisting(existingSport, response.Sport, stoppingToken);
                }

                await Task.Delay(60000, stoppingToken);
            }
        }

        private async Task HandleNew(SportModel sport, CancellationToken cancellationToken)
        {
            var newSport = MappingUtils.MapToEntity(sport);
            _context.Sports.Add(newSport);

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task HandleExisting(Sport sport, SportModel sportModel, CancellationToken cancellationToken)
        {
            var eventsResponse = sportModel.Events.ToLookup(x => x.Id);
            var eventsDb = new List<EventModel>();
            foreach (var eventDb in sport.Events)
            {
                var newEvent = eventsResponse[eventDb.ExternalId]?.FirstOrDefault();
                if (newEvent is null)
                {
                    eventDb.IsActive = false;
                    continue;
                }

                eventsDb.Add(newEvent);
                HandleMatches(eventDb, newEvent);
            }

            sport.Events = sportModel.Events
                .Except(eventsDb)
                .Select(x => MappingUtils.MapToEntity(x, sport.Id))
                .Concat(sport.Events)
                .ToList();

            await _context.SaveChangesAsync(cancellationToken);
        }

        private static void HandleMatches(Event eventDb, EventModel newEvent)
        {
            var newEventMatches = newEvent.Matches;
            var newMatches = newEventMatches.ToLookup(x => x.Id);
            var matchesDb = new List<MatchModel>();

            foreach (var matchDb in eventDb.Matches)
            {
                var newMatch = newMatches[matchDb.ExternalId]?.FirstOrDefault();
                if (newMatch is null)
                {
                    matchDb.IsActive = false;
                    continue;
                }

                matchDb.MatchType = newMatch.MatchType;
                matchDb.StartDate = newMatch.StartDate;
                matchesDb.Add(newMatch);
                HandleBets(matchDb, newMatch);
            }

            eventDb.Matches = newEventMatches
                .Except(matchesDb)
                .Select(x => MappingUtils.MapToEntity(x, eventDb.Id))
                .Concat(eventDb.Matches)
                .ToList();
        }

        private static void HandleBets(Match matchDb, MatchModel newMatch)
        {
            var betsDb = new List<BetModel>();

            var newBets = newMatch.Bets.ToLookup(x => x.Id);
            foreach (var betDb in matchDb.Bets)
            {
                var newBet = newBets[betDb.ExternalId]?.FirstOrDefault();
                if (newBet is null)
                {
                    betDb.IsActive = false;
                    continue;
                }

                betsDb.Add(newBet);
                HandleOdds(betDb, newBet);
            }

            matchDb.Bets = newMatch.Bets
                .Except(betsDb)
                .Select(x => MappingUtils.MapToEntity(x, matchDb.Id))
                .Concat(matchDb.Bets)
                .ToList();
        }

        private static void HandleOdds(Bet betDb, BetModel newBet)
        {
            var responseOdds = newBet.Odds.ToLookup(x => x.Id);
            var oddsDb = new List<OddModel>();

            foreach (var oddDb in betDb.Odds)
            {
                var responseOdd = responseOdds[oddDb.ExternalId]?.FirstOrDefault();
                if (responseOdd is null)
                {
                    oddDb.IsActive = false;
                    continue;
                }

                oddDb.Value = responseOdd.Value;
                oddsDb.Add(responseOdd);
            }

            betDb.Odds = newBet.Odds
                .Except(oddsDb)
                .Select(x => MappingUtils.MapToEntity(x, betDb.Id))
                .Concat(betDb.Odds)
                .ToList();
        }
    }
}