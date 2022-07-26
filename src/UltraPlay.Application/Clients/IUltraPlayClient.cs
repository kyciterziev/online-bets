using Refit;
using UltraPlay.Application.Models;

namespace UltraPlay.Application.Clients
{
    public interface IUltraPlayClient
    {
        [Get("/sportsxml")]
        Task<SportsModel> GetSports(string clientKey, int sportId, int days);
    }
}