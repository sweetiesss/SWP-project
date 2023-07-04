using Microsoft.AspNetCore.Mvc;
using SWP_Management.Repo.Entities;
using SWP_Management.Repo.Repositories;

namespace SWP_Management.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            var teams = _teamRepository.GetList();
            return Ok(teams);
        }

        [HttpGet("{teamId}")]
        public IActionResult GetTeam(string teamId)
        {
            var team = _teamRepository.GetById(teamId);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost]
        public IActionResult CreateTeam(Team team)
        {
            _teamRepository.Add(team);
            return CreatedAtAction(nameof(GetTeam), new { teamId = team.Id }, team);
        }

        [HttpPut("{teamId}")]
        public IActionResult UpdateTeam(string teamId, Team team)
        {
            if (teamId != team.Id)
            {
                return BadRequest();
            }

            var existingTeam = _teamRepository.GetById(teamId);
            if (existingTeam == null)
            {
                return NotFound();
            }

            _teamRepository.Update(team);
            return NoContent();
        }

        [HttpDelete("{teamId}")]
        public IActionResult DeleteTeam(string teamId)
        {
            var team = _teamRepository.GetById(teamId);
            if (team == null)
            {
                return NotFound();
            }

            _teamRepository.Delete(teamId);
            return NoContent();
        }
    }
}
