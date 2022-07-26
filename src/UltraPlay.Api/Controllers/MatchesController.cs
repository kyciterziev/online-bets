using Microsoft.AspNetCore.Mvc;
using UltraPlay.Api.Models;
using UltraPlay.Api.Services.Interfaces;

namespace UltraPlay.Api.Controllers
{
    [Route("api/matches")]
    public class MatchesController : Controller
    {
        private readonly ILogger<MatchesController> _logger;
        private readonly IMatchService _matchService;

        public MatchesController(ILogger<MatchesController> logger, IMatchService matchService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _matchService = matchService ?? throw new ArgumentNullException(nameof(matchService));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<MatchResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMatches()
        {
            var result = await this._matchService.GetAllMatches();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MatchResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMatchesById(int id)
        {
            var result = await this._matchService.GetMatchById(id);
            return Ok(result);
        }
    }
}