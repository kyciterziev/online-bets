using System.Text.Json.Serialization;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Api.Models
{
    public class BetResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("is_live")]
        public bool IsLive { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("odds")]
        public IEnumerable<OddResponse> Odds { get; set; }

        public static BetResponse From(Bet bet, bool isMatchActive)
        {
            if (bet is null)
            {
                return null;
            }

            var isBetActive = isMatchActive ? bet.IsActive : false;

            return new BetResponse
            {
                Name = bet.Name,
                IsLive = bet.IsLive,
                IsActive = isBetActive,
                Odds = bet.Odds?.Select(x => OddResponse.From(x, isBetActive))
            };
        }

    }
}