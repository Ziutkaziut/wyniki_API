using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wyniki_API.Entities;

namespace Wyniki_API.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class TeamsController : ControllerBase
    {
        private readonly WynikiDbContext _context;

        public TeamsController(WynikiDbContext context)
        {
            _context = context;
        }

        // GET: api/teams
        [HttpGet]
        public IActionResult GetAllTeams()
        {
            var teams = _context.Teams.ToList();
            return Ok(teams);
        }

        // GET: api/teams/{id}
        [HttpGet("{id}")]
        public IActionResult GetTeamById(int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (team == null)
                return NotFound($"Team with ID {id} not found.");
            return Ok(team);
        }

        // POST: api/teams
        [HttpPost]
        public IActionResult CreateTeam([FromBody] Team team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Teams.Add(team);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team);
        }

        // PUT: api/teams/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTeam(int id, [FromBody] Team updatedTeam)
        {
            var existingTeam = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (existingTeam == null)
                return NotFound($"Team with ID {id} not found.");

            existingTeam.Name = updatedTeam.Name;
            existingTeam.Coach = updatedTeam.Coach;

            _context.SaveChanges();
            return Ok();
        }

        // DELETE: api/teams/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (team == null)
                return NotFound($"Team with ID {id} not found.");

            _context.Teams.Remove(team);
            _context.SaveChanges();

            return Ok();
        }
    }
}
