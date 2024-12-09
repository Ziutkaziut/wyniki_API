using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wyniki_API.Entities;

namespace Wyniki_API.Controllers
{
    [ApiController]
    [Route("api/matches")]
    public class MatchesController : ControllerBase
    {
        private readonly WynikiDbContext _context;

        public MatchesController(WynikiDbContext context)
        {
            _context = context;
        }

        // GET: api/matches
        [HttpGet]
        public IActionResult GetAllMatches()
        {
            var matches = _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToList();
            return Ok(matches);
        }

        // GET: api/matches/{id}
        [HttpGet("{id}")]
        public IActionResult GetMatchById(int id)
        {
            var match = _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .FirstOrDefault(m => m.Id == id);

            if (match == null)
                return NotFound($"Match with ID {id} not found.");

            return Ok(match);
        }

        // POST: api/matches
        [HttpPost]
        public IActionResult CreateMatch([FromBody] Match match)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Matches.Add(match);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMatchById), new { id = match.Id }, match);
        }

        // PUT: api/matches/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateMatch(int id, [FromBody] Match updatedMatch)
        {
            var existingMatch = _context.Matches.FirstOrDefault(m => m.Id == id);
            if (existingMatch == null)
                return NotFound($"Match with ID {id} not found.");

            existingMatch.HomeTeamId = updatedMatch.HomeTeamId;
            existingMatch.AwayTeamId = updatedMatch.AwayTeamId;
            existingMatch.HomeTeamScore = updatedMatch.HomeTeamScore;
            existingMatch.AwayTeamScore = updatedMatch.AwayTeamScore;
            existingMatch.MatchDate = updatedMatch.MatchDate;
            existingMatch.Stadium = updatedMatch.Stadium;

            _context.SaveChanges();
            return Ok();
        }

        // DELETE: api/matches/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteMatch(int id)
        {
            var match = _context.Matches.FirstOrDefault(m => m.Id == id);
            if (match == null)
                return NotFound($"Match with ID {id} not found.");

            _context.Matches.Remove(match);
            _context.SaveChanges();

            return Ok();
        }
    }
}
