using UltraPlay.Api.Models;

namespace UltraPlay.Api.Services.Interfaces
{
    public interface IMatchService
    {
        Task<IEnumerable<MatchResponse>> GetAllMatches();
        Task<MatchResponse> GetMatchById(int id);
    }
}