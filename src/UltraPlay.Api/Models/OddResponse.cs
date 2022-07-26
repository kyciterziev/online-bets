using System.Text.Json.Serialization;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Api.Models
{
    public class OddResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("special_bet_value")]
        public string? SpecialBetValue { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        public static OddResponse From(Odd odd, bool isBetActive)
        {
            if (odd is null)
            {
                return null;
            }

            return new OddResponse
            {
                Name = odd.Name,
                SpecialBetValue = odd.SpecialBetValue,
                Value = odd.Value,
                IsActive = isBetActive ? odd.IsActive : false
            };
        }
    }
}