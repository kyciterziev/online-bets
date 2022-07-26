using System.Text.Json.Serialization;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Api.Models
{
    public class MatchResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("bets")]
        public IEnumerable<BetResponse> Bets { get; set; }

        public static MatchResponse From(Match match)
        {
            if (match is null)
            {
                return null;
            }

            return new MatchResponse()
            {
                Id = match.Id,
                Name = match.Name,
                StartDate = match.StartDate.ToString(),
                IsActive = match.IsActive,
                Bets = match.Bets?.Select(x => BetResponse.From(x, match.IsActive))
            };
        }
    }
}