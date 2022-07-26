using UltraPlay.Api.Services.Interfaces;
using UltraPlay.Application.Interfaces;
using UltraPlay.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace UltraPlay.Api.Services
{
    public class MatchService : IMatchService
    {
        private readonly ILogger<IMatchService> logger;
        private readonly IApplicationDbContext dbContext;

        public MatchService(ILogger<IMatchService> logger, IApplicationDbContext dbContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<IEnumerable<MatchResponse>> GetAllMatches()
        {
            var betValues = new string[] { "Match Winner", "Map Advantage", "Total Maps Played" };

            return Task.FromResult(dbContext.Matches
                .Include(x => x.Bets)
                    .ThenInclude(x => x.Odds)
                .Where(x => x.StartDate >= DateTime.UtcNow && x.IsActive)
                .AsEnumerable()
                .Select(x => new MatchResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    StartDate = x.StartDate.ToString(),
                    Bets = x.Bets
                    .Where(y => y.IsActive && betValues.Contains(y.Name))
                    .Select(a => new BetResponse
                    {
                        Name = a.Name,
                        IsLive = a.IsLive,
                        Odds = a.Odds
                        .Where(l => l.IsActive)
                        .GroupBy(o => o.SpecialBetValue)
                        .OrderByDescending(t => t.Key)
                        .First()
                        .Select(b => new OddResponse
                        {
                            Name = b.Name,
                            SpecialBetValue = b.SpecialBetValue,
                            Value = b.Value
                        })
                    })
                }));
        }

        public async Task<MatchResponse> GetMatchById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Match Id is required.");
            }

            var match = await this.dbContext.Matches
                .Include(x => x.Bets)
                    .ThenInclude(x => x.Odds)
                .FirstOrDefaultAsync(x => x.Id == id);
            return MatchResponse.From(match);
        }
    }
}